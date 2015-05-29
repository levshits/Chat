using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using ChatEntities;
using ChatInterfaces;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Unity;

namespace ServerSpecificServices.Services
{
    public class ServerChatService:IServerChatService
    {
        private readonly ISocketCommunicationService socketCommunicationService;
        private readonly Timer userCheckTimer;

        [Dependency]
        public ILoggerFacade Logger { get; set; }
        public ServerChatService(ISocketCommunicationService socketCommunicationService, IDispatchService dispatchService)
        {
            Users = new List<ChatUser>();
            this.socketCommunicationService = socketCommunicationService;
            this.socketCommunicationService.DispatchService = dispatchService;
            dispatchService.SocketCommunicationService = this.socketCommunicationService;
            dispatchService.ChatService = this;
            userCheckTimer = new Timer(TimerTickHandler);
            userCheckTimer.Change(TimeSpan.FromMinutes(0.5), TimeSpan.Zero);
        }

        private List<ChatUser> pingedUsers = new List<ChatUser>();

        private void TimerTickHandler(object state)
        {
            Logger.Log("Timer tick", Category.Debug, Priority.Low);
            pingedUsers.Clear();
            pingedUsers.AddRange(Users);
            lock (pingedUsers)
            {
                pingedUsers.Clear();
                pingedUsers.AddRange(Users);
                foreach (var chatUser in pingedUsers)
                {
                    try
                    {
                        socketCommunicationService.Send(
                            new IPEndPoint(IPAddress.Parse(chatUser.IpAddress), chatUser.Port),
                            new CommunicationPacket
                            {
                                Type = PacketType.Ping,
                                IpAddressFrom = ConnectionSetting.IpAddress,
                                PortFrom = ConnectionSetting.Port
                            });
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(string.Format("Problem with connecting to user", chatUser.Login), Category.Info,
                            Priority.Low);
                    }
                }
            }
            Thread.Yield();
            Thread.Sleep(TimeSpan.FromSeconds(5));
            Users = Users.Except(pingedUsers).ToList();
            userCheckTimer.Change(TimeSpan.FromMinutes(0.5), TimeSpan.Zero);
        }

        public void Start()
        {
            Start(null, "Server");
        }

        public void Start(IPEndPoint address, string login)
        {
            socketCommunicationService.Run(address, isServerMode: true);
            ConnectionSetting = new ChatUser
            {
                IpAddress = socketCommunicationService.EndPointConfiguration.Address.ToString(),
                Port = socketCommunicationService.EndPointConfiguration.Port,
                Login = login
            };
        }

        public void RegisterPingResponse(string ipAddress, int port)
        {
            lock (pingedUsers)
            {
                var user = Users.First(element => element.IpAddress == ipAddress && element.Port == port);
                Logger.Log(string.Format("ping {0} {1}", ipAddress, port), Category.Info, Priority.Low);
                if (user != null)
                {
                    pingedUsers.Remove(user);
                }
            }
        }

        public void Stop()
        {
            socketCommunicationService.Stop();
        }

        public ChatUser ConnectionSetting { get; private set; }
        public void RemoveUser(ChatUser user)
        {
            var temp = Users.First(element => 
                user.Login == element.Login &&
                user.IpAddress == element.IpAddress &&
                user.Port==element.Port);
            if (temp != null)
            {
                Users.Remove(temp);
                Logger.Log(string.Format("User removed {0}", user.Login),Category.Info, Priority.Low );
            }
        }

        public List<ChatUser> Users { get; private set; }

        public void AddUser(ChatUser user)
        {
            Users.Add(user);
            Logger.Log(string.Format("User added {0}", user.Login), Category.Info, Priority.Low);
        }

        public void SendMessage(ChatUser user, ChatMessage msg)
        {
            socketCommunicationService.Send(new IPEndPoint(IPAddress.Parse(user.IpAddress), user.Port),
                new CommunicationPacket
            {
                Type = PacketType.Message, 
                Content = msg, 
                IpAddressFrom = ConnectionSetting.IpAddress
            });
        }
    }
}
