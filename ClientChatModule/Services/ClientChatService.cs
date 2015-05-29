using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using ChatEntities;
using ChatInterfaces;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.PubSubEvents;
using Timer = System.Threading.Timer;

namespace ClientSpecificServices.Services
{
    public class ClientChatService : IClientChatService
    {
        private readonly ChatMessageEvent _chatMessageEvent;
        private readonly ILoggerFacade _logger;
        private readonly UpdateUserListEvent _updateUserListEvent;
        private readonly Timer _userCheckTimer;
        private readonly ISocketCommunicationService socketCommunicationService;
        private IPEndPoint _serverAddress;

        public ClientChatService(ISocketCommunicationService socketCommunicationService,
            IDispatchService dispatchService, IEventAggregator eventAggregator, ILoggerFacade logger)
        {
            this.socketCommunicationService = socketCommunicationService;
            _logger = logger;
            Users = new List<ChatUser>();
            socketCommunicationService.DispatchService = dispatchService;
            dispatchService.ChatService = this;
            dispatchService.SocketCommunicationService = socketCommunicationService;

            socketCommunicationService.Run();
            ConnectionSetting = new ChatUser
            {
                IpAddress = socketCommunicationService.EndPointConfiguration.Address.ToString(),
                Port = socketCommunicationService.EndPointConfiguration.Port
            };
            _chatMessageEvent = eventAggregator.GetEvent<ChatMessageEvent>();
            _updateUserListEvent = eventAggregator.GetEvent<UpdateUserListEvent>();
            _userCheckTimer = new Timer(TimerTickHandler);
        }

        public ChatUser ConnectionSetting { get; private set; }

        public void RemoveUser(ChatUser user)
        {
            var temp = Users.FirstOrDefault(element => (user.Login == element.Login) &&
                                                       (user.Port == element.Port) &&
                                                       (user.IpAddress == element.IpAddress));
            if (temp != null)
            {
                Users.Remove(temp);
                _updateUserListEvent.Publish(new UpdateUserListPacket
                {
                    User = user,
                    Type = OperationType.Remove
                });
            }
        }

        public List<ChatUser> Users { get; private set; }

        public void AddUser(ChatUser user)
        {
            if (user.Login != ConnectionSetting.Login)
            {
                var temp = Users.FirstOrDefault(element => (user.Login == element.Login) &&
                                                           (user.Port == element.Port) &&
                                                           (user.IpAddress == element.IpAddress));
                if (temp == null)
                {
                    Users.Add(user);
                    _updateUserListEvent.Publish(new UpdateUserListPacket
                    {
                        User = user,
                        Type = OperationType.Add
                    });
                }
            }
        }

        public void SendMessage(ChatUser user, ChatMessage msg)
        {
            try
            {
                socketCommunicationService.Send(new IPEndPoint(IPAddress.Parse(user.IpAddress), user.Port),
                    new CommunicationPacket
                    {
                        Type = PacketType.Message,
                        Content = msg,
                        IpAddressFrom = ConnectionSetting.IpAddress
                    });
            }
            catch (Exception ex)
            {
                _logger.Log("Problem with connection", Category.Exception, Priority.High);
                MessageBox.Show("Problem with connection");
            }
        }

        public void Connect(IPEndPoint address, string login)
        {
            _serverAddress = address;
            if (!socketCommunicationService.IsRunned)
            {
                socketCommunicationService.Run();
                ConnectionSetting.IpAddress = socketCommunicationService.EndPointConfiguration.Address.ToString();
                ConnectionSetting.Port = socketCommunicationService.EndPointConfiguration.Port;
            }
            ConnectionSetting.Login = login;
            socketCommunicationService.Send(address, new CommunicationPacket
            {
                PortFrom = ConnectionSetting.Port,
                IpAddressFrom = ConnectionSetting.IpAddress,
                Type = PacketType.Enter,
                Content = ConnectionSetting
            });
            _userCheckTimer.Change(TimeSpan.FromSeconds(2), TimeSpan.Zero);
        }

        public void ArriveMessage(ChatMessage message)
        {
            _chatMessageEvent.Publish(message);
        }

        public event EventHandler UserListUpdated;
        public event EventHandler<ChatMessage> MessageArrived;

        private void TimerTickHandler(object state)
        {
            try
            {
                socketCommunicationService.Send(_serverAddress, new CommunicationPacket
                {
                    IpAddressFrom = ConnectionSetting.IpAddress,
                    PortFrom = ConnectionSetting.Port,
                    Type = PacketType.GetList
                });
                _userCheckTimer.Change(TimeSpan.FromMinutes(0.5), TimeSpan.Zero);
            }
            catch (Exception ex)
            {
                _logger.Log("Problem with connection", Category.Exception, Priority.High);
                MessageBox.Show("Problem with connection");
            }
        }
    }
}