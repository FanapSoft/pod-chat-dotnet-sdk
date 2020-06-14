using System.Collections.Generic;
using POD_Async.Base;
using POD_Async.Exception;
using POD_Chat.Model.ServiceOutput;

namespace POD_Chat.Base
{
    public class ChatConfig
    {
        internal string UserInfoUniqueId { get; set; }
        internal bool Initialized { get; set; }

        private AsyncConfig config;
        public AsyncConfig Config
        {
            get => config;
            set => config = value ?? throw PodException.BuildException(
                                new InvalidParameter(
                                    new KeyValuePair<string, string>(nameof(config), "This field is Required")));
        }

        private string token;
        public string Token
        {
            get => token;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw PodException.BuildException(
                        new InvalidParameter(
                            new KeyValuePair<string, string>(nameof(token), "This field is Required")));

                token = value;
                if (Initialized) UserInfoUniqueId = ServiceLocator.ChatService.GetUserInfo();
            }
        }

        public int TokenIssuer { get; set; } = 1;
        public long UserId { get; private set; }

        public ChatConfig(AsyncConfig config, string token)
        {
            Config = config;
            Token = token;
        }

        internal void OnGetUserId(ChatResponseSrv<GetUserInfoResponse> message)
        {
            if (message != null && message.UniqueId.Equals(UserInfoUniqueId))
            {
                UserId = message.Result.User.Id;
            }
        }
    }
}
