using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Text;

namespace CarApp.Core.Models
{
    public class Trip
    {
        

        public int Id { get; set; }

        public Car Car { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public double Distance { get; set; }



        public Trip(Car car, DateTime startTime, DateTime endTime, double distance)
        {

            Car = car;

            StartTime = startTime;

            EndTime = endTime;

            Distance = distance;

            Id = -1000;

        }


        // Beregn varighed i minutter

        public double DurationMinutes =>

        (EndTime - StartTime).TotalMinutes;

        

        public TimeSpan CalculateDuration()
        {
            return EndTime - StartTime;
        }

        // Tjekker bilens type og beregner det korrekte energiforbrug
        public double CalculateFuelUsed()
        {
            if (Car is FuelCar fuelCar)
            {
                return Distance / fuelCar.KmPerLiter;
            }
            if (Car is ElectricCar electricCar)
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
            string unit = Car is ElectricCar ? "kWh" : "liter";
            return $"Trip for {Distance} km on {StartTime.ToShortDateString()} took {CalculateDuration()}. Consumed: {CalculateFuelUsed():F2} {unit}.";
        }
    }
}
