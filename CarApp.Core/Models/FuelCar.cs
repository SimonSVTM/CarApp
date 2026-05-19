using System;
using System.Collections.Generic;
using System.Text;

namespace CarApp.Core.Models
{
    public class FuelCar : Car
    {
        public double TankCapacity { get; set; }
        public double FuelLevel { get; set; }
        public double KmPerLiter { get; set; }
        public double Price { get; set; }

        public FuelCar(string brand, string model, int year, string licensePlate, double odometer,
                       double tankCapacity, double fuelLevel, double kmPerLiter, double price)
            : base(brand, model, year, licensePlate, odometer)
        {
            TankCapacity = tankCapacity;
            FuelLevel = fuelLevel;
            KmPerLiter = kmPerLiter;
            Price = price;
        }

        public override void UpdateEnergyLevel(double km)
        {
            double usedFuel = km / KmPerLiter;
            FuelLevel = Math.Max(0, FuelLevel - usedFuel);
        }

        public override string ToString()
        {
            return $"FuelCar,{base.ToString()},{TankCapacity},{FuelLevel},{KmPerLiter},{Price}";
        }

        public static FuelCar FromString(string data)
        {
            string[] parts = data.Split(',');
            // parts[0] er typen "FuelCar"
            return new FuelCar(
                parts[1],                  // Brand
                parts[2],                  // Model
                int.Parse(parts[3]),       // Year
                parts[4],                  // LicensePlate
                double.Parse(parts[5]),    // Odometer
                double.Parse(parts[6]),    // TankCapacity
                double.Parse(parts[7]),    // FuelLevel
                double.Parse(parts[8]),    // KmPerLiter
                double.Parse(parts[9])     // Price
            );
        }
    }
}
