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
            // Sikrer at filen eksisterer inden læsning/skrivning
            if (!File.Exists(FilePath))
            {
                File.Create(FilePath).Dispose();
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

                    string type = line.Split(',')[0]; // Tjekker første felt for biltype

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
            if (GetByLicensePlate(car.LicensePlate) != null)
                throw new ArgumentException("Bilen findes allerede i filen.");

            using (StreamWriter writer = new StreamWriter(FilePath, append: true))
            {
                writer.WriteLine(car.ToString());
            }
        }

        public void Update(Car car)
        {
            List<Car> cars = GetAll().ToList();
            Car existing = cars.FirstOrDefault(c => c.LicensePlate.Equals(car.LicensePlate, StringComparison.OrdinalIgnoreCase));

            if (existing != null)
            {
                int index = cars.IndexOf(existing);
                cars[index] = car;
                SaveAll(cars);
            }
        }

        public void Delete(string licensePlate)
        {
            List<Car> cars = GetAll().ToList();
            Car carToRemove = cars.FirstOrDefault(c => c.LicensePlate.Equals(licensePlate, StringComparison.OrdinalIgnoreCase));

            if (carToRemove != null)
            {

                cars.Remove(carToRemove);
                SaveAll(cars);
            }
        }

        private void SaveAll(List<Car> cars)
        {
            using (StreamWriter writer = new StreamWriter(FilePath, append: false)) // Overskriver hele filen med den nye liste
            {
                foreach (Car car in cars)
                {
                    writer.WriteLine(car.ToString());
                }
            }
        }
    }
}
