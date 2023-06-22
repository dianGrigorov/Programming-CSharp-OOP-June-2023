using System;

namespace Restaurant
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            Cake cake = new("Cake");
            Console.WriteLine(cake.Price);
        }
    }
}