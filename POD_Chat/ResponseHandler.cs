using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using POD_Async.Base;
using POD_Async.Model;
using POD_Chat.Base;
using POD_Chat.Base.Enum;
using POD_Chat.Model.ServiceOutput;
using POD_Chat.Model.ValueObject;

namespace POD_Chat
{
    public delegate void ChatMessageReceived<in T>(T wrappedResult);

    public class ResponseHandler
    {
        #region Field

        private readonly ChatConfig chatConfig;

        #endregion Field

        #region Constructor

        internal ResponseHandler(ChatConfig chatConfig)
        {
            this.chatConfig = chatConfig;
        }

        #endregion Constructor

        #region Event

        public event ChatMessageReceived<ChatResponseSrv<GetUserInfoResponse>> UserInfo_MessageReceived;
        public event ChatMessageReceived<ChatResponseSrv<GetThreadsModel>> GetThreads_MessageReceived;
        public event ChatMessageReceived<ChatResponseSrv<Conversation>> CreateThread_MessageReceived;
        public event ChatMessageReceived<ChatResponseSrv<Conversation>> UpdateThreadInfo_MessageReceived;
        public event ChatMessageReceived<ChatResponseSrv<Conversation>> ThreadInfoUpdated_MessageReceived;
        public event ChatMessageReceived<ChatResponseSrv<SendMessageModel>> Send_MessageReceived;
        public event ChatMessageReceived<ChatResponseSrv<SendMessageModel>> Sent_MessageReceived;
        public event ChatMessageReceived<ChatResponseSrv<SendMessageModel>> Forward_MessageReceived;
        public event ChatMessageReceived<ChatResponseSrv<AddParticipantModel>> AddParticipant_MessageReceived;
        public event ChatMessageReceived<ChatResponseSrv<GetThreadParticipantsModel>> GetParticipants_MessageReceived;
        public event ChatMessageReceived<ChatResponseSrv<GetThreadParticipantsModel>> RemoveParticipants_MessageReceived;
        public event ChatMessageReceived<ChatResponseSrv<SendMessageModel>> Seen_MessageReceived;
        public event ChatMessageReceived<ChatResponseSrv<SendMessageModel>> Delivery_MessageReceived;
        public event ChatMessageReceived<ChatResponseSrv<GetHistoryModel>> GetHistory_MessageReceived;
        public event ChatMessageReceived<ChatResponseSrv<DeleteMessageModel>> Delete_MessageReceived;
        public event ChatMessageReceived<ChatResponseSrv<GetContactsResponse>> GetContacts_MessageReceived;
        public event ChatMessageReceived<ChatResponseSrv<Conversation>> JoinPublicThread_MessageReceived;
        public event ChatMessageReceived<ChatResponseSrv<IsAvailableNameModel>> IsPublicThreadNameAvailable_MessageReceived;
        public event ChatMessageReceived<ChatResponseSrv<LeaveThreadModel>> LeaveThread_MessageReceived;
        public event ChatMessageReceived<ChatResponseSrv<SendMessageModel>> EditMessage_MessageReceived;
        public event ChatMessageReceived<ChatResponseSrv<MuteUnmuteThreadModel>> MuteThread_MessageReceived;
        public event ChatMessageReceived<ChatResponseSrv<MuteUnmuteThreadModel>> UnMuteThread_MessageReceived;
        public event ChatMessageReceived<ChatResponseSrv<BlockUnblockUserResponse>> Block_MessageReceived;
        public event ChatMessageReceived<ChatResponseSrv<BlockUnblockUserResponse>> UnBlock_MessageReceived;
        public event ChatMessageReceived<ChatResponseSrv<GetBlockedUserListResponse>> BlockList_MessageReceived;
        public event ChatMessageReceived<ChatResponseSrv<ClearHistoryModel>> ClearHistory_MessageReceived;
        public event ChatMessageReceived<ChatResponseSrv<PinUnpinThreadModel>> PinThread_MessageReceived;
        public event ChatMessageReceived<ChatResponseSrv<PinUnpinThreadModel>> UnPinThread_MessageReceived;
        public event ChatMessageReceived<ChatResponseSrv<PinUnpinMessageModel>> PinMessage_MessageReceived;
        public event ChatMessageReceived<ChatResponseSrv<PinUnpinMessageModel>> UnPinMessage_MessageReceived;
        public event ChatMessageReceived<ChatResponseSrv<UserRolesModel>> SetRoleToUser_MessageReceived;
        public event ChatMessageReceived<ChatResponseSrv<UserRolesModel>> RemoveRoleFromUser_MessageReceived;
        public event ChatMessageReceived<ChatResponseSrv<Profile>> UpdateProfile_MessageReceived;
        public event ChatMessageReceived<ChatResponseSrv<GetCurrentUserRolesModel>> GetUserRoles_MessageReceived;
        public event ChatMessageReceived<ChatResponseSrv<ResultCreateBot>> CreateBot_MessageReceived;
        public event ChatMessageReceived<ChatResponseSrv<ResultStartBot>> StartBot_MessageReceived;
        public event ChatMessageReceived<ChatResponseSrv<ResultStartBot>> StopBot_MessageReceived;
        public event ChatMessageReceived<ChatResponseSrv<ResultDefineCommandBot>> DefineBotCommand_MessageReceived;
        public event ChatMessageReceived<ChatResponseSrv<UnreadMessageCountModel>> AllUnreadMessageCount_MessageReceived;
        public event ChatMessageReceived<ChatResponseSrv<AsyncErrorMessage>> ChatError_MessageReceived;
        public event ChatMessageReceived<AsyncErrorMessage> AsyncError;

