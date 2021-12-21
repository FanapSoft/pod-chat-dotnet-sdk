using NLog;
using POD_Async.Base;
using POD_Async.Exception;
using POD_Async.Model;
using POD_Chat;
using POD_Chat.Base;
using POD_Chat.Model.ServiceOutput;
using POD_Chat.Model.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        private Dictionary<string, string> uniqueIds;

        public Form1()
        {
            InitializeComponent();
            InitChat();
        }

        private void GetThreadsClicked(object sender, EventArgs e)
        {
            GetThreads();
        }

        private void GetContactsClicked(object sender, EventArgs e)
        {
            GetContacts();
        }

        private void InitChat()
        {
            try
            {
                PodLogger.AddRule(LogLevel.Info, true, true);

                //If youd to know what is log path
                var logPath = PodLogger.LogPath;

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
                ServiceLocator.ResponseHandler.GetThreads_MessageReceived += OnGetThreads;
                ServiceLocator.ResponseHandler.GetContacts_MessageReceived += OnGetContacts;
                uniqueIds = new Dictionary<string, string> { { "GetThreads", string.Empty } };
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

        public void GetThreads()
        {
            var getThreadsRequest = GetThreadsRequest.ConcreteBuilder
                .Build();

            uniqueIds["GetThreads"] = ServiceLocator.ChatService.GetThreads(getThreadsRequest);
        }

        private void OnGetThreads(ChatResponseSrv<GetThreadsModel> chatResponseSrv)
        {
            SetLableOnMainThread(string.Join(",\n", chatResponseSrv.Result.Threads.Select(x => x.Title)));
        }

        private void OnGetContacts(ChatResponseSrv<GetContactsResponse> chatResponseSrv)
        {
            SetLableOnMainThread(string.Join(",\n", chatResponseSrv.Result.Contacts.Select(x => x.UserName + " " + x.FirstName)));
        }

        public void GetContacts()
        {
            var getContact = GetContactsRequest.ConcreteBuilder
                .Build();
            uniqueIds["GetContacts"] = ServiceLocator.ChatService.GetContacts(getContact);
        }

        private void SetLableOnMainThread(string text) {
            label1.Invoke((MethodInvoker)delegate
            {
                label1.Text = text;
            });
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
