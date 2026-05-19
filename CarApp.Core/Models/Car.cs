using System;
using System.Collections.Generic;
using System.Text;


namespace CarApp.Core.Models
{

    public abstract class Car
    {
        public string Brand { get; private set; }
        public string Model { get; private set; }
        public int Year { get; private set; }
        public string LicensePlate { get; private set; }
        public double Odometer { get; protected set; }
        public bool IsEngineOn { get; private set; }
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
