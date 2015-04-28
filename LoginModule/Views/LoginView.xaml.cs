using System.Windows.Controls;
using LoginModule.ViewModels;
using Microsoft.Practices.Unity;

namespace LoginModule.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView(IUnityContainer unityContainer)
        {
            InitializeComponent();
            Loaded += (e, s) =>
            {
                DataContext = unityContainer.Resolve<LoginViewModel>();
            };
        }
    }
}
