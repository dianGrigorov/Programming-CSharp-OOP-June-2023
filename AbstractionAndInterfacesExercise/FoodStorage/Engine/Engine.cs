
using BirthdaySelebrations.Models;
using BirthdaySelebrations.Models.Interfaces;
using BorderControl.Engine.Interfaces;
using BorderControl.Models;
using BorderControl.Models.Interfaces;
using FoodStorage.Models;
using FoodStorage.Models.Interfaces;

namespace BorderControl.Engine;

public class Engine : IEngine
{
    public void Start()
    {
        int n = int.Parse(Console.ReadLine());
        List<IBuyer> buyers= new();

        for (int i = 0; i < n; i++)
        {
            string[] tokens = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length == 4)
            {
                buyers.Add(new Citizen(
                    tokens[0], int.Parse(tokens[1]),
                    tokens[2], tokens[3]));
            }
            else
            {
                buyers.Add(new Rebel(tokens[0], int.Parse(tokens[1]), tokens[2]));
            }
        }
        string command;
        while ((command = Console.ReadLine()) !="End")
        {
            buyers.FirstOrDefault(b => b.Name == command)?.BuyFood();
        }
        Console.WriteLine(buyers.Sum(b => b.Food));
    }
}
