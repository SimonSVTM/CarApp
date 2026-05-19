using System;
using System.Collections.Generic;
using System.Text;

namespace CarApp.Core.Models
{
    public class Trip
    {
        private double distance;
        private DateTime tripDate, startTime, endTime;
        private Car car;
        public Trip(Car car, double distance, DateTime startTime, DateTime endTime)
        {
            this.car = car;
            this.distance = distance;
            this.tripDate = startTime.Date;
            this.startTime = startTime;
            this.endTime = endTime;
        }

        public double getDistance() => distance;
        public Car getCar() => car;
        public DateTime getDate() => tripDate;
        public DateTime getStartTime() => startTime;

        public TimeSpan calculateDuration()
        {
            return endTime - startTime;
        }

        public double calculateFuelUsed()
        {
            if (car is FuelCar fuelCar)
            {
                // car er en FuelCar, og vi kan nu bruge variablen 'fuelCar'
                return distance / fuelCar.KmPerLiter;
            }
            else if (car is ElectricCar electricCar)
            {
                // car er en ElectricCar, og vi kan nu bruge variablen 'electricCar'
                return distance / electricCar.KmPerKwh;
            }

            // Hvis der mod forventning kommer en ukendt biltype ind
            throw new InvalidOperationException("Ukendt biltype. Kan ikke beregne energiforbrug.");
        }

        public double calculateTripPrice(double literPrice)
        {
            double tripCost = calculateFuelUsed() * literPrice;
            return double.Round(tripCost, 2);
        }

        public string GetTripDetails()
        {
            return $"Trip for {distance} km on {tripDate.ToString()} took {calculateDuration().ToString()}";
        }


    }
}
