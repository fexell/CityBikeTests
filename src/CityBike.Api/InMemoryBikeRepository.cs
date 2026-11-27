using CityBike.Core;

namespace CityBike.Api;

public class InMemoryBikeRepository : IBikeRepository
{
    private readonly List<Bike> _bikes = new()
    {
        new Bike(1, "Available", 82),
        new Bike(2, "Booked", 64),
        new Bike(3, "Available", 7)
    };

    public Bike? GetById(int id)
    {
        return _bikes.FirstOrDefault(b => b.Id == id);
    }
}
