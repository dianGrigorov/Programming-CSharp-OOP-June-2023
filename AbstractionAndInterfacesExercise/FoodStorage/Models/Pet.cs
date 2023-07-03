using BirthdaySelebrations.Models.Interfaces;

namespace BirthdaySelebrations.Models;

public class Pet : INameable, IBirthable
{
    public Pet(string name, string birthdate)
    {
        Name = name;
        Birthdate = birthdate;
    }

    public string Name { get; private set; }
    public string Birthdate { get; private set; }
}
