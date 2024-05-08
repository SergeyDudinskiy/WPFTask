using System.Windows;

namespace Task
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LogInViewModel.logInViewModel = new LogInViewModel(new DefaultDialogService());
            DataContext = LogInViewModel.logInViewModel;            
        }
    }
}