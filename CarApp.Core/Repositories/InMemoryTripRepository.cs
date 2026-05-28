using CarApp.Core.Models;
using CarApp.Core.Repositories;

public class InMemoryTripRepository : ITripRepository
{
    private readonly List<Trip> _trips = new List<Trip>();
    private int _nextId = 1;

    public IEnumerable<Trip> GetAll()
    {
        // Returnerer en kopi af listen som IEnumerable for at beskytte den interne struktur
        return _trips;
    }

    public Trip GetById(int id)
    {
        // Finder den første tur med matchene ID, eller returnerer null
        return _trips.FirstOrDefault(t => t.Id == id);
    }

    public void Add(Trip trip)
    {
        if (trip == null)
        {
            throw new ArgumentNullException(nameof(trip), "Trip cannot be null");
        }

        // Tildeler det næste ledige ID og tæller tælleren op
        trip.Id = _nextId++;
        _trips.Add(trip);
    }

    public void Delete(int id)
    {
        // Finder turen først
        var tripToDelete = GetById(id);

        if (tripToDelete != null)
        {
            _trips.Remove(tripToDelete);
        }
    }

    public void DeleteByTrip(Trip t)
    {
        
        if (t != null)
        {
            _trips.Remove(t);
        }
    }

    public IEnumerable<Trip> GetByCarLicensePlate(string licensePlate)
    {
        // Håndterer hvis licensePlate er null for at undgå fejl i streng-sammenligningen
        if (string.IsNullOrEmpty(licensePlate))
        {
            return Enumerable.Empty<Trip>();
        }

        // Returnerer alle ture, der matcher nummerpladen (ignorerer store/små bogstaver)
        return _trips.Where(t => t.Car.LicensePlate != null &&
                                 t.Car.LicensePlate.Equals(licensePlate, StringComparison.OrdinalIgnoreCase));
    }
}