using System.ComponentModel;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Windows;
using SwaggerPetstoreOpenAPI30.Standard.Models;

namespace Task
{
    public class LogInViewModel : INotifyPropertyChanged
    {
        public static LogInViewModel logInViewModel;
        private LogIn _user;        
        private LogIn _developer;       
        private FormElements _formElements;
        public ICommand LoginCommand { get; }
        public ICommand LogoutCommand { get; }
        public ICommand ShowView1Command { get; }
        public ICommand ShowView2Command { get; }
        DefaultDialogService defaultDialogService;
        public ObservableCollection<LogIn> Users { get; set; }
        public ObservableCollection<LogIn> lastUser { get; set; }

        public LogInViewModel(DefaultDialogService defaultDialogService)
        {
            this.defaultDialogService = defaultDialogService;
            _user = new LogIn();            
            _developer = new LogIn() { Id = 1, Login = "Admin", Password="123", Phone="123123213", Name="Sergey", Surname = "Dudinskiy", Email = "ss@mail.ru"};
            _formElements = new FormElements();
            Users = new ObservableCollection<LogIn>();
            lastUser = new ObservableCollection<LogIn>();
            LoginCommand = new RelayCommand((param) => LoggedIn(param));
            LogoutCommand = new RelayCommand((param) => LoggedOut(param));
            ShowView1Command = new RelayCommand((param) => ShowView1(param));
            ShowView2Command = new RelayCommand((param) => ShowView2(param));
        }

        /// <summary>
        /// для класса LogIn
        /// </summary>
        public int Id
        {
            get => _user.Id;
            set
            {
                _user.Id = value;
               // OnPropertyChanged(nameof(Id));
            }
        }

        public string Login 
        {
            get => _user.Login;
            set
            {
                _user.Login = value;
                //OnPropertyChanged(nameof(Login));
            }
        }

        public string Password
        {
            get => _user.Password;
            set
            {
                _user.Password = value;
                //OnPropertyChanged(nameof(Password));
            }
        }

        public string Name
        {
            get => _user.Name;
            set
            {
                _user.Name = value;
                //OnPropertyChanged(nameof(Name));
            }
        }

        public string Surname
        {
            get => _user.Surname;
            set
            {
                _user.Surname = value;
                //OnPropertyChanged(nameof(Surname));
            }
        }

        public string Phone
        {
            get => _user.Phone;
            set
            {
                _user.Phone = value;  
                //OnPropertyChanged(nameof(Phone));
            }
        }

        public string Email
        {
            get => _user.Email;
            set
            {
                _user.Email = value;         
                //OnPropertyChanged(nameof(Email));
            }
        }       

        /// <summary>
        /// для класса FormElements
        /// </summary>
        public Visibility LoginVisible
        {
            get => _formElements.LoginVisible;
            set
            {
                _formElements.LoginVisible = value;                
            }
        }

        public Visibility LogoutVisible
        {
            get => _formElements.LogoutVisible;
            set
            {
                _formElements.LogoutVisible = value;                
            }
        }        

        private void LoggedIn(object parametr)
        {
            if (!string.IsNullOrEmpty(_user.Login) && !string.IsNullOrEmpty(_user.Password))
            {
                bool flagFound = false;

                for (int i = 0; i < Users.Count; i++)
                {
                    if (_user.Login == Users[i].Login && _user.Password == Users[i].Password)
                    {
                        _user = Users[i];
                        flagFound = true;
                        break;
                    }
                }

                if (flagFound)
                {
                    _formElements.LoginVisible = Visibility.Hidden;
                    _formElements.LogoutVisible = Visibility.Visible;
                    OnPropertyChanged(nameof(LoginVisible));
                    OnPropertyChanged(nameof(LogoutVisible));
                    OnPropertyChanged(nameof(Login));
                    OnPropertyChanged(nameof(Password));                   
                }
                else
                {                    
                    defaultDialogService.ShowMessage("Пользователь не найден!");
                }
            }

            UserEnter();
        }

        private void UserEnter()
        {
            lastUser.Clear();

            if (_user.Login == null)
            {
                _user.Text = "";
            }

            lastUser.Add(_user);
            OnPropertyChanged(nameof(lastUser));
        }

        private void LoggedOut(object parametr)
        {
            _user = new LogIn();
            lastUser.Clear();

            _formElements.LoginVisible = Visibility.Visible;
            _formElements.LogoutVisible = Visibility.Hidden;

            OnPropertyChanged(nameof(Login));
            OnPropertyChanged(nameof(Password));
            OnPropertyChanged(nameof(LoginVisible));
            OnPropertyChanged(nameof(LogoutVisible));                        
        }

        private void ShowView1(object parametr)
        {
            UserEnter();                         
        }

        private void ShowView2(object parametr)
        {
            _developer.Text = _developer.Login;

            if (!Users.Contains(_developer))
            {
                Users.Add(_developer);                
            }

            lastUser.Clear();
            lastUser.Add(_developer);
            OnPropertyChanged(nameof(lastUser));
        }

        private RelayCommand openCommand;
        public RelayCommand OpenCommand
        {
            get
            {
                return openCommand ??
                  (openCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          if (defaultDialogService.OpenWindow() == true)
                          {
                              defaultDialogService.ShowMessage("Окно открыто");
                          }
                      }
                      catch (Exception ex)
                      {
                          defaultDialogService.ShowMessage(ex.Message);
                      }
                  }));
            }
        }

        private RelayCommand registrationCommand;
        public RelayCommand RegistrationCommand
        {
            get => registrationCommand ??
                  (registrationCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          if (defaultDialogService.EndRegistration(_user, Users, lastUser) == true)
                          {
                              defaultDialogService.ShowMessage("Регистрация завершена!");
                              _formElements.LoginVisible = Visibility.Hidden;
                              _formElements.LogoutVisible = Visibility.Visible;
                              OnPropertyChanged(nameof(LoginVisible));
                              OnPropertyChanged(nameof(LogoutVisible));
                              OnPropertyChanged(nameof(Login));
                              OnPropertyChanged(nameof(Password));
                              OnPropertyChanged(nameof(lastUser));
                          }
                          else
                          {
                              defaultDialogService.ShowMessage("Не заполнены все текстовые поля и(или) пользователь с введённым логином уже существует!");
                          }
                      }
                      catch (Exception ex)
                      {
                          defaultDialogService.ShowMessage(ex.Message);
                      }
                      finally 
                      {
                          defaultDialogService.CloseWindow();
                      }
                  }));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));        
    }
}