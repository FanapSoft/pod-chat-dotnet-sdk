using NLog;
using POD_Async.Base;
using POD_Async.Exception;
using POD_Async.Model;
using POD_Chat;
using POD_Chat.Base;
using POD_Chat.Base.Enum;
using POD_Chat.Model.ServiceOutput;
using POD_Chat.Model.ValueObject;
using System;
using System.Collections.Generic;

namespace POD_Demo
{
    public class BotMethodInvoke
    {
        private Dictionary<string, string> uniqueIds;
        public BotMethodInvoke()
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
                                {"UpdateThreadInfo",string.Empty },
                                {"AddParticipants",string.Empty },
                                {"GetThreadParticipants",string.Empty },
                                {"CreateBot",string.Empty },
                                {"StartBot",string.Empty },
                                {"StopBot",string.Empty },
                                {"DefineBotCommand",string.Empty },
                            };

                #endregion configuration

                #region Method-Calls

                /// UnComment every method you need

                //GetThreads();
                //CreateThread();
                //UpdateThreadInfo();
                //AddParticipants();
                //GetThreadParticipants();
                //CreateBot();
                //StartBot();
                //StopBot();
                //DefineBotCommand();

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
            ServiceLocator.ResponseHandler.AddParticipant_MessageReceived += OnAddParticipant;
            ServiceLocator.ResponseHandler.GetParticipants_MessageReceived += OnGetParticipants;
            ServiceLocator.ResponseHandler.CreateBot_MessageReceived += OnCreateBot;
            ServiceLocator.ResponseHandler.StartBot_MessageReceived += OnStartBot;
            ServiceLocator.ResponseHandler.StopBot_MessageReceived += OnStopBot;
            ServiceLocator.ResponseHandler.DefineBotCommand_MessageReceived += OnDefineBotCommand;
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

        public void AddParticipants()
        {
            try
            {
                var addParticipantsRequest = AddParticipantsRequest.ConcreteBuilder
                    .SetThreadId(7991)
                    .SetContactIds(new long[] { 0 })
                    //.SetUserNames(new []{ "" })
                    //.SetCoreUserIds(new long[]{ 7001 })
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

        private void CreateBot()
        {
            try
            {
                var createBotRequest = CreateBotRequest.ConcreteBuilder
                    .SetBotName("nadsk56")
                    //.SetTypeCode("")
                    .Build();

                uniqueIds["CreateBot"] = ServiceLocator.ChatService.CreateBot(createBotRequest);
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

        private void StartBot()
        {
            try
            {
                var startAndStopBotRequest = StartAndStopBotRequest.ConcreteBuilder
                    .SetBotName("botname")
                    .SetThreadId(0)
                    //.SetTypeCode("")
                    .Build();

                uniqueIds["StartBot"] = ServiceLocator.ChatService.StartBot(startAndStopBotRequest);
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

        private void StopBot()
        {
            try
            {
                var startAndStopBotRequest = StartAndStopBotRequest.ConcreteBuilder
                    .SetBotName("botname")
                    .SetThreadId(0)
                    //.SetTypeCode("")
                    .Build();

                uniqueIds["StopBot"] = ServiceLocator.ChatService.StopBot(startAndStopBotRequest);
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

        private void DefineBotCommand()
        {
            try
            {
                var defineBotCommandRequest = DefineBotCommandRequest.ConcreteBuilder
                    .SetBotName("botname")
                    .SetCommandList(new[] { "command" })
                    //.SetTypeCode("")
                    .Build();

                uniqueIds["DefineBotCommand"] = ServiceLocator.ChatService.DefineBotCommand(defineBotCommandRequest);
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
            if (uniqueIds.TryGetValue("GetThreads", out string uniqueId))
            {
                if (result.UniqueId == uniqueId)
                {
                    //To do your logic
                }
            }
        }

        public void OnCreateThread(ChatResponseSrv<Conversation> result)
        {
            if (uniqueIds.TryGetValue("CreateThread", out string uniqueId))
            {
                if (result.UniqueId == uniqueId)
                {
                    //To do your logic
                }
            }
        }

        public void OnUpdateThreadInfo(ChatResponseSrv<Conversation> result)
        {
            if (uniqueIds.TryGetValue("UpdateThreadInfo", out string uniqueId))
            {
                if (result.UniqueId == uniqueId)
                {
                    //To do your logic
                }
            }
        }

        public void OnAddParticipant(ChatResponseSrv<AddParticipantModel> result)
        {
            if (uniqueIds.TryGetValue("AddParticipant", out string uniqueId))
            {
                if (result.UniqueId == uniqueId)
                {
                    //To do your logic
                }
            }
        }

        public void OnGetParticipants(ChatResponseSrv<GetThreadParticipantsModel> result)
        {
            if (uniqueIds.TryGetValue("GetParticipants", out string uniqueId))
            {
                if (result.UniqueId == uniqueId)
                {
                    //To do your logic
                }
            }
        }

        private void OnCreateBot(ChatResponseSrv<ResultCreateBot> result)
        {
            if (uniqueIds.TryGetValue("CreateBot", out string uniqueId))
            {
                if (result.UniqueId == uniqueId)
                {
                    //To do your logic
                }
            }
        }

        private void OnDefineBotCommand(ChatResponseSrv<ResultDefineCommandBot> result)
        {
            if (uniqueIds.TryGetValue("DefineBotCommand", out string uniqueId))
            {
                if (result.UniqueId == uniqueId)
                {
                    //To do your logic
                }
            }
        }

        private void OnStopBot(ChatResponseSrv<ResultStartBot> result)
        {
            if (uniqueIds.TryGetValue("StopBot", out string uniqueId))
            {
                if (result.UniqueId == uniqueId)
                {
                    //To do your logic
                }
            }
        }

        private void OnStartBot(ChatResponseSrv<ResultStartBot> result)
        {
            if (uniqueIds.TryGetValue("StartBot", out string uniqueId))
            {
                if (result.UniqueId == uniqueId)
                {
                    //To do your logic
                }
            }
        }

        public void OnChatError(ChatResponseSrv<AsyncErrorMessage> result)
        {
            //To do your logic
        }

        #endregion Response
    }
}
