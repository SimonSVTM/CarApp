using System;
using System.Collections.Generic;
using System.Text;

namespace CarApp.Core.Repositories
{
    using CarApp.Core.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class FileCarRepository : ICarRepository
    {
        public string FilePath { get; set; }

        public FileCarRepository(string filePath)
        {
            FilePath = filePath;
            // Sikrer at filen eksisterer, så StreamReader ikke kaster en fejl
            if (!File.Exists(FilePath))
            {
                File.Create(FilePath).Dispose(); // Create åbner en stream, Dispose lukker den igen med det samme
            }
        }

        public IEnumerable<Car> GetAll()
        {
            List<Car> cars = new List<Car>();

            using (StreamReader reader = new StreamReader(FilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    string[] parts = line.Split(',');
                    string type = parts[0]; // Tjekker første felt for at bestemme typen (Tip fra øvelsen)

                    if (type == "FuelCar")
                    {
                        cars.Add(FuelCar.FromString(line));
                    }
                    else if (type == "ElectricCar")
                    {
                        cars.Add(ElectricCar.FromString(line));
                    }
                }
            }

            return cars;
        }

        public Car GetByLicensePlate(string licensePlate)
        {
            return GetAll().FirstOrDefault(c => c.LicensePlate.Equals(licensePlate, StringComparison.OrdinalIgnoreCase));
        }

        public void Add(Car car)
        {
            // Tjek om bilen allerede findes i filen
            if (GetByLicensePlate(car.LicensePlate) != null)
            {
                throw new ArgumentException($"En bil med nummerplade {car.LicensePlate} eksisterer allerede i filen.");
            }

            // Skriver car.ToString() i bunden af filen (append: true)
            using (StreamWriter writer = new StreamWriter(FilePath, append: true))
            {
                writer.WriteLine(car.ToString());
            }
        }

        public void Update(Car car)
        {
            // Indlæs alle eksisterende biler i hukommelsen
            List<Car> cars = GetAll().ToList();
            Car existing = cars.FirstOrDefault(c => c.LicensePlate.Equals(car.LicensePlate, StringComparison.OrdinalIgnoreCase));

            if (existing != null)
            {
                int index = cars.IndexOf(existing);
                cars[index] = car; // Erstat med den opdaterede bil

                // Skriv hele listen tilbage til filen (overskriver den gamle fil)
                WriteAllToFile(cars);
            }
        }

        public void Delete(string licensePlate)
        {
            List<Car> cars = GetAll().ToList();
            Car carToRemove = cars.FirstOrDefault(c => c.LicensePlate.Equals(licensePlate, StringComparison.OrdinalIgnoreCase));

            if (carToRemove != null)
            {
                cars.Remove(carToRemove);
                // Skriv listen tilbage uden den slettede bil
                WriteAllToFile(cars);
            }
        }

        // Hjælpemetode til at genoverskrive filen ved Update og Delete
        private void WriteAllToFile(List<Car> cars)
        {
            using (StreamWriter writer = new StreamWriter(FilePath, append: false)) // append: false overskriver filen
            {
                foreach (Car car in cars)
                {
                    writer.WriteLine(car.ToString());
                }
            }
        }
    }
}
