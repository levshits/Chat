using ChatInterfaces;
using ChatSocketCommunicationService.Services;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

namespace ChatSocketCommunicationService.ModuleDefinitions
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
            container.RegisterType<ISocketCommunicationService, SocketCommunicationService>();
        }
    }
}
