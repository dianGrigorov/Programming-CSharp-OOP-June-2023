namespace Animals;

public class Kitten : Animal
{
    private const string KittenGender = "Female";

    public Kitten(string name, int age)
        : base(name, age, KittenGender)
    {
    }
    public override string ProduceSound() => "Meow";
}
