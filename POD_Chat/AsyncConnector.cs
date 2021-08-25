using POD_Async;
using POD_Chat.Base.Enum;
using POD_Chat.Model.ValueObject;
using System;
using System.Collections.Generic;
using System.Timers;
using Newtonsoft.Json.Linq;
using POD_Async.Base;
using POD_Chat.Base;
using System.Linq;

namespace POD_Chat
{
    internal class AsyncConnector
    {
        #region Field

        private Timer pingTimer;
        private DateTime lastSentMessageTime;
        private readonly AsyncService asyncService;
        private readonly ChatConfig chatConfig;

        #endregion Field

        #region Constructor

        internal AsyncConnector(ChatConfig chatConfig)
        {
            this.chatConfig = chatConfig;
            asyncService = new AsyncService(chatConfig.Config);
            asyncService.AsyncMessageReceived += ServiceLocator.ResponseHandler.OnMessageReceived;
            asyncService.AsyncError += ServiceLocator.ResponseHandler.OnAsyncError;
            SetTimer();
        }

        #endregion Constructor

        #region Execute

        internal string Execute(ChatMessageContent content, ChatMessageType type, long? subjectId = null)
        {
            var chatMessage = WrapMessage(content, type, subjectId);
            asyncService.SendMessage(chatMessage);
            lastSentMessageTime = DateTime.UtcNow;
            return chatMessage.UniqueId;
        }

        internal string ExecuteSendMessage(SendTextMessageRequest content, ChatMessageType type,long? repliedTo=null)
        {
            var chatMessage = WrapMessage(content, type, content.ThreadId);
            chatMessage.SystemMetadata = content.SystemMetadata;
            chatMessage.Metadata = content.Metadata;
            chatMessage.MessageType = content.MessageType;
            chatMessage.RepliedTo = repliedTo;
            asyncService.SendMessage(chatMessage);
            lastSentMessageTime = DateTime.UtcNow;
            return chatMessage.UniqueId;
        }

        internal string ExecuteEditMessage(EditMessageRequest content, ChatMessageType type, long? threadId)
        {
            var chatMessage = WrapMessage(content, type, threadId);
            chatMessage.SystemMetadata = content.SystemMetadata;
            asyncService.SendMessage(chatMessage);
            lastSentMessageTime = DateTime.UtcNow;
            return chatMessage.UniqueId;
        }

        internal string[] ExecuteForwardMessage(ForwardMessageRequest content, ChatMessageType type)
        {
            var chatMessage = WrapMessage(content, type, content.ThreadId);
            var forwardUniqueIds = new string[content.MessageIds.Length];
            for (var i = 0; i < content.MessageIds.Length; i++)
            {
                forwardUniqueIds[i] = Guid.NewGuid().ToString();
            }

            chatMessage.UniqueId = forwardUniqueIds.ToJson();
            asyncService.SendMessage(chatMessage);
            lastSentMessageTime = DateTime.UtcNow;
            return forwardUniqueIds;
        }

        internal string[] ExecuteMultipleDeleteMessage(DeleteMultipleMessagesRequest content, ChatMessageType type)
        {
            var chatMessage = WrapMessage(content, type,null);
            var multipleDeleteUniqueIds = new string[content.MessageIds.Length];
            for (var i = 0; i < content.MessageIds.Length; i++)
            {
                multipleDeleteUniqueIds[i] = Guid.NewGuid().ToString();
            }

            var contentObj = new
            {
                ids = JArray.Parse(content.MessageIds.ToJson()),
                uniqueIds = JArray.Parse(multipleDeleteUniqueIds.ToJson()),
                deleteForAll = content.DeleteForAll
            };

            chatMessage.UniqueId = null;
            chatMessage.Content = contentObj.ToJson();
            asyncService.SendMessage(chatMessage);
            lastSentMessageTime = DateTime.UtcNow;
            return multipleDeleteUniqueIds;
        }

        internal string[] ExecuteCreateThreadWithMessage(CreateThreadWithMessageRequest content, ChatMessageType type)
        {
            var chatMessage = WrapMessage(content, type, null);
            var uniqueIds = new List<string>();
            if (content.MessageInput.ForwardedUniqueIds != null)
            {
                uniqueIds.AddRange(content.MessageInput.ForwardedUniqueIds.ToList());
            }

            uniqueIds.Add(content.MessageInput.UniqueId);
            uniqueIds.Add(chatMessage.UniqueId);                       
            asyncService.SendMessage(chatMessage);
            lastSentMessageTime = DateTime.UtcNow;
            return uniqueIds.ToArray();
        }

        private ChatMessageVo WrapMessage(ChatMessageContent content, ChatMessageType type, long? subjectId)
        {
            var chatMessage = new ChatMessageVo
            {
                TypeCode = !string.IsNullOrEmpty(content?.TypeCode) ? content.TypeCode : "default",
                Type = (int)type,
                TokenIssuer = chatConfig.TokenIssuer,
                UniqueId = Guid.NewGuid().ToString(),
                Token = chatConfig.Token,
                Content = content?.GetJsonContent(),
                SubjectId = subjectId
            };

            return chatMessage;
        }

        #endregion Execute

        #region Ping

        /// <summary>
        /// Keep chat connection alive
        /// </summary>
        private void Ping()
        {
            var lastSentMessageTimeout = new TimeSpan(90000000);
            var currentTime = DateTime.UtcNow;
            if (currentTime.Ticks - lastSentMessageTime.Ticks > lastSentMessageTimeout.Ticks)
            {
                _ = Execute(null, ChatMessageType.PING);
            }
        }

        #region Timer

        private void SetTimer()
        {
            pingTimer = new Timer(20000);
            pingTimer.Elapsed += OnTimedEvent;
            pingTimer.AutoReset = true;
            pingTimer.Enabled = true;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Ping();
        }

        #endregion Timer

        #endregion Ping
    }
}
