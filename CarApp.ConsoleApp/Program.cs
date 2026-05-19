using CarApp.Core.Models;
using CarApp.Core.Repositories;


namespace CarApp.ConsoleApp
{

    internal class Program
    {
        static void Main(string[] args)
        {
            // SKIFT REPOSITORY HERVED AT UD-/IND-KOMMENTERE:
            // ICarRepository repo = new InMemoryCarRepository();
            ICarRepository repo = new FileCarRepository("cars.txt");

            Console.WriteLine("--- Tilføjer biler (hvis ikke de findes) ---");
            if (repo.GetByLicensePlate("AB12345") == null)
            {
                repo.Add(new FuelCar("Toyota", "Corolla", 2022, "AB12345", 12000, 45000, 50, 18));
            }
            if (repo.GetByLicensePlate("CD67890") == null)
            {
                repo.Add(new ElectricCar("Tesla", "Model 3", 2023, "CD67890", 8000, 380000, 75, 6.5));
            }

            Console.WriteLine("\n--- Udskriver alle biler registreret i systemet ---");
            foreach (Car car in repo.GetAll())
            {
                Console.WriteLine($"- {car.Brand} {car.Model} [{car.LicensePlate}] | KM-Tæller: {car.Odometer}");
            }

            Console.WriteLine("\n--- Tester en køretur og opdatering ---");
            Car myTesla = repo.GetByLicensePlate("CD67890");
            if (myTesla != null)
            {
                myTesla.TurnOnEngine();
                Trip commute = new Trip(myTesla, 65, DateTime.Now, DateTime.Now.AddHours(1));
                myTesla.Drive(commute);

                repo.Update(myTesla); // Gemmer den nye kilometerstand
                Console.WriteLine($"Turen fuldført. Ny kilometerstand: {myTesla.Odometer} km");
                Console.WriteLine(commute.GetTripDetails());
            }

           
        }
    }
}
