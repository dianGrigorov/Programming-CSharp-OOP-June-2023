using PizzaCalories.Models;

Dough dough = new("Tip500", "Chewy", 100);

Console.WriteLine($"{dough.Calories:f2}");