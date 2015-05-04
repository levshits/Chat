using ChatInterfaces;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using ServerSpecificServices.Services;

namespace ServerSpecificServices.ModuleDefinitions
{
    public class Module:IModule
    {
        private readonly IUnityContainer _container;
        public Module(IUnityContainer container)
        {
            _container = container;
        }

        public void Initialize()
        {
            _container.RegisterType<IDispatchService, ServerDispatchService>();
            _container.RegisterType<IChatService, ServerChatService>();
        }
    }
}
