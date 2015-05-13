using System;
using System.Windows;
using Chat.Services;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;


namespace ChatServer
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override ILoggerFacade CreateLogger()
        {
            return new NLogILoggerFacade();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return
                Microsoft.Practices.Prism.Modularity.ModuleCatalog.CreateFromXaml(new Uri(@"catalog.xaml",
                    UriKind.Relative));
        } 
        protected override DependencyObject CreateShell()
        {
            return null;
        }
    }
}
