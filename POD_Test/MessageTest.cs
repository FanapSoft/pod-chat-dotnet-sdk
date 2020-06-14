using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using POD_Async.Core;
using POD_Async.Core.ResultModel;
using POD_Async.Model;
using POD_Chat;
using POD_Chat.Base;
using POD_Chat.Base.Enum;
using POD_Chat.Model.ServiceOutput;
using POD_Chat.Model.ValueObject;

namespace POD_Test
{
    [TestFixture]
    public class MessageTest
    {
        #region Setup

        private string apiToken = "**********";
        private string imagePath;

        [SetUp]
        public void SetConfig()
        {
            var config = POD_Async.Base.AsyncConfig.ConcreteBuilder.SetQueueUrl(new List<QueueUrl>
                {
                    QueueUrl.ConcreteBuilder.SetIp("**********").SetPort(0).Build()
                })
                .SetQueueUsername("**********")
                .SetQueuePassword("**********")
                .SetQueueReceive("**********")
                .SetQueueSend("**********")
                .SetQueueConnectionTimeout(0)
                .SetServerName("**********")
                .SetFileServer("**********")
                .SetPlatformHost("**********")
                .SetSsoHost("**********")
                .Build();

            var chatConfig = new ChatConfig(config, apiToken);
            CoreConfig.ServerType = ServerType.SandBox;
            new ServiceLocator(chatConfig);
            var workingDirectory = Directory.GetCurrentDirectory();
            imagePath= Directory.GetParent(workingDirectory).Parent.Parent.FullName+ @"\Resources\UploadImg.jpg";
        }

        #endregion Setup

        #region Profile

        [Test]
        public void UpdateProfile()
        {
            var resultUniqueId = string.Empty;
            var manualResetEvent = new ManualResetEvent(false);
            var requestUpdateProfile = UpdateChatProfileRequest.ConcreteBuilder
                .SetMetadata("test")
                .SetBio("Hello")
                //.SetTypeCode("")
                .Build();

            var generatedUniqueId = ServiceLocator.ChatService.UpdateProfile(requestUpdateProfile);

            ServiceLocator.ResponseHandler.UpdateProfile_MessageReceived +=
                delegate (ChatResponseSrv<Profile> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        resultUniqueId = result.UniqueId;
                        manualResetEvent.Set();
                    }
                };

            manualResetEvent.WaitOne();
            Assert.AreEqual(resultUniqueId, generatedUniqueId, "There is Something wrong with UpdateProfile");
        }

        [Test]
        public void GetUserInfo()
        {
            var resultUniqueId = string.Empty;
            var manualResetEvent = new ManualResetEvent(false);
            var generatedUniqueId = ServiceLocator.ChatService.GetUserInfo();

            ServiceLocator.ResponseHandler.UserInfo_MessageReceived +=
               delegate (ChatResponseSrv<GetUserInfoResponse> result)
               {
                   if (result.UniqueId.Equals(generatedUniqueId))
                   {
                       resultUniqueId = result.UniqueId;
                       manualResetEvent.Set();
                   }
               };

            manualResetEvent.WaitOne();
            Assert.AreEqual(resultUniqueId, generatedUniqueId, "There is Something wrong with GetUserInfo");
        }

        #endregion Profile

        #region Contact

        [Test]
        public void AddContacts()
        {
            AddContact();
        }

        public long AddContact()
        {
            var output = new ResultSrv<List<Contact>>();
            var addContacts = AddContactsRequest.ConcreteBuilder
                .SetAddContactList(new List<AddContactRequest>
                {
                    AddContactRequest.ConcreteBuilder.SetUsername("**********").Build()
                })
                .Build();

            ServiceLocator.ChatService.AddContact(addContacts, response => CoreListener.GetResult(response, out output)).Wait();

            Assert.IsNotNull(output.Result?.FirstOrDefault()?.Id);
            Assert.Greater(output.Result.FirstOrDefault()?.Id, 0);

            return output.Result.FirstOrDefault().Id;
        }

        [Test]
        public void GetContacts()
        {
            var resultUniqueId = string.Empty;
            var manualResetEvent = new ManualResetEvent(false);
            AddContact();
            var getContact = GetContactsRequest.ConcreteBuilder
                //.SetOffset(0)
                //.SetSize(0)
                //.SetId(0)
                //.SetCellphoneNumber("")
                //.SetEmail("")
                //.SetUniqueId("")
                //.SetTypeCode("")
                .Build();

            var generatedUniqueId = ServiceLocator.ChatService.GetContacts(getContact);
            ServiceLocator.ResponseHandler.GetContacts_MessageReceived +=
                delegate (ChatResponseSrv<GetContactsResponse> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        resultUniqueId = result.UniqueId;
                        manualResetEvent.Set();
                    }
                };

