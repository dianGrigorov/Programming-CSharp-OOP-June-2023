using NUnit.Framework;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace RobotFactory.Tests
{
    public class Tests
    {
        private Factory factory;

        [SetUp]
        public void Setup()
        {
            factory = new("Dido", 10);
        }

        [Test]
        public void ConstructorShouldWorkCorectly()
        {

            string expectedName = "Dido";
            int expectedCapacyti = 10;

            Assert.AreEqual(expectedName, factory.Name);
            Assert.AreEqual(expectedCapacyti, factory.Capacity);
            Assert.NotNull(factory.Robots);
            Assert.NotNull(factory.Supplements);

        }
        [Test]
        public void NameSetterShouldWorkCorectly()
        {
            string expectedName = "Gosho";

            factory.Name = expectedName;

            Assert.AreEqual(expectedName, factory.Name);

        }
        [Test]
        public void CapacitySetterShouldWorkCorectly()
        {
            int expectedCapacity = 5;

            factory.Capacity = expectedCapacity;

            Assert.AreEqual(expectedCapacity, factory.Capacity);

        }

        [Test]
        public void ProduceRobotShouldAddRobotToInnerColection()
        {
            Robot expectedRobot = new("Iron Man", 12.34, 24);

            string expectedMessage = $"Produced --> Robot model: {expectedRobot.Model} IS: {expectedRobot.InterfaceStandard}, Price: {expectedRobot.Price:f2}";

            string actualMessage = factory.ProduceRobot(expectedRobot.Model, expectedRobot.Price, expectedRobot.InterfaceStandard);

            Robot actualRobot = factory.Robots.Single();

            Assert.AreEqual(expectedRobot.Model, actualRobot.Model);
            Assert.AreEqual(expectedRobot.Price, actualRobot.Price);
            Assert.AreEqual(expectedRobot.InterfaceStandard, actualRobot.InterfaceStandard);
            Assert.AreEqual(expectedMessage, actualMessage);
        }
        [Test]
        public void ProduceRobotShouldNotAddRobotIfCapacityLimitIsReached()
        {
            factory.Capacity = 0;

            string expectedMessage = $"The factory is unable to produce more robots for this production day!";

            string actualMessage = factory.ProduceRobot("Robocop", 12.34, 4);

            Assert.AreEqual(expectedMessage, actualMessage);
        }
        [Test]
        public void ProduceSupplementShouldAddSupplementInSupplementsColection()
        {
            Supplement expectedSupplement = new("Arm", 25);

            string expectedResult = $"Supplement: {expectedSupplement.Name} IS: {expectedSupplement.InterfaceStandard}";

            string actualResult = factory.ProduceSupplement(expectedSupplement.Name, expectedSupplement.InterfaceStandard);

            Supplement actualSupplement = factory.Supplements.Single();
            Assert.AreEqual(expectedResult, actualResult);
            Assert.AreEqual(expectedSupplement.Name, actualSupplement.Name);
            Assert.AreEqual(expectedSupplement.InterfaceStandard, actualSupplement.InterfaceStandard);
        }

        [Test]
        public void UpgradeShouldAddSupplementAndReturnTrue()
        {
            Robot expectedRobot = new("Iron Man", 12.34, 25);
            Supplement expectedSupplement = new("Arm", 25);

            bool actualResul = factory.UpgradeRobot(expectedRobot, expectedSupplement);

            Supplement actualSupplement = expectedRobot.Supplements.Single();

            Assert.That(actualResul, Is.True);
            Assert.AreEqual(expectedSupplement.InterfaceStandard, actualSupplement.InterfaceStandard);
        }
        [Test]
        public void UpgradeShouldNotAddSupplementAndReturnFalseWhenSupplementAllredyAdded()
        {
            Robot expectedRobot = new("Iron Man", 12.34, 25);
            Supplement expectedSupplement = new("Arm", 25);

            factory.UpgradeRobot(expectedRobot, expectedSupplement);
            bool expectedResult = factory.UpgradeRobot(expectedRobot, expectedSupplement);

            Assert.False(expectedResult);
            Assert.AreEqual(1, expectedRobot.Supplements.Count);

        }
        [Test]
        public void UpgradeShouldNotAddSupplementAndReturnFalseWhenInterfaceStandartsDoesNotMatch()
        {
            int interfaceStandart = 25;

            Robot Robot = new("Iron Man", 12.34, interfaceStandart);
            Supplement expectedSupplement = new("Arm", interfaceStandart + 1);

            bool expectedResult = factory.UpgradeRobot(Robot, expectedSupplement);

            Assert.False(expectedResult);
            Assert.AreEqual(0, Robot.Supplements.Count);
        }

        [Test]
        public void SellRobotShouldReturnCorrectRobot()
        {
            Robot expectedRobot = new("Iron Man", 700, 25);

            factory.ProduceRobot(expectedRobot.Model, expectedRobot.Price, expectedRobot.InterfaceStandard);
            factory.ProduceRobot("Terminator", 1000, 30);
            factory.ProduceRobot("Robocop", 500, 26);

            Robot actualRobot = factory.SellRobot(800);


            Assert.AreEqual(expectedRobot.Model, actualRobot.Model);
            Assert.AreEqual(expectedRobot.Price, actualRobot.Price);
            Assert.AreEqual(expectedRobot.InterfaceStandard, actualRobot.InterfaceStandard);
        }
        [Test]
        public void SellRobotShouldReturnNullIfPriceIsToLow()
        {
            Robot expectedRobot = new("Iron Man", 700, 25);

            factory.ProduceRobot(expectedRobot.Model, expectedRobot.Price, expectedRobot.InterfaceStandard);
            factory.ProduceRobot("Terminator", 1000, 30);
            factory.ProduceRobot("Robocop", 500, 26);

            Robot actualRobot = factory.SellRobot(200);


            Assert.Null(actualRobot);

        }
        [Test]
        public void SellRobotShouldReturnNullIfRobotColectionIsEmpty()
        {
            Robot actualdRobot = factory.SellRobot(200);


            Assert.Null(actualdRobot);

        }

    }
}