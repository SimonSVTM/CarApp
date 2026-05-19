
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
            return !(SelectedCar == null 
                || SelectedCar.LicensePlate.Equals("")
                || SelectedCar.Brand.Equals("")
                || SelectedCar.Model.Equals(""));
            // TODO: Returner true hvis SelectedCar ikke er null og 

            //       LicensePlate, Brand og Model ikke er tomme 

            

        }



        private void AddCar()

        {
            _repository.Add(SelectedCar);
            Cars.Add(SelectedCar);
            SelectedCar = new FuelCar("", "", DateTime.Now.Year, "", 40, 10, 0);
            // TODO: Tilføj SelectedCar til _repository og til Cars-listen 

            // TODO: Nulstil SelectedCar til en ny tom FuelCar 

        }



        private void FindCar()

        {
            Car foundCar = _repository.GetByLicensePlate(SearchPlate);
            if (foundCar != null)
            {
                SelectedCar = foundCar;
                SearchPlate = "";
            }
            else
                MessageBox.Show("Bil ikke fundet");

            // TODO: Brug _repository.GetByLicensePlate(SearchPlate) 

                // TODO: Hvis fundet: sæt SelectedCar = fundet bil, ryd SearchPlate 

                // TODO: Hvis ikke fundet: vis MessageBox.Show("Bil ikke fundet") 

        }



        private bool CanUpdateOrDelete()

        {
            return SelectedCar.LicensePlate != "";
            // TODO: Returner true hvis SelectedCar har en ikke-tom LicensePlate 

        }



        // ── Disse to metoder får I i Øvelse 6 ────────────── 


        private void UpdateCar()

        {

            _repository.Update(SelectedCar);

            RefreshCarList();

        }



        private void DeleteCar()

        {

            var result = MessageBox.Show(

                $"Vil du slette {SelectedCar.Brand} {SelectedCar.Model}?",

                "Bekræft sletning",

                MessageBoxButton.YesNo,

                MessageBoxImage.Warning);



            if (result == MessageBoxResult.Yes)

            {

                _repository.Delete(SelectedCar.LicensePlate);

                Cars.Remove(SelectedCar);

                SelectedCar = new FuelCar("", "", DateTime.Now.Year, "", 40, 10, 0);

            }

        }





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



