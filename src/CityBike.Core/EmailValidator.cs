namespace CityBike.Core;

public class EmailValidator
{
    public bool IsValid(string? email)
    {
        if (string.IsNullOrWhiteSpace(email) || email.Contains(' '))
        {
            return false;
        }

        int at = email.IndexOf('@');
        if (at <= 0 || at != email.LastIndexOf('@'))
        {
            return false;
        }

        string domain = email[(at + 1)..];

        return domain.Contains('.')
            && !domain.StartsWith('.')
            && !domain.EndsWith('.');
    }
}
