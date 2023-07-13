
using Vehicles.Models.Interfaces;

namespace Vehicles.Models;

public abstract class Vehicle : IVehicle
{
    private double increasedConsumption;
    private double fuelQuantity;
    public Vehicle(double fuelQuantity, double fuelConsumption, double tankCapacity, double increasedConsumption)
    {
        TankCapacity = tankCapacity;
        FuelQuantity = fuelQuantity;
        FuelConsumption = fuelConsumption;
        this.increasedConsumption = increasedConsumption;
    }
    public double FuelQuantity 
    {
        get => fuelQuantity;
        private set
        {
            if (TankCapacity < value)
            {
                fuelQuantity = 0;
            }
            else
            {
                fuelQuantity = value;
            }
        }
    }

    public double FuelConsumption { get; private set; }

    public double TankCapacity { get; private set; }
    public string Drive(double distance, bool isIncreacedConsumption = true)
    {
        double consumption = isIncreacedConsumption
            ?FuelConsumption + increasedConsumption
            :FuelConsumption;

        if (FuelQuantity < distance * consumption)
        {
            throw new ArgumentException($"{GetType().Name} needs refueling");
        }
        FuelQuantity -= distance * consumption;

        return $"{GetType().Name} travelled {distance} km";
    }

    public virtual void Refuel(double amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Fuel must be a positive number");
        }
        if (FuelQuantity + amount > TankCapacity)
        {
            throw new ArgumentException($"Cannot fit {amount} fuel in the tank");
        }

        FuelQuantity += amount;
    }

    public override string ToString()
        => $"{this.GetType().Name}: {FuelQuantity:f2}";
}
