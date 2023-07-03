
using BorderControl.Engine.Interfaces;
using BorderControl.Models;
using BorderControl.Models.Interfaces;

namespace BorderControl.Engine;

public class Engine : IEngine
{
    public void Start()
    {
        string input;
        List<IIdentifiable> citizensRobots = new();
        while ((input = Console.ReadLine()) != "End")
        {
            string[] tokens = input.Split();

            if (tokens.Length == 3)
            {
                citizensRobots.Add(new Citizen(tokens[0], int.Parse(tokens[1]),tokens[2]));
            }
            else
            {
                citizensRobots.Add(new Robot(tokens[0], tokens[1]));
            }
        }
        string chekId = Console.ReadLine();

        foreach (var item in citizensRobots)
        {
            if (item.Id.EndsWith(chekId))
            {
                Console.WriteLine(item.Id);
            }
        }
    }
}
