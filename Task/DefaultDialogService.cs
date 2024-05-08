using System.Collections.ObjectModel;
using System.Windows;

namespace Task
{
    public class DefaultDialogService : IDialogService
    {
        RegistrationWindow registrationWindow;

        public bool OpenWindow()
        {
            registrationWindow = new RegistrationWindow();

            if (registrationWindow.ShowDialog() == true)
            {
                return true;
            }

            return false;
        }

        public void CloseWindow()
        {
            registrationWindow.Close();
        }

        public bool EndRegistration(LogIn user, ObservableCollection<LogIn> users, ObservableCollection<LogIn> lastUser)
        {
            
            if (string.IsNullOrEmpty(user.Login) || 
                string.IsNullOrEmpty(user.Password) ||
                string.IsNullOrEmpty(user.Name) ||
                string.IsNullOrEmpty(user.Surname) ||
                string.IsNullOrEmpty(user.Phone) ||
                string.IsNullOrEmpty(user.Email))
            {
                return false;
            }

            for (int i = 0; i < users.Count; i++)
            {
                if (user.Login == users[i].Login)
                {
                    return false;
                }
            }

            user.Text = user.Login;
            users.Add(user);
            lastUser.Clear();
            lastUser.Add(user);
            return true;           
        }

        public void ShowMessage(string message)
        {            
            MessageBox.Show(message);
        }
    }
}
