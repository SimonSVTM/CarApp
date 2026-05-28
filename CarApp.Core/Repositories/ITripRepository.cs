using System.Collections.Generic;

using CarApp.Core.Models;


namespace CarApp.Core.Repositories
{ 
    public interface ITripRepository

    {

        IEnumerable<Trip> GetAll();

        Trip GetById(int id);

        void Add(Trip trip);

        void Delete(int id);

    }

}