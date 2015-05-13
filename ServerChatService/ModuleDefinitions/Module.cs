using ChatInterfaces;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using ServerSpecificServices.Services;

namespace ServerSpecificServices.ModuleDefinitions
{
    public class Module:IModule
    {
        private readonly IUnityContainer container;
        public Module(IUnityContainer container)
        {
            this.container = container;
        }

        public void Initialize()
        {
            container.RegisterType<IDispatchService, ServerDispatchService>();
            container.RegisterType<IServerChatService, ServerChatService>();
        }
    }
}
