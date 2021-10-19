using Newtonsoft.Json.Linq;
using POD_Async.Base;
using POD_Async.Core;
using POD_Async.Core.ResultModel;
using POD_Chat.Base;
using POD_Chat.Base.Enum;
using POD_Chat.Model.ServiceOutput;
using POD_Chat.Model.ValueObject;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace POD_Chat
{
    public class ChatService
    {
        #region Field

        private readonly ChatConfig chatConfig;

        #endregion Field

        #region Constructor

        internal ChatService(ChatConfig chatConfig)
        {
            this.chatConfig = chatConfig;
        }

        #endregion Constructor

        #region Profile

        public string GetUserInfo()
        {
            var uniqueId = ServiceLocator.AsyncConnector.Execute(null, ChatMessageType.USER_INFO);
            return uniqueId;
        }

        public string UpdateProfile(UpdateChatProfileRequest UpdateProfile)
        {
            var uniqueId =
                ServiceLocator.AsyncConnector.Execute(UpdateProfile, ChatMessageType.UPDATE_PROFILE);
            return uniqueId;
        }

        #endregion Profile

        #region Contact

        public async Task AddContact(AddContactsRequest addContactsRequest, Action<IRestResponse<ResultSrv<List<Contact>>>> callback)
        {
            var url = BaseUrl.PlatformAddress + "/nzh/addContacts/";
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("_token_", chatConfig.Token)
                .AddHeader("_token_issuer_", chatConfig.TokenIssuer.ToString())
                .AddHeader("content-type", "application/x-www-form-urlencoded");

            var parameters = addContactsRequest.FilterNotNull(PodParameterName.ParametersName, false);
            foreach (var parameter in parameters)
            {
                request.AddParameter(parameter.Key, parameter.Value);
            }

            var response = await client.ExecuteAsync<ResultSrv<List<Contact>>>(request);
            callback(response);
        }

        public async Task UpdateContact(UpdateContactsRequest updateContactsRequest, Action<IRestResponse<ResultSrv<List<Contact>>>> callback)
        {
            var url = BaseUrl.PlatformAddress + "/nzh/updateContacts/";
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            request.AddHeader("_token_", chatConfig.Token)
                   .AddHeader("_token_issuer_", chatConfig.TokenIssuer.ToString());
            var parameters = updateContactsRequest.FilterNotNull(PodParameterName.ParametersName, false);
            foreach (var parameter in parameters)
            {
                request.AddParameter(parameter.Key, parameter.Value);
            }

            var response = await client.ExecuteAsync<ResultSrv<List<Contact>>>(request);
            callback(response);
        }

        public async Task RemoveContact(RemoveContactsRequest removeContactsRequest, Action<IRestResponse<ResultSrv<bool>>> callback)
        {
            var url = BaseUrl.PlatformAddress + "/nzh/removeContacts/";
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            request.AddHeader("_token_", chatConfig.Token)
                   .AddHeader("_token_issuer_", chatConfig.TokenIssuer.ToString())
                   .AddParameter("id", removeContactsRequest.Id.ToString());

            var response = await client.ExecuteAsync<ResultSrv<bool>>(request);
            callback(response);
        }

        public async Task SearchContact(SearchContactsRequest searchContactsRequest, Action<IRestResponse<ResultSrv<List<Contact>>>> callback)
        {
            var url = BaseUrl.PlatformAddress + "/nzh/listContacts/";
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            request.AddHeader("_token_", chatConfig.Token)
                   .AddHeader("_token_issuer_", chatConfig.TokenIssuer.ToString())
                   .AddParameter("id", searchContactsRequest.Id.ToString())
                   .AddParameter("firstName", searchContactsRequest.FirstName)
                   .AddParameter("lastName", searchContactsRequest.LastName)
                   .AddParameter("cellphoneNumber", searchContactsRequest.CellphoneNumber)
                   .AddParameter("email", searchContactsRequest.Email)
                   .AddParameter("q", searchContactsRequest.Query)
                   .AddParameter("size", searchContactsRequest.Size.ToString())
                   .AddParameter("offset", searchContactsRequest.Offset.ToString())
                   .AddParameter("ownerId", searchContactsRequest.OwnerId);

            var response = await client.ExecuteAsync<ResultSrv<List<Contact>>>(request);
            callback(response);
        }

        public string GetContacts(GetContactsRequest getContactsRequest)
        {
            var uniqueId =
                ServiceLocator.AsyncConnector.Execute(getContactsRequest, ChatMessageType.GET_CONTACTS);
            return uniqueId;
        }

        #endregion Contact

        #region Thread

        public string CreateThread(CreateThreadRequest createThreadRequest)
        {
            var uniqueId = ServiceLocator.AsyncConnector.Execute(createThreadRequest, ChatMessageType.CREATE_THREAD);
            if (createThreadRequest.UploadInput != null)
            {
                long threadId = 0;
                var userGroupHash = string.Empty;
                var manualResetEvent = new ManualResetEvent(false);
                ServiceLocator.ResponseHandler.CreateThread_MessageReceived +=
                    delegate (ChatResponseSrv<Conversation> result)
                    {
                        if (result.UniqueId.Equals(uniqueId))
                        {
                            threadId = result.Result.Id ?? 0;
                            userGroupHash = result.Result.UserGroupHash;
                            manualResetEvent.Set();
                        }
                    };

                manualResetEvent.WaitOne();

                createThreadRequest.UploadInput.UserGroupHash = userGroupHash;
                var output = new ResultSrv<UploadToPodSpaceResponse>();
                UploadImageToPodSpace(createThreadRequest.UploadInput, response => CoreListener.GetResult(response, out output));
                var imageResult = output.Result;
                var jObject = new JObject
                {
                    {"fileHash", imageResult.HashCode}
                };

                if (!string.IsNullOrEmpty(createThreadRequest.Metadata))
                {
                    var metadata = JObject.Parse(createThreadRequest.Metadata);
                    jObject.Add(metadata);
                }

                var updateThreadInfoRequest = UpdateThreadInfoRequest.ConcreteBuilder
                                              .SetThreadId(threadId)
                                              .SetMetadata(jObject.ToString())
                                              .SetDescription(createThreadRequest.Description)
                                              .SetTitle(createThreadRequest.Title)
                                              .SetImage(createThreadRequest.Image)
                                              .Build();

                var updateThreadUniqueId = UpdateThreadInfo(updateThreadInfoRequest);
                return updateThreadUniqueId;
            }

            return uniqueId;
        }

        public string[] CreateThreadWithMessage(CreateThreadWithMessageRequest createThreadWithMessageRequest)
        {
            var uniqueId = ServiceLocator.AsyncConnector.ExecuteCreateThreadWithMessage(createThreadWithMessageRequest, ChatMessageType.CREATE_THREAD);
            return uniqueId;
        }

        public string CreateThreadWithFileMessage(CreateThreadWithFileMessageRequest createThreadWithFileMessageRequest)
        {
            var createThreadInput = createThreadWithFileMessageRequest.CreateThreadWithMessageInput.CreateThreadInput;
            var generatedUniqueId = CreateThread(createThreadInput);
            long threadId = 0;
            var userGroupHash = string.Empty;
            var manualResetEvent = new ManualResetEvent(false);
            ServiceLocator.ResponseHandler.CreateThread_MessageReceived +=
                delegate (ChatResponseSrv<Conversation> result)
                {
                    if (result.UniqueId.Equals(generatedUniqueId))
                    {
                        threadId = result.Result.Id ?? 0;
                        userGroupHash = result.Result.UserGroupHash;
                        manualResetEvent.Set();
                    }
                };

            manualResetEvent.WaitOne();
            var messageInput = createThreadWithFileMessageRequest.CreateThreadWithMessageInput.MessageInput;
            var sendFileMessageRequest = SendFileMessageRequest.ConcreteBuilder
               .SetUserGroupHash(userGroupHash)
               .SetMessageInput(SendTextMessageRequest.ConcreteBuilder
                                                    .SetThreadId(threadId)
                                                    .SetMessageType((MessageType)messageInput.MessageType)
                                                    .SetTextMessage(messageInput.Text)
                                                    .SetMetadata(messageInput.Metadata)
                                                    .SetSystemMetadata(messageInput.SystemMetadata)
                                                    .SetTypeCode(createThreadInput.TypeCode)
                                                    .Build())
               .SetUploadInput(createThreadWithFileMessageRequest.UploadInput)
               .Build();

            var uniqueId = SendFileMessage(sendFileMessageRequest);     
            return uniqueId;
        }

        public string GetThreads(GetThreadsRequest getThreadsRequest)
        {
            var uniqueId = ServiceLocator.AsyncConnector.Execute(getThreadsRequest, ChatMessageType.GET_THREADS);
            return uniqueId;
        }

        public string UpdateThreadInfo(UpdateThreadInfoRequest updateThreadInfoRequest)
        {
            if (updateThreadInfoRequest.UploadInput != null)
            {
                var output = new ResultSrv<UploadToPodSpaceResponse>();
                UploadImageToPodSpace(updateThreadInfoRequest.UploadInput, response => CoreListener.GetResult(response, out output));
                var imageResult = output.Result;
                var jObject = new JObject
                {
                    {"fileHash", imageResult.HashCode}
                };

                if (!string.IsNullOrEmpty(updateThreadInfoRequest.Metadata))
                {
                    var metadata = JObject.Parse(updateThreadInfoRequest.Metadata);
                    jObject.Add(metadata);
                }

                updateThreadInfoRequest.Metadata = jObject.ToString();
            }

            var uniqueId = ServiceLocator.AsyncConnector.Execute(updateThreadInfoRequest, ChatMessageType.UPDATE_THREAD_INFO, updateThreadInfoRequest.ThreadId);
            return uniqueId;
        }

        public string AddParticipants(AddParticipantsRequest addParticipantsRequest)
        {
            var uniqueId =
                ServiceLocator.AsyncConnector.Execute(addParticipantsRequest, ChatMessageType.ADD_PARTICIPANT, addParticipantsRequest.ThreadId);
            return uniqueId;
        }

        public string GetThreadParticipants(GetThreadParticipantsRequest getThreadParticipantsRequest)
        {
            var uniqueId =
                ServiceLocator.AsyncConnector.Execute(getThreadParticipantsRequest, ChatMessageType.THREAD_PARTICIPANTS, getThreadParticipantsRequest.ThreadId);
            return uniqueId;
        }

        public string RemoveParticipants(RemoveParticipantsRequest removeParticipantsRequest)
        {
            var uniqueId =
                ServiceLocator.AsyncConnector.Execute(removeParticipantsRequest, ChatMessageType.REMOVE_PARTICIPANT, removeParticipantsRequest.ThreadId);
            return uniqueId;
        }

        public string JoinPublicThread(JoinPublicThreadRequest joinPublicThreadRequest)
        {
            var uniqueId =
                ServiceLocator.AsyncConnector.Execute(joinPublicThreadRequest, ChatMessageType.JOIN_THREAD);
            return uniqueId;
        }

        public string IsPublicThreadNameAvailable(IsPublicThreadNameAvailableRequest isPublicThreadNameAvailableRequest)
        {
            var uniqueId =
                ServiceLocator.AsyncConnector.Execute(isPublicThreadNameAvailableRequest, ChatMessageType.IS_NAME_AVAILABLE);
            return uniqueId;
        }

        public string LeaveThread(ThreadGeneralRequest threadGeneralRequest)
        {
            var uniqueId =
                ServiceLocator.AsyncConnector.Execute(threadGeneralRequest, ChatMessageType.LEAVE_THREAD, threadGeneralRequest.ThreadId);
            return uniqueId;
        }

        public string MuteThread(ThreadGeneralRequest threadGeneralRequest)
        {
            var uniqueId =
                ServiceLocator.AsyncConnector.Execute(threadGeneralRequest, ChatMessageType.MUTE_THREAD, threadGeneralRequest.ThreadId);
            return uniqueId;
        }

        public string UnMuteThread(ThreadGeneralRequest threadGeneralRequest)
        {
            var uniqueId =
                ServiceLocator.AsyncConnector.Execute(threadGeneralRequest, ChatMessageType.UN_MUTE_THREAD, threadGeneralRequest.ThreadId);
            return uniqueId;
        }

        public string PinThread(ThreadGeneralRequest threadGeneralRequest)
        {
            var uniqueId =
                ServiceLocator.AsyncConnector.Execute(threadGeneralRequest, ChatMessageType.PIN_THREAD, threadGeneralRequest.ThreadId);
            return uniqueId;
        }

        public string UnPinThread(ThreadGeneralRequest threadGeneralRequest)
        {
            var uniqueId =
                ServiceLocator.AsyncConnector.Execute(threadGeneralRequest, ChatMessageType.UNPIN_THREAD, threadGeneralRequest.ThreadId);
            return uniqueId;
        }

        public string Block(BlockRequest blockRequest)
        {
            var uniqueId =
                ServiceLocator.AsyncConnector.Execute(blockRequest, ChatMessageType.BLOCK);
            return uniqueId;
        }

        public string UnBlock(UnblockRequest unblockRequest)
        {
            var uniqueId =
                ServiceLocator.AsyncConnector.Execute(unblockRequest, ChatMessageType.UNBLOCK, unblockRequest.BlockId);
            return uniqueId;
        }

        public string GetBlockList(GetBlockedListRequest getBlockedListRequest)
        {
            var uniqueId =
                ServiceLocator.AsyncConnector.Execute(getBlockedListRequest, ChatMessageType.GET_BLOCKED);
            return uniqueId;
        }

        public string SpamPrivateThread(ThreadGeneralRequest threadGeneralRequest)
        {
            var uniqueId =
                ServiceLocator.AsyncConnector.Execute(threadGeneralRequest, ChatMessageType.SPAM_PV_THREAD, threadGeneralRequest.ThreadId);
            return uniqueId;
        }

        #endregion Thread

        #region Thread_Roles

        public string SetAdmin(SetRemoveRoleRequest setRemoveRoleRequest)
        {
            var uniqueId =
                ServiceLocator.AsyncConnector.Execute(setRemoveRoleRequest, ChatMessageType.SET_ROLE_TO_USER, setRemoveRoleRequest.ThreadId);
            return uniqueId;
        }

        public string RemoveAdmin(SetRemoveRoleRequest setRemoveRoleRequest)
        {
            var uniqueId =
                ServiceLocator.AsyncConnector.Execute(setRemoveRoleRequest, ChatMessageType.REMOVE_ROLE_FROM_USER, setRemoveRoleRequest.ThreadId);
            return uniqueId;
        }

        public string SetAuditor(SetRemoveRoleRequest setRemoveRoleRequest)
        {
            var uniqueId = SetAdmin(setRemoveRoleRequest);
            return uniqueId;
        }

        public string RemoveAuditor(SetRemoveRoleRequest setRemoveRoleRequest)
        {
            var uniqueId = RemoveAdmin(setRemoveRoleRequest);
            return uniqueId;
        }

        public string GetThreadAdmins(GetThreadParticipantsRequest getThreadParticipantsRequest)
        {
            getThreadParticipantsRequest.Admin = true;
            var uniqueId =
                ServiceLocator.AsyncConnector.Execute(getThreadParticipantsRequest, ChatMessageType.THREAD_PARTICIPANTS, getThreadParticipantsRequest.ThreadId);
            return uniqueId;
        }

        public string GetCurrentUserRoles(ThreadGeneralRequest threadGeneralRequest)
        {
            var uniqueId =
                ServiceLocator.AsyncConnector.Execute(threadGeneralRequest, ChatMessageType.USER_ROLES, threadGeneralRequest.ThreadId);
            return uniqueId;
        }

        #endregion Thread_Roles

        #region Message

        public string SendTextMessage(SendTextMessageRequest sendTextMessageRequest)
        {
            var uniqueId = ServiceLocator.AsyncConnector.ExecuteSendMessage(sendTextMessageRequest, ChatMessageType.MESSAGE);
            return uniqueId;
        }

        public string SendFileMessage(SendFileMessageRequest sendFileMessageRequest)
        {
            var messageInput = sendFileMessageRequest.MessageInput;
            var metaData = GetMetaData(sendFileMessageRequest.UploadInput);
            messageInput.Metadata = metaData.ToJsonWithNotNullProperties();

            var uniqueId = ServiceLocator.AsyncConnector.ExecuteSendMessage(messageInput, ChatMessageType.MESSAGE);

            return uniqueId;
        }

        public string ReplyTextMessage(ReplyTextMessageRequest replyTextMessageRequest)
        {
            var uniqueId = ServiceLocator.AsyncConnector.ExecuteSendMessage(replyTextMessageRequest.TextMessage, ChatMessageType.MESSAGE, replyTextMessageRequest.RepliedTo);
            return uniqueId;
        }

        public string ReplyFileMessage(SendReplyFileMessageRequest sendReplyFileMessageRequest)
        {
            var sendFileMessage = sendReplyFileMessageRequest.SendFileMessage;
            var metaData = GetMetaData(sendFileMessage.UploadInput);
            var messageInput = sendFileMessage.MessageInput;
            messageInput.Metadata = metaData.ToJsonWithNotNullProperties();

            var uniqueId = ServiceLocator.AsyncConnector.ExecuteSendMessage(messageInput, ChatMessageType.MESSAGE, sendReplyFileMessageRequest.RepliedTo ?? 0);

            return uniqueId;
        }

        public string EditMessage(EditMessageRequest editMessageRequest)
        {
            var uniqueId = ServiceLocator.AsyncConnector.ExecuteEditMessage(editMessageRequest, ChatMessageType.EDIT_MESSAGE, editMessageRequest.MessageId);
            return uniqueId;
        }

        public string DeleteMessage(DeleteMessageRequest deleteMessageRequest)
        {
            var uniqueId =
                ServiceLocator.AsyncConnector.Execute(deleteMessageRequest, ChatMessageType.DELETE_MESSAGE, deleteMessageRequest.MessageId);
            return uniqueId;
        }

        public string[] DeleteMultipleMessages(DeleteMultipleMessagesRequest deleteMultipleMessagesRequest)
        {
            var uniqueIds =
                ServiceLocator.AsyncConnector.ExecuteMultipleDeleteMessage(deleteMultipleMessagesRequest, ChatMessageType.DELETE_MESSAGE);
            return uniqueIds;
        }

        public string[] ForwardMessage(ForwardMessageRequest forwardMessageRequest)
        {
            var uniqueIds =
                ServiceLocator.AsyncConnector.ExecuteForwardMessage(forwardMessageRequest, ChatMessageType.FORWARD_MESSAGE);
            return uniqueIds;
        }

        public string GetHistory(GetHistoryRequest getHistoryRequest)
        {
            var uniqueId =
                ServiceLocator.AsyncConnector.Execute(getHistoryRequest, ChatMessageType.GET_HISTORY, getHistoryRequest.ThreadId);
            return uniqueId;
        }

        public string PinMessage(PinUnpinMessageRequest PinMessage)
        {
            var uniqueId =
                ServiceLocator.AsyncConnector.Execute(PinMessage, ChatMessageType.PIN_MESSAGE, PinMessage.MessageId);
            return uniqueId;
        }

        public string UnPinMessage(PinUnpinMessageRequest UnPinMessage)
        {
            var uniqueId =
                ServiceLocator.AsyncConnector.Execute(UnPinMessage, ChatMessageType.UNPIN_MESSAGE, UnPinMessage.MessageId);
            return uniqueId;
        }

        public string ClearHistory(ThreadGeneralRequest threadGeneralRequest)
        {
            var uniqueId =
                ServiceLocator.AsyncConnector.Execute(threadGeneralRequest, ChatMessageType.CLEAR_HISTORY, threadGeneralRequest.ThreadId);
            return uniqueId;
        }

        public string GetMentionedMessages(GetMentionedRequest getMentionedRequest)
        {
            var uniqueId =
                ServiceLocator.AsyncConnector.Execute(getMentionedRequest, ChatMessageType.GET_HISTORY, getMentionedRequest.ThreadId);
            return uniqueId;
        }

        public string GetUnreadMessageCount(UnreadMessageCountRequest unreadMessageCountRequest)
        {
            var uniqueId =
                ServiceLocator.AsyncConnector.Execute(unreadMessageCountRequest, ChatMessageType.ALL_UNREAD_MESSAGE_COUNT);
            return uniqueId;
        }

        #endregion Message

        #region Upload & Download_File

        private FileMetaData GetMetaData(UploadRequest uploadRequest)
        {
            if (uploadRequest.IsImage)
            {
                var output = new ResultSrv<UploadToPodSpaceResponse>();
                UploadImageToPodSpace(uploadRequest, response => CoreListener.GetResult(response, out output));
                var result = output.Result;

                var link = !string.IsNullOrEmpty(result.HashCode)
                    ? Util.GetImageUrl(result.Id, result.HashCode, false)
                    : uploadRequest.FilePath;

                var imageMetaDataContent = new FileMetaDataContent
                {
                    Id = result.Id,
                    OriginalName = uploadRequest.FileData.Name,
                    Link = link,
                    HashCode = result.HashCode,
                    Name = uploadRequest.FileData.Name,
                    MimeType = uploadRequest.MimeType,
                    Size = uploadRequest.FileData.Length
                };

                var metaData = new FileMetaData
                {
                    File = imageMetaDataContent,
                    Id = result.Id,
                    Name = uploadRequest.FileData.Name,
                    FileHash= result.HashCode
                };

                return metaData;

            }
            else
            {
                var output = new ResultSrv<UploadToPodSpaceResponse>();
                UploadFileToPodSpace(uploadRequest, response => CoreListener.GetResult(response, out output));
                var result = output.Result;

                var link = !string.IsNullOrEmpty(result.HashCode)
                    ? Util.GetFile(result.Id, result.HashCode, true)
                    : uploadRequest.FilePath;

                var fileMetaDataContent = new FileMetaDataContent
                {
                    Id = result.Id,
                    OriginalName = uploadRequest.FileData.Name,
                    Link = link,
                    HashCode = result.HashCode,
                    Name = uploadRequest.FileData.Name,
                    MimeType = uploadRequest.MimeType,
                    Size = uploadRequest.FileData.Length
                };

                var metaData = new FileMetaData
                {
                    File = fileMetaDataContent,
                    Id = result.Id,
                    Name = uploadRequest.FileData.Name,
                    FileHash = result.HashCode
                };

                return metaData;
            }
        }

        private void UploadImage(UploadRequest uploadImageRequest, Action<IRestResponse<ResultSrv<ResultImageFile>>> callback)
        {
            var url = BaseUrl.FileServerAddress + "/nzh/uploadImage/";
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("_token_", chatConfig.Token)
                   .AddHeader("_token_issuer_", chatConfig.TokenIssuer.ToString())
                   .AddHeader("Content-Type", "multipart/form-data")
                   .AddParameter("fileName", uploadImageRequest.FileName)
                   .AddParameter("xC", uploadImageRequest.XC)
                   .AddParameter("yC", uploadImageRequest.YC)
                   .AddParameter("wC", uploadImageRequest.WC)
                   .AddParameter("hC", uploadImageRequest.HC)
                   .AddFile("image", uploadImageRequest.FilePath);


            callback(client.Execute<ResultSrv<ResultImageFile>>(request));
        }

        private void UploadFile(UploadFileRequest uploadFileRequest, Action<IRestResponse<ResultSrv<UploadToPodSpaceResponse>>> callback)
        {
            var url = BaseUrl.FileServerAddress + "/nzh/uploadFile/";
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("_token_", chatConfig.Token)
                   .AddHeader("_token_issuer_", chatConfig.TokenIssuer.ToString())
                   .AddHeader("Content-Type", "multipart/form-data")
                   .AddParameter("fileName", uploadFileRequest.FileName)
                   .AddFile("file", uploadFileRequest.FilePath);

            callback(client.Execute<ResultSrv<UploadToPodSpaceResponse>>(request));
        }

        private void UploadImageToPodSpace(UploadRequest uploadImageRequest, Action<IRestResponse<ResultSrv<UploadToPodSpaceResponse>>> callback)
        {
            var url = BaseUrl.PodSpaceFileAddress + "/userGroup/uploadImage/";
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("_token_", chatConfig.Token)
                   .AddHeader("_token_issuer_", chatConfig.TokenIssuer.ToString())
                   .AddHeader("Content-Type", "multipart/form-data")
                   .AddParameter("filename", uploadImageRequest.FileName)
                   .AddParameter("xC", uploadImageRequest.XC)
                   .AddParameter("yC", uploadImageRequest.YC)
                   .AddParameter("wC", uploadImageRequest.WC)
                   .AddParameter("hC", uploadImageRequest.HC)
                   .AddParameter("userGroupHash", uploadImageRequest.UserGroupHash)
                   .AddFile("file", uploadImageRequest.FilePath);


            callback(client.Execute<ResultSrv<UploadToPodSpaceResponse>>(request));
        }

        private void UploadPublicImageToPodSpace(UploadRequest uploadImageRequest, Action<IRestResponse<ResultSrv<ResultImageFile>>> callback)
        {
            var url = BaseUrl.PodSpaceFileAddress + "/nzh/drive/uploadImage/";
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("_token_", chatConfig.Token)
                   .AddHeader("_token_issuer_", chatConfig.TokenIssuer.ToString())
                   .AddHeader("Content-Type", "multipart/form-data")
                   .AddParameter("filename", uploadImageRequest.FileName)
                   .AddParameter("xC", uploadImageRequest.XC)
                   .AddParameter("yC", uploadImageRequest.YC)
                   .AddParameter("wC", uploadImageRequest.WC)
                   .AddParameter("hC", uploadImageRequest.HC)
                   .AddParameter("isPublic", true)
                   .AddFile("image", uploadImageRequest.FilePath);


            callback(client.Execute<ResultSrv<ResultImageFile>>(request));
        }

        public FileObject DownloadPodSpaceImage(DownloadPodSpaceImageRequest downloadPodSpaceImageRequest)
        {
            var url = BaseUrl.PodSpaceFileAddress + "/nzh/drive/downloadImage/";
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            request.AddHeader("_token_", chatConfig.Token)
                   .AddHeader("_token_issuer_", chatConfig.TokenIssuer.ToString())
                   .AddParameter("hash", downloadPodSpaceImageRequest.Hash)
                   .AddParameter("size", downloadPodSpaceImageRequest.Size)
                   .AddParameter("crop", downloadPodSpaceImageRequest.Crop)
                   .AddParameter("quality", downloadPodSpaceImageRequest.Quality);

            var response = client.Execute(request);
            var fullName = $"{downloadPodSpaceImageRequest.FileName}{response.ContentType.GetExtension()}";
            var fullPath = Path.Combine(downloadPodSpaceImageRequest.DownloadPath, fullName);
            File.WriteAllBytes(fullPath, response.RawBytes);
            var fileObject = new FileObject
            {
                HashCode = downloadPodSpaceImageRequest.Hash,
                Name = downloadPodSpaceImageRequest.FileName,
                Size = response.ContentLength,
                Type = response.ContentType,
                FullPath = fullPath
            };

            return fileObject;
        }

        private void UploadFileToPodSpace(UploadRequest uploadRequest, Action<IRestResponse<ResultSrv<UploadToPodSpaceResponse>>> callback)
        {
            var url = BaseUrl.PodSpaceFileAddress + "/userGroup/uploadFile/";
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("_token_", chatConfig.Token)
                   .AddHeader("_token_issuer_", chatConfig.TokenIssuer.ToString())
                   .AddHeader("Content-Type", "multipart/form-data")
                   .AddParameter("filename", uploadRequest.FileName)
                   .AddParameter("userGroupHash", uploadRequest.UserGroupHash)
                   .AddFile("file", uploadRequest.FilePath);

            callback(client.Execute<ResultSrv<UploadToPodSpaceResponse>>(request));
        }

        private void UploadPublicFileToPodSpace(UploadFileRequest uploadFileRequest, Action<IRestResponse<ResultSrv<UploadToPodSpaceResponse>>> callback)
        {
            var url = BaseUrl.PodSpaceFileAddress + "/nzh/drive/uploadFile/";
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("_token_", chatConfig.Token)
                   .AddHeader("_token_issuer_", chatConfig.TokenIssuer.ToString())
                   .AddHeader("Content-Type", "multipart/form-data")
                   .AddParameter("filename", uploadFileRequest.FileName)
                   .AddFile("file", uploadFileRequest.FilePath);

            callback(client.Execute<ResultSrv<UploadToPodSpaceResponse>>(request));
        }

        public FileObject DownloadPodSpaceFile(DownloadPodSpaceFileRequest downloadPodSpaceFileRequest)
        {
            var url = BaseUrl.PodSpaceFileAddress + "/nzh/drive/downloadFile/";
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            request.AddHeader("_token_", chatConfig.Token)
                   .AddHeader("_token_issuer_", chatConfig.TokenIssuer.ToString())
                   .AddParameter("hash", downloadPodSpaceFileRequest.Hash);

            var response = client.Execute(request);

            var fullName = $"{downloadPodSpaceFileRequest.FileName}{response.ContentType.GetExtension()}";
            var fullPath = Path.Combine(downloadPodSpaceFileRequest.DownloadPath, fullName);

            File.WriteAllBytes(fullPath, response.RawBytes);
            var fileObject = new FileObject
            {
                HashCode = downloadPodSpaceFileRequest.Hash,
                Name = downloadPodSpaceFileRequest.FileName,
                Size = response.ContentLength,
                Type = response.ContentType,
                FullPath = fullPath
            };

            return fileObject;
        }

        #endregion Upload & Download_File

        #region Bot

        public string CreateBot(CreateBotRequest createBotRequest)
        {
            var uniqueId =
                ServiceLocator.AsyncConnector.Execute(createBotRequest, ChatMessageType.CREATE_BOT);
            return uniqueId;
        }

        public string StartBot(StartAndStopBotRequest startAndStopBotRequest)
        {
            var uniqueId =
                ServiceLocator.AsyncConnector.Execute(startAndStopBotRequest, ChatMessageType.START_BOT, startAndStopBotRequest.ThreadId);
            return uniqueId;
        }

        public string StopBot(StartAndStopBotRequest startAndStopBotRequest)
        {
            var uniqueId =
                ServiceLocator.AsyncConnector.Execute(startAndStopBotRequest, ChatMessageType.STOP_BOT, startAndStopBotRequest.ThreadId);
            return uniqueId;
        }

        public string DefineBotCommand(DefineBotCommandRequest defineBotCommandRequest)
        {
            var uniqueId =
                ServiceLocator.AsyncConnector.Execute(defineBotCommandRequest, ChatMessageType.DEFINE_BOT_COMMAND);
            return uniqueId;
        }

        #endregion Bot

        #region Call_Management
        public string RequestCall(CallRequest callRequest) {
            var uniqueId = ServiceLocator.AsyncConnector.Execute(callRequest, ChatMessageType.CALL_REQUEST);
            return uniqueId;
        }

        public string RequestGroupCall(CallRequest callRequest)
        {
            var uniqueId = ServiceLocator.AsyncConnector.Execute(callRequest, ChatMessageType.GROUP_CALL_REQUEST);
            return uniqueId;
        }

        public string EndCallRequest(EndCallRequest endCallRequest)
        {
            var uniqueId = ServiceLocator.AsyncConnector.Execute(endCallRequest, ChatMessageType.END_CALL_REQUEST,subjectId:endCallRequest.CallId);
            return uniqueId;
        }

        public string AddCallParticipant(AddParticipantsRequest participant)
        {
            var uniqueId = ServiceLocator.AsyncConnector.Execute(participant, ChatMessageType.ADD_CALL_PARTICIPANT,subjectId:participant.ThreadId);
            return uniqueId;
        }

        public string RemoveCallParticipant(RemoveCallParticipantsRequest participant)
        {
            var uniqueId = ServiceLocator.AsyncConnector.Execute(participant, ChatMessageType.REMOVE_CALL_PARTICIPANT, subjectId: participant.ThreadId);
            return uniqueId;
        }

        public string TerminateCall(TerminateCallRequest terminateCallRequest)
        {
            var uniqueId = ServiceLocator.AsyncConnector.Execute(terminateCallRequest, ChatMessageType.TERMINATE_CALL , subjectId:terminateCallRequest.CallId);
            return uniqueId;
        }

        public string TurnOnVideoCall(TurnOnVideoCallRequest turnOnVideoCallRequest)
        {
            var uniqueId = ServiceLocator.AsyncConnector.Execute(turnOnVideoCallRequest, ChatMessageType.TURN_ON_VIDEO_CALL,subjectId:turnOnVideoCallRequest.CallId);
            return uniqueId;
        }

        public string TurnOffVideoCall(TurnOffVideoCallRequest turnOffVideoCallRequest)
        {
            var uniqueId = ServiceLocator.AsyncConnector.Execute(turnOffVideoCallRequest, ChatMessageType.TURN_OFF_VIDEO_CALL,subjectId:turnOffVideoCallRequest.CallId);
            return uniqueId;
        }

        public string MuteCall(MuteUnMuteCallParticipantsRequest muteCallParticipantsRequest)
        {
            var uniqueId = ServiceLocator.AsyncConnector.Execute(muteCallParticipantsRequest, ChatMessageType.MUTE_CALL_PARTICIPANT, subjectId: muteCallParticipantsRequest.CallId);
            return uniqueId;
        }

        public string UNMuteCall(MuteUnMuteCallParticipantsRequest muteCallParticipantsRequest)
        {
            var uniqueId = ServiceLocator.AsyncConnector.Execute(muteCallParticipantsRequest, ChatMessageType.UNMUTE_CALL_PARTICIPANT, subjectId: muteCallParticipantsRequest.CallId);
            return uniqueId;
        }

        public string GetActiveCallParticipants(long callId)
        {
            var uniqueId = ServiceLocator.AsyncConnector.Execute(null, ChatMessageType.ACTIVE_CALL_PARTICIPANTS, subjectId: callId);
            return uniqueId;
        }

        public string StartRecording(StartRecordingRequest startRecordingRequest)
        {
            var uniqueId = ServiceLocator.AsyncConnector.Execute(startRecordingRequest, ChatMessageType.START_RECORDING, subjectId: startRecordingRequest.CallId);
            return uniqueId;
        }

        public string StopRecording(StopRecordingRequest stopRecordingRequest)
        {
            var uniqueId = ServiceLocator.AsyncConnector.Execute(stopRecordingRequest, ChatMessageType.STOP_RECORDING, subjectId: stopRecordingRequest.CallId);
            return uniqueId;
        }

        public string GetCallsList(GetCallHistoryRequest callHistoryRequest)
        {
            var uniqueId = ServiceLocator.AsyncConnector.Execute(callHistoryRequest, ChatMessageType.GET_CALLS);
            return uniqueId;
        }
        #endregion Call_Management

    }
}
