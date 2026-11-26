namespace CityBike.Core;

public class PriceCalculator
{
    public const decimal UnlockFee = 10m;
    public const decimal PricePerStartedMinute = 2.50m;
    public const decimal MaxPrice = 250m;

    public decimal Calculate(int seconds)
    {
        if (seconds < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(seconds), "Tiden får inte vara negativ.");
        }

        int startedMinutes = (seconds + 59) / 60;
        decimal price = UnlockFee + startedMinutes * PricePerStartedMinute;

        return Math.Min(price, MaxPrice);
    }
}
