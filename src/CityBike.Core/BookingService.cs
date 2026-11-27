namespace CityBike.Core;

public class BookingService
{
    public const int MinBatteryPercent = 10;

    private readonly IBikeRepository _bikes;
    private readonly IUnlockClient _unlockClient;

    public BookingService(IBikeRepository bikes, IUnlockClient unlockClient)
    {
        _bikes = bikes;
        _unlockClient = unlockClient;
    }

    public BookingResult Book(int bikeId)
    {
        Bike? bike = _bikes.GetById(bikeId);

        if (bike is null)
        {
            return BookingResult.BikeNotFound;
        }

        if (bike.Status != "Available")
        {
            return BookingResult.BikeNotAvailable;
        }

        if (bike.BatteryPercent < MinBatteryPercent)
        {
            return BookingResult.BatteryTooLow;
        }

        bool unlocked = _unlockClient.Unlock(bikeId);

        return unlocked ? BookingResult.Unlocked : BookingResult.UnlockFailed;
    }
}
