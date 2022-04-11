using Competitions.Commands;
using Competitions.Database;
using Competitions.Helpers;
using Competitions.ViewModels.Controls;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Competitions
{
    class HomePageViewModel : ViewModelBase
    {
        public ObservableCollection<ActiveCompetitionViewModel> ActivateCompetitionsList { get; set; } = new ObservableCollection<ActiveCompetitionViewModel>();
        public ObservableCollection<AttachCompetitionViewModel> AttachCompetitionsList { get; set; } = new ObservableCollection<AttachCompetitionViewModel>();

        public ICommand LogOutCommand { get; set; }

        public ICommand JoinCommand { get; set; }

        private string name = DatabaseLocator.user.Imie;

        public string Name
        {
            get { return name; }
            private set { }
            
        }

        private string surname = DatabaseLocator.user.Nazwisko;

        public string Surname
        {
            get { return surname; }
            private set { }
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

        public HomePageViewModel()
        {
            LogOutCommand = new RelayCommand(LogOut);
            displayCompetitions();
            JoinCommand = new RelayCommand(Join);
        }

        private void Join()
        {
            var selectedCompetition = AttachCompetitionsList.Where(k => k.toJoin).ToList();

            foreach (var c in selectedCompetition)
            {
                    var com = DatabaseLocator.Database.Zawody.OrderBy(k => k.Numer).FirstOrDefault(k => k.ID_bieg == c.id);
                    int number;
                    if (com == null)
                        number = 1;
                    else{
                        number = com.Numer + 1;
                    }
                    var competition = new ActiveCompetitionViewModel
                    {
                        Name = c.Name,
                        Date = c.Date,
                        Distance = c.Distance,
                        Place = c.Place,
                        Number = number
                    };
                    DatabaseLocator.Database.Zawody.Add(new Zawody { ID_bieg = c.id, ID_uzytkownik = DatabaseLocator.user.ID, Numer = number });
                    DatabaseLocator.Database.SaveChanges();
                    ActivateCompetitionsList.Add(competition);
                    AttachCompetitionsList.Remove(c);
            }
        }

        private void LogOut()
        {
            IsVisible = false;
            DatabaseLocator.user = null;
            LoginView l = new LoginView();
            l.Show();
        }

        private void saveActivateCompetitions()
        {
            var competitions = from bieg in DatabaseLocator.Database.Bieg
                               from zawody in DatabaseLocator.Database.Zawody
                               where zawody.ID_bieg == bieg.ID && zawody.ID_uzytkownik == DatabaseLocator.user.ID
                               select new
                               {
                                   bieg.ID,
                                   bieg.Nazwa,
                                   bieg.Miejsce,
                                   bieg.Dystans,
                                   bieg.Data,
                                   zawody.Numer
                               };
            foreach (var c in competitions)
            {
                var competition = new ActiveCompetitionViewModel
                {
                    Name = c.Nazwa,
                    Date = c.Data,
                    Distance = c.Dystans,
                    Place = c.Miejsce,
                    Number = c.Numer,
                    id = c.ID
                };
                ActivateCompetitionsList.Add(competition);
            }

        }

        private void displayCompetitions()
        {
            saveActivateCompetitions();
            var competitions = from bieg in DatabaseLocator.Database.Bieg
                               select new
                               {
                                   bieg.Nazwa,
                                   bieg.Miejsce,
                                   bieg.Dystans,
                                   bieg.Data,
                                   bieg.ID
                               };
            foreach(var c in competitions)
            {
                var competition = new AttachCompetitionViewModel
                {
                    Name = c.Nazwa,
                    Date = c.Data,
                    Distance = c.Dystans,
                    Place = c.Miejsce,
                    id = c.ID
                };
                if(DatabaseLocator.Database.Zawody.Where(k => k.ID_uzytkownik == DatabaseLocator.user.ID && k.ID_bieg == c.ID).Count() == 0)
                AttachCompetitionsList.Add(competition);
            }
        }
    }
}
