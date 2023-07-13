using Vehicles.Core;
using Vehicles.Core.Interfaces;
using Vehicles.Factories;
using Vehicles.Factories.Interfaces;

IVehicleFactory vehicleFactory = new VehicleFactory();
IEngine eng = new Engine(vehicleFactory);
eng.Run();