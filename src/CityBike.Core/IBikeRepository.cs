namespace CityBike.Core;

public interface IBikeRepository
{
    Bike? GetById(int id);
}
