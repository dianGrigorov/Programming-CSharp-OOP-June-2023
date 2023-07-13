
namespace Vehicles.Models.Interfaces;

public interface IVehicle
{
    double FuelQuantity { get; }
    double FuelConsumption { get; }
   
    string Drive(double distance, bool isIncreacedConsumption = true);
    void Refuel(double amount);

}
