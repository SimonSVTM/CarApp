
// CarApp.Wpf/ViewModels/CarViewModel.cs 

using System;

using System.Collections.ObjectModel;

using System.ComponentModel;

using System.Windows;

using System.Windows.Input;

using CarApp.Core.Models;

using CarApp.Core.Repositories;



namespace CarApp.Wpf.ViewModels

{

    public class CarViewModel : INotifyPropertyChanged

    {

        private readonly ICarRepository _repository;



        public ObservableCollection<Car> Cars { get; set; }



        private Car _selectedCar;

        public Car SelectedCar

        {

            get => _selectedCar;

            set

            {

                _selectedCar = value;

                OnPropertyChanged(nameof(SelectedCar));

                (UpdateCarCommand as RelayCommand)?.RaiseCanExecuteChanged();

                (DeleteCarCommand as RelayCommand)?.RaiseCanExecuteChanged();

            }

        }



        private string _searchPlate;

        public string SearchPlate

        {

            get => _searchPlate;

            set { _searchPlate = value; OnPropertyChanged(nameof(SearchPlate)); }

        }



        public ICommand AddCarCommand { get; }

        public ICommand FindCarCommand { get; }

        public ICommand UpdateCarCommand { get; }

        public ICommand DeleteCarCommand { get; }



        public CarViewModel(ICarRepository repository)

        {

            _repository = repository;

            Cars = new ObservableCollection<Car>(_repository.GetAll());

            SelectedCar = new FuelCar("", "", DateTime.Now.Year, "", 40, 10, 0);



            AddCarCommand = new RelayCommand(_ => AddCar(), _ => CanAddCar());

            FindCarCommand = new RelayCommand(_ => FindCar(), _ => !string.IsNullOrWhiteSpace(SearchPlate));

            UpdateCarCommand = new RelayCommand(_ => UpdateCar(), _ => CanUpdateOrDelete());

            DeleteCarCommand = new RelayCommand(_ => DeleteCar(), _ => CanUpdateOrDelete());

        }



        // ── I skal implementere disse fire metoder ──────────── 



        private bool CanAddCar()

        {

            // TODO: Returner true hvis SelectedCar ikke er null og 

            //       LicensePlate, Brand og Model ikke er tomme 

            return false; // midlertidig 

        }



        private void AddCar()

        {

            // TODO: Tilføj SelectedCar til _repository og til Cars-listen 

            // TODO: Nulstil SelectedCar til en ny tom FuelCar 

        }



        private void FindCar()

        {

            // TODO: Brug _repository.GetByLicensePlate(SearchPlate) 

            // TODO: Hvis fundet: sæt SelectedCar = fundet bil, ryd SearchPlate 

            // TODO: Hvis ikke fundet: vis MessageBox.Show("Bil ikke fundet") 

        }



        private bool CanUpdateOrDelete()

        {

            // TODO: Returner true hvis SelectedCar har en ikke-tom LicensePlate 

            return false; // midlertidig 

        }



        // ── Disse to metoder får I i Øvelse 6 ────────────── 

        private void UpdateCar() { /* udleveres i Øvelse 6 */ }

        private void DeleteCar() { /* udleveres i Øvelse 6 */ }



        private void RefreshCarList()

        {

            Cars.Clear();

            foreach (var car in _repository.GetAll())

                Cars.Add(car);

        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name) =>

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    }

}



