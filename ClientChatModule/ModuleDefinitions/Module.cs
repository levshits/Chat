using ChatInterfaces;
using ClientChatModule.Services;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

namespace ClientChatModule.ModuleDefinitions
{
    public class Module :IModule
    {
        private readonly IUnityContainer _container;
        public Module(IUnityContainer container)
        {
            _container = container;
        }

        public void Initialize()
        {
            _container.RegisterType<IClientChatService, ClientChatService>();
        }
    }
}
