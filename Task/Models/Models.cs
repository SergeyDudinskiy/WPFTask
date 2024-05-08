using System.Windows;

namespace Task
{
    public class LogIn
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        private string _text;
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _text = "Просьба авторизироваться!";
                }
                else
                {
                    _text = value;
                }
            }
        }        
    }

    public class FormElements
    {
        public Visibility LogoutVisible { get; set; }
        public Visibility LoginVisible { get; set; }        
    }
}
