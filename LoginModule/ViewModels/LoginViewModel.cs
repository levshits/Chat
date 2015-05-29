using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ChatInterfaces;
using LoginModule.Annotations;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Unity;

namespace LoginModule.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly IUnityContainer container;
        private string ipAddress;
        private int port;
        private string login;
        private ICommand loginCommand;

        public string IpAddress
        {
            get { return ipAddress; }
            set
            {
                ipAddress = value;
                OnPropertyChanged();
            }
        }

        public int Port
        {
            get
            {
                return port;
            }
            set
            {
                port = value;
                OnPropertyChanged();
            }
        }

        public string Login
        {
            get
            {
                return login;
            }
            set
            {
                login = value;
                OnPropertyChanged();
            }
        }

        [NotNull]
        public ICommand LoginCommand { get
        {
            return loginCommand ??
                   (loginCommand =
                       new LoginCommand(this, container));
        }
            private set
            {
                if (value == null) throw new ArgumentNullException("value");
                loginCommand = value;
            }
        }

        public LoginViewModel(IUnityContainer container)
        {
            this.container = container;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion INotifyPropertyChanged
    }
}
