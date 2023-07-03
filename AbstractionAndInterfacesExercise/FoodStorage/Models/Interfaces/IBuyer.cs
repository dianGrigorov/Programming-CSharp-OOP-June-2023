using BirthdaySelebrations.Models.Interfaces;

namespace FoodStorage.Models.Interfaces;

public interface IBuyer : INameable
{
    int Food { get; }
    void BuyFood();
}
