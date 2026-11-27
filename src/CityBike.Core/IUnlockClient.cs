namespace CityBike.Core;

public interface IUnlockClient
{
    bool Unlock(int bikeId);
}
