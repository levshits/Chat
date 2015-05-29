using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ChatInterfaces;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace LoginModule.ViewModels
{
    public class LoginCommand:ICommand
    {
        private readonly LoginViewModel viewModel;
        private readonly IClientChatService clientChatService;
        private readonly ILoggerFacade logger;
        private readonly IRegionManager regionManager;
        public LoginCommand(LoginViewModel viewModel, IUnityContainer container)
        {
            this.viewModel = viewModel;
            regionManager = container.Resolve<IRegionManager>();
            clientChatService = container.Resolve<IClientChatService>();
            logger = container.Resolve<ILoggerFacade>();
        }

        public bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(viewModel.Login);
        }

        public void Execute(object parameter)
        {
            try
            {
                clientChatService.Connect(new IPEndPoint(IPAddress.Parse(viewModel.IpAddress), viewModel.Port), viewModel.Login);
                regionManager.RequestNavigate("WindowRegion", "MainView");

            }
            catch (Exception exception)
            {
                logger.Log(exception.Message, Category.Exception, Priority.Low);
                MessageBox.Show("Unable to connect. Please check connection settings", "Info");
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
