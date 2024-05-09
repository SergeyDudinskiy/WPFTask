using SwaggerPetstore.Standard.Controllers;
using SwaggerPetstore.Standard.Exceptions;
using SwaggerPetstore.Standard.Models;
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

        public async Task<bool> EndRegistration(LogIn user, ObservableCollection<LogIn> lastUser)
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

            try
            {
                User foundedUser = await LogInViewModel.userController.GetUserByNameAsync(user.Login);
                return false;
            }
            catch (ApiException e)
            {
                if (e.Message == "User not found")
                {
                    try
                    {
                        User u = new User(1, user.Login, user.Name, user.Surname, user.Email, user.Password, user.Phone, 1);
                        await LogInViewModel.userController.CreateUserAsync(u);                        
                        user.Text = user.Login;
                        lastUser.Clear();
                        lastUser.Add(user);
                        return true;
                    }
                    catch (ApiException e1)
                    {
                        return false;
                    }                    
                }

                
            }
            
            return false;
        }

        public void ShowMessage(string message)
        {            
            MessageBox.Show(message);
        }
    }
}
