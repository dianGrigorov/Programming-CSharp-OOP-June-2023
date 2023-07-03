
using BirthdaySelebrations.Models;
using BirthdaySelebrations.Models.Interfaces;
using BorderControl.Engine.Interfaces;
using BorderControl.Models;
using BorderControl.Models.Interfaces;

namespace BorderControl.Engine;

public class Engine : IEngine
{
    public void Start()
    {
        string input;
        List<IBirthable> citizensPets = new();
        while ((input = Console.ReadLine()) != "End")
        {
            string[] tokens = input.Split();
            string type = tokens[0];

            switch (type)
            {
                case "Citizen":
                    citizensPets.Add(new Citizen(tokens[1], int.Parse(tokens[2]), tokens[3], tokens[4]));
                    break;
                case "Pet":
                    citizensPets.Add(new Pet(tokens[1], tokens[2]));
                    break;
            }

        }
        string chekBirthdate = Console.ReadLine();

        foreach (var element in citizensPets)
        {
            if (element.Birthdate.EndsWith(chekBirthdate))
            {
                Console.WriteLine(element.Birthdate);
            }
        }
    }
}
