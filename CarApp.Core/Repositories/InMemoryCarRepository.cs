using CarApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarApp.Core.Repositories
{
    public class InMemoryCarRepository : ICarRepository
    {
        private List<Car> _cars = new List<Car>();

        public IEnumerable<Car> GetAll()
        {
            return _cars;
        }

        public Car GetByLicensePlate(string licensePlate)
        {
            return _cars.FirstOrDefault(c => c.LicensePlate.Equals(licensePlate, StringComparison.OrdinalIgnoreCase));
        }

        public void Add(Car car)
        {
            if (GetByLicensePlate(car.LicensePlate) != null)
                throw new ArgumentException("En bil med denne nummerplade eksisterer allerede.");

            _cars.Add(car);
        }

        public void Update(Car car)
        {
            Car existing = GetByLicensePlate(car.LicensePlate);
            if (existing != null)
            {
                int index = _cars.IndexOf(existing);
                _cars[index] = car;
            }
        }

        public void Delete(string licensePlate)
        {
            Car car = GetByLicensePlate(licensePlate);
            if (car != null)
            {
                _cars.Remove(car);
            }
        }
    }
}
