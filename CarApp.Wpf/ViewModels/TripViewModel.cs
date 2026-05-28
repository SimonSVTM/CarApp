using CarApp.Core.Models;
using CarApp.Core.Repositories;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.ConstrainedExecution;
using System.Windows;
using System.Windows.Input;

namespace CarApp.Wpf.ViewModels

{

    public class TripViewModel : INotifyPropertyChanged

    {

        private readonly ITripRepository _tripRepository;

        private readonly ICarRepository _carRepository;


        // Listen af ture der vises i UI

        public ObservableCollection<Trip> Trips { get; set; }


        // Biler brugeren kan vælge til en ny tur

        public ObservableCollection<Car> AvailableCars { get; set; }


        // Den bil brugeren har valgt i dropdownlisten

        


        // Den tur brugeren har valgt i listen

        private Trip _selectedTrip;

        public Trip SelectedTrip

        {

            get => _selectedTrip;

            set
            {
                _selectedTrip = value;

                OnPropertyChanged(nameof(SelectedTrip));

                

            }

        }



        public ICommand AddTripCommand { get; }

        public ICommand DeleteTripCommand { get; }


        public TripViewModel(ITripRepository tripRepository, ICarRepository carRepository)

        {

            _tripRepository = tripRepository;

            _carRepository = carRepository;

            SelectedTrip = new Trip(null, DateTime.Now, DateTime.Now, 0);

            Trips = new ObservableCollection<Trip>(_tripRepository.GetAll());
            AvailableCars = new ObservableCollection<Car>(_carRepository.GetAll());


            AddTripCommand = new RelayCommand(_ => AddTrip(), _ => CanAddTrip());

            DeleteTripCommand = new RelayCommand(_ => DeleteTrip(), _ => CanDeleteTrip());

        }

        private bool CanDeleteTrip()
        {
            return _tripRepository.GetById(SelectedTrip.Id) != null;
        }

        public void updateCars()
        {
            AvailableCars = new ObservableCollection<Car>(_carRepository.GetAll());
        }
 
        private bool CanAddTrip()
        {
            // Returnerer true hvis SelectedCar ikke er null og Distance er over 0
            return SelectedTrip.Car != null && SelectedTrip.Distance > 0;
        }

        private void AddTrip()
        {
            
            // Antager at Trip-modellen har disse properties baseret på din TODO
            

            // 1. Tilføj til datalaget (repository opdaterer Trip.Id automatisk)
            _tripRepository.Add(SelectedTrip);

            // 2. Tilføj til den ObservableCollection som UI'et kigger på
            Trips.Add(SelectedTrip);

            // 3. Nulstil Distance til 0 (det trigger også OnPropertyChanged og opdaterer UI samt knap-status)
            SelectedTrip = new Trip(null, DateTime.Now, DateTime.Now, 0);
        }

        private void DeleteTrip()
        {

            var result = MessageBox.Show(

                $"Vil du slette {SelectedTrip.Car.Brand} {SelectedTrip.Distance}?",

                "Bekræft sletning",

                MessageBoxButton.YesNo,

                MessageBoxImage.Warning);



            if (result == MessageBoxResult.Yes)

            {
                
                _tripRepository.Delete(SelectedTrip.Id);
                Trips.Remove(SelectedTrip);


                SelectedTrip = new Trip(null, DateTime.Now, DateTime.Now, 0);

            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name) =>

        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    }

}