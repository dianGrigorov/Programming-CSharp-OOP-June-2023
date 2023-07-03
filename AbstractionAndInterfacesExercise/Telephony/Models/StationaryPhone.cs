using Telephony.Models.Interfaces;

namespace Telephony.Models;

public class StationaryPhone : ICallable
{
    public string Calling(string phoneNumber)
    {
        if (!ValidNumber(phoneNumber))
        {
            throw new ArgumentException("Invalid number!");
        }
            return $"Dialing... {phoneNumber}";
    }

    bool ValidNumber(string phoneNumber)
        => phoneNumber.All(n => char.IsDigit(n));
}
