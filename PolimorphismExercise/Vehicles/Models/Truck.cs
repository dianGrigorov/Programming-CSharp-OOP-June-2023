namespace Vehicles.Models;

public class Truck : Vehicle
{
    const double IncreasedConsumption = 1.6;
    public Truck(double fuelQuantity, double fuelConsumption, double tankCapacity)
        : base(fuelQuantity, fuelConsumption, tankCapacity, IncreasedConsumption)
    {
    }
}
