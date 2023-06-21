using System;

namespace NeedForSpeed
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            SportCar mc = new(100, 100);
            mc.Drive(10);
            Console.WriteLine(mc.Fuel);
        }
    }
}
