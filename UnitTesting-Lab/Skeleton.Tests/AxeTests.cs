using NUnit.Framework;
using System;

namespace Skeleton.Tests
{
    [TestFixture]
    public class AxeTests
    {
        public void AxeDhouldInitWithCorrectValues()
        {
            //Arrange and Act
            Axe axe = new(100, 100);

            //Assert
            Assert.AreEqual(100, axe.DurabilityPoints);
            Assert.AreEqual(100, axe.AttackPoints);
        }
        [Test]
        public void AttackedMethodShouldDecreaseDurabilityPoints()
        {
            //Arrange
            Dummy target = new(10, 10);
            Axe axe = new(10, 10);

            //Act
            axe.Attack(target);

            //Assert
            Assert.That(axe.DurabilityPoints, Is.EqualTo(9), "Axe Durability doesen't change after attack.");
        }
        [Test]
        public void AttackMethodShouldThorwExeptionIfDurabilityIsZero()
        {
            //Arrange
            Dummy target = new(100, 100);
            Axe axe = new(10, 10);

            //Act
            for (int i = 0; i < 10; i++)
            {
                axe.Attack(target);
            }
            //Assert
            Assert.Throws<InvalidOperationException>(() => axe.Attack(target), "Axe is broken.");
        }
    }
}