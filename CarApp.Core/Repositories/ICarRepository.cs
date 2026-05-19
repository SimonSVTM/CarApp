using CarApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarApp.Core.Repositories
{
    public interface ICarRepository
    {
        IEnumerable<Car> GetAll();
        Car GetByLicensePlate(string licensePlate);
        void Add(Car car);
        void Update(Car car);
        void Delete(string licensePlate);
    }
}
