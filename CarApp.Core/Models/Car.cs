using System;
using System.Collections.Generic;
using System.Text;


namespace CarApp.Core.Models
{

    using System;

    public abstract class Car
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string LicensePlate { get; set; } // Unikt ID brugt til CRUD
        public double Odometer { get; set; }

        protected Car(string brand, string model, int year, string licensePlate, double odometer)
        {
            Brand = brand;
            Model = model;
            Year = year;
            LicensePlate = licensePlate;
            Odometer = odometer;
        }

        public override string ToString()
        {
            return $"{Brand},{Model},{Year},{LicensePlate},{Odometer}";
        }

        public abstract void UpdateEnergyLevel(double km);
    }
}
