using LoginModule.ViewModels;
using LoginModule.Views;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace LoginModule.ModuleDefinitions
{
    public class Module:IModule 
    {
        private IUnityContainer container;
        private IRegionManager regionManager;

        public Module(IUnityContainer container, IRegionManager regionManager)
        {
            this.container = container;
            this.regionManager = regionManager;
        }
        public void Initialize()
        {
            regionManager.RegisterViewWithRegion( "WindowRegion", 
                                                       () => container.Resolve<LoginView>());
        }
    }
}
