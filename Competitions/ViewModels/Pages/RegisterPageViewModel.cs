using Competitions.Commands;
using Competitions.Database;
using Competitions.Helpers;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Competitions
{
    class RegisterPageViewModel : ViewModelBase
    {

        public ICommand RegisterCommand { get; set; }

        public ICommand OpenLoginViewCommand { get; set; }

        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        private string surname;

        public string Surname
        {
            get { return surname; }
            set
            {
                surname = value;
                OnPropertyChanged();
            }
        }

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

        private string confirmPassword;

        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                confirmPassword = value;
                OnPropertyChanged();
            }
        }

        private string registerMessage;

        public string RegisterMessage
        {
            get { return registerMessage; }
            set
            {
                registerMessage = value;
                OnPropertyChanged();
            }
        }

        private Visibility turnRegisterMessage = Visibility.Hidden;

        public Visibility TurnRegisterMessage
        {
            get { return turnRegisterMessage; }
            set
            {
                turnRegisterMessage = value;
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

        public bool canRegister => !string.IsNullOrEmpty(name) &&
            !string.IsNullOrEmpty(surname) &&
            !string.IsNullOrEmpty(username) &&
            !string.IsNullOrEmpty(password) &&
            !string.IsNullOrEmpty(confirmPassword);

        public bool samePassword => !string.IsNullOrEmpty(Password) && Password.Equals(ConfirmPassword);

        public RegisterPageViewModel()
        {
            RegisterCommand = new RelayCommand(Register);
            OpenLoginViewCommand = new RelayCommand(OpenLoginView);
        }

        private void Register()
        {
            if (canRegister)
            {
                if (samePassword)
                {
                    var user = DatabaseLocator.Database.Uzytkownik.FirstOrDefault(k => k.Haslo.Equals(password));
                    if (user != null)
                    {
                        TurnRegisterMessage = Visibility.Visible;
                        RegisterMessage = "Hasło jest zajęte!";
                    }
                    else if(Password.Length < 8)
                    {
                        TurnRegisterMessage = Visibility.Visible;
                        RegisterMessage = "Hasło jest za krótkie!";
                    }
                    else
                    {
                        var newUser = new Uzytkownik { Imie = name, Nazwisko = surname, Login = username, Haslo = password };
                        DatabaseLocator.Database.Uzytkownik.Add(newUser);
                        DatabaseLocator.Database.SaveChanges();
                        MessageBox.Show("Rejestracja udana!");
                        OpenLoginView();
                    }
                }
                else
                {
                    TurnRegisterMessage = Visibility.Visible;
                    RegisterMessage = "Hasła muszą być takie same!";
                }
            }
            else
            {
                TurnRegisterMessage = Visibility.Visible;
                RegisterMessage = "Wszystkie pola muszą być uzupełnione!";
            }
        }

        private void OpenLoginView()
        {
            IsVisible = false;
            LoginView l = new LoginView();
            l.Show();
        }

    }
}
