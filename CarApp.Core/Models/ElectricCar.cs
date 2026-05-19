using System;
using System.Collections.Generic;
using System.Text;

namespace CarApp.Core.Models
{
    public class ElectricCar : Car
    {
        public double BatteryCapacity { get; set; }
        public double BatteryLevel { get; set; }
        public double KmPerKwh { get; set; }
        public double Price { get; set; }

        public ElectricCar(string brand, string model, int year, string licensePlate, double odometer,
                           double batteryCapacity, double batteryLevel, double kmPerKwh, double price)
            : base(brand, model, year, licensePlate, odometer)
        {
            BatteryCapacity = batteryCapacity;
            BatteryLevel = batteryLevel;
            KmPerKwh = kmPerKwh;
            Price = price;
        }

        public override void UpdateEnergyLevel(double km)
        {
            double usedKwh = km / KmPerKwh;
            BatteryLevel = Math.Max(0, BatteryLevel - usedKwh);
        }

        public override string ToString()
        {
            return $"ElectricCar,{base.ToString()},{BatteryCapacity},{BatteryLevel},{KmPerKwh},{Price}";
        }

        public static ElectricCar FromString(string data)
        {
            string[] parts = data.Split(',');
            // parts[0] er typen "ElectricCar"
            return new ElectricCar(
                parts[1],                  // Brand
                parts[2],                  // Model
                int.Parse(parts[3]),       // Year
                parts[4],                  // LicensePlate
                double.Parse(parts[5]),    // Odometer
                double.Parse(parts[6]),    // BatteryCapacity
                double.Parse(parts[7]),    // BatteryLevel
                double.Parse(parts[8]),    // KmPerKwh
                double.Parse(parts[9])     // Price
            );
        }
    }
}
