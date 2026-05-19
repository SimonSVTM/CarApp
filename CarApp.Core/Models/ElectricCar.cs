using System;
using System.Collections.Generic;
using System.Text;

namespace CarApp.Core.Models
{
    public class ElectricCar : Car
    {
        public double BatteryCapacity { get; private set; }
        public double BatteryLevel { get; private set; }
        public double KmPerKwh { get; private set; }

        public ElectricCar(string brand, string model, int year, string licensePlate, double odometer, double price, double batteryCapacity, double kmPerKwh)
            : base(brand, model, year, licensePlate, odometer, price)
        {
            BatteryCapacity = batteryCapacity;
            KmPerKwh = kmPerKwh;
            BatteryLevel = batteryCapacity; // Starter fuldt opladet
        }

        public override void UpdateEnergyLevel(double km)
        {
            BatteryLevel -= km / KmPerKwh;
            if (BatteryLevel < 0) BatteryLevel = 0;
        }

        public void Charge(double kwh)
        {
            BatteryLevel = Math.Min(BatteryLevel + kwh, BatteryCapacity);
        }

        public override string ToString()
        {
            return $"ElectricCar,{base.ToString()},{BatteryCapacity},{BatteryLevel},{KmPerKwh}";
        }

        public static ElectricCar FromString(string data)
        {
            string[] parts = data.Split(',');
            ElectricCar car = new ElectricCar(
                parts[1],               // Brand
                parts[2],               // Model
                int.Parse(parts[3]),    // Year
                parts[4],               // LicensePlate
                double.Parse(parts[5]), // Odometer
                double.Parse(parts[6]), // Price
                double.Parse(parts[7]), // BatteryCapacity
                double.Parse(parts[9])  // KmPerKwh (parts[8] er BatteryLevel)
            );
            car.Charge(double.Parse(parts[8]) - car.BatteryLevel); // Sætter det gemte batteriniveau
            return car;
        }
    }
}
