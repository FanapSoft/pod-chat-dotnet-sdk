using System.Collections.Generic;
using POD_Async.Exception;
using POD_Chat.Base;

namespace POD_Chat
{
    public class ServiceLocator
    {
        internal static AsyncConnector AsyncConnector { get; private set; }
        public static ChatService ChatService { get; private set; }
        public static ResponseHandler ResponseHandler { get; private set; }

        public ServiceLocator(ChatConfig chatConfig)
        {
            if (chatConfig == null)
                throw PodException.BuildException(
                               new InvalidParameter(new KeyValuePair<string, string>(nameof(chatConfig), "This field is Required")));

            ResponseHandler = new ResponseHandler(chatConfig);
            AsyncConnector = new AsyncConnector(chatConfig);
            ChatService = new ChatService(chatConfig);
            ConfigUserInfo(chatConfig);
        }

        private void ConfigUserInfo(ChatConfig chatConfig)
        {
            chatConfig.Initialized = true;
            ResponseHandler.UserInfo_MessageReceived += chatConfig.OnGetUserId;
            chatConfig.UserInfoUniqueId = ChatService.GetUserInfo();
        }
    }
}
