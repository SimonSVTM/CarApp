using System;
using System.Collections.Generic;
using System.Text;

namespace CarApp.Core.Models
{
    public class FuelCar : Car
    {
        public double TankCapacity { get;  set; }
        public double FuelLevel { get;  set; }
        public double KmPerLiter { get;  set; }

        public FuelCar(string brand, string model, int year, string licensePlate, double odometer, double tankCapacity, double kmPerLiter)
            : base(brand, model, year, licensePlate, odometer)
        {
            TankCapacity = tankCapacity;
            KmPerLiter = kmPerLiter;
            FuelLevel = tankCapacity; // Starter fuldt tanket
        }

        public override void UpdateEnergyLevel(double km)
        {
            FuelLevel -= km / KmPerLiter;
            if (FuelLevel < 0) FuelLevel = 0;
        }

        public void Refuel(double liters)
        {
            FuelLevel = Math.Min(FuelLevel + liters, TankCapacity);
        }

        public override string ToString()
        {
            return $"FuelCar,{base.ToString()},{TankCapacity},{FuelLevel},{KmPerLiter}";
        }

        public static FuelCar FromString(string data)
        {
            string[] parts = data.Split(',');
            FuelCar car = new FuelCar(
                parts[1],               // Brand
                parts[2],               // Model
                int.Parse(parts[3]),    // Year
                parts[4],               // LicensePlate
                double.Parse(parts[5]), // Odometer
                double.Parse(parts[6]), // TankCapacity
                double.Parse(parts[8])  // KmPerLiter (parts[7] er FuelLevel, vi genberegner eller overskriver herunder)
            );
            car.Refuel(double.Parse(parts[8]) - car.FuelLevel); // Sætter det gemte brændstofniveau
            return car;
        }
    }
}
