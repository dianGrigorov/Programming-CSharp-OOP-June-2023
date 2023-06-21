using System;

namespace PlayersAndMonsters
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            Wizard wz = new("dido", 21);
            Console.WriteLine(wz);
        }
    }
}