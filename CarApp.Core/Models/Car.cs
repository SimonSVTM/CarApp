using System;
using System.Collections.Generic;
using System.Text;


namespace CarApp.Core.Models
{

    public abstract class Car
    {
        private string _brand;
        public string Brand
        {
            get => _brand;
            set
            {
                
                _brand = value;
            }
        }

        private string _model;
        public string Model
        {
            get => _model;
            set
            {
                
                _model = value;
            }
        }

        private int _year;
        public int Year
        {
            get => _year;
            set
            {
                _year = value;
            }
        }

        private string _licensePlate;
        public string LicensePlate
        {
            get => _licensePlate;
            set
            {
                _licensePlate = value;
            }
        }
        public double Odometer { get;  set; }
        public bool IsEngineOn { get;  set; }
        public double Price { get; set; } // Medtaget fra Opgavesæt 10 DCD

        protected List<Trip> _trips = new List<Trip>();

        protected Car(string brand, string model, int year, string licensePlate, double odometer)
        {
            Brand = brand;
            Model = model;
            Year = year;
            LicensePlate = licensePlate;
            Odometer = odometer;
            Price = 35;
        }

        public abstract void UpdateEnergyLevel(double km);

        public void Drive(Trip trip)
        {
            if (IsEngineOn)
            {
                Odometer += trip.Distance;
                UpdateEnergyLevel(trip.Distance);
                _trips.Add(trip);
            }
            else
            {
                Console.WriteLine("Fejl: Motoren er ikke tændt.");
            }
        }

        public void TurnOnEngine() => IsEngineOn = true;
        public void TurnOffEngine() => IsEngineOn = false;
        public List<Trip> GetTrips() => _trips;

        public override string ToString()
        {
            return $"{Brand},{Model},{Year},{LicensePlate},{Odometer},{Price}";
        }
    }
}
