# Pod ASYNC .NET SDK
<img src="https://fanap.ir/images/fanap-logo.png" width="64" height="64" />
<br />
<br />

[![.NET](https://img.shields.io/badge/Standard-2+-orange?style=flat-square)](https://img.shields.io/badge/-Orange?style=flat-square)
[![Platforms](https://img.shields.io/badge/Platforms-.NET-yellowgreen?style=flat-square)](https://img.shields.io/badge/Platforms-macOS_iOS_tvOS_watchOS_Linux_Windows-Green?style=flat-square)
[![Compatible](https://img.shields.io/badge/Nuget-v0.9.1.3-blue)](https://img.shields.io/badge/Pod-v0.10-blue)

#### [NUGET](https://www.nuget.org/packages/POD_Async/) 
#### [Github](https://github.com/FanapSoft/pod-chat-dotnet-sdk/tree/master/POD_Async)

Fanap's POD Async Service - .NET SDK
## Features

- [x] Simplify connection with AQTIVEMQ

## Installation

In `Package Manager`:

```console
PM> 'Install-Package POD_Async -Version 0.9.1.3'
```

## Intit 

```c#
var config =  new AsyncConfig.Builder().SetQueuePassword("queuePassword")
                    .SetQueueReceive("queueReceive")
                    .SetQueueSend("queueSend")
                    .SetQueueConnectionTimeout(2000)
                    .SetServerName("serverName")
                    .SetFileServer("https://core.pod.ir")
                    .SetPlatformHost("https://sandbox.pod.ir:8043/srv/basic-platform")
                    .SetSsoHost("https://accounts.pod.ir")
                    .SetConsumersCount(1)
                    .Build();
var asyncService                    = new AsyncService(config);
asyncService.AsyncMessageReceived  += OnMessageReceived;
asyncService.AsyncError            += OnAsyncError;
```

## Usage 
```c#
asyncService.SendMessage(chatMessage);
```
<br/>
<br/>

## Developer Application 
For more example and usage you can use [developer implementation app](https://github.com/FanapSoft/pod-chat-dotnet-sdk)
