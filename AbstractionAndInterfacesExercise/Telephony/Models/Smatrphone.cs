using Telephony.Models.Interfaces;

namespace Telephony.Models;

public class Smatrphone : IBrowsable, ICallable
{
    public string Browse(string url)
    {
        if (!ValidUrl(url))
        {
            throw new ArgumentException("Invalid URL!");
        }
            return $"Browsing: {url}!";
    }

    public string Calling(string phoneNumber)
    {
        if (!ValidPhoneNumber(phoneNumber))
        {
            throw new ArgumentException("Invalid number!");
        }
        
            return $"Calling... {phoneNumber}";
    }

    private bool ValidUrl(string url)
         => url.All(u => !char.IsDigit(u));

    private bool ValidPhoneNumber(string phoneNumber)
     => phoneNumber.All(u => char.IsDigit(u));
}
