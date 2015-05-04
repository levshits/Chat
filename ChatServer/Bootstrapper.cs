using System;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;

namespace ChatServer
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override IModuleCatalog CreateModuleCatalog()
        {
            return
                Microsoft.Practices.Prism.Modularity.ModuleCatalog.CreateFromXaml(new Uri(@"catalog.xaml",
                    UriKind.Relative));
        } 
        protected override System.Windows.DependencyObject CreateShell()
        {
            return null;
        }
    }
}
