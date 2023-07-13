using NUnit.Framework;
using System;
using System.Runtime.InteropServices;

namespace Skeleton.Tests
{
    [TestFixture]
    public class DummyTests
    {
        [Test]
        public void ConstructorShouldInitilizeCorrectly()
        {
            //Arrange and Act
            Dummy dummy = new(100, 100);
            //Assert
            Assert.AreEqual(100, dummy.Health);
        }
        [Test]
        public void TakeAttackShouldDecreaseHelth()
        {
            //Arrange
            Dummy dummy = new(100, 100);
            //Act
            dummy.TakeAttack(50);
            //Assert
            Assert.AreEqual(50, dummy.Health);

        }
        [Test]
        public void TakeAttackShouldTrowExeptionIfDummyIsDead()
        {
            //Arrange
            Dummy dummy = new(100, 100);
            //Act
            dummy.TakeAttack(50);
            dummy.TakeAttack(50);
            //Assert
            Assert.Throws<InvalidOperationException>(
                () => dummy.TakeAttack(50), "Dummy is dead.");

        }
        [Test]
        public void GiveExpShouldReturnExperianceIfDummyIsDead()
        {
            //Arrange
            Dummy dummy = new(100, 100);

            //Act
            dummy.TakeAttack(50);
            dummy.TakeAttack(50);

            //Assert
            Assert.AreEqual(100, dummy.GiveExperience());
        }
        [Test]
        public void GiveExpShouldThrowExceptionIfDummyIsNotDead()
        {
            //Arrange
            Dummy dummy = new(100, 100);

            //Act
            dummy.TakeAttack(50);

            //Assert
            Assert.Throws<InvalidOperationException>(
                () => dummy.GiveExperience(), "Target is not dead.");
        }
        [Test]
        public void IsDeadShouldCheckIfHealthIsLowOrEqualToZero()
        {
            //Arrange
            Dummy dummy = new(50, 100);

            //Act
            dummy.TakeAttack(50);

            //Assert
            Assert.IsTrue(dummy.IsDead());

        }
    }
}