            manualResetEvent.WaitOne();
            Assert.AreEqual(resultUniqueId, generatedUniqueId, "There is Something wrong with GetContacts");
        }

        [Test]
        public void RemoveContact()
        {
            var resultUniqueId = string.Empty;
            var contactId = AddContact();
            var output = new ResultSrv<bool>();
            var removeContacts = RemoveContactsRequest.ConcreteBuilder
                .SetId(contactId)
                .Build();

            ServiceLocator.ChatService.RemoveContact(removeContacts,
               response => CoreListener.GetResult(response, out output)).Wait();

            Assert.True(output.Result, "There is Something wrong with RemoveContact");
        }

        [Test]
        public void UpdateContact()
        {
            var resultUniqueId = string.Empty;
            var contactId = AddContact();

            var output = new ResultSrv<List<Contact>>();
            var requestUpdateContact = UpdateContactsRequest.ConcreteBuilder
                .SetId(contactId)
                .SetFirstName("**********")
                .SetLastName("**********")
                .SetEmail("**********")
                .SetCellphoneNumber("**********")
                .SetUniqueId("**********")
                .SetTypeCode("default")
                .Build();

            ServiceLocator.ChatService.UpdateContact(requestUpdateContact,
                response => CoreListener.GetResult(response, out output)).Wait();

            Assert.True(output.Result.Any(), "There is Something wrong with UpdateContact");
        }

        [Test]
        public void SearchContact()
        {
            var resultUniqueId = string.Empty;
            var manualResetEvent = new ManualResetEvent(false);
            var contactId = AddContact();

            var output = new ResultSrv<List<Contact>>();
            var requestSearchContact = SearchContactsRequest.ConcreteBuilder
                .SetId(contactId)
                //.SetFirstName("**********")
                //.SetLastName("**********")
                //.SetEmail("**********")
                //.SetCellphoneNumber("**********")
                //.SetUniqueId("**********")
                //.SetQuery("")
                //.SetTypeCode("")
                //.SetOwnerId("")
                //.SetSize(0)
                //.SetOffset(0)
                .Build();

            ServiceLocator.ChatService.SearchContact(requestSearchContact,
                response => CoreListener.GetResult(response, out output)).Wait();

            Assert.True(output.Result.Any(), "There is Something wrong with SearchContact");
        }

        #endregion Contact

        #region Thread

        [Test]
        public void CreateThread()
        {
            CreateThreadImplement();
        }

        public (long ContactId,long ThreadId,string UserGroupHash) CreateThreadImplement()
        {
            var resultUniqueId = string.Empty;
            long threadId = 0;
            var userGroupHash = string.Empty;
            var manualResetEvent = new ManualResetEvent(false);
            var contactId = AddContact();
            var requestCreateThreadVo = CreateThreadRequest.ConcreteBuilder
                .SetInvitees(new List<InviteVo>()
                {
                    InviteVo.ConcreteBuilder
                        .SetId(contactId.ToString())
                        .SetIdType(InviteType.TO_BE_USER_CONTACT_ID)
                        .Build()
                })
                .SetType(ThreadType.NORMAL)
                //.SetTitle("sendMessage")
                .Build();

            var generatedUniqueId = ServiceLocator.ChatService.CreateThread(requestCreateThreadVo);
            ServiceLocator.ResponseHandler.CreateThread_MessageReceived +=
                delegate(ChatResponseSrv<Conversation> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        threadId = result.Result.Id??0;
                        resultUniqueId = result.UniqueId;
                        userGroupHash = result.Result.UserGroupHash;
                        manualResetEvent.Set();
                    }
                };

            manualResetEvent.WaitOne();
            Assert.AreEqual(resultUniqueId, generatedUniqueId, "There is Something wrong with CreateThread");
            return (contactId, threadId,userGroupHash);
        }

        public (long ThreadId,string UniqueName,long ContactId) CreatePublicThread()
        {
            var resultUniqueId = string.Empty;
            long threadId = 0;
            var random = new Random();
            var uniquename = "publicgroup" + random.Next(3000);
            var manualResetEvent = new ManualResetEvent(false);
            var contactId = AddContact();
            var requestCreateThreadVo = CreateThreadRequest.ConcreteBuilder
                .SetInvitees(new List<InviteVo>()
                {
                    InviteVo.ConcreteBuilder
                        .SetId(contactId.ToString())
                        .SetIdType(InviteType.TO_BE_USER_CONTACT_ID)
                        .Build()
                })
                .SetType(ThreadType.PUBLIC_GROUP)
                .SetUniqueName(uniquename)
                .SetTitle("publicthread")
                .Build();

            var generatedUniqueId = ServiceLocator.ChatService.CreateThread(requestCreateThreadVo);
            ServiceLocator.ResponseHandler.CreateThread_MessageReceived +=
                delegate (ChatResponseSrv<Conversation> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        threadId = result.Result.Id ?? 0;
                        resultUniqueId = result.UniqueId;
                        manualResetEvent.Set();
                    }
                };

            manualResetEvent.WaitOne();
            Assert.AreEqual(resultUniqueId, generatedUniqueId, "There is Something wrong with CreatePublicThread");
            return (threadId, requestCreateThreadVo.UniqueName,contactId);
        }

        [Test]
        public void CreateThreadWithMessage()
        {
            var resultUniqueId = string.Empty;
            long threadId = 0;
            var manualResetEvent = new ManualResetEvent(false);
            var contactId = AddContact();

            var createThreadWithMessageRequest = CreateThreadWithMessageRequest.ConcreteBuilder
                .SetCreateThreadInput(CreateThreadRequest.ConcreteBuilder
                                         .SetInvitees(new List<InviteVo>()
                                         {
                                             InviteVo.ConcreteBuilder.SetId(contactId.ToString()).SetIdType(InviteType.TO_BE_USER_CONTACT_ID).Build()
                                         })
                                         .SetType(ThreadType.NORMAL)
                                         //.SetTitle("tst")
                                         //.SetUniqueName("groutst8")
                                         //.SetDescription("")
                                         //.SetImage("")
                                         //.SetMetadata("")     
                                         //.SetTypeCode("default")
                                         .Build())
                .SetMessageInput(CreateThreadMessageInput.ConcreteBuilder
                                         .SetMessageType(MessageType.TEXT)
                                         .SetText("txt46464")
                                         //.SetForwardedMessageIds(new long[]{ 114464 , 114463})
                                         .Build())
                .Build();
          
            var generatedUniqueId = ServiceLocator.ChatService.CreateThreadWithMessage(createThreadWithMessageRequest);
            ServiceLocator.ResponseHandler.CreateThread_MessageReceived +=
                delegate (ChatResponseSrv<Conversation> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        threadId = result.Result.Id ?? 0;
                        resultUniqueId = result.UniqueId;
                        manualResetEvent.Set();
                    }
                };

            manualResetEvent.WaitOne();
            Assert.AreEqual(resultUniqueId, generatedUniqueId, "There is Something wrong with CreateThreadWithMessage");
        }

        [Test]
        public void CreateThreadWithFileMessage()
        {
            var resultUniqueId = string.Empty;
            long threadId = 0;
            var manualResetEvent = new ManualResetEvent(false);
            var contactId = AddContact();

            var createThreadWithMessageRequest = CreateThreadWithMessageRequest.ConcreteBuilder
                  .SetCreateThreadInput(CreateThreadRequest.ConcreteBuilder
                      .SetInvitees(new List<InviteVo>()
                      {
                        InviteVo.ConcreteBuilder.SetId(contactId.ToString()).SetIdType(InviteType.TO_BE_USER_CONTACT_ID).Build()
                      })
                      .SetType(ThreadType.NORMAL)
                      //.SetTitle("tst")
                      //.SetUniqueName("groutst8")
                      //.SetDescription("")
                      //.SetImage("")
                      //.SetMetadata("")     
                      //.SetTypeCode("default")
                      .Build())
                  .SetMessageInput(CreateThreadMessageInput.ConcreteBuilder
                      .SetMessageType(MessageType.TEXT)
                      .SetText("txt555")
                      //.SetForwardedMessageIds(new long[]{ 114464 , 114463})
                      .Build())
                  .Build();

            var createThreadWithFileMessageRequest = CreateThreadWithFileMessageRequest.ConcreteBuilder
                        .SetCreateThreadWithMessageInput(createThreadWithMessageRequest)
                        .SetUploadInput(UploadRequest.ConcreteBuilder
                            .SetFilePath(imagePath)
                            .SetFileName("podtst")
                            //.SetXC(0)
                            //.SetYC(0)
                            //.SetHC(0)
                            //.SetWC(0)
                            .Build())
                        .Build();

            var generatedUniqueId = ServiceLocator.ChatService.CreateThreadWithFileMessage(createThreadWithFileMessageRequest);

            ServiceLocator.ResponseHandler.Sent_MessageReceived +=
                delegate (ChatResponseSrv<SendMessageModel> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        resultUniqueId = result.UniqueId;
                        manualResetEvent.Set();
                    }
                };

            ServiceLocator.ResponseHandler.Send_MessageReceived +=
                delegate (ChatResponseSrv<SendMessageModel> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        resultUniqueId = result.UniqueId;
                        manualResetEvent.Set();
                    }
                };

            manualResetEvent.WaitOne();
            Assert.AreEqual(resultUniqueId, generatedUniqueId, "There is Something wrong with CreateThreadWithFileMessage");
        }

        [Test]
        public void UpdateThreadInfo()
        {
            var resultUniqueId = string.Empty;
            var manualResetEvent = new ManualResetEvent(false);
            var threadInfo = CreateThreadImplement();

            var updateThreadInfoRequest = UpdateThreadInfoRequest.ConcreteBuilder
                .SetThreadId(threadInfo.ThreadId)
                //.SetImage("")
                //.SetDescription("")
                .SetTitle("tst updateThreadInfo")
                //.SetUploadInput(UploadRequest.ConcreteBuilder
                //    .SetFilePath(imagePath)
                //    .SetFileName("podtst")
                //    //.SetXC(0)
                //    //.SetYC(0)
                //    //.SetHC(0)
                //    //.SetWC(0)
                //    .Build())
                //.SetUserGroupHash("")
                //.SetMetadata("")
                //.SetTypeCode("default")
                .Build();

            var generatedUniqueId = ServiceLocator.ChatService.UpdateThreadInfo(updateThreadInfoRequest);
            ServiceLocator.ResponseHandler.UpdateThreadInfo_MessageReceived +=
                delegate (ChatResponseSrv<Conversation> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        resultUniqueId = result.UniqueId;
                        manualResetEvent.Set();
                    }
                };

            manualResetEvent.WaitOne();
            Assert.AreEqual(resultUniqueId, generatedUniqueId, "There is Something wrong with UpdateThreadInfo");
        }

        [Test]
        public void GetThreads()
        {
            var resultUniqueId = string.Empty;
            var manualResetEvent = new ManualResetEvent(false);
            CreateThreadImplement();
            var requestThreadVo = GetThreadsRequest.ConcreteBuilder
                .SetOffset(0)
                .Build();

            var generatedUniqueId = ServiceLocator.ChatService.GetThreads(requestThreadVo);
            ServiceLocator.ResponseHandler.GetThreads_MessageReceived +=
                delegate(ChatResponseSrv<GetThreadsModel> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        resultUniqueId = result.UniqueId;
                        manualResetEvent.Set();
                    }
                };

            manualResetEvent.WaitOne();
            Assert.AreEqual(resultUniqueId, generatedUniqueId);
        }

        [Test]
        public void AddParticipant()
        {
            AddParticipantsImplement();
        }

        public (long ParticipantId, long ThreadId) AddParticipantsImplement()
        {
            var resultUniqueId = string.Empty;
            long participantId = 0;
            var manualResetEvent = new ManualResetEvent(false);
            var threadInfo = CreatePublicThread();

            var addParticipants = AddParticipantsRequest.ConcreteBuilder
                .SetThreadId(threadInfo.ThreadId)
                .SetContactIds(new [] {threadInfo.ContactId})
                .Build();

            var generatedUniqueId = ServiceLocator.ChatService.AddParticipants(addParticipants);
            ServiceLocator.ResponseHandler.AddParticipant_MessageReceived +=
                delegate(ChatResponseSrv<AddParticipantModel> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        participantId = result.Result.Thread.Participants.FirstOrDefault().Id;
                        resultUniqueId = result.UniqueId;
                        manualResetEvent.Set();
                    }
                };

            manualResetEvent.WaitOne();
            Assert.AreEqual(resultUniqueId, generatedUniqueId, "There is Something wrong with AddParticipants");
            return (participantId, threadInfo.ThreadId);
        }

        [Test]
        public void GetThreadParticipants()
        {
            var resultUniqueId = string.Empty;
            var manualResetEvent = new ManualResetEvent(false);
            var participantInfo = AddParticipantsImplement();
            var threadParticipant = GetThreadParticipantsRequest.ConcreteBuilder
                .SetThreadId(participantInfo.ThreadId)
                .Build();

            var generatedUniqueId = ServiceLocator.ChatService.GetThreadParticipants(threadParticipant);
            ServiceLocator.ResponseHandler.GetParticipants_MessageReceived +=
                delegate(ChatResponseSrv<GetThreadParticipantsModel> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        resultUniqueId = result.UniqueId;
                        manualResetEvent.Set();
                    }
                };

            manualResetEvent.WaitOne();
            Assert.AreEqual(resultUniqueId, generatedUniqueId, "There is Something wrong with GetThreadParticipants");
        }

        [Test]
        public void RemoveParticipant()
        {
            var resultUniqueId = string.Empty;
            var manualResetEvent = new ManualResetEvent(false);
            var participantInfo = AddParticipantsImplement();
            var requestRemoveParticipants = RemoveParticipantsRequest.ConcreteBuilder
                .SetThreadId(participantInfo.ThreadId)
                .SetParticipantIds(new[] {participantInfo.ParticipantId})
                .Build();

            var generatedUniqueId = ServiceLocator.ChatService.RemoveParticipants(requestRemoveParticipants);
            ServiceLocator.ResponseHandler.RemoveParticipants_MessageReceived +=
                delegate(ChatResponseSrv<GetThreadParticipantsModel> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        resultUniqueId = result.UniqueId;
                        manualResetEvent.Set();
                    }
                };

            manualResetEvent.WaitOne();
            Assert.AreEqual(resultUniqueId, generatedUniqueId, "There is Something wrong with RemoveParticipant");
        }

        [Test]
        public void JoinPublicThread()
        {
            var resultUniqueId = string.Empty;
            var manualResetEvent = new ManualResetEvent(false);
            var random = new Random();
            var threadInfo = CreatePublicThread();
            var requestGetHistory = JoinPublicThreadRequest.ConcreteBuilder
                .SetUniqueName(threadInfo.UniqueName)
                .Build();

            var generatedUniqueId = ServiceLocator.ChatService.JoinPublicThread(requestGetHistory);

            ServiceLocator.ResponseHandler.JoinPublicThread_MessageReceived +=
                delegate (ChatResponseSrv<Conversation> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        resultUniqueId = result.UniqueId;
                        manualResetEvent.Set();
                    }
                };

            manualResetEvent.WaitOne();
            Assert.AreEqual(resultUniqueId, generatedUniqueId, "There is Something wrong with JoinPublicThread");
        }

        [Test]
        public void IsPublicThreadNameAvailable()
        {
            var resultUniqueId = string.Empty;
            var manualResetEvent = new ManualResetEvent(false);
            var isPublicThreadNameAvailableRequest = IsPublicThreadNameAvailableRequest.ConcreteBuilder
                .SetUniqueName("tstNasHd65")
                .Build();

            var generatedUniqueId = ServiceLocator.ChatService.IsPublicThreadNameAvailable(isPublicThreadNameAvailableRequest);

            ServiceLocator.ResponseHandler.IsPublicThreadNameAvailable_MessageReceived +=
                delegate (ChatResponseSrv<IsAvailableNameModel> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        resultUniqueId = result.UniqueId;
                        manualResetEvent.Set();
                    }
                };

            manualResetEvent.WaitOne();
            Assert.AreEqual(resultUniqueId, generatedUniqueId, "There is Something wrong with IsPublicThreadNameAvailable");
        }

        [Test]
        public void LeaveThread()
        {
            var resultUniqueId = string.Empty;
            var manualResetEvent = new ManualResetEvent(false);
            var threadInfo = CreateThreadImplement();
            var threadGeneralRequest = ThreadGeneralRequest.ConcreteBuilder
                .SetThreadId(threadInfo.ThreadId)
                //.SetTypeCode("")
                .Build();

            var generatedUniqueId = ServiceLocator.ChatService.LeaveThread(threadGeneralRequest);

            ServiceLocator.ResponseHandler.LeaveThread_MessageReceived +=
                delegate (ChatResponseSrv<LeaveThreadModel> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        resultUniqueId = result.UniqueId;
                        manualResetEvent.Set();
                    }
                };

            manualResetEvent.WaitOne();
            Assert.AreEqual(resultUniqueId, generatedUniqueId, "There is Something wrong with LeaveThread");
        }

        [Test]
        public void MuteThread()
        {
            MuteThreadImplement();
        }

        private long MuteThreadImplement()
        {
            var resultUniqueId = string.Empty;
            var manualResetEvent = new ManualResetEvent(false);
            var threadInfo = CreateThreadImplement();
            var threadGeneralRequest = ThreadGeneralRequest.ConcreteBuilder
                .SetThreadId(threadInfo.ThreadId)
                //.SetTypeCode("")
                .Build();

            var generatedUniqueId = ServiceLocator.ChatService.MuteThread(threadGeneralRequest);

            ServiceLocator.ResponseHandler.MuteThread_MessageReceived +=
                delegate (ChatResponseSrv<MuteUnmuteThreadModel> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        resultUniqueId = result.UniqueId;
                        manualResetEvent.Set();
                    }
                };

            manualResetEvent.WaitOne();
            Assert.AreEqual(resultUniqueId, generatedUniqueId, "There is Something wrong with MuteThread");
            return threadInfo.ThreadId;
        }

        [Test]
        public void UnMuteThread()
        {
            var resultUniqueId = string.Empty;
            var manualResetEvent = new ManualResetEvent(false);
            var threadId = MuteThreadImplement();
            var threadGeneralRequest = ThreadGeneralRequest.ConcreteBuilder
               .SetThreadId(threadId)
               //.SetTypeCode("")
               .Build();

            var generatedUniqueId = ServiceLocator.ChatService.UnMuteThread(threadGeneralRequest);

            ServiceLocator.ResponseHandler.UnMuteThread_MessageReceived +=
                delegate (ChatResponseSrv<MuteUnmuteThreadModel> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        resultUniqueId = result.UniqueId;
                        manualResetEvent.Set();
                    }
                };

            manualResetEvent.WaitOne();
            Assert.AreEqual(resultUniqueId, generatedUniqueId, "There is Something wrong with GetContacts");
        }

        
        public void Block()
        {
            BlockImplement();
        }

        private long BlockImplement()
        {
            var resultUniqueId = string.Empty;
            long blockId = 0;
            var manualResetEvent = new ManualResetEvent(false);
            var contactId = AddContact();
            var blockRequest = BlockRequest.ConcreteBuilder
                .SetContactId(contactId)
                //.SetThreadId(0)
                //.SetUserId(0)
                //.SetTypeCode("")
                .Build();

            var generatedUniqueId = ServiceLocator.ChatService.Block(blockRequest);

            ServiceLocator.ResponseHandler.Block_MessageReceived +=
                delegate (ChatResponseSrv<BlockUnblockUserResponse> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        blockId = result.Result.BlockedContact.Id;
                        resultUniqueId = result.UniqueId;
                        manualResetEvent.Set();
                    }
                };

            manualResetEvent.WaitOne();
            Assert.AreEqual(resultUniqueId, generatedUniqueId, "There is Something wrong with Block");
            return blockId;
        }

        
        public void UnBlock()
        {
            var resultUniqueId = string.Empty;
            var manualResetEvent = new ManualResetEvent(false);
            var blockId = BlockImplement();
            var unblockRequest = UnblockRequest.ConcreteBuilder
                .SetBlockId(blockId)
                //.SetThreadId(0)
                //.SetContactId(0)
                //.SetUserId(0)
                //.SetTypeCode("")
                .Build();

            var generatedUniqueId = ServiceLocator.ChatService.UnBlock(unblockRequest);

            ServiceLocator.ResponseHandler.UnBlock_MessageReceived +=
                delegate (ChatResponseSrv<BlockUnblockUserResponse> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        resultUniqueId = result.UniqueId;
                        manualResetEvent.Set();
                    }
                };

            manualResetEvent.WaitOne();
            Assert.AreEqual(resultUniqueId, generatedUniqueId, "There is Something wrong with UnBlock");
        }

        
        public void GetBlockList()
        {
            var resultUniqueId = string.Empty;
            var manualResetEvent = new ManualResetEvent(false);
            BlockImplement();
            var getBlockedListRequest = GetBlockedListRequest.ConcreteBuilder
                .SetOffset(0)
                .SetCount(3)
                //.SetTypeCode("")
                .Build();

            var generatedUniqueId = ServiceLocator.ChatService.GetBlockList(getBlockedListRequest);

            ServiceLocator.ResponseHandler.BlockList_MessageReceived +=
                delegate (ChatResponseSrv<GetBlockedUserListResponse> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        resultUniqueId = result.UniqueId;
                        manualResetEvent.Set();
                    }
                };

            manualResetEvent.WaitOne();
            Assert.AreEqual(resultUniqueId, generatedUniqueId, "There is Something wrong with GetBlockList");
        }

        [Test]
        public void PinThread()
        {
            PinThreadImplement();
        }

        public long PinThreadImplement()
        {
            var resultUniqueId = string.Empty;
            var manualResetEvent = new ManualResetEvent(false);
            var threadInfo = CreateThreadImplement();
            var threadGeneralRequest = ThreadGeneralRequest.ConcreteBuilder
                .SetThreadId(threadInfo.ThreadId)
                //.SetTypeCode("")
                .Build();

            var generatedUniqueId = ServiceLocator.ChatService.PinThread(threadGeneralRequest);

            ServiceLocator.ResponseHandler.PinThread_MessageReceived +=
                delegate (ChatResponseSrv<PinUnpinThreadModel> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        resultUniqueId = result.UniqueId;
                        manualResetEvent.Set();
                    }
                };

            manualResetEvent.WaitOne();
            Assert.AreEqual(resultUniqueId, generatedUniqueId, "There is Something wrong with PinThread");
            return threadInfo.ThreadId;
        }

        [Test]
        public void UnPinThread()
        {
            var resultUniqueId = string.Empty;
            var manualResetEvent = new ManualResetEvent(false);
            var threadId = PinThreadImplement();
            var threadGeneralRequest = ThreadGeneralRequest.ConcreteBuilder
                .SetThreadId(threadId)
                //.SetTypeCode("")
                .Build();

            var generatedUniqueId = ServiceLocator.ChatService.UnPinThread(threadGeneralRequest);

            ServiceLocator.ResponseHandler.UnPinThread_MessageReceived +=
                delegate (ChatResponseSrv<PinUnpinThreadModel> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        resultUniqueId = result.UniqueId;
                        manualResetEvent.Set();
                    }
                };

            manualResetEvent.WaitOne();
            Assert.AreEqual(resultUniqueId, generatedUniqueId, "There is Something wrong with UnPinThread");
        }

        [Test]
        public void SpamPrivateThread()
        {
            var resultUniqueId = string.Empty;
            var manualResetEvent = new ManualResetEvent(false);

            var threadGeneralRequest = ThreadGeneralRequest.ConcreteBuilder
                .SetThreadId(0)
                //.SetTypeCode("")
                .Build();

            var generatedUniqueId = ServiceLocator.ChatService.SpamPrivateThread(threadGeneralRequest);

            ServiceLocator.ResponseHandler.GetHistory_MessageReceived +=
                delegate (ChatResponseSrv<GetHistoryModel> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        resultUniqueId = result.UniqueId;
                        manualResetEvent.Set();
                    }
                };

            manualResetEvent.WaitOne();
            Assert.AreEqual(resultUniqueId, generatedUniqueId, "There is Something wrong with SpamPrivateThread");
        }

        #endregion Thread

        #region Thread_Roles

        
        public void SetAdmin()
        {
            SetAdminImplement();
        }

        public long SetAdminImplement()
        {
            var resultUniqueId = string.Empty;
            var manualResetEvent = new ManualResetEvent(false);
            var threadInfo = CreateThreadImplement();
            var setRemoveRoleRequest = SetRemoveRoleRequest.ConcreteBuilder
                .SetThreadId(threadInfo.ThreadId)
                .SetRoles(new List<RoleModel>
                {
                    RoleModel.ConcreteBuilder.SetUserId(0).SetRoles(new []{RoleType.add_new_user}).Build()
                })
                //.SetTypeCode("")
                .Build();

            var generatedUniqueId = ServiceLocator.ChatService.SetAdmin(setRemoveRoleRequest);

            ServiceLocator.ResponseHandler.SetRoleToUser_MessageReceived +=
                delegate (ChatResponseSrv<UserRolesModel> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        resultUniqueId = result.UniqueId;
                        manualResetEvent.Set();
                    }
                };

            manualResetEvent.WaitOne();
            Assert.AreEqual(resultUniqueId, generatedUniqueId, "There is Something wrong with SetAdmin");
            return threadInfo.ThreadId;
        }

        
        public void RemoveAdmin()
        {
            var resultUniqueId = string.Empty;
            var manualResetEvent = new ManualResetEvent(false);
            var threadId = SetAdminImplement();
            var setRemoveRoleRequest = SetRemoveRoleRequest.ConcreteBuilder
                .SetThreadId(threadId)
                .SetRoles(new List<RoleModel>
                {
                    RoleModel.ConcreteBuilder.SetUserId(0).SetRoles(new []{RoleType.add_new_user}).Build()
                })
                //.SetTypeCode("")
                .Build();

            var generatedUniqueId = ServiceLocator.ChatService.RemoveAdmin(setRemoveRoleRequest);

            ServiceLocator.ResponseHandler.RemoveRoleFromUser_MessageReceived +=
                delegate (ChatResponseSrv<UserRolesModel> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        resultUniqueId = result.UniqueId;
                        manualResetEvent.Set();
                    }
                };

            manualResetEvent.WaitOne();
            Assert.AreEqual(resultUniqueId, generatedUniqueId, "There is Something wrong with RemoveAdmin");
        }

        [Test]
        public void SetAuditor()
        {
            SetAuditorImplement();
        }

        public (long UserId, long ThreadId) SetAuditorImplement()
        {
            var resultUniqueId = string.Empty;
            var manualResetEvent = new ManualResetEvent(false);
            var threadInfo = CreatePublicThread();
            var userId = 0;
            var setRemoveRoleRequest = SetRemoveRoleRequest.ConcreteBuilder
                .SetThreadId(threadInfo.ThreadId)
                .SetRoles(new List<RoleModel>
                {
                    RoleModel.ConcreteBuilder.SetUserId(userId).SetRoles(new []{RoleType.read_thread}).Build()
                })
                //.SetTypeCode("")
                .Build();

            var generatedUniqueId = ServiceLocator.ChatService.SetAuditor(setRemoveRoleRequest);

            ServiceLocator.ResponseHandler.SetRoleToUser_MessageReceived +=
                delegate (ChatResponseSrv<UserRolesModel> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        resultUniqueId = result.UniqueId;
                        manualResetEvent.Set();
                    }
                };

            manualResetEvent.WaitOne();
            Assert.AreEqual(resultUniqueId, generatedUniqueId, "There is Something wrong with SetAuditor");
            return (userId, threadInfo.ThreadId);
        }

        [Test]
        public void RemoveAuditor()
        {
            var resultUniqueId = string.Empty;
            var manualResetEvent = new ManualResetEvent(false);
            var (UserId, ThreadId) = SetAuditorImplement();
            var setRemoveRoleRequest = SetRemoveRoleRequest.ConcreteBuilder
                .SetThreadId(ThreadId)
                .SetRoles(new List<RoleModel>
                {
                    RoleModel.ConcreteBuilder.SetUserId(UserId).SetRoles(new []{RoleType.read_thread}).Build()
                })
                //.SetTypeCode("")
                .Build();

            var generatedUniqueId = ServiceLocator.ChatService.RemoveAuditor(setRemoveRoleRequest);

            ServiceLocator.ResponseHandler.RemoveRoleFromUser_MessageReceived +=
                delegate (ChatResponseSrv<UserRolesModel> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        resultUniqueId = result.UniqueId;
                        manualResetEvent.Set();
                    }
                };

            manualResetEvent.WaitOne();
            Assert.AreEqual(resultUniqueId, generatedUniqueId, "There is Something wrong with RemoveAuditor");
        }

        [Test]
        public void GetThreadAdmins()
        {
            var resultUniqueId = string.Empty;
            var manualResetEvent = new ManualResetEvent(false);
            var threadInfo = CreateThreadImplement();
            var threadParticipant = GetThreadParticipantsRequest.ConcreteBuilder
                .SetThreadId(threadInfo.ThreadId)
                //.SetName("")
                //.SetOffset(0)
                //.SetCount(2)
                //.SetTypeCode("")
                .Build();

            var generatedUniqueId = ServiceLocator.ChatService.GetThreadAdmins(threadParticipant);

            ServiceLocator.ResponseHandler.GetParticipants_MessageReceived +=
                delegate (ChatResponseSrv<GetThreadParticipantsModel> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        resultUniqueId = result.UniqueId;
                        manualResetEvent.Set();
                    }
                };

            manualResetEvent.WaitOne();
            Assert.AreEqual(resultUniqueId, generatedUniqueId, "There is Something wrong with GetThreadAdmins");
        }

        [Test]
        public void GetCurrentUserRoles()
        {
            var resultUniqueId = string.Empty;
            var manualResetEvent = new ManualResetEvent(false);
            var threadInfo = CreateThreadImplement();
            var threadGeneralRequest = ThreadGeneralRequest.ConcreteBuilder
                .SetThreadId(threadInfo.ThreadId)
                //.SetTypeCode("")
                .Build();

            var generatedUniqueId = ServiceLocator.ChatService.GetCurrentUserRoles(threadGeneralRequest);

            ServiceLocator.ResponseHandler.GetUserRoles_MessageReceived +=
                delegate (ChatResponseSrv<GetCurrentUserRolesModel> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        resultUniqueId = result.UniqueId;
                        manualResetEvent.Set();
                    }
                };

            manualResetEvent.WaitOne();
            Assert.AreEqual(resultUniqueId, generatedUniqueId, "There is Something wrong with GetCurrentUserRoles");
        }

        #endregion Thread_Roles

        #region Message

        [Test]
        public void SendMessage()
        {
            SendMessageImplement();
        }

        public (long ThreadId,long MessageId,string UserGroupHash) SendMessageImplement()
        {
            var resultUniqueId = string.Empty;
            long messageId = 0;
            var manualResetEvent = new ManualResetEvent(false);
            var threadInfo = CreateThreadImplement();
            var requestThread = SendTextMessageRequest.ConcreteBuilder
                .SetTextMessage("salam Send2")
                .SetThreadId(threadInfo.ThreadId)
                .SetMessageType(MessageType.TEXT)
                .Build();

            var generatedUniqueId = ServiceLocator.ChatService.SendTextMessage(requestThread);

            ServiceLocator.ResponseHandler.Sent_MessageReceived +=
                delegate (ChatResponseSrv<SendMessageModel> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        resultUniqueId = result.UniqueId;
                    }
                };

            ServiceLocator.ResponseHandler.Send_MessageReceived +=
                delegate(ChatResponseSrv<SendMessageModel> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        messageId = result.Result.Message.Id;
                        resultUniqueId = result.UniqueId;
                        manualResetEvent.Set();
                    }
                };

            manualResetEvent.WaitOne();
            Assert.AreEqual(resultUniqueId, generatedUniqueId, "There is Something wrong with SendMessage");
            return (threadInfo.ThreadId, messageId,threadInfo.UserGroupHash);
        }

        public (long ThreadId, long MessageId) SendMessageInPublicGroupImplement()
        {
            var resultUniqueId = string.Empty;
            long messageId = 0;
            var manualResetEvent = new ManualResetEvent(false);
            var threadInfo = CreatePublicThread();
            var requestThread = SendTextMessageRequest.ConcreteBuilder
                .SetTextMessage("tst SendMessageInPublicGroup")
                .SetThreadId(threadInfo.ThreadId)
                .SetMessageType(MessageType.TEXT)
                .Build();

            var generatedUniqueId = ServiceLocator.ChatService.SendTextMessage(requestThread);

            ServiceLocator.ResponseHandler.Sent_MessageReceived +=
                delegate (ChatResponseSrv<SendMessageModel> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        resultUniqueId = result.UniqueId;
                    }
                };

            ServiceLocator.ResponseHandler.Send_MessageReceived +=
                delegate (ChatResponseSrv<SendMessageModel> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        messageId = result.Result.Message.Id;
                        resultUniqueId = result.UniqueId;
                        manualResetEvent.Set();
                    }
                };

            manualResetEvent.WaitOne();
            Assert.AreEqual(resultUniqueId, generatedUniqueId, "There is Something wrong with SendMessage");
            return (threadInfo.ThreadId, messageId);
        }

        [Test]
        public void ReplyMessage()
        {
            var resultUniqueId = string.Empty;
            var sentFired = false;
            var manualResetEvent = new ManualResetEvent(false);
            var messageInfo = SendMessageImplement();
            var requestReplyMessage = ReplyTextMessageRequest.ConcreteBuilder
                .SetRepliedTo(messageInfo.MessageId)
                .SetTextMessageRequest(SendTextMessageRequest.ConcreteBuilder.SetMessageType(MessageType.TEXT)
                .SetTextMessage("tst")
                .SetThreadId(messageInfo.ThreadId)
                .Build())
                .Build();

            var generatedUniqueId = ServiceLocator.ChatService.ReplyTextMessage(requestReplyMessage);

            ServiceLocator.ResponseHandler.Sent_MessageReceived +=
                delegate(ChatResponseSrv<SendMessageModel> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        sentFired = true;
                    }
                };

            ServiceLocator.ResponseHandler.Send_MessageReceived +=
                delegate(ChatResponseSrv<SendMessageModel> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        resultUniqueId = result.UniqueId;
                        manualResetEvent.Set();
                    }
                };

            manualResetEvent.WaitOne();
            Assert.IsTrue(sentFired,"Message has not been received");
            Assert.AreEqual(resultUniqueId, generatedUniqueId, "There is Something wrong with SendMessage");
        }

        [Test]
        public void ForwardMessage()
        {
            var resultUniqueId = string.Empty;
            var sentFired = false;
            var manualResetEvent = new ManualResetEvent(false);
            var messageInfo = SendMessageImplement();
            var forwardMessage = ForwardMessageRequest.ConcreteBuilder
                .SetThreadId(messageInfo.ThreadId)
                .SetMessageIds(new [] { messageInfo.MessageId })
                .Build();

            var generatedUniqueId = ServiceLocator.ChatService.ForwardMessage(forwardMessage)[0];

            ServiceLocator.ResponseHandler.Sent_MessageReceived +=
                delegate (ChatResponseSrv<SendMessageModel> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        sentFired = true;
                    }
                };

            ServiceLocator.ResponseHandler.Forward_MessageReceived +=
                delegate (ChatResponseSrv<SendMessageModel> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        resultUniqueId = result.UniqueId;
                        manualResetEvent.Set();
                    }
                };

            manualResetEvent.WaitOne();
            Assert.IsTrue(sentFired, "Message has not been received");
            Assert.AreEqual(resultUniqueId, generatedUniqueId, "There is Something wrong with ForwardMessage");
        }

        [Test]
        public void DeleteMessage()
        {
            var resultUniqueId = string.Empty;
            var manualResetEvent = new ManualResetEvent(false);
            var messageInfo = SendMessageImplement();
            var deleteMessage = DeleteMessageRequest.ConcreteBuilder
                .SetMessageId(messageInfo.MessageId)
                .SetDeleteForAll(false)
                .Build();

            var generatedUniqueId = ServiceLocator.ChatService.DeleteMessage(deleteMessage);

            ServiceLocator.ResponseHandler.Delete_MessageReceived +=
                delegate (ChatResponseSrv<DeleteMessageModel> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        resultUniqueId = result.UniqueId;
                        manualResetEvent.Set();
                    }
                };

            manualResetEvent.WaitOne();
            Assert.AreEqual(resultUniqueId, generatedUniqueId, "There is Something wrong with DeleteMessage");
        }

        [Test]
        public void DeleteMultipleMessage()
        {
            var resultUniqueId = string.Empty;
            var manualResetEvent = new ManualResetEvent(false);
            var messageInfo = SendMessageImplement();
            var messageInfo2 = SendMessageImplement();

            var deleteMessage = DeleteMultipleMessagesRequest.ConcreteBuilder
                .SetMessageIds(new[] { messageInfo.MessageId, messageInfo2.MessageId })
                //.SetDeleteForAll(false)
                //.SetTypeCode("")
                .Build();

            var generatedUniqueId = ServiceLocator.ChatService.DeleteMultipleMessages(deleteMessage);

            ServiceLocator.ResponseHandler.Delete_MessageReceived +=
                delegate (ChatResponseSrv<DeleteMessageModel> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        resultUniqueId = result.UniqueId;
                        manualResetEvent.Set();
                    }
                };

            manualResetEvent.WaitOne();
            Assert.AreEqual(resultUniqueId, generatedUniqueId, "There is Something wrong with DeleteMessage");
        }

        [Test]
        public void GetHistory()
        {
            var resultUniqueId = string.Empty;
            var manualResetEvent = new ManualResetEvent(false);
            var messageInfo = SendMessageImplement();
            var requestGetHistory = GetHistoryRequest.ConcreteBuilder
                .SetThreadId(messageInfo.ThreadId)
                //.SetId(0)
                //.SetOffset(0)
                //.SetCount(3)
                //.SetFirstMessageId(0)
                //.SetLastMessageId(0)
                //.SetFromTime(0)
                //.SetToTime(0)
                //.SetFromTimeNanos(0)
                //.SetToTimeNanos(0)
                //.SetQuery("")
                //.SetUserId(0)
                //.SetUniqueIds(new string[] { })
                //.SetOrder(OrderType.asc)
                //.SetMetadataCriteria("")
                //.SetMessageType(SendMessageType.TEXT)
                //.SetUnreadMentioned(false)
                //.SetAllMentioned(false)
                .Build();

            var generatedUniqueId = ServiceLocator.ChatService.GetHistory(requestGetHistory);

            ServiceLocator.ResponseHandler.GetHistory_MessageReceived +=
                delegate (ChatResponseSrv<GetHistoryModel> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        resultUniqueId = result.UniqueId;
                        manualResetEvent.Set();
                    }
                };

            manualResetEvent.WaitOne();
            Assert.AreEqual(resultUniqueId, generatedUniqueId, "There is Something wrong with GetHistory");
        }

        
        public void EditMessage()
        {
            var resultUniqueId = string.Empty;
            var manualResetEvent = new ManualResetEvent(false);
            var messageInfo = SendMessageImplement();
            var editMessageRequest = EditMessageRequest.ConcreteBuilder
                .SetMessageId(messageInfo.MessageId)
                .SetTextMessage("test 2021")
                //.SetSystemMetadata("")              
                //.SetTypeCode("")
                .Build();

            var generatedUniqueId = ServiceLocator.ChatService.EditMessage(editMessageRequest);

            ServiceLocator.ResponseHandler.EditMessage_MessageReceived +=
                delegate (ChatResponseSrv<SendMessageModel> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        resultUniqueId = result.UniqueId;
                        manualResetEvent.Set();
                    }
                };

            manualResetEvent.WaitOne();
            Assert.AreEqual(resultUniqueId, generatedUniqueId, "There is Something wrong with EditMessage");
        }

        
        public void ClearHistory()
        {
            var resultUniqueId = string.Empty;
            var manualResetEvent = new ManualResetEvent(false);
            var messageInfo = SendMessageImplement();
            var threadGeneralRequest = ThreadGeneralRequest.ConcreteBuilder
                .SetThreadId(messageInfo.MessageId)
                //.SetTypeCode("")
                .Build();

            var generatedUniqueId = ServiceLocator.ChatService.ClearHistory(threadGeneralRequest);

            ServiceLocator.ResponseHandler.ClearHistory_MessageReceived +=
                delegate (ChatResponseSrv<ClearHistoryModel> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        resultUniqueId = result.UniqueId;
                        manualResetEvent.Set();
                    }
                };

            manualResetEvent.WaitOne();
            Assert.AreEqual(resultUniqueId, generatedUniqueId, "There is Something wrong with ClearHistory");
        }

        
        public void GetMentionList()
        {
            var resultUniqueId = string.Empty;
            var manualResetEvent = new ManualResetEvent(false);
            var messageInfo = SendMessageImplement();
            var getMentionedRequest = GetMentionedRequest.ConcreteBuilder
                .SetThreadId(messageInfo.ThreadId)
                //.SetOffset(0)
                //.SetCount(2)
                //.SetAllMentioned(false)
                //.SetunreadMentioned(false)
                .Build();

            var generatedUniqueId = ServiceLocator.ChatService.GetMentionedMessages(getMentionedRequest);

            ServiceLocator.ResponseHandler.GetHistory_MessageReceived +=
                delegate (ChatResponseSrv<GetHistoryModel> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        resultUniqueId = result.UniqueId;
                        manualResetEvent.Set();
                    }
                };

            manualResetEvent.WaitOne();
            Assert.AreEqual(resultUniqueId, generatedUniqueId, "There is Something wrong with GetMentionedMessages");
        }

        [Test]
        public void PinMessage()
        {
            PinMessageImplement();
        }

        public long PinMessageImplement()
        {
            var resultUniqueId = string.Empty;
            var manualResetEvent = new ManualResetEvent(false);
            var messageInfo = SendMessageInPublicGroupImplement();
            var pinUnpinMessageRequest = PinUnpinMessageRequest.ConcreteBuilder
                .SetMessageId(messageInfo.MessageId)
                //.SetNotifyAll(false)
                //.SetTypeCode("")
                .Build();

            var generatedUniqueId = ServiceLocator.ChatService.PinMessage(pinUnpinMessageRequest);

            ServiceLocator.ResponseHandler.PinMessage_MessageReceived +=
                delegate (ChatResponseSrv<PinUnpinMessageModel> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        resultUniqueId = result.UniqueId;
                        manualResetEvent.Set();
                    }
                };

            manualResetEvent.WaitOne();
            Assert.AreEqual(resultUniqueId, generatedUniqueId, "There is Something wrong with PinMessage");
            return messageInfo.MessageId;
        }

        [Test]
        public void UnPinMessage()
        {
            var resultUniqueId = string.Empty;
            var manualResetEvent = new ManualResetEvent(false);
            var messageId = PinMessageImplement();
            var pinUnpinMessageRequest = PinUnpinMessageRequest.ConcreteBuilder
                .SetMessageId(messageId)
                //.SetNotifyAll(false)
                //.SetTypeCode("")
                .Build();

            var generatedUniqueId = ServiceLocator.ChatService.UnPinMessage(pinUnpinMessageRequest);

            ServiceLocator.ResponseHandler.UnPinMessage_MessageReceived +=
                delegate (ChatResponseSrv<PinUnpinMessageModel> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        resultUniqueId = result.UniqueId;
                        manualResetEvent.Set();
                    }
                };

            manualResetEvent.WaitOne();
            Assert.AreEqual(resultUniqueId, generatedUniqueId, "There is Something wrong with UnPinMessage");
        }

        [Test]
        public void SendFileMessage()
        {
            var resultUniqueId = string.Empty;
            var manualResetEvent = new ManualResetEvent(false);
            var (contactId, threadId, userGroupHash) = CreateThreadImplement();
            var sendFileMessageRequest = SendFileMessageRequest.ConcreteBuilder
              .SetUserGroupHash(userGroupHash)
              .SetMessageInput(SendTextMessageRequest.ConcreteBuilder.SetThreadId(threadId)
              .SetMessageType(MessageType.POD_SPACE_PICTURE)
              .SetTextMessage($"Tst Send FileMessage-{threadId}")
              //.SetMetadata("")
              //.SetSystemMetadata("")
              //.SetTypeCode("")
              .Build())
              .SetUploadInput(UploadRequest.ConcreteBuilder
                                  .SetFilePath(imagePath)
                                  .SetFileName($"UploadImage-{threadId}")
                                  //.SetXC(0)
                                  //.SetYC(0)
                                  //.SetHC(0)
                                  //.SetWC(0)
                                  .Build())
                .Build();

            var generatedUniqueId = ServiceLocator.ChatService.SendFileMessage(sendFileMessageRequest);

            ServiceLocator.ResponseHandler.Sent_MessageReceived +=
                   delegate (ChatResponseSrv<SendMessageModel> result)
                   {
                       if (result.UniqueId.Equals(generatedUniqueId))
                       {
                           resultUniqueId = result.UniqueId;
                       }
                   };

            ServiceLocator.ResponseHandler.Send_MessageReceived +=
                delegate (ChatResponseSrv<SendMessageModel> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        resultUniqueId = result.UniqueId;
                        manualResetEvent.Set();
                    }
                };

            manualResetEvent.WaitOne();
            Assert.AreEqual(resultUniqueId, generatedUniqueId, "There is Something wrong with SendFileMessage");
        }

        [Test]
        public void ReplyFileMessage()
        {
            var resultUniqueId = string.Empty;
            var manualResetEvent = new ManualResetEvent(false);
            var messageInfo = SendMessageImplement();
            var sendReplyFileMessageRequest = SendReplyFileMessageRequest.ConcreteBuilder
                .SetRepliedTo(messageInfo.MessageId)
              .SetSendFileMessage(SendFileMessageRequest.ConcreteBuilder
               .SetUserGroupHash(messageInfo.UserGroupHash)
               .SetMessageInput(SendTextMessageRequest.ConcreteBuilder.SetThreadId(messageInfo.ThreadId)
                   .SetMessageType(MessageType.POD_SPACE_PICTURE)
                   .SetTextMessage($"Reply to {messageInfo.MessageId}")
                   //.SetMetadata("")
                   //.SetSystemMetadata("")
                   //.SetTypeCode("")
                   .Build())
             .SetUploadInput(UploadRequest.ConcreteBuilder
                                 .SetFilePath(imagePath)
                                 .SetFileName($"File-{messageInfo.MessageId}")
                                 //.SetXC(0)
                                 //.SetYC(0)
                                 //.SetHC(0)
                                 //.SetWC(0)
                                 .Build())
               .Build())
               .Build();

            var generatedUniqueId = ServiceLocator.ChatService.ReplyFileMessage(sendReplyFileMessageRequest);

            ServiceLocator.ResponseHandler.Sent_MessageReceived +=
                delegate(ChatResponseSrv<SendMessageModel> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        resultUniqueId = result.UniqueId;
                    }
                };

            ServiceLocator.ResponseHandler.Send_MessageReceived +=
                delegate (ChatResponseSrv<SendMessageModel> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        resultUniqueId = result.UniqueId;
                        manualResetEvent.Set();
                    }
                };

            manualResetEvent.WaitOne();
            Assert.AreEqual(resultUniqueId, generatedUniqueId, "There is Something wrong with ReplyFileMessage");
        }

        
        public void GetUnreadMessageCount()
        {
            var resultUniqueId = string.Empty;
            var manualResetEvent = new ManualResetEvent(false);
            var unreadMessageCountRequest = UnreadMessageCountRequest.ConcreteBuilder
                .SetMute(false)
                //.SetTypeCode("")
                .Build();

            var generatedUniqueId = ServiceLocator.ChatService.GetUnreadMessageCount(unreadMessageCountRequest);

            ServiceLocator.ResponseHandler.AllUnreadMessageCount_MessageReceived +=
                delegate (ChatResponseSrv<UnreadMessageCountModel> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        resultUniqueId = result.UniqueId;
                        manualResetEvent.Set();
                    }
                };

            manualResetEvent.WaitOne();
            Assert.AreEqual(resultUniqueId, generatedUniqueId, "There is Something wrong with GetUnreadMessageCount");
        }

        #endregion Message

        #region Bot

        [Test]
        public void CreateBot()
        {
            CreateBotImplement();
        }

        public string CreateBotImplement()
        {
            var resultUniqueId = string.Empty;
            var manualResetEvent = new ManualResetEvent(false);
            var random = new Random();
            var botName = "MsTestPod" + random.Next(3000) + "BOT";
            var createBotRequest = CreateBotRequest.ConcreteBuilder
                .SetBotName(botName)
                //.SetTypeCode("")
                .Build();

            var generatedUniqueId = ServiceLocator.ChatService.CreateBot(createBotRequest);

            ServiceLocator.ResponseHandler.CreateBot_MessageReceived +=
                delegate (ChatResponseSrv<ResultCreateBot> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        resultUniqueId = result.UniqueId;
                        manualResetEvent.Set();
                    }
                };

            manualResetEvent.WaitOne();
            Assert.AreEqual(resultUniqueId, generatedUniqueId, "There is Something wrong with CreateBot");
            return botName;
        }

        [Test]
        public void StartBot()
        {
            StartBotImplement();
        }

        public (string botName, long threadId) StartBotImplement()
        {
            var resultUniqueId = string.Empty;
            var participantResultUniqueId = string.Empty;
            var manualResetEvent = new ManualResetEvent(false);
            var botName = CreateBotImplement();
            var threadInfo = CreatePublicThread();

            var addParticipantsRequest = AddParticipantsRequest.ConcreteBuilder
               .SetThreadId(threadInfo.ThreadId)
               .SetUserNames(new[] { botName })
               .Build();

            var participantGeneratedUniqueId = ServiceLocator.ChatService.AddParticipants(addParticipantsRequest);
            ServiceLocator.ResponseHandler.AddParticipant_MessageReceived +=
                delegate (ChatResponseSrv<AddParticipantModel> result)
                {
                    if (result.UniqueId.Equals(participantGeneratedUniqueId))
                    {
                        participantResultUniqueId = result.UniqueId;
                        manualResetEvent.Set();
                    }
                };

            manualResetEvent.WaitOne();
            Assert.AreEqual(participantResultUniqueId, participantGeneratedUniqueId, "There is Something wrong with AddParticipants on StartBot");

            var startAndStopBotRequest = StartAndStopBotRequest.ConcreteBuilder
                .SetBotName(botName)
                .SetThreadId(threadInfo.ThreadId)
                //.SetTypeCode("")
                .Build();

            var generatedUniqueId = ServiceLocator.ChatService.StartBot(startAndStopBotRequest);
            ServiceLocator.ResponseHandler.StartBot_MessageReceived +=
               delegate (ChatResponseSrv<ResultStartBot> result)
               {
                   if (result.UniqueId.Equals(generatedUniqueId))
                   {
                       resultUniqueId = result.UniqueId;
                       manualResetEvent.Set();
                   }
               };

            manualResetEvent.WaitOne();
            Assert.AreEqual(resultUniqueId, generatedUniqueId, "There is Something wrong with StartBot");
            return (botName, threadInfo.ThreadId);
        }

        [Test]
        public void StopBot()
        {
            var resultUniqueId = string.Empty;
            var manualResetEvent = new ManualResetEvent(false);
            var (botName, threadId) = StartBotImplement();
            var startAndStopBotRequest = StartAndStopBotRequest.ConcreteBuilder
                .SetBotName(botName)
                .SetThreadId(threadId)
                //.SetTypeCode("")
                .Build();

            var generatedUniqueId = ServiceLocator.ChatService.StopBot(startAndStopBotRequest);

            ServiceLocator.ResponseHandler.GetHistory_MessageReceived +=
                delegate (ChatResponseSrv<GetHistoryModel> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        resultUniqueId = result.UniqueId;
                        manualResetEvent.Set();
                    }
                };

            manualResetEvent.WaitOne();
            Assert.AreEqual(resultUniqueId, generatedUniqueId, "There is Something wrong with StopBot");
        }

        [Test]
        public void DefineBotCommand()
        {
            var resultUniqueId = string.Empty;
            var manualResetEvent = new ManualResetEvent(false);
            var (botName, _) = StartBotImplement();
            var requestDefineBotCommand = DefineBotCommandRequest.ConcreteBuilder
                .SetBotName(botName)
                .SetCommandList(new[] { "got12", "/got13" })
                //.SetTypeCode("")
                .Build();

            var generatedUniqueId = ServiceLocator.ChatService.DefineBotCommand(requestDefineBotCommand);

            ServiceLocator.ResponseHandler.DefineBotCommand_MessageReceived +=
                delegate (ChatResponseSrv<ResultDefineCommandBot> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        resultUniqueId = result.UniqueId;
                        manualResetEvent.Set();
                    }
                };

            manualResetEvent.WaitOne();
            Assert.AreEqual(resultUniqueId, generatedUniqueId, "There is Something wrong with DefineBotCommand");
        }

        #endregion Bot
    }
}
