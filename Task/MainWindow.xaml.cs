using SwaggerPetstore.Standard.Models;
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

            LogInViewModel.client = new SwaggerPetstore.Standard.SwaggerPetstoreClient.Builder()
            .OAuthScopes(new List<OAuthScopeEnum>() { OAuthScopeEnum.Readpets, OAuthScopeEnum.Writepets })
            .Environment(SwaggerPetstore.Standard.Environment.Production).OAuthToken(new OAuthToken("special-key", tokenType: "string"))
            .Build();
            LogInViewModel.userController = LogInViewModel.client.UserController;
        }
    }
}