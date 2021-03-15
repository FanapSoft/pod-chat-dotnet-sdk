using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using POD_Async.Base;
using POD_Async.Core;
using POD_Async.Core.ResultModel;
using POD_Async.Exception;
using POD_Async.Model;
using POD_Chat;
using POD_Chat.Base;
using POD_Chat.Base.Enum;
using POD_Chat.Model.ServiceOutput;
using POD_Chat.Model.ValueObject;
using NLog;

namespace POD_Demo
{
    public class ChatMethodInvoke
    {
        private Dictionary<string, string> uniqueIds;
        public ChatMethodInvoke()
        {
            try
            {
                #region Log
                //Add your log level
                PodLogger.AddRule(LogLevel.Error, true, false);
                //PodLogger.AddRule(LogLevel.Debug, true, false);
                //PodLogger.AddRule(LogLevel.Info, true, false);               

                //If youd to know what is log path
                var logPath = PodLogger.LogPath;

                #endregion Log               

                #region Configuration
                //Initial necessary config
                var config = AsyncConfig.ConcreteBuilder.SetQueueUrl(new List<QueueUrl>
                    {
                        QueueUrl.ConcreteBuilder.SetIp("queueHost").SetPort(0).Build()
                    })
                    .SetQueueUsername("queueUsername")
                    .SetQueuePassword("queuePassword")
                    .SetQueueReceive("queueReceive")
                    .SetQueueSend("queueSend")
                    .SetQueueConnectionTimeout(2000)
                    .SetServerName("serverName")
                    .SetFileServer("https://core.pod.ir")
                    .SetPlatformHost("https://sandbox.pod.ir:8043/srv/basic-platform")
                    .SetSsoHost("https://accounts.pod.ir")
                    .SetConsumersCount(1)
                    .Build();

                var chatConfig = new ChatConfig(config, "apiToken");
                //POD_Base_Service.Base.Config.ServerType = ServerType.SandBox;
                new ServiceLocator(chatConfig);
                InitializeEventHandler();

                uniqueIds = new Dictionary<string, string>
                            {
                                {"GetThreads",string.Empty },
                                {"CreateThread",string.Empty },
                                {"CreateThreadWithMessage",string.Empty },
                                {"UpdateThreadInfo",string.Empty },
                                {"CreateThreadWithFileMessage",string.Empty },
                                {"SendTextMessage",string.Empty },
                                {"AddParticipants",string.Empty },
                                {"RemoveParticipant",string.Empty },
                                {"GetThreadParticipants",string.Empty },
                                {"DeleteMessage",string.Empty },
                                {"DeleteMultipleMessage",string.Empty },
                                {"GetHistory",string.Empty },
                                {"ReplyTextMessage",string.Empty },
                                {"ForwardMessage",string.Empty },
                                {"GetContacts",string.Empty },
                                {"JoinPublicThread",string.Empty },
                                {"IsPublicThreadNameAvailable",string.Empty },
                                {"LeaveThread",string.Empty },
                                {"EditMessage",string.Empty },
                                {"MuteThread",string.Empty },
                                {"UnMuteThread",string.Empty },
                                {"Block",string.Empty },
                                {"UnBlock",string.Empty },
                                {"GetBlockList",string.Empty },
                                {"ClearHistory",string.Empty },
                                {"GetMentionedMessages",string.Empty },
                                {"SendFileMessage",string.Empty },
                                {"ReplyFileMessage",string.Empty },
                                {"PinThread",string.Empty },
                                {"UnPinThread",string.Empty },
                                {"PinMessage",string.Empty },
                                {"UnPinMessage",string.Empty },
                                {"SetAdmin",string.Empty },
                                {"RemoveAdmin",string.Empty },
                                {"GetThreadAdmins",string.Empty },
                                {"SpamPrivateThread",string.Empty },
                                {"SetAuditor",string.Empty },
                                {"RemoveAuditor",string.Empty },
                                {"UpdateProfile",string.Empty },
                                {"GetCurrentUserRoles",string.Empty },
                                {"GetUnreadMessageCount",string.Empty }
                            };

                #endregion Configuration

                #region Method-Calls

                /// UnComment every method you need

                //GetThreads();
                //CreateThread();
                //CreateThreadWithMessage();
                //UpdateThreadInfo();
                //CreateThreadWithFileMessage();
                //SendTextMessage();
                //AddParticipants();
                //AddContact();
                //RemoveContact();
                //UpdateContact();
                //SearchContact();
                //RemoveParticipant();
                //GetThreadParticipants();
                //DeleteMessage();
                //DeleteMultipleMessage();
                //GetHistory();
                //ReplyTextMessage();
                //ForwardMessage();
                //GetContacts();
                //JoinPublicThread();
                //IsPublicThreadNameAvailable();
                //LeaveThread();
                //EditMessage();
                //MuteThread();
                //UnMuteThread();
                //Block();
                //UnBlock();
                //GetBlockList();
                //ClearHistory();
                //GetMentionedMessages();
                //SendFileMessage();
                //ReplyFileMessage();
                //PinThread();
                //UnPinThread();
                //PinMessage();
                //UnPinMessage();
                //SetAdmin();
                //RemoveAdmin();
                //GetThreadAdmins();
                //SpamPrivateThread();
                //SetAuditor();
                //RemoveAuditor();
                //UpdateProfile();
                //GetCurrentUserRoles();
                //GetUnreadMessageCount();
                //DownloadImage();
                //DownloadFile();

                #endregion Method-Calls
            }
            catch (PodException podException)
            {
                Console.WriteLine(
                    $"-- {podException.Code}-an error has occured : {Environment.NewLine}{podException.Message}");
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        #region Event-Subscibers
        public void InitializeEventHandler()
        {
            ServiceLocator.ResponseHandler.GetThreads_MessageReceived += OnGetThreads;
            ServiceLocator.ResponseHandler.CreateThread_MessageReceived += OnCreateThread;
            ServiceLocator.ResponseHandler.UpdateThreadInfo_MessageReceived += OnUpdateThreadInfo;
            ServiceLocator.ResponseHandler.Send_MessageReceived += OnSendMessage;
            ServiceLocator.ResponseHandler.Forward_MessageReceived += OnForwardMessage;
            ServiceLocator.ResponseHandler.AddParticipant_MessageReceived += OnAddParticipant;
            ServiceLocator.ResponseHandler.RemoveParticipants_MessageReceived += OnRemoveParticipant;
            ServiceLocator.ResponseHandler.GetParticipants_MessageReceived += OnRemoveParticipant;
            ServiceLocator.ResponseHandler.Seen_MessageReceived += OnSeen;
            ServiceLocator.ResponseHandler.Sent_MessageReceived += OnSent;
            ServiceLocator.ResponseHandler.Delivery_MessageReceived += OnDelivery;
            ServiceLocator.ResponseHandler.GetHistory_MessageReceived += OnGetHistory;
            ServiceLocator.ResponseHandler.GetContacts_MessageReceived += OnGetContacts;
            ServiceLocator.ResponseHandler.JoinPublicThread_MessageReceived += OnJoinPublicThread;
            ServiceLocator.ResponseHandler.IsPublicThreadNameAvailable_MessageReceived += OnIsPublicThreadNameAvailable;
            ServiceLocator.ResponseHandler.LeaveThread_MessageReceived += OnLeaveThread;
            ServiceLocator.ResponseHandler.EditMessage_MessageReceived += OnEditMessage;
            ServiceLocator.ResponseHandler.MuteThread_MessageReceived += OnMuteThread;
            ServiceLocator.ResponseHandler.UnMuteThread_MessageReceived += OnUnMuteThread;
            ServiceLocator.ResponseHandler.Block_MessageReceived += OnBlock;
            ServiceLocator.ResponseHandler.UnBlock_MessageReceived += OnUnBlock;
            ServiceLocator.ResponseHandler.BlockList_MessageReceived += OnBlockList;
            ServiceLocator.ResponseHandler.ClearHistory_MessageReceived += OnClearHistory;
            ServiceLocator.ResponseHandler.PinThread_MessageReceived += OnPinThread;
            ServiceLocator.ResponseHandler.UnPinThread_MessageReceived += OnUnPinThread;
            ServiceLocator.ResponseHandler.PinMessage_MessageReceived += OnPinMessage;
            ServiceLocator.ResponseHandler.UnPinMessage_MessageReceived += OnUnPinMessage;
            ServiceLocator.ResponseHandler.SetRoleToUser_MessageReceived += OnSetRoleToUser;
            ServiceLocator.ResponseHandler.RemoveRoleFromUser_MessageReceived += OnRemoveRoleFromUser;
            ServiceLocator.ResponseHandler.UpdateProfile_MessageReceived += OnUpdateProfile;
            ServiceLocator.ResponseHandler.GetUserRoles_MessageReceived += OnGetCurrentUserRoles;
            ServiceLocator.ResponseHandler.Delete_MessageReceived += OnDelete;
            ServiceLocator.ResponseHandler.ChatError_MessageReceived += OnChatError;
        }

        #endregion Event-Subscibers

        #region Request

        public void GetThreads()
        {
            try
            {
                var getThreadsRequest = GetThreadsRequest.ConcreteBuilder
                    //.SetPartnerCoreContactId(0)
                    //.SetThreadIds(new List<long>() { 0 })
                    //.SetThreadName("")
                    //.SetCreatorCoreUserId(0)
                    //.SetPartnerCoreUserId(0)
                    //.SetCount(0)
                    //.SetOffset(0)
                    //.SetIsNew(true)
                    .Build();

                uniqueIds["GetThreads"] = ServiceLocator.ChatService.GetThreads(getThreadsRequest);
            }
            catch (PodException podException)
            {
                Console.WriteLine(
                    $"-- {podException.Code}-an error has occured : {Environment.NewLine}{podException.Message}");
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        public void CreateThread()
        {
            try
            {
                var createThreadRequest = CreateThreadRequest.ConcreteBuilder
                    .SetInvitees(new List<InviteVo>()
                    {
                    InviteVo.ConcreteBuilder.SetId("").SetIdType(InviteType.TO_BE_USER_CONTACT_ID).Build(),
                    InviteVo.ConcreteBuilder.SetId("").SetIdType(InviteType.TO_BE_USER_ID).Build()
                    })
                    .SetType(ThreadType.NORMAL)
                    //.SetTitle("threadTitle")
                    //.SetUniqueName("thread UniqueName")
                    //.SetDescription("test")
                    //.SetImage("")
                    //.SetMetadata("")     
                    //.SetUploadInput(UploadRequest.ConcreteBuilder
                    //    .SetFilePath(@"D:\test.jpg")
                    //    .SetDescription("pic")
                    //    .SetFileName("picthread")
                    //    //.SetXC(0)
                    //    //.SetYC(0)
                    //    //.SetHC(0)
                    //    //.SetWC(0)
                    //    .Build())
                    //.SetTypeCode("default")
                    .Build();

                uniqueIds["CreateThread"] = ServiceLocator.ChatService.CreateThread(createThreadRequest);
            }
            catch (PodException podException)
            {
                Console.WriteLine(
                    $"-- {podException.Code}-an error has occured : {Environment.NewLine}{podException.Message}");
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        public void UpdateThreadInfo()
        {
            try
            {
                var updateThreadInfoRequest = UpdateThreadInfoRequest.ConcreteBuilder
                    .SetThreadId(0)
                    //.SetImage("")
                    //.SetDescription("test")
                    //.SetTitle("yy")
                    //.SetUploadInput(UploadRequest.ConcreteBuilder
                    //            .SetFilePath(@"D:\test.jpg")
                    //            .SetFileName("podtst")
                    //            //.SetXC(0)
                    //            //.SetYC(0)
                    //            //.SetHC(0)
                    //            //.SetWC(0)
                    //            .Build())
                    //.SetUserGroupHash("B2PMAOJU3FMUKC")
                    //.SetMetadata("")
                    //.SetTypeCode("default")               
                    .Build();

                uniqueIds["UpdateThreadInfo"] = ServiceLocator.ChatService.UpdateThreadInfo(updateThreadInfoRequest);
            }
            catch (PodException podException)
            {
                Console.WriteLine(
                    $"-- {podException.Code}-an error has occured : {Environment.NewLine}{podException.Message}");
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        public void CreateThreadWithMessage()
        {
            try
            {
                var createThreadWithMessageRequest = CreateThreadWithMessageRequest.ConcreteBuilder
                    .SetCreateThreadInput(CreateThreadRequest.ConcreteBuilder
                                             .SetInvitees(new List<InviteVo>()
                                             {
                                                  InviteVo.ConcreteBuilder.SetId("").SetIdType(InviteType.TO_BE_USER_CONTACT_ID).Build(),
                                                  InviteVo.ConcreteBuilder.SetId("").SetIdType(InviteType.TO_BE_USER_ID).Build()
                                             })
                                             .SetType(ThreadType.NORMAL)
                                             .SetTitle("")
                                             //.SetUniqueName("groutst8")
                                             //.SetDescription("")
                                             //.SetImage("")
                                             //.SetMetadata("")     
                                             //.SetTypeCode("default")
                                             .Build())
                    .SetMessageInput(CreateThreadMessageInput.ConcreteBuilder
                                             .SetMessageType(MessageType.TEXT)
                                             .SetText("")
                                             //.SetForwardedMessageIds(new long[]{ 0 , 0})
                                             .Build())
                    .Build();

                var uniques = ServiceLocator.ChatService.CreateThreadWithMessage(createThreadWithMessageRequest);
            }
            catch (PodException podException)
            {
                Console.WriteLine(
                    $"-- {podException.Code}-an error has occured : {Environment.NewLine}{podException.Message}");
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        public void CreateThreadWithFileMessage()
        {
            try
            {
                var createThreadWithMessageRequest = CreateThreadWithMessageRequest.ConcreteBuilder
                    .SetCreateThreadInput(CreateThreadRequest.ConcreteBuilder
                        .SetInvitees(new List<InviteVo>()
                        {
                            InviteVo.ConcreteBuilder.SetId("").SetIdType(InviteType.TO_BE_USER_CONTACT_ID).Build(),
                            InviteVo.ConcreteBuilder.SetId("").SetIdType(InviteType.TO_BE_USER_ID).Build()
                        })
                        .SetType(ThreadType.NORMAL)
                        //.SetTitle("")
                        //.SetUniqueName("uniqueNameTest")
                        //.SetDescription("")
                        //.SetImage("")
                        //.SetMetadata("")     
                        //.SetTypeCode("default")
                        .Build())
                    .SetMessageInput(CreateThreadMessageInput.ConcreteBuilder
                        .SetMessageType(MessageType.TEXT)
                        .SetText("")
                        //.SetForwardedMessageIds(new long[]{ 114464 , 114463})
                        .Build())
                    .Build();

                var createThreadWithFileMessageRequest = CreateThreadWithFileMessageRequest.ConcreteBuilder
                            .SetCreateThreadWithMessageInput(createThreadWithMessageRequest)
                            .SetUploadInput(UploadRequest.ConcreteBuilder
                                .SetFilePath(@"D:\test.jpg")
                                .SetFileName("")
                                //.SetXC(0)
                                //.SetYC(0)
                                //.SetHC(0)
                                //.SetWC(0)
                                .Build())
                            .Build();

                uniqueIds["CreateThreadWithFileMessage"] = ServiceLocator.ChatService.CreateThreadWithFileMessage(createThreadWithFileMessageRequest);
            }
            catch (PodException podException)
            {
                Console.WriteLine(
                    $"-- {podException.Code}-an error has occured : {Environment.NewLine}{podException.Message}");
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        public void SendTextMessage()
        {
            try
            {
                var sendTextMessageRequest = SendTextMessageRequest.ConcreteBuilder
                    .SetTextMessage("")
                    .SetThreadId(0)
                    .SetMessageType(MessageType.TEXT)
                    //.SetSystemMetadata("")
                    //.SetMetadata("")
                    //.SetTypeCode("default")               
                    .Build();

                uniqueIds["SendTextMessage"] = ServiceLocator.ChatService.SendTextMessage(sendTextMessageRequest);
            }
            catch (PodException podException)
            {
                Console.WriteLine(
                    $"-- {podException.Code}-an error has occured : {Environment.NewLine}{podException.Message}");
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        public async Task AddContact()
        {
            try
            {
                var output = new ResultSrv<List<Contact>>();
                var addContacts = AddContactsRequest.ConcreteBuilder
                    .SetAddContactList(new List<AddContactRequest>
                    {
                         AddContactRequest.ConcreteBuilder.SetUsername("").Build()
                    })
                    .Build();

                await ServiceLocator.ChatService.AddContact(addContacts, response => CoreListener.GetResult(response, out output));
            }
            catch (PodException podException)
            {
                Console.WriteLine(
                    $"-- {podException.Code}-an error has occured : {Environment.NewLine}{podException.Message}");
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        public async Task RemoveContact()
        {
            try
            {
                var output = new ResultSrv<bool>();
                var removeContacts = RemoveContactsRequest.ConcreteBuilder
                    .SetId(0)
                    .Build();

                await ServiceLocator.ChatService.RemoveContact(removeContacts, response => CoreListener.GetResult(response, out output));
            }
            catch (PodException podException)
            {
                Console.WriteLine(
                    $"-- {podException.Code}-an error has occured : {Environment.NewLine}{podException.Message}");
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        public async Task UpdateContact()
        {
            try
            {
                var output = new ResultSrv<List<Contact>>();
                var requestUpdateContact = UpdateContactsRequest.ConcreteBuilder
                    .SetId(0)
                    .SetFirstName("")
                    .SetLastName("")
                    .SetEmail("")
                    .SetCellphoneNumber("")
                    .SetUniqueId("")
                    //.SetTypeCode("")
                    .Build();

                await ServiceLocator.ChatService.UpdateContact(requestUpdateContact, response => CoreListener.GetResult(response, out output));
            }
            catch (PodException podException)
            {
                Console.WriteLine(
                    $"-- {podException.Code}-an error has occured : {Environment.NewLine}{podException.Message}");
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        public async Task SearchContact()
        {
            try
            {
                var output = new ResultSrv<List<Contact>>();
                var requestSearchContact = SearchContactsRequest.ConcreteBuilder
                    .SetId(0)
                    //.SetFirstName("")
                    //.SetLastName("")
                    //.SetEmail("")
                    //.SetCellphoneNumber("")
                    //.SetUniqueId("")
                    //.SetQuery("")
                    //.SetTypeCode("")
                    //.SetOwnerId("")
                    //.SetSize(0)
                    //.SetOffset(0)
                    //.SetTypeCode("")
                    .Build();

                await ServiceLocator.ChatService.SearchContact(requestSearchContact,
                     response => CoreListener.GetResult(response, out output));
            }
            catch (PodException podException)
            {
                Console.WriteLine(
                    $"-- {podException.Code}-an error has occured : {Environment.NewLine}{podException.Message}");
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        public void GetContacts()
        {
            try
            {
                var getContact = GetContactsRequest.ConcreteBuilder
                    //.SetOffset(0)
                    //.SetSize(0)
                    //.SetId(contactIdShekari)
                    //.SetCellphoneNumber("")
                    //.SetEmail("")
                    //.SetUniqueId("")
                    //.SetQuery("")
                    //.SetTypeCode("")
                    .Build();

                uniqueIds["GetContacts"] = ServiceLocator.ChatService.GetContacts(getContact);
            }
            catch (PodException podException)
            {
                Console.WriteLine(
                    $"-- {podException.Code}-an error has occured : {Environment.NewLine}{podException.Message}");
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        public void AddParticipants()
        {
            try
            {
                var addParticipantsRequest = AddParticipantsRequest.ConcreteBuilder
                    .SetThreadId(0)
                    .SetContactIds(new long[] { 0 })
                    //.SetUserNames(new []{ "" })
                    //.SetCoreUserIds(new long[]{ 0 })
                    //.SetTypeCode("")
                    .Build();

                uniqueIds["AddParticipants"] = ServiceLocator.ChatService.AddParticipants(addParticipantsRequest);
            }
            catch (PodException podException)
            {
                Console.WriteLine(
                    $"-- {podException.Code}-an error has occured : {Environment.NewLine}{podException.Message}");
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        public void RemoveParticipant()
        {
            try
            {
                var removeParticipantsRequest = RemoveParticipantsRequest.ConcreteBuilder
                    .SetThreadId(0)
                    .SetParticipantIds(new long[] { 0 })
                    //.SetTypeCode("")
                    .Build();
                uniqueIds["RemoveParticipants"] = ServiceLocator.ChatService.RemoveParticipants(removeParticipantsRequest);
            }
            catch (PodException podException)
            {
                Console.WriteLine(
                    $"-- {podException.Code}-an error has occured : {Environment.NewLine}{podException.Message}");
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        public void GetThreadParticipants()
        {
            try
            {
                var getThreadParticipantsRequest = GetThreadParticipantsRequest.ConcreteBuilder
                    .SetThreadId(7991)
                    //.SetOffset(0)
                    //.SetCount(0)
                    //.SetTypeCode("")
                    .Build();

                uniqueIds["GetThreadParticipants"] = ServiceLocator.ChatService.GetThreadParticipants(getThreadParticipantsRequest);
            }
            catch (PodException podException)
            {
                Console.WriteLine(
                    $"-- {podException.Code}-an error has occured : {Environment.NewLine}{podException.Message}");
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        public void DeleteMessage()
        {
            try
            {
                var deleteMessageRequest = DeleteMessageRequest.ConcreteBuilder
                    .SetMessageId(89320)
                    //.SetDeleteForAll(false)
                    //.SetTypeCode("")
                    .Build();

                uniqueIds["DeleteMessage"] = ServiceLocator.ChatService.DeleteMessage(deleteMessageRequest);
            }
            catch (PodException podException)
            {
                Console.WriteLine(
                    $"-- {podException.Code}-an error has occured : {Environment.NewLine}{podException.Message}");
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        public void DeleteMultipleMessage()
        {
            try
            {
                var deleteMultipleMessagesRequest = DeleteMultipleMessagesRequest.ConcreteBuilder
                    .SetMessageIds(new long[] { 0, 0 })
                    //.SetDeleteForAll(true)
                    //.SetTypeCode("default")
                    .Build();

                var uniques = ServiceLocator.ChatService.DeleteMultipleMessages(deleteMultipleMessagesRequest);
            }
            catch (PodException podException)
            {
                Console.WriteLine(
                    $"-- {podException.Code}-an error has occured : {Environment.NewLine}{podException.Message}");
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        public void ForwardMessage()
        {
            try
            {
                var forwardMessageRequest = ForwardMessageRequest.ConcreteBuilder
                    .SetThreadId(0)
                    .SetMessageIds(new long[] { 0, 0 })
                    //.SetMetadata("")
                    //.SetTypeCode("")
                    .Build();

                var uniques = ServiceLocator.ChatService.ForwardMessage(forwardMessageRequest);
            }
            catch (PodException podException)
            {
                Console.WriteLine(
                    $"-- {podException.Code}-an error has occured : {Environment.NewLine}{podException.Message}");
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        public void ReplyTextMessage()
        {
            try
            {
                var replyTextMessageRequest = ReplyTextMessageRequest.ConcreteBuilder
                    .SetRepliedTo(0)
                    .SetTextMessageRequest
                    (
                       SendTextMessageRequest.ConcreteBuilder
                                             .SetThreadId(0)
                                             .SetTextMessage("")
                                             .SetMessageType(MessageType.TEXT)
                                             .SetMetadata("")
                                             .SetSystemMetadata("")
                                             .SetTypeCode("")
                                             .Build()
                    )
                    .Build();

                uniqueIds["ReplyTextMessage"] = ServiceLocator.ChatService.ReplyTextMessage(replyTextMessageRequest);
            }
            catch (PodException podException)
            {
                Console.WriteLine(
                    $"-- {podException.Code}-an error has occured : {Environment.NewLine}{podException.Message}");
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        private void GetHistory()
        {
            try
            {
                var getHistoryRequest = GetHistoryRequest.ConcreteBuilder
                    //.SetThreadId(0)
                    //.SetId(0)
                    //.SetOffset(0)
                    //.SetCount(20)
                    //.SetFirstMessageId(0)
                    //.SetLastMessageId(0)
                    //.SetFromTime(0)
                    //.SetToTime(0)
                    //.SetFromTimeNanos(0)
                    //.SetToTimeNanos(0)
                    //.SetQuery("")
                    //.SetUserId(0)
                    //.SetuniqueIds["GetThreads"]s(new string[] { })
                    //.SetOrder(OrderType.asc)
                    //.SetMetadataCriteria("")
                    //.SetMessageType(SendMessageType.TEXT)
                    //.SetUnreadMentioned(false)
                    //.SetAllMentioned(false)
                    .Build();

                uniqueIds["GetHistory"] = ServiceLocator.ChatService.GetHistory(getHistoryRequest);
            }
            catch (PodException podException)
            {
                Console.WriteLine(
                    $"-- {podException.Code}-an error has occured : {Environment.NewLine}{podException.Message}");
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        private void JoinPublicThread()
        {
            try
            {
                var joinPublicThreadRequest = JoinPublicThreadRequest.ConcreteBuilder
                    .SetUniqueName("uniqueNameTest")
                    .Build();

                uniqueIds["JoinPublicThread"] = ServiceLocator.ChatService.JoinPublicThread(joinPublicThreadRequest);
            }
            catch (PodException podException)
            {
                Console.WriteLine(
                    $"-- {podException.Code}-an error has occured : {Environment.NewLine}{podException.Message}");
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        private void IsPublicThreadNameAvailable()
        {
            try
            {
                var isPublicThreadNameAvailableRequest = IsPublicThreadNameAvailableRequest.ConcreteBuilder
                    .SetUniqueName("test")
                    .Build();

                uniqueIds["IsPublicThreadNameAvailable"] = ServiceLocator.ChatService.IsPublicThreadNameAvailable(isPublicThreadNameAvailableRequest);
            }
            catch (PodException podException)
            {
                Console.WriteLine(
                    $"-- {podException.Code}-an error has occured : {Environment.NewLine}{podException.Message}");
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        private void LeaveThread()
        {
            try
            {
                var threadGeneralRequest = ThreadGeneralRequest.ConcreteBuilder
                    .SetThreadId(0)
                    //.SetTypeCode("")
                    .Build();

                uniqueIds["LeaveThread"] = ServiceLocator.ChatService.LeaveThread(threadGeneralRequest);
            }
            catch (PodException podException)
            {
                Console.WriteLine(
                    $"-- {podException.Code}-an error has occured : {Environment.NewLine}{podException.Message}");
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        private void EditMessage()
        {
            try
            {
                var editMessageRequest = EditMessageRequest.ConcreteBuilder
                    .SetMessageId(0)
                    //.SetTextMessage("testMessage")
                    //.SetSystemMetadata("")              
                    //.SetTypeCode("")
                    .Build();

                uniqueIds["EditMessage"] = ServiceLocator.ChatService.EditMessage(editMessageRequest);
            }
            catch (PodException podException)
            {
                Console.WriteLine(
                    $"-- {podException.Code}-an error has occured : {Environment.NewLine}{podException.Message}");
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        private void MuteThread()
        {
            try
            {
                var threadGeneralRequest = ThreadGeneralRequest.ConcreteBuilder
                    .SetThreadId(0)
                    //.SetTypeCode("")
                    .Build();

                uniqueIds["MuteThread"] = ServiceLocator.ChatService.MuteThread(threadGeneralRequest);
            }
            catch (PodException podException)
            {
                Console.WriteLine(
                    $"-- {podException.Code}-an error has occured : {Environment.NewLine}{podException.Message}");
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        private void UnMuteThread()
        {
            try
            {
                var threadGeneralRequest = ThreadGeneralRequest.ConcreteBuilder
                   .SetThreadId(0)
                   //.SetTypeCode("")
                   .Build();

                uniqueIds["UnMuteThread"] = ServiceLocator.ChatService.UnMuteThread(threadGeneralRequest);
            }
            catch (PodException podException)
            {
                Console.WriteLine(
                    $"-- {podException.Code}-an error has occured : {Environment.NewLine}{podException.Message}");
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        private void Block()
        {
            try
            {
                var blockRequest = BlockRequest.ConcreteBuilder
                    .SetContactId(0)
                    //.SetThreadId(7435)
                    //.SetUserId(0)
                    //.SetTypeCode("")
                    .Build();

                uniqueIds["Block"] = ServiceLocator.ChatService.Block(blockRequest);
            }
            catch (PodException podException)
            {
                Console.WriteLine(
                    $"-- {podException.Code}-an error has occured : {Environment.NewLine}{podException.Message}");
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        private void UnBlock()
        {
            try
            {
                var unblockRequest = UnblockRequest.ConcreteBuilder
                    //.SetBlockId(0)
                    //.SetThreadId(0)
                    //.SetContactId(0)
                    //.SetUserId(0)
                    //.SetTypeCode("")
                    .Build();

                uniqueIds["UnBlock"] = ServiceLocator.ChatService.UnBlock(unblockRequest);
            }
            catch (PodException podException)
            {
                Console.WriteLine(
                    $"-- {podException.Code}-an error has occured : {Environment.NewLine}{podException.Message}");
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        private void GetBlockList()
        {
            try
            {
                var getBlockedListRequest = GetBlockedListRequest.ConcreteBuilder
                    .SetOffset(0)
                    .SetCount(10)
                    //.SetTypeCode("")
                    .Build();

                uniqueIds["GetBlockList"] = ServiceLocator.ChatService.GetBlockList(getBlockedListRequest);
            }
            catch (PodException podException)
            {
                Console.WriteLine(
                    $"-- {podException.Code}-an error has occured : {Environment.NewLine}{podException.Message}");
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        private void ClearHistory()
        {
            try
            {
                var threadGeneralRequest = ThreadGeneralRequest.ConcreteBuilder
                    .SetThreadId(0)
                    //.SetTypeCode("")
                    .Build();

                uniqueIds["ClearHistory"] = ServiceLocator.ChatService.ClearHistory(threadGeneralRequest);
            }
            catch (PodException podException)
            {
                Console.WriteLine(
                    $"-- {podException.Code}-an error has occured : {Environment.NewLine}{podException.Message}");
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        private void GetMentionedMessages()
        {
            try
            {
                var getMentionedRequest = GetMentionedRequest.ConcreteBuilder
                    .SetThreadId(0)
                    //.SetOffset(0)
                    //.SetCount(2)
                    //.SetAllMentioned(false)
                    //.SetunreadMentioned(false)
                    .Build();

                uniqueIds["GetMentionedMessages"] = ServiceLocator.ChatService.GetMentionedMessages(getMentionedRequest);
            }
            catch (PodException podException)
            {
                Console.WriteLine(
                    $"-- {podException.Code}-an error has occured : {Environment.NewLine}{podException.Message}");
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        private void PinThread()
        {
            try
            {
                var threadGeneralRequest = ThreadGeneralRequest.ConcreteBuilder
                    .SetThreadId(0)
                    //.SetTypeCode("")
                    .Build();

                uniqueIds["PinThread"] = ServiceLocator.ChatService.PinThread(threadGeneralRequest);
            }
            catch (PodException podException)
            {
                Console.WriteLine(
                    $"-- {podException.Code}-an error has occured : {Environment.NewLine}{podException.Message}");
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        private void UnPinThread()
        {
            try
            {
                var threadGeneralRequest = ThreadGeneralRequest.ConcreteBuilder
                    .SetThreadId(0)
                    //.SetTypeCode("")
                    .Build();

                uniqueIds["UnPinThread"] = ServiceLocator.ChatService.UnPinThread(threadGeneralRequest);
            }
            catch (PodException podException)
            {
                Console.WriteLine(
                    $"-- {podException.Code}-an error has occured : {Environment.NewLine}{podException.Message}");
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        private void PinMessage()
        {
            try
            {
                var pinUnpinMessageRequest = PinUnpinMessageRequest.ConcreteBuilder
                    .SetMessageId(0)
                    //.SetNotifyAll(false)
                    //.SetTypeCode("")
                    .Build();

                uniqueIds["PinMessage"] = ServiceLocator.ChatService.PinMessage(pinUnpinMessageRequest);
            }
            catch (PodException podException)
            {
                Console.WriteLine(
                    $"-- {podException.Code}-an error has occured : {Environment.NewLine}{podException.Message}");
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        private void UnPinMessage()
        {
            try
            {
                var pinUnpinMessageRequest = PinUnpinMessageRequest.ConcreteBuilder
                    .SetMessageId(0)
                    //.SetNotifyAll(false)
                    //.SetTypeCode("")
                    .Build();

                uniqueIds["UnPinMessage"] = ServiceLocator.ChatService.UnPinMessage(pinUnpinMessageRequest);
            }
            catch (PodException podException)
            {
                Console.WriteLine(
                    $"-- {podException.Code}-an error has occured : {Environment.NewLine}{podException.Message}");
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        private void SetAdmin()
        {
            try
            {
                var setRemoveRoleRequest = SetRemoveRoleRequest.ConcreteBuilder
                    .SetThreadId(0)
                    .SetRoles(new List<RoleModel>
                    {
                    RoleModel.ConcreteBuilder.SetUserId(0).SetRoles(new []{RoleType.add_new_user}).Build()
                    })
                    //.SetTypeCode("")
                    .Build();

                uniqueIds["SetAdmin"] = ServiceLocator.ChatService.SetAdmin(setRemoveRoleRequest);
            }
            catch (PodException podException)
            {
                Console.WriteLine(
                    $"-- {podException.Code}-an error has occured : {Environment.NewLine}{podException.Message}");
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        private void RemoveAdmin()
        {
            try
            {
                var setRemoveRoleRequest = SetRemoveRoleRequest.ConcreteBuilder
                    .SetThreadId(0)
                    .SetRoles(new List<RoleModel>
                    {
                    RoleModel.ConcreteBuilder.SetUserId(15505).SetRoles(new []{RoleType.add_new_user}).Build()
                    })
                    //.SetTypeCode("")
                    .Build();

                uniqueIds["RemoveAdmin"] = ServiceLocator.ChatService.RemoveAdmin(setRemoveRoleRequest);
            }
            catch (PodException podException)
            {
                Console.WriteLine(
                    $"-- {podException.Code}-an error has occured : {Environment.NewLine}{podException.Message}");
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        private void SetAuditor()
        {
            try
            {
                var setRemoveRoleRequest = SetRemoveRoleRequest.ConcreteBuilder
                    .SetThreadId(0)
                    .SetRoles(new List<RoleModel>
                    {
                    RoleModel.ConcreteBuilder.SetUserId(0).SetRoles(new []{RoleType.read_thread}).Build()
                    })
                    //.SetTypeCode("")
                    .Build();

                uniqueIds["SetAuditor"] = ServiceLocator.ChatService.SetAuditor(setRemoveRoleRequest);
            }
            catch (PodException podException)
            {
                Console.WriteLine(
                    $"-- {podException.Code}-an error has occured : {Environment.NewLine}{podException.Message}");
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        private void RemoveAuditor()
        {
            try
            {
                var setRemoveRoleRequest = SetRemoveRoleRequest.ConcreteBuilder
                    .SetThreadId(7569)
                    .SetRoles(new List<RoleModel>
                    {
                    RoleModel.ConcreteBuilder.SetUserId(15596).SetRoles(new []{RoleType.thread_admin}).Build()
                    })
                    //.SetTypeCode("")
                    .Build();

                uniqueIds["RemoveAuditor"] = ServiceLocator.ChatService.RemoveAuditor(setRemoveRoleRequest);
            }
            catch (PodException podException)
            {
                Console.WriteLine(
                    $"-- {podException.Code}-an error has occured : {Environment.NewLine}{podException.Message}");
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        public void GetThreadAdmins()
        {
            try
            {
                var getThreadParticipantsRequest = GetThreadParticipantsRequest.ConcreteBuilder
                    .SetThreadId(0)
                    //.SetName("")
                    //.SetOffset(0)
                    //.SetCount(2)
                    //.SetTypeCode("")
                    .Build();

                uniqueIds["GetThreadAdmins"] = ServiceLocator.ChatService.GetThreadAdmins(getThreadParticipantsRequest);
            }
            catch (PodException podException)
            {
                Console.WriteLine(
                    $"-- {podException.Code}-an error has occured : {Environment.NewLine}{podException.Message}");
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        private void SpamPrivateThread()
        {
            try
            {
                var threadGeneralRequest = ThreadGeneralRequest.ConcreteBuilder
                    .SetThreadId(0)
                    //.SetTypeCode("")
                    .Build();

                uniqueIds["SpamPrivateThread"] = ServiceLocator.ChatService.SpamPrivateThread(threadGeneralRequest);
            }
            catch (PodException podException)
            {
                Console.WriteLine(
                    $"-- {podException.Code}-an error has occured : {Environment.NewLine}{podException.Message}");
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        private void UpdateProfile()
        {
            try
            {
                var updateChatProfileRequest = UpdateChatProfileRequest.ConcreteBuilder
                    .SetMetadata("")
                    .SetBio("")
                    //.SetTypeCode("")
                    .Build();

                uniqueIds["UpdateProfile"] = ServiceLocator.ChatService.UpdateProfile(updateChatProfileRequest);
            }
            catch (PodException podException)
            {
                Console.WriteLine(
                    $"-- {podException.Code}-an error has occured : {Environment.NewLine}{podException.Message}");
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        private void GetCurrentUserRoles()
        {
            try
            {
                var threadGeneralRequest = ThreadGeneralRequest.ConcreteBuilder
                    .SetThreadId(0)
                    //.SetTypeCode("")
                    .Build();

                uniqueIds["GetCurrentUserRoles"] = ServiceLocator.ChatService.GetCurrentUserRoles(threadGeneralRequest);
            }
            catch (PodException podException)
            {
                Console.WriteLine(
                    $"-- {podException.Code}-an error has occured : {Environment.NewLine}{podException.Message}");
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        private void SendFileMessage()
        {
            try
            {
                var sendFileMessageRequest = SendFileMessageRequest.ConcreteBuilder
                    .SetUserGroupHash("B2PMAOJU3FMUKC")
                    .SetMessageInput(SendTextMessageRequest.ConcreteBuilder
                                                         .SetThreadId(0)
                                                         .SetMessageType(MessageType.POD_SPACE_FILE)
                                                         .SetTextMessage("testMessage")
                                                         //.SetMetadata("")
                                                         //.SetSystemMetadata("")
                                                         //.SetTypeCode("")
                                                         .Build())
                    .SetUploadInput(UploadRequest.ConcreteBuilder
                                               .SetFilePath(@"D:\test.txt")
                                               .SetFileName("filenametest")
                                               //.SetXC(0)
                                               //.SetYC(0)
                                               //.SetHC(0)
                                               //.SetWC(0)
                                               .Build())
                    .Build();

                uniqueIds["SendFileMessage"] = ServiceLocator.ChatService.SendFileMessage(sendFileMessageRequest);
            }
            catch (PodException podException)
            {
                Console.WriteLine(
                    $"-- {podException.Code}-an error has occured : {Environment.NewLine}{podException.Message}");
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        public void ReplyFileMessage()
        {
            try
            {
                var sendReplyFileMessageRequest = SendReplyFileMessageRequest.ConcreteBuilder
                   .SetRepliedTo(0)
                   .SetSendFileMessage(SendFileMessageRequest.ConcreteBuilder
                                                    .SetUserGroupHash("")
                                                    .SetMessageInput(SendTextMessageRequest.ConcreteBuilder
                                                        .SetThreadId(8071)
                                                        .SetMessageType(MessageType.POD_SPACE_PICTURE)
                                                        .SetTextMessage("")
                                                        //.SetMetadata("")
                                                        //.SetSystemMetadata("")
                                                        //.SetTypeCode("")
                                                        .Build())
                                                    .SetUploadInput(UploadRequest.ConcreteBuilder
                                                        .SetFilePath("")
                                                        //.SetDescription("")
                                                        .SetFileName("")
                                                        //.SetXC(0)
                                                        //.SetYC(0)
                                                        //.SetHC(0)
                                                        //.SetWC(0)
                                                        .Build())
                                        .Build())
                    .Build();

                uniqueIds["ReplyFileMessage"] = ServiceLocator.ChatService.ReplyFileMessage(sendReplyFileMessageRequest);
            }
            catch (PodException podException)
            {
                Console.WriteLine(
                    $"-- {podException.Code}-an error has occured : {Environment.NewLine}{podException.Message}");
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        private void GetUnreadMessageCount()
        {
            try
            {
                var unreadMessageCountRequest = UnreadMessageCountRequest.ConcreteBuilder
                    .SetMute(false)
                    //.SetTypeCode("")
                    .Build();

                uniqueIds["GetUnreadMessageCount"] = ServiceLocator.ChatService.GetUnreadMessageCount(unreadMessageCountRequest);
            }
            catch (PodException podException)
            {
                Console.WriteLine(
                    $"-- {podException.Code}-an error has occured : {Environment.NewLine}{podException.Message}");
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        public void DownloadImage()
        {
            try
            {
                var requestSearchContact = DownloadPodSpaceImageRequest.ConcreteBuilder
                    .SetHash("JGLGXRRKATD6B3BK")
                    .SetDownloadPath(@"D:\test")
                    .SetFileName("filename")
                    //.SetCrop(false)
                    //.SetQuality(0)
                    //.SetSize(0)
                    .Build();

                var fileObject = ServiceLocator.ChatService.DownloadPodSpaceImage(requestSearchContact);
            }
            catch (PodException podException)
            {
                Console.WriteLine(
                    $"-- {podException.Code}-an error has occured : {Environment.NewLine}{podException.Message}");
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        public void DownloadFile()
        {
            try
            {
                var downloadPodSpaceFileRequest = DownloadPodSpaceFileRequest.ConcreteBuilder
                    .SetHash("")
                    .SetDownloadPath("")
                    .Build();

                var fileObject = ServiceLocator.ChatService.DownloadPodSpaceFile(downloadPodSpaceFileRequest);
            }
            catch (PodException podException)
            {
                Console.WriteLine(
                    $"-- {podException.Code}-an error has occured : {Environment.NewLine}{podException.Message}");
                throw;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        #endregion Request

        #region Response

        public void OnGetThreads(ChatResponseSrv<GetThreadsModel> result)
        {
            if (result.uniqueIds["GetThreads"].Equals(uniqueIds["GetThreads"]))
            {

            }
        }

        public void OnCreateThread(ChatResponseSrv<Conversation> result)
        {
            if (result.uniqueIds["GetThreads"].Equals(createThreaduniqueIds["GetThreads"]))
            {

            }
        }

        public void OnUpdateThreadInfo(ChatResponseSrv<Conversation> result)
        {
            if (result.uniqueIds["GetThreads"].Equals(uniqueIds["GetThreads"]))
            {

            }
        }

        public void OnSendMessage(ChatResponseSrv<SendMessageModel> result)
        {
            if (result.uniqueIds["GetThreads"].Equals(sendMessageuniqueIds["GetThreads"]))
            {

            }
        }

        public void OnForwardMessage(ChatResponseSrv<SendMessageModel> result)
        {
            if (forwarduniqueIds["GetThreads"].Contains(result.uniqueIds["GetThreads"]))
            {

            }
            var ff = forwarduniqueIds["GetThreads"].FirstOrDefault(s => s == result.uniqueIds["GetThreads"]);
            if (forwarduniqueIds["GetThreads"].FirstOrDefault(s => s == result.uniqueIds["GetThreads"]) != null)
            {

            }
        }

        public void OnAddParticipant(ChatResponseSrv<AddParticipantModel> result)
        {
            if (result.uniqueIds["GetThreads"].Equals(uniqueIds["GetThreads"]))
            {

            }
        }
        public void OnRemoveParticipant(ChatResponseSrv<GetThreadParticipantsModel> result)
        {
            if (result.uniqueIds["GetThreads"].Equals(uniqueIds["GetThreads"]))
            {

            }
        }

        public void OnSeen(ChatResponseSrv<SendMessageModel> result)
        {
            if (result.uniqueIds["GetThreads"].Equals(uniqueIds["GetThreads"]))
            {

            }
        }

        public void OnSent(ChatResponseSrv<SendMessageModel> result)
        {
            if (result.uniqueIds["GetThreads"].Equals(sendMessageuniqueIds["GetThreads"]))
            {
                //105287
                //integ : 97290
            }
        }
        public void OnDelivery(ChatResponseSrv<SendMessageModel> result)
        {
            if (result.uniqueIds["GetThreads"].Equals(uniqueIds["GetThreads"]))
            {

            }
        }

        public void OnGetHistory(ChatResponseSrv<GetHistoryModel> result)
        {
            if (result.uniqueIds["GetThreads"].Equals(uniqueIds["GetThreads"]))
            {

            }
        }

        public void OnDelete(ChatResponseSrv<DeleteMessageModel> result)
        {
            if (result.uniqueIds["GetThreads"].Equals(uniqueIds["GetThreads"]))
            {

            }
            var ff = multipleDeleteuniqueIds["GetThreads"].FirstOrDefault(s => s == result.uniqueIds["GetThreads"]);
            if (multipleDeleteuniqueIds["GetThreads"].FirstOrDefault(s => s == result.uniqueIds["GetThreads"]) != null)
            {

            }
        }
        public void OnChatError(ChatResponseSrv<AsyncErrorMessage> result)
        {
            if (result.uniqueIds["GetThreads"].Equals(uniqueIds["GetThreads"]))
            {

            }
        }

        private void OnGetContacts(ChatResponseSrv<GetContactsResponse> result)
        {
            if (result.uniqueIds["GetThreads"].Equals(uniqueIds["GetThreads"]))
            {
                //contactid =8558 , userid=15508  , username= "masoudmanson" , 8686,15505,fatemeh
            }
        }

        private void OnJoinPublicThread(ChatResponseSrv<Conversation> result)
        {
            if (result.uniqueIds["GetThreads"].Equals(uniqueIds["GetThreads"]))
            {

            }
        }

        private void OnIsPublicThreadNameAvailable(ChatResponseSrv<IsAvailableNameModel> result)
        {
            if (result.uniqueIds["GetThreads"].Equals(uniqueIds["GetThreads"]))
            {

            }
        }

        private void OnLeaveThread(ChatResponseSrv<LeaveThreadModel> result)
        {
            if (result.uniqueIds["GetThreads"].Equals(uniqueIds["GetThreads"]))
            {

            }
        }

        private void OnEditMessage(ChatResponseSrv<SendMessageModel> result)
        {
            if (result.uniqueIds["GetThreads"].Equals(uniqueIds["GetThreads"]))
            {

            }
        }

        private void OnMuteThread(ChatResponseSrv<MuteUnmuteThreadModel> result)
        {
            if (result.uniqueIds["GetThreads"].Equals(uniqueIds["GetThreads"]))
            {

            }
        }

        private void OnUnMuteThread(ChatResponseSrv<MuteUnmuteThreadModel> result)
        {
            if (result.uniqueIds["GetThreads"].Equals(uniqueIds["GetThreads"]))
            {

            }
        }

        private void OnBlock(ChatResponseSrv<BlockUnblockUserResponse> result)
        {
            if (result.uniqueIds["GetThreads"].Equals(uniqueIds["GetThreads"]))
            {

            }
        }

        private void OnUnBlock(ChatResponseSrv<BlockUnblockUserResponse> result)
        {
            if (result.uniqueIds["GetThreads"].Equals(uniqueIds["GetThreads"]))
            {

            }
        }

        private void OnBlockList(ChatResponseSrv<GetBlockedUserListResponse> result)
        {
            if (result.uniqueIds["GetThreads"].Equals(uniqueIds["GetThreads"]))
            {

            }
        }

        private void OnClearHistory(ChatResponseSrv<ClearHistoryModel> result)
        {
            if (result.uniqueIds["GetThreads"].Equals(uniqueIds["GetThreads"]))
            {

            }
        }

        private void OnPinThread(ChatResponseSrv<PinUnpinThreadModel> result)
        {
            if (result.uniqueIds["GetThreads"].Equals(uniqueIds["GetThreads"]))
            {

            }
        }

        private void OnUnPinThread(ChatResponseSrv<PinUnpinThreadModel> result)
        {
            if (result.uniqueIds["GetThreads"].Equals(uniqueIds["GetThreads"]))
            {

            }
        }

        private void OnPinMessage(ChatResponseSrv<PinUnpinMessageModel> result)
        {
            if (result.uniqueIds["GetThreads"].Equals(uniqueIds["GetThreads"]))
            {

            }
        }

        private void OnUnPinMessage(ChatResponseSrv<PinUnpinMessageModel> result)
        {
            if (result.uniqueIds["GetThreads"].Equals(uniqueIds["GetThreads"]))
            {

            }
        }

        private void OnSetRoleToUser(ChatResponseSrv<UserRolesModel> result)
        {
            if (result.uniqueIds["GetThreads"].Equals(uniqueIds["GetThreads"]))
            {

            }
        }

        private void OnRemoveRoleFromUser(ChatResponseSrv<UserRolesModel> result)
        {
            if (result.uniqueIds["GetThreads"].Equals(uniqueIds["GetThreads"]))
            {

            }
        }

        private void OnUpdateProfile(ChatResponseSrv<Profile> result)
        {
            if (result.uniqueIds["GetThreads"].Equals(uniqueIds["GetThreads"]))
            {

            }
        }

        private void OnGetCurrentUserRoles(ChatResponseSrv<GetCurrentUserRolesModel> result)
        {
            if (result.uniqueIds["GetThreads"].Equals(uniqueIds["GetThreads"]))
            {

            }
        }

        #endregion Response
    }
}
