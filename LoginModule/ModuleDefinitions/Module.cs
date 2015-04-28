using LoginModule.Views;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace LoginModule.ModuleDefinitions
{
    public class Module:IModule 
    {
        private IUnityContainer _container;
        private IRegionManager _regionManager;

        public Module(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }
        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion( "WindowRegion", 
                                                       () => _container.Resolve<LoginView>());
        }
    }
}
