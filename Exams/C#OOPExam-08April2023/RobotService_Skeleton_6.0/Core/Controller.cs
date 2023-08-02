using RobotService.Core.Contracts;
using RobotService.Models;
using RobotService.Models.Contracts;
using RobotService.Repositories;
using RobotService.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Core;

public class Controller : IController
{
    private SupplementRepository supplements;
    private RobotRepository robots;

    public Controller()
    {
        supplements = new SupplementRepository();
        robots = new RobotRepository();
    }

    public string CreateRobot(string model, string typeName)
    {
        if (typeName == nameof(DomesticAssistant))
        {
            robots.AddNew(new DomesticAssistant(model));
        }
        else if (typeName == nameof(IndustrialAssistant))
        {
            robots.AddNew(new IndustrialAssistant(model));
        }
        else
        {
            return string.Format(OutputMessages.RobotCannotBeCreated,typeName);
        }

        return string.Format(OutputMessages.RobotCreatedSuccessfully, typeName, model);
    }

    public string CreateSupplement(string typeName)
    {
        if (typeName == nameof(LaserRadar))
        {
            supplements.AddNew(new LaserRadar());
        }
        else if (typeName == nameof(SpecializedArm))
        {
            supplements.AddNew(new SpecializedArm());
        }
        else
        {
            return string.Format(OutputMessages.SupplementCannotBeCreated, typeName);
        }

        return string.Format(OutputMessages.SupplementCreatedSuccessfully, typeName);
    }

    public string UpgradeRobot(string model, string supplementTypeName)
    {
        ISupplement supplement = supplements
            .Models()
            .FirstOrDefault(s => s.GetType().Name == supplementTypeName);
        IRobot robot = robots
            .Models()
            .FirstOrDefault(r => r.Model == model && !r.InterfaceStandards.Contains(supplement.InterfaceStandard));

        if (robot is null)
        {
            return string.Format(OutputMessages.AllModelsUpgraded, model);
        }

        robot.InstallSupplement(supplement);
        supplements.RemoveByName(supplementTypeName);
        return string.Format(OutputMessages.UpgradeSuccessful,model, supplementTypeName);
    }

    public string PerformService(string serviceName, int intefaceStandard, int totalPowerNeeded)
    {
        IEnumerable<IRobot> filteredRobors = robots
            .Models()
            .Where(r => r.InterfaceStandards.Contains(intefaceStandard))
            .OrderByDescending(r => r.BatteryLevel);
        if (!filteredRobors.Any())
        {
            return string.Format(OutputMessages.UnableToPerform, intefaceStandard);
        }

        int availablePower = filteredRobors.Sum(r => r.BatteryLevel);

        if (availablePower < totalPowerNeeded)
        {
            return string.Format(OutputMessages.MorePowerNeeded, serviceName, totalPowerNeeded - availablePower);
        }

        int countRobots = 0;

        foreach (IRobot robot in filteredRobors)
        {
            countRobots++;

            if (robot.BatteryLevel >= totalPowerNeeded)
            {
                robot.ExecuteService(totalPowerNeeded);
                break;
            }

            totalPowerNeeded -= robot.BatteryLevel;
            robot.ExecuteService(robot.BatteryLevel);
        }
        return string.Format(OutputMessages.PerformedSuccessfully, serviceName, countRobots);
    }

    public string RobotRecovery(string model, int minutes)
    {
        IEnumerable<IRobot> filteredRobots = robots
            .Models()
            .Where(r => r.Model == model && r.BatteryCapacity / 2 > r.BatteryLevel);

        int robotsCount = 0;

        foreach (IRobot robot in filteredRobots)
        {
            robotsCount++;
            robot.Eating(minutes);
        }

        return string.Format(OutputMessages.RobotsFed, robotsCount);
    }

    public string Report()
    {

        IEnumerable<IRobot> orderedRobots = robots
            .Models()
            .OrderByDescending(r => r.BatteryLevel)
            .ThenBy(r => r.BatteryCapacity);
        StringBuilder sb = new StringBuilder();

        foreach(IRobot robot in orderedRobots)
        {
            sb.AppendLine(robot.ToString());
        }

        return sb.ToString().TrimEnd();
    }


    
}
