using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ChatEntities;
using ChatInterfaces;
using LoginModule.Annotations;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.PubSubEvents;

namespace MessagingModule.ViewModels
{
    public class MessagingViewModel:INotifyPropertyChanged
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IClientChatService _chatService;
        private ObservableCollection<ChatUser> _users;
        private ObservableCollection<ChatMessage> _messages;
        private ChatUser _currentUser;
        private ICommand _sendCommand;
        private string _messagetext;

        public ObservableCollection<ChatUser> Users
        {
            get
            {
                return _users;
            }
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ChatMessage> Messages
        {
            get
            {
                return _messages;
            }
            set
            {
                _messages = value;
                OnPropertyChanged();
            }
        }

        public ConcurrentDictionary<ChatUser, List<ChatMessage>> Conversations { get; set; }

        public ChatUser CurrentUser
        {
            get
            {
                return _currentUser;
            }
            set
            {
                _currentUser = value;
                Messages = new ObservableCollection<ChatMessage>(Conversations[value]);
                OnPropertyChanged();
            }
        }

        public ICommand SendCommand { get { return _sendCommand ?? (_sendCommand = new DelegateCommand(() =>
        {
            if (CurrentUser != null)
            {
                var msg = new ChatMessage {From = Me, To = CurrentUser, Text = MessageText, Time = DateTime.Now};
                _chatService.SendMessage(CurrentUser, msg);
                Messages.Add(msg);
                MessageText = String.Empty;
            }
        })); } }

        public ChatUser Me { get; set; }

        public string MessageText
        {
            get
            {
                return _messagetext;
            }
            set
            {
                _messagetext = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MessagingViewModel(IEventAggregator eventAggregator, IClientChatService chatService)
        {
            _eventAggregator = eventAggregator;
            _chatService = chatService;
            var updateUserListEvent = eventAggregator.GetEvent<UpdateUserListEvent>();
            updateUserListEvent.Subscribe(UpdateUserListHandler, ThreadOption.UIThread);
            var messageArrivedEvent = eventAggregator.GetEvent<ChatMessageEvent>();
            messageArrivedEvent.Subscribe(MessageArrivedHandler, ThreadOption.UIThread);
            Conversations = new ConcurrentDictionary<ChatUser, List<ChatMessage>>();
            Users = new ObservableCollection<ChatUser>();
            Messages = new ObservableCollection<ChatMessage>();
            Me = _chatService.ConnectionSetting;
        }

        private void MessageArrivedHandler(ChatMessage obj)
        {
            var user =
                Users.FirstOrDefault(
                    e => obj.From.Login == e.Login);
            if (user != null)
            {
                Conversations[user].Add(obj);
                if (user == CurrentUser)
                {
                    Messages.Add(obj);
                }
            }

        }

        private void UpdateUserListHandler(UpdateUserListPacket obj)
        {
            if (obj.Type == OperationType.Add)
            {
                Users.Add(obj.User);
                Conversations[obj.User] = new List<ChatMessage>();
            }
            else
            {
                Users.Remove(obj.User);
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
