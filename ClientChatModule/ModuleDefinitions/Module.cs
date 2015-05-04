using ChatInterfaces;
using ClientSpecificServices.Services;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

namespace ClientSpecificServices.ModuleDefinitions
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
            _container.RegisterType<IDispatchService, ClientDispatchService>();
            _container.RegisterType<IChatService, ClientChatService>();
        }
    }
}
