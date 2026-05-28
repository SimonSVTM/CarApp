using System.ComponentModel;

using System.Windows.Input;

using CarApp.Core.Repositories;


namespace CarApp.Wpf.ViewModels

{

    public class MainViewModel : INotifyPropertyChanged

    {

        // De to ViewModels — oprettes én gang og genbruges

        public CarViewModel CarVM { get; }

        public TripViewModel TripVM { get; }


        // Den ViewModel der aktuelt vises i ContentControl

        private object _currentViewModel;

        public object CurrentViewModel

        {

            get => _currentViewModel;

            set
            {
                _currentViewModel = value;

                OnPropertyChanged(nameof(CurrentViewModel));
            }

        }


        // Navigationskommandoer

        public ICommand ShowCarsCommand { get; }

        public ICommand ShowTripsCommand { get; }


        public MainViewModel(ICarRepository carRepo, ITripRepository tripRepo)

        {

            CarVM = new CarViewModel(carRepo);

            TripVM = new TripViewModel(tripRepo, carRepo);


            // Start med biler som standardvisning

            CurrentViewModel = CarVM;


            ShowCarsCommand = new RelayCommand(_ => CurrentViewModel = CarVM);

            ShowTripsCommand = new RelayCommand(_ => { CurrentViewModel = TripVM;
                TripVM.updateCars();
            });

        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name) =>

        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    }

}