using CarApp.Core.Models;
using CarApp.Core.Repositories;


namespace CarApp.ConsoleApp
{

    internal class Program
    {
        static void Main(string[] args)
        {
            // =========================================================================
            // SKIFT REPOSITORY HERVED AT UD-/IND-KOMMENTERE ÉN AF LINJERNE HERUNDER:
            // =========================================================================
            // ICarRepository repo = new InMemoryCarRepository();
            ICarRepository repo = new FileCarRepository("cars.txt");
            // =========================================================================

            Console.WriteLine("--- Opretter og tilføjer testbiler ---");
            try
            {
                // Opretter biler (Brug de rigtige parametre til dine constructorer)
                FuelCar toyota = new FuelCar("Toyota", "Corolla", 2022, "AB12345", 15000, 50, 45, 18, 45000);
                ElectricCar tesla = new ElectricCar("Tesla", "Model 3", 2023, "CD67890", 8000, 75, 70, 6.5, 380000);

                // Tilføj kun hvis de ikke findes i forvejen (vigtigt hvis FileRepository køres flere gange)
                if (repo.GetByLicensePlate("AB12345") == null) repo.Add(toyota);
                if (repo.GetByLicensePlate("CD67890") == null) repo.Add(tesla);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Info/Fejl ved tilføjelse: {ex.Message}");
            }

            Console.WriteLine("\n--- Henter alle biler fra repository ---");
            foreach (Car car in repo.GetAll())
            {
                Console.WriteLine($"{car.Brand} {car.Model} — Nummerplade: {car.LicensePlate}");
            }

            Console.WriteLine("\n--- Søger efter specifik bil (AB12345) ---");
            Car found = repo.GetByLicensePlate("AB12345");
            if (found != null)
            {
                Console.WriteLine($"Fundet: {found.Brand} {found.Model}");

                // Tester en Update (Ændrer kilometertæller)
                Console.WriteLine("Opdaterer kilometertæller på den fundne bil...");
                found.Odometer = 20000;
                repo.Update(found);
            }
            else
            {
                Console.WriteLine("Bilen blev ikke fundet.");
            }

            Console.WriteLine("\n--- Sletter bil (AB12345) for at teste Delete ---");
            repo.Delete("AB12345");
            Console.WriteLine($"Antal biler tilbage i repo: {repo.GetAll().Count()}");

            
        }
    }
}
