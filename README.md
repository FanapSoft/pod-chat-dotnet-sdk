# POD-CHAT-DOT-NET-SDK
<img src="https://fanap.ir/images/fanap-logo.png" width="64" height="64" />
<br />
<br />

[![.NET](https://img.shields.io/badge/.Net-SDK-blue)](https://img.shields.io/badge/-Orange?style=flat-square)

Fanap's POD Chat Service - .Net SDK
## Installation

#### [Nuget](https://www.nuget.org/) 
```nuget
Install-Package POD_Chat -Version 0.10.5.1
```

## Intit 

```cs
Chat.sharedInstance.createChatObject(config: .init(socketAddress          : socketAddresss,
                                                    serverName            : serverName,
                                                    token                 : token,
                                                    ssoHost               : ssoHost,
                                                    platformHost          : platformHost,
                                                    fileServer            : fileServer,
                                                    enableCache           : true,
                                                    reconnectOnClose      : true,
                                                    isDebuggingLogEnabled : true
))

Chat.sharedInstance.delegate = self
```

## Usage 
```cs
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
new ServiceLocator(chatConfig);
```
<br/>
<br/>

## Event Handler 
```cs
ServiceLocator.ResponseHandler.GetThreads_MessageReceived += OnGetThreads;
```
<br/>

## Call Method
```cs
 public void GetThreads()
{
    try
    {
        var getThreadsRequest = GetThreadsRequest.ConcreteBuilder.Build();
        uniqueIds["GetThreads"] = ServiceLocator.ChatService.GetThreads(getThreadsRequest);
    }
    catch (Exception exception)
    {
        Console.WriteLine(exception.Message);
        throw;
    }
}
```
<br/>

## Documentation
For more information about how to use Chat SDK visit [Documentation](https://docs.pod.ir/v0.10.5.0/Chat/Csharp/5804/installation) 
<br/>
<br/>

## Developer Application 
In this repo, you can run a demo app inside the project directly to test functionality.
