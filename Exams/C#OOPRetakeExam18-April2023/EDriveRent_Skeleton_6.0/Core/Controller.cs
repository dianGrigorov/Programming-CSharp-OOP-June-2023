using EDriveRent.Core.Contracts;
using EDriveRent.Models;
using EDriveRent.Models.Contracts;
using EDriveRent.Repositories;
using EDriveRent.Repositories.Contracts;
using EDriveRent.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Core;

public class Controller : IController
{

    private readonly IRepository<IUser> users;
    private readonly IRepository<IVehicle> vehicles;
    private readonly IRepository<IRoute> routes;

    public Controller()
    {
        users = new UserRepository();
        vehicles = new VehicleRepository();
        routes = new RouteRepository();
    }
    public string AllowRoute(string startPoint, string endPoint, double length)
    {
        var route = routes.GetAll()
            .FirstOrDefault(r => r.StartPoint == startPoint && r.EndPoint == endPoint && r.Length == length);
        if (route != null)
        {
            return string.Format(OutputMessages.RouteExisting, startPoint, endPoint, length);
        }
        route = routes.GetAll()
            .FirstOrDefault(r => r.StartPoint == startPoint && r.EndPoint == endPoint && r.Length < length);
        if (route != null)
        {
            return string.Format(OutputMessages.RouteIsTooLong, startPoint, endPoint);
        }
        route = routes.GetAll()
            .FirstOrDefault(r => r.StartPoint == startPoint && r.EndPoint == endPoint && r.Length > length);

        if (route != null)
        {
            route.LockRoute();
        }
        int routId = routes.GetAll().Count();
        Route newRoute = new Route(startPoint,endPoint, length, routId + 1);
        routes.AddModel(newRoute);
        return string.Format(OutputMessages.NewRouteAdded, startPoint, endPoint, length);
    }

    public string MakeTrip(string drivingLicenseNumber, string licensePlateNumber, string routeId, bool isAccidentHappened)
    {
        var user = users.FindById(drivingLicenseNumber);
        var vehicle = vehicles.FindById(licensePlateNumber);
        var rout = routes.FindById(routeId);

        if (user.IsBlocked)
        {
            return string.Format(OutputMessages.UserBlocked, drivingLicenseNumber);
        }
        if (vehicle.IsDamaged)
        {
            return string.Format(OutputMessages.VehicleDamaged, licensePlateNumber);
        }
        if (rout.IsLocked)
        {
            return string.Format(OutputMessages.RouteLocked,routeId);
        }
        vehicle.Drive(rout.Length);
        if (isAccidentHappened)
        {
            vehicle.ChangeStatus();
            user.DecreaseRating();
        }
        else
        {
            user.IncreaseRating();
        }
        return vehicle.ToString();
    }

    public string RegisterUser(string firstName, string lastName, string drivingLicenseNumber)
    {
        IUser user = users.FindById(drivingLicenseNumber);
        if (user != null)
        {
            return string.Format(OutputMessages.UserWithSameLicenseAlreadyAdded, drivingLicenseNumber);
        }

        user = new User(firstName, lastName, drivingLicenseNumber);
        users.AddModel(user);

        return string.Format(OutputMessages.UserSuccessfullyAdded, firstName, lastName, drivingLicenseNumber);
    }

    public string RepairVehicles(int count)
    {
       var vehiclesToRepair = vehicles
            .GetAll()
            .Where(x => x.IsDamaged)
            .OrderBy(v => v.Brand)
            .ThenBy(v => v.Model)
            .Take(count)
            .ToList();

        foreach (var vehicle in vehiclesToRepair)
        {
            vehicle.Recharge();
            vehicle.ChangeStatus();
        }
        return string.Format(OutputMessages.RepairedVehicles, vehiclesToRepair.Count);
    }

    public string UploadVehicle(string vehicleType, string brand, string model, string licensePlateNumber)
    {
        if (vehicleType != "PassengerCar" && vehicleType != "CargoVan")
        {
            return string.Format(OutputMessages.VehicleTypeNotAccessible, vehicleType);
        }

        IVehicle vehicle = vehicles.FindById(licensePlateNumber);

        if (vehicle != null)
        {
            return string.Format(OutputMessages.LicensePlateExists,licensePlateNumber);
        }
        if (vehicleType == "CargoVan")
        {
            vehicle = new CargoVan(brand, model, licensePlateNumber);
        }
        if (vehicleType == "PassengerCar")
        {
            vehicle = new PassengerCar(brand, model, licensePlateNumber);
        }

        vehicles.AddModel(vehicle);
        return string.Format(OutputMessages.VehicleAddedSuccessfully,brand, model, licensePlateNumber);

    }

    public string UsersReport()
    {
         StringBuilder stringBuilder = new StringBuilder();

        var allUsers = users.GetAll()
            .OrderByDescending(u => u.Rating)
            .ThenBy(u => u.LastName)
            .ThenBy(u => u.FirstName)
            .ToList();

        stringBuilder.AppendLine("*** E-Drive-Rent ***");
        foreach (var user in allUsers)
        {
            stringBuilder.AppendLine(user.ToString());
        }
        return stringBuilder.ToString().TrimEnd();
    }
}
