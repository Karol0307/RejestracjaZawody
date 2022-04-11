using Competitions.Commands;
using Competitions.Helpers;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Competitions
{
    class LoginPageViewModel : ViewModelBase
    {
        public ICommand LoginCommand { get; set; }

        public ICommand OpenRegisterViewCommand { get; set; }

        private string username;

        public string Username
        {
            get { return username; }
            set { username = value;
                OnPropertyChanged();
            }
        }


        private string password;

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        private string loginMessage;

        public string LoginMessage
        {
            get { return loginMessage; }
            set { loginMessage = value;
                OnPropertyChanged();
            }
        }

        private bool isVisible = true;
        public bool IsVisible
        {
            get { return isVisible; }
            set
            {
                isVisible = value;
                OnPropertyChanged();
            }
        }

        private Visibility turnLoginMessage = Visibility.Hidden;

        public Visibility TurnLoginMessage
        {
            get { return turnLoginMessage; }
            set { turnLoginMessage = value;
                OnPropertyChanged();
            }
        }

        public bool canLogin => !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password);



        public LoginPageViewModel()
        {
            LoginCommand = new RelayCommand(Login);
            OpenRegisterViewCommand = new RelayCommand(OpenRegisterView);
        }

        private void Login()
        {
            if (canLogin)
            {
                var user = DatabaseLocator.Database.Uzytkownik.FirstOrDefault(k => k.Login.Equals(username) && k.Haslo.Equals(password));
                if (user != null)
                {
                    TurnLoginMessage = Visibility.Hidden;
                    DatabaseLocator.user = user;
                    IsVisible = false;
                    MainView m = new MainView();
                    m.Show();
                }
                else
                {
                    LoginMessage = "Dane logowania nieprawidłowe!";
                    TurnLoginMessage = Visibility.Visible;
                }
            }
            else
            {
                LoginMessage = "Uzupełnij wszystkie pola!";
                TurnLoginMessage = Visibility.Visible;
            }
        }

        private void OpenRegisterView()
        {
            IsVisible = false;
            RegisterView r = new RegisterView();
            r.Show();
        }


    }
}
