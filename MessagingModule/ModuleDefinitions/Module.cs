using MessagingModule.Views;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace MessagingModule.ModuleDefinitions
{
    public class Module :IModule
    {
        private readonly IUnityContainer unityContainer;
        private readonly IRegionManager regionManager;

        public Module(IUnityContainer unityContainer, IRegionManager regionManager)
        {
            this.unityContainer = unityContainer;
            this.regionManager = regionManager;
        }

        public void Initialize()
        {
            unityContainer.RegisterType<object, MessagingView>("MainView");
        }
    }
}
