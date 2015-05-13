using System;
using System.Windows;
using Chat.Services;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;

namespace Chat
{
    class Bootstraper : UnityBootstrapper
    {
        protected override IModuleCatalog CreateModuleCatalog()
        {
            return
                Microsoft.Practices.Prism.Modularity.ModuleCatalog.CreateFromXaml(new Uri(@"catalog.xaml",
                    UriKind.Relative));
        }

        protected override ILoggerFacade CreateLogger()
        {
            return new NLogILoggerFacade();
        }

        protected override DependencyObject CreateShell()
        {
            ShellView view = Container.TryResolve<ShellView>();
            return view;
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            Application.Current.MainWindow = (Window)Shell;
            Application.Current.MainWindow.Show();
        }
    }
}
