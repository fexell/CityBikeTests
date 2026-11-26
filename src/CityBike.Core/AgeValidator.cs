namespace CityBike.Core;

public class AgeValidator
{
    public const int AdultAge = 18;

    public bool IsAdult(DateOnly birthDate, DateOnly today)
    {
        int age = today.Year - birthDate.Year;

        return age >= AdultAge;
    }
}
