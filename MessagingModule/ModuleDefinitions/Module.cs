using MessagingModule.Views;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace MessagingModule.ModuleDefinitions
{
    public class Module :IModule
    {
        private readonly IUnityContainer _unityContainer;
        private readonly IRegionManager _regionManager;

        public Module(IUnityContainer unityContainer, IRegionManager regionManager)
        {
            _unityContainer = unityContainer;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _unityContainer.RegisterType<object, MainView>("MainView");
            _regionManager.RegisterViewWithRegion("ConversationHistoryRegion", () =>
                _unityContainer.Resolve<ConversationHistoryView>()
                );
            _regionManager.RegisterViewWithRegion("ClientListRegion", () =>
                _unityContainer.Resolve<ChatClientListView>()
                );
            _regionManager.RegisterViewWithRegion("SendMessageRegion", () =>
                _unityContainer.Resolve<SendMessageView>()
                );
        }
    }
}