        #endregion Event

        #region EventHandler

        internal void OnMessageReceived(string content)
        {
            try
            {
                var chatMessage = JsonConvert.DeserializeObject<ChatMessageVo>(content);

                if (chatMessage.Type == null) return;
                switch ((ChatMessageType)chatMessage.Type)
                {
                    case ChatMessageType.USER_INFO:
                        HandleGetUserInfo(chatMessage);
                        break;

                    case ChatMessageType.GET_THREADS:
                        HandleGetThreads(chatMessage);
                        break;

                    case ChatMessageType.INVITATION:
                        HandleCreateThread(chatMessage);
                        break;

                    case ChatMessageType.UPDATE_THREAD_INFO:
                        HandleUpdateThreadInfo(chatMessage);
                        break;

                    case ChatMessageType.THREAD_INFO_UPDATED:
                        HandleThreadInfoUpdated(chatMessage);
                        break;

                    case ChatMessageType.FORWARD_MESSAGE:
                        HandleForwardMessage(chatMessage);
                        break;

                    case ChatMessageType.MESSAGE: //Send Message , Reply Message
                        HandleSendMessage(chatMessage);
                        break;

                    case ChatMessageType.ADD_PARTICIPANT:
                        HandleAddParticipant(chatMessage);
                        break;

                    case ChatMessageType.SENT:
                        HandleSent(chatMessage);
                        break;

                    case ChatMessageType.THREAD_PARTICIPANTS:
                        HandleGetParticipants(chatMessage);
                        break;

                    case ChatMessageType.REMOVE_PARTICIPANT:
                        HandleRemoveParticipants(chatMessage);
                        break;

                    case ChatMessageType.DELIVERY:
                        HandleDeliveryMessage(chatMessage);
                        break;

                    case ChatMessageType.SEEN:
                        HandleSeenMessage(chatMessage);
                        break;

                    case ChatMessageType.DELETE_MESSAGE: //Delete , Multiple Delete
                        HandleDeleteMessage(chatMessage);
                        break;

                    case ChatMessageType.GET_HISTORY:
                        HandleGetHistory(chatMessage);
                        break;

                    case ChatMessageType.GET_CONTACTS:
                        HandleGetContacts(chatMessage);
                        break;

                    case ChatMessageType.JOIN_THREAD:
                        HandleJoinThread(chatMessage);
                        break;

                    case ChatMessageType.IS_NAME_AVAILABLE:
                        HandleIsNameAvailable(chatMessage);
                        break;

                    case ChatMessageType.LEAVE_THREAD:
                        HandleLeaveThread(chatMessage);
                        break;

                    case ChatMessageType.EDIT_MESSAGE:
                        HandleEditMessage(chatMessage);
                        break;

                    case ChatMessageType.MUTE_THREAD:
                        HandleMuteThread(chatMessage);
                        break;

                    case ChatMessageType.UN_MUTE_THREAD:
                        HandleUnMuteThread(chatMessage);
                        break;

                    case ChatMessageType.BLOCK:
                        HandleBlock(chatMessage);
                        break;

                    case ChatMessageType.UNBLOCK:
                        HandleUnBlock(chatMessage);
                        break;

                    case ChatMessageType.GET_BLOCKED:
                        HandleBlockList(chatMessage);
                        break;

                    case ChatMessageType.CLEAR_HISTORY:
                        HandleClearHistory(chatMessage);
                        break;

                    case ChatMessageType.PIN_MESSAGE:
                        HandlePinMessage(chatMessage);
                        break;

                    case ChatMessageType.UNPIN_MESSAGE:
                        HandleUnPinMessage(chatMessage);
                        break;

                    case ChatMessageType.PIN_THREAD:
                        HandlePinThread(chatMessage);
                        break;

                    case ChatMessageType.UNPIN_THREAD:
                        HandleUnPinThread(chatMessage);
                        break;

                    case ChatMessageType.SET_ROLE_TO_USER:
                        HandleSetRoleToUser(chatMessage);
                        break;

                    case ChatMessageType.REMOVE_ROLE_FROM_USER:
                        HandleRemoveRoleFromUser(chatMessage);
                        break;

                    case ChatMessageType.UPDATE_PROFILE:
                        HandleUpdateProfile(chatMessage);
                        break;

                    case ChatMessageType.USER_ROLES:
                        HandleUserRoles(chatMessage);
                        break;

                    case ChatMessageType.CREATE_BOT:
                        HandleCreateBot(chatMessage);
                        break;

                    case ChatMessageType.START_BOT:
                        HandleStartBot(chatMessage);
                        break;

                    case ChatMessageType.STOP_BOT:
                        HandleStopBot(chatMessage);
                        break;

                    case ChatMessageType.DEFINE_BOT_COMMAND:
                        HandleDefineBotCommandBot(chatMessage);
                        break;

                    case ChatMessageType.ALL_UNREAD_MESSAGE_COUNT:
                        HandleAllUnreadMessageCount(chatMessage);
                        break;

                    case ChatMessageType.PING:
                        HandlePing();
                        break;

                    case ChatMessageType.ERROR:
                        HandleError(chatMessage);
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }
        }

        internal void OnAsyncError(string content)
        {
            var errorMessage = JsonConvert.DeserializeObject<AsyncErrorMessage>(content);
            AsyncError?.Invoke(errorMessage);
        }

        #endregion

        #region ResponseHandler

        #region Profile

        private void HandleGetUserInfo(ChatMessageVo chatMessage)
        {
            PodLogger.Logger.Info($"UniqueId = {chatMessage.UniqueId} : {chatMessage.Content}");
            var userInfo = JsonConvert.DeserializeObject<User>(chatMessage.Content);
            var result = new GetUserInfoResponse
            {
                User = userInfo
            };

            var wrappedResult = Wrap(result, chatMessage);
            UserInfo_MessageReceived?.Invoke(wrappedResult);
        }

        private void HandleUpdateProfile(ChatMessageVo chatMessage)
        {
            PodLogger.Logger.Info($"UniqueId = {chatMessage.UniqueId} : {chatMessage.Content}");
            var resultUpdateProfile = JsonConvert.DeserializeObject<Profile>(chatMessage.Content);
            var wrappedResult = Wrap(resultUpdateProfile, chatMessage);
            UpdateProfile_MessageReceived?.Invoke(wrappedResult);
        }

        #endregion Profile

        #region Thread

        private void HandleCreateThread(ChatMessageVo chatMessage)
        {
            PodLogger.Logger.Info($"UniqueId = {chatMessage.UniqueId} : {chatMessage.Content}");
            var thread = JsonConvert.DeserializeObject<Conversation>(chatMessage.Content);
            var wrappedResult = Wrap(thread, chatMessage);
            CreateThread_MessageReceived?.Invoke(wrappedResult);
        }

        private void HandleUpdateThreadInfo(ChatMessageVo chatMessage)
        {
            PodLogger.Logger.Info($"UniqueId = {chatMessage.UniqueId} : {chatMessage.Content}");
            var thread = JsonConvert.DeserializeObject<Conversation>(chatMessage.Content);
            var wrappedResult = Wrap(thread, chatMessage);
            UpdateThreadInfo_MessageReceived?.Invoke(wrappedResult);
        }

        private void HandleThreadInfoUpdated(ChatMessageVo chatMessage)
        {
            PodLogger.Logger.Info($"UniqueId = {chatMessage.UniqueId} : {chatMessage.Content}");
            var thread = JsonConvert.DeserializeObject<Conversation>(chatMessage.Content);
            var wrappedResult = Wrap(thread, chatMessage);
            UpdateThreadInfo_MessageReceived?.Invoke(wrappedResult);
        }

        private void HandleGetThreads(ChatMessageVo chatMessage)
        {
            PodLogger.Logger.Info($"UniqueId = {chatMessage.UniqueId} : {chatMessage.Content}");
            var threads = JsonConvert.DeserializeObject<List<Conversation>>(chatMessage.Content);
            var result = new GetThreadsModel { Threads = threads, ContentCount = chatMessage.ContentCount };
            var wrappedResult = Wrap(result, chatMessage);
            GetThreads_MessageReceived?.Invoke(wrappedResult);
        }

        private void HandleAddParticipant(ChatMessageVo chatMessage)
        {
            PodLogger.Logger.Info($"UniqueId = {chatMessage.UniqueId} : {chatMessage.Content}");
            var thread = JsonConvert.DeserializeObject<Conversation>(chatMessage.Content);
            var resultAddParticipant = new AddParticipantModel
            {
                Thread = thread
            };

            var wrappedResult = Wrap(resultAddParticipant, chatMessage);
            AddParticipant_MessageReceived?.Invoke(wrappedResult);
        }

        private void HandleRemoveParticipants(ChatMessageVo chatMessage)
        {
            PodLogger.Logger.Info($"UniqueId = {chatMessage.UniqueId} : {chatMessage.Content}");
            var participants = JsonConvert.DeserializeObject<List<Participant>>(chatMessage.Content);
            var resultParticipant = new GetThreadParticipantsModel
            {
                ContentCount = chatMessage.ContentCount,
                Participants = participants
            };

            var wrappedResult = Wrap(resultParticipant, chatMessage);
            RemoveParticipants_MessageReceived?.Invoke(wrappedResult);
        }

        private void HandleGetParticipants(ChatMessageVo chatMessage)
        {
            PodLogger.Logger.Info($"UniqueId = {chatMessage.UniqueId} : {chatMessage.Content}");
            var participants = JsonConvert.DeserializeObject<List<Participant>>(chatMessage.Content);
            var resultParticipant = new GetThreadParticipantsModel
            {
                ContentCount = chatMessage.ContentCount,
                Participants = participants
            };

            var wrappedResult = Wrap(resultParticipant, chatMessage);
            GetParticipants_MessageReceived?.Invoke(wrappedResult);
        }

        private void HandleJoinThread(ChatMessageVo chatMessage)
        {
            PodLogger.Logger.Info($"UniqueId = {chatMessage.UniqueId} : {chatMessage.Content}");
            var thread = JsonConvert.DeserializeObject<Conversation>(chatMessage.Content);
            var wrappedResult = Wrap(thread, chatMessage);
            JoinPublicThread_MessageReceived?.Invoke(wrappedResult);
        }

        private void HandleIsNameAvailable(ChatMessageVo chatMessage)
        {
            PodLogger.Logger.Info($"UniqueId = {chatMessage.UniqueId} : {chatMessage.Content}");
            var thread = JsonConvert.DeserializeObject<IsAvailableNameModel>(chatMessage.Content);
            var wrappedResult = Wrap(thread, chatMessage);
            IsPublicThreadNameAvailable_MessageReceived?.Invoke(wrappedResult);
        }

        private void HandleLeaveThread(ChatMessageVo chatMessage)
        {
            PodLogger.Logger.Info($"UniqueId = {chatMessage.UniqueId} : {chatMessage.Content}");
            var leaveThread = JsonConvert.DeserializeObject<LeaveThreadModel>(chatMessage.Content);
            leaveThread.ThreadId = chatMessage.SubjectId ?? 0;
            var wrappedResult = Wrap(leaveThread, chatMessage);
            LeaveThread_MessageReceived?.Invoke(wrappedResult);
        }

        private void HandleMuteThread(ChatMessageVo chatMessage)
        {
            PodLogger.Logger.Info($"UniqueId = {chatMessage.UniqueId} : {chatMessage.Content}");
            var threadId = JsonConvert.DeserializeObject<long>(chatMessage.Content);
            var resultMute = new MuteUnmuteThreadModel
            {
                ThreadId = threadId
            };

            var wrappedResult = Wrap(resultMute, chatMessage);
            MuteThread_MessageReceived?.Invoke(wrappedResult);
        }

        private void HandleUnMuteThread(ChatMessageVo chatMessage)
        {
            PodLogger.Logger.Info($"UniqueId = {chatMessage.UniqueId} : {chatMessage.Content}");
            var threadId = JsonConvert.DeserializeObject<long>(chatMessage.Content);
            var resultUnMute = new MuteUnmuteThreadModel
            {
                ThreadId = threadId
            };

            var wrappedResult = Wrap(resultUnMute, chatMessage);
            UnMuteThread_MessageReceived?.Invoke(wrappedResult);
        }

        private void HandleBlock(ChatMessageVo chatMessage)
        {
            PodLogger.Logger.Info($"UniqueId = {chatMessage.UniqueId} : {chatMessage.Content}");
            var blockedUser = JsonConvert.DeserializeObject<BlockedUser>(chatMessage.Content);
            var resultBlock = new BlockUnblockUserResponse
            {
                BlockedContact = blockedUser
            };

            var wrappedResult = Wrap(resultBlock, chatMessage);
            Block_MessageReceived?.Invoke(wrappedResult);
        }

        private void HandleUnBlock(ChatMessageVo chatMessage)
        {
            PodLogger.Logger.Info($"UniqueId = {chatMessage.UniqueId} : {chatMessage.Content}");
            var blockedUser = JsonConvert.DeserializeObject<BlockedUser>(chatMessage.Content);
            var resultBlock = new BlockUnblockUserResponse
            {
                BlockedContact = blockedUser
            };

            var wrappedResult = Wrap(resultBlock, chatMessage);
            UnBlock_MessageReceived?.Invoke(wrappedResult);
        }

        private void HandleBlockList(ChatMessageVo chatMessage)
        {
            PodLogger.Logger.Info($"UniqueId = {chatMessage.UniqueId} : {chatMessage.Content}");
            var blockedUserList = JsonConvert.DeserializeObject<List<BlockedUser>>(chatMessage.Content);
            var resultBlock = new GetBlockedUserListResponse
            {
                Contacts = blockedUserList,
                ContentCount = chatMessage.ContentCount
            };

            var wrappedResult = Wrap(resultBlock, chatMessage);
            BlockList_MessageReceived?.Invoke(wrappedResult);
        }


        private void HandlePinThread(ChatMessageVo chatMessage)
        {
            PodLogger.Logger.Info($"UniqueId = {chatMessage.UniqueId} : {chatMessage.Content}");
            var threadId = JsonConvert.DeserializeObject<long>(chatMessage.Content);
            var resultPinThread = new PinUnpinThreadModel
            {
                ThreadId = threadId
            };

            var wrappedResult = Wrap(resultPinThread, chatMessage);
            PinThread_MessageReceived?.Invoke(wrappedResult);
        }

        private void HandleUnPinThread(ChatMessageVo chatMessage)
        {
            PodLogger.Logger.Info($"UniqueId = {chatMessage.UniqueId} : {chatMessage.Content}");
            var threadId = JsonConvert.DeserializeObject<long>(chatMessage.Content);
            var resultUnPinThread = new PinUnpinThreadModel
            {
                ThreadId = threadId
            };

            var wrappedResult = Wrap(resultUnPinThread, chatMessage);
            UnPinThread_MessageReceived?.Invoke(wrappedResult);
        }

        #endregion Thread

        #region Thread_Role

        private void HandleSetRoleToUser(ChatMessageVo chatMessage)
        {
            PodLogger.Logger.Info($"UniqueId = {chatMessage.UniqueId} : {chatMessage.Content}");
            var userRoles = JsonConvert.DeserializeObject<List<UserRole>>(chatMessage.Content);
            var userRolesModel = new UserRolesModel()
            {
                UserRoles = userRoles
            };

            var wrappedResult = Wrap(userRolesModel, chatMessage);
            SetRoleToUser_MessageReceived?.Invoke(wrappedResult);
        }

        private void HandleRemoveRoleFromUser(ChatMessageVo chatMessage)
        {
            PodLogger.Logger.Info($"UniqueId = {chatMessage.UniqueId} : {chatMessage.Content}");
            var admins = JsonConvert.DeserializeObject<List<UserRole>>(chatMessage.Content);
            var userRolesModel = new UserRolesModel
            {
                UserRoles = admins
            };

            var wrappedResult = Wrap(userRolesModel, chatMessage);
            RemoveRoleFromUser_MessageReceived?.Invoke(wrappedResult);
        }

        private void HandleUserRoles(ChatMessageVo chatMessage)
        {
            PodLogger.Logger.Info($"UniqueId = {chatMessage.UniqueId} : {chatMessage.Content}");
            var userRoles = JsonConvert.DeserializeObject<string[]>(chatMessage.Content);
            var resultCurrentUserRoles = new GetCurrentUserRolesModel
            {
                UserRoles = userRoles
            };
            var wrappedResult = Wrap(resultCurrentUserRoles, chatMessage);
            GetUserRoles_MessageReceived?.Invoke(wrappedResult);
        }

        #endregion Thread_Role

        #region Message

        private void HandleSendMessage(ChatMessageVo chatMessage)
        {
            PodLogger.Logger.Info($"UniqueId = {chatMessage.UniqueId} : {chatMessage.Content}");
            var messageVo = JsonConvert.DeserializeObject<MessageVO>(chatMessage.Content);
            var resultNewMessage = new SendMessageModel
            {
                Message = messageVo,
                ThreadId = chatMessage.SubjectId
            };

            var wrappedResult = Wrap(resultNewMessage, chatMessage);
            Send_MessageReceived?.Invoke(wrappedResult);

            if (messageVo?.Participant?.Id != null && messageVo?.Participant?.Id != chatConfig.UserId)
            {
                var deliveryMessage = SendDeliverSeenRequest.ConcreteBuilder.SetMessageId(messageVo.Id).Build();
                ServiceLocator.AsyncConnector.Execute(deliveryMessage, ChatMessageType.DELIVERY);
                PodLogger.Logger.Info("SEND_DELIVERY_MESSAGE");
            }
        }

        private void HandleEditMessage(ChatMessageVo chatMessage)
        {
            PodLogger.Logger.Info($"UniqueId = {chatMessage.UniqueId} : {chatMessage.Content}");
            var messageVo = JsonConvert.DeserializeObject<MessageVO>(chatMessage.Content);
            var resultNewMessage = new SendMessageModel
            {
                Message = messageVo,
                ThreadId = chatMessage.SubjectId
            };

            var wrappedResult = Wrap(resultNewMessage, chatMessage);
            EditMessage_MessageReceived?.Invoke(wrappedResult);
        }

        private void HandleForwardMessage(ChatMessageVo chatMessage)
        {
            PodLogger.Logger.Info($"UniqueId = {chatMessage.UniqueId} : {chatMessage.Content}");
            var messageVo = JsonConvert.DeserializeObject<MessageVO>(chatMessage.Content);
            var resultNewMessage = new SendMessageModel
            {
                Message = messageVo,
                ThreadId = chatMessage.SubjectId
            };

            var wrappedResult = Wrap(resultNewMessage, chatMessage);
            Forward_MessageReceived?.Invoke(wrappedResult);

            if (messageVo?.Participant?.Id != null && messageVo?.Participant?.Id != chatConfig.UserId)
            {
                var deliveryMessage = SendDeliverSeenRequest.ConcreteBuilder.SetMessageId(messageVo.Id).Build();
                ServiceLocator.AsyncConnector.Execute(deliveryMessage, ChatMessageType.DELIVERY);
                PodLogger.Logger.Info("SEND_DELIVERY_MESSAGE");
            }
        }

        private void HandleSeenMessage(ChatMessageVo chatMessage)
        {
            PodLogger.Logger.Info($"UniqueId = {chatMessage.UniqueId} : {chatMessage.Content}");
            var resultMessage = JsonConvert.DeserializeObject<SendMessageModel>(chatMessage.Content);
            var wrappedResult = Wrap(resultMessage, chatMessage);
            Seen_MessageReceived?.Invoke(wrappedResult);
        }

        private void HandleDeliveryMessage(ChatMessageVo chatMessage)
        {
            PodLogger.Logger.Info($"UniqueId = {chatMessage.UniqueId} : {chatMessage.Content}");
            var resultMessage = JsonConvert.DeserializeObject<SendMessageModel>(chatMessage.Content);
            var wrappedResult = Wrap(resultMessage, chatMessage);
            Delivery_MessageReceived?.Invoke(wrappedResult);
        }

        private void HandleSent(ChatMessageVo chatMessage)
        {
            PodLogger.Logger.Info($"UniqueId = {chatMessage.UniqueId} : {chatMessage.Content}");
            var sendMessageModel = new SendMessageModel
            {
                ConversationId = chatMessage.SubjectId ?? 0,
                MessageId = long.Parse(chatMessage.Content)
            };

            var wrappedResult = Wrap(sendMessageModel, chatMessage);
            Sent_MessageReceived?.Invoke(wrappedResult);
        }

        private void HandleDeleteMessage(ChatMessageVo chatMessage)
        {
            PodLogger.Logger.Info($"UniqueId = {chatMessage.UniqueId} : {chatMessage.Content}");
            var messageId = JsonConvert.DeserializeObject<long>(chatMessage.Content);
            var deleteMessage = new DeleteMessageModel
            {
                DeletedMessageId = messageId
            };

            var wrappedResult = Wrap(deleteMessage, chatMessage);
            Delete_MessageReceived?.Invoke(wrappedResult);
        }

        private void HandleGetHistory(ChatMessageVo chatMessage)
        {
            PodLogger.Logger.Info($"UniqueId = {chatMessage.UniqueId} : {chatMessage.Content}");
            var histories = JsonConvert.DeserializeObject<List<MessageVO>>(chatMessage.Content);
            var resultHistory = new GetHistoryModel
            {
                ContentCount = chatMessage.ContentCount,
                History = histories
            };
            var wrappedResult = Wrap(resultHistory, chatMessage);
            GetHistory_MessageReceived?.Invoke(wrappedResult);
        }

        private void HandleClearHistory(ChatMessageVo chatMessage)
        {
            PodLogger.Logger.Info($"UniqueId = {chatMessage.UniqueId} : {chatMessage.Content}");
            var threadId = JsonConvert.DeserializeObject<long>(chatMessage.Content);
            var resultClearHistory = new ClearHistoryModel
            {
                ThreadId = threadId
            };

            var wrappedResult = Wrap(resultClearHistory, chatMessage);
            ClearHistory_MessageReceived?.Invoke(wrappedResult);
        }

        private void HandlePinMessage(ChatMessageVo chatMessage)
        {
            PodLogger.Logger.Info($"UniqueId = {chatMessage.UniqueId} : {chatMessage.Content}");
            var resultPinThread = JsonConvert.DeserializeObject<PinUnpinMessageModel>(chatMessage.Content);
            var wrappedResult = Wrap(resultPinThread, chatMessage);
            PinMessage_MessageReceived?.Invoke(wrappedResult);
        }

        private void HandleUnPinMessage(ChatMessageVo chatMessage)
        {
            PodLogger.Logger.Info($"UniqueId = {chatMessage.UniqueId} : {chatMessage.Content}");
            var resultUnPinThread = JsonConvert.DeserializeObject<PinUnpinMessageModel>(chatMessage.Content);
            var wrappedResult = Wrap(resultUnPinThread, chatMessage);
            UnPinMessage_MessageReceived?.Invoke(wrappedResult);
        }

        private void HandleAllUnreadMessageCount(ChatMessageVo chatMessage)
        {
            PodLogger.Logger.Info($"UniqueId = {chatMessage.UniqueId} : {chatMessage.Content}");
            var unreadMessageCount = JsonConvert.DeserializeObject<long>(chatMessage.Content);
            var unreadMessageCountModel = new UnreadMessageCountModel
            {
                Count = unreadMessageCount
            };

            var wrappedResult = Wrap(unreadMessageCountModel, chatMessage);
            AllUnreadMessageCount_MessageReceived?.Invoke(wrappedResult);
        }

        #endregion Message

        #region Contact

        private void HandleGetContacts(ChatMessageVo chatMessage)
        {
            PodLogger.Logger.Info($"UniqueId = {chatMessage.UniqueId} : {chatMessage.Content}");
            var contacts = JsonConvert.DeserializeObject<List<Contact>>(chatMessage.Content);
            var contactsResponse = new GetContactsResponse()
            {
                Contacts = contacts,
                ContentCount = chatMessage.ContentCount
            };

            var wrappedResult = Wrap(contactsResponse, chatMessage);
            GetContacts_MessageReceived?.Invoke(wrappedResult);
        }

        #endregion Contact      

        #region Bot

        private void HandleCreateBot(ChatMessageVo chatMessage)
        {
            PodLogger.Logger.Info($"UniqueId = {chatMessage.UniqueId} : {chatMessage.Content}");
            var botInfo = JsonConvert.DeserializeObject<BotVo>(chatMessage.Content);
            var resultCreateBot = new ResultCreateBot
            {
                BotVO = botInfo
            };
            var wrappedResult = Wrap(resultCreateBot, chatMessage);
            CreateBot_MessageReceived?.Invoke(wrappedResult);
        }

        private void HandleStartBot(ChatMessageVo chatMessage)
        {
            PodLogger.Logger.Info($"UniqueId = {chatMessage.UniqueId} : {chatMessage.Content}");
            var resultStartBot = new ResultStartBot
            {
                BotName = chatMessage.Content
            };

            var wrappedResult = Wrap(resultStartBot, chatMessage);
            StartBot_MessageReceived?.Invoke(wrappedResult);
        }

        private void HandleStopBot(ChatMessageVo chatMessage)
        {
            PodLogger.Logger.Info($"UniqueId = {chatMessage.UniqueId} : {chatMessage.Content}");
            var resultStopBot = new ResultStartBot
            {
                BotName = chatMessage.Content
            };

            var wrappedResult = Wrap(resultStopBot, chatMessage);
            StopBot_MessageReceived?.Invoke(wrappedResult);
        }

        private void HandleDefineBotCommandBot(ChatMessageVo chatMessage)
        {
            PodLogger.Logger.Info($"UniqueId = {chatMessage.UniqueId} : {chatMessage.Content}");
            var resultDefineCommandBot = JsonConvert.DeserializeObject<ResultDefineCommandBot>(chatMessage.Content);
            var wrappedResult = Wrap(resultDefineCommandBot, chatMessage);
            DefineBotCommand_MessageReceived?.Invoke(wrappedResult);
        }

        #endregion Bot

        #region Error

        private void HandleError(ChatMessageVo chatMessage)
        {
            var error = JsonConvert.DeserializeObject<Error>(chatMessage.Content);
            var errorOutPut = new AsyncErrorMessage
            {
                ErrorCode = error.Code,
                ErrorMessage = error.Message,
                UniqueId = chatMessage?.UniqueId
            };

            PodLogger.Logger.Error(errorOutPut.ToJson());
            var wrappedResult = Wrap(errorOutPut, chatMessage);
            ChatError_MessageReceived?.Invoke(wrappedResult);
        }

        #endregion Error

        #region Ping

        private void HandlePing()
        {
            PodLogger.Logger.Info("*** Ping Recieved ***");
        }

        #endregion Ping

        private static ChatResponseSrv<T> Wrap<T>(T result, ChatMessageVo chatMessage)
        {
            var chatResponse = new ChatResponseSrv<T>
            {
                UniqueId = chatMessage.UniqueId,
                Result = result,
                SubjectId = chatMessage.SubjectId
            };

            return chatResponse;
        }

        #endregion ResponseHandler
    }
}
