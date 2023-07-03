
using Telephony.Core.Interfaces;
using Telephony.Models.Interfaces;
using Telephony.Models;

namespace Telephony.Core;

public class Engine : IEngine
{
    public void Start()
    {
        string[] phoneNumbers = Console.ReadLine().Split();
        string[] urls = Console.ReadLine().Split();

        ICallable callable;
        foreach (string phoneNumber in phoneNumbers)
        {
            if (phoneNumber.Length == 7)
            {
                callable = new StationaryPhone();
            }
            else
            {
                callable = new Smatrphone();
            }
            try
            {
                Console.WriteLine(callable.Calling(phoneNumber));
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        IBrowsable browsable = new Smatrphone();

        foreach (string url in urls)
        {
            try
            {
                Console.WriteLine(browsable.Browse(url));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
