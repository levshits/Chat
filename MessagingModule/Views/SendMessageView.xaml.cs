using System.Windows.Controls;
using MessagingModule.ViewModels;
using Microsoft.Practices.Unity;

namespace MessagingModule.Views
{
    /// <summary>
    /// Interaction logic for SendMessageView.xaml
    /// </summary>
    public partial class SendMessageView : UserControl
    {
        public SendMessageView(IUnityContainer unityContainer)
        {
            InitializeComponent();
            Loaded += (e, s) =>
            {
                DataContext = unityContainer.Resolve<SendMessageViewModel>();
            };
        }
    }
}