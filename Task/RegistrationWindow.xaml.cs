using System.Windows;

namespace Task
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
            DataContext = LogInViewModel.logInViewModel;
        }
    }
}
