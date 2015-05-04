using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using LoginModule.Annotations;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;

namespace LoginModule.ViewModels
{
    class LoginViewModel : INotifyPropertyChanged
    {
        private readonly IRegionManager _regionManager;
        private string _ipAddress;
        private int _port;
        private string _login;
        private ICommand _loginCommand;

        public string IpAddress
        {
            get { return _ipAddress; }
            set
            {
                _ipAddress = value;
                OnPropertyChanged();
            }
        }

        public int Port
        {
            get
            {
                return _port;
            }
            set
            {
                _port = value;
                OnPropertyChanged();
            }
        }

        public string Login
        {
            get
            {
                return _login;
            }
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }

        [NotNull]
        public ICommand LoginCommand { get
        {
            return _loginCommand ??
                   (_loginCommand =
                       new DelegateCommand(
                           () =>
                           {
                               _regionManager.RequestNavigate("WindowRegion", new Uri("MainView", UriKind.Relative));
                           }));
        }
            private set
            {
                if (value == null) throw new ArgumentNullException("value");
                _loginCommand = value;
            }
        }

        public LoginViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
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
