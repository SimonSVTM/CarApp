using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Text;

namespace CarApp.Core.Models
{
    public class Trip
    {
        public double Distance { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public DateTime TripDate { get; private set; }
        private Car _car;

        public Trip(Car car, double distance, DateTime startTime, DateTime endTime)
        {
            _car = car;
            Distance = distance;
            StartTime = startTime;
            EndTime = endTime;
            TripDate = startTime.Date;
        }

        public TimeSpan CalculateDuration()
        {
            return EndTime - StartTime;
        }

        // Tjekker bilens type og beregner det korrekte energiforbrug
        public double CalculateFuelUsed()
        {
            if (_car is FuelCar fuelCar)
            {
                return Distance / fuelCar.KmPerLiter;
            }
            if (_car is ElectricCar electricCar)
            {
                return Distance / electricCar.KmPerKwh;
            }
            throw new InvalidOperationException("Ukendt biltype. Kan ikke beregne forbrug.");
        }

        public double CalculateTripPrice(double pricePerUnit)
        {
            double totalCost = CalculateFuelUsed() * pricePerUnit;
            return Math.Round(totalCost, 2);
        }

        public string GetTripDetails()
        {
            string unit = _car is ElectricCar ? "kWh" : "liter";
            return $"Trip for {Distance} km on {TripDate.ToShortDateString()} took {CalculateDuration()}. Consumed: {CalculateFuelUsed():F2} {unit}.";
        }
    }
}
