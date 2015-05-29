using ChatInterfaces;
using ClientSpecificServices.Services;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

namespace ClientSpecificServices.ModuleDefinitions
{
    public class Module :IModule
    {
        private readonly IUnityContainer container;
        public Module(IUnityContainer container)
        {
            this.container = container;
        }

        public void Initialize()
        {
            container.RegisterType<IDispatchService, ClientDispatchService>();
            container.RegisterType<IClientChatService, ClientChatService>(new ContainerControlledLifetimeManager());
        }
    }
}
