using System.Windows.Controls;
using MessagingModule.ViewModels;
using Microsoft.Practices.Unity;

namespace MessagingModule.Views
{
    /// <summary>
    /// Interaction logic for ConversationHistoryView.xaml
    /// </summary>
    public partial class ConversationHistoryView : UserControl
    {
        public ConversationHistoryView(IUnityContainer unityContainer)
        {
            InitializeComponent();
            Loaded += (e, s) =>
            {
                DataContext = unityContainer.Resolve<ConversationHistoryViewModel>();
            };
        }
    }
}