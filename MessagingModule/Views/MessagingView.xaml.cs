using System.Windows.Controls;
using MessagingModule.ViewModels;

namespace MessagingModule.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MessagingView : UserControl
    {
        public MessagingView(MessagingViewModel viewModel)
        {
            InitializeComponent();
            Loaded += (e, s) =>
            {
                DataContext = viewModel;
            };
        }

    }
}
