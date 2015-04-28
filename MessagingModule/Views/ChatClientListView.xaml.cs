using System.Windows.Controls;
using MessagingModule.ModuleDefinitions;
using Microsoft.Practices.Unity;

namespace MessagingModule.Views
{
    /// <summary>
    /// Interaction logic for ChatClientListView.xaml
    /// </summary>
    public partial class ChatClientListView : UserControl
    {
        public ChatClientListView(IUnityContainer unityContainer)
        {
            InitializeComponent();
            Loaded += (e, s) =>
            {
                DataContext = unityContainer.Resolve<ChatClientListViewModel>();
            };
        }
    }
}