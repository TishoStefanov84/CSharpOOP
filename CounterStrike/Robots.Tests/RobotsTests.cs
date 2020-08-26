namespace Robots.Tests
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;

    [TestFixture]
    public class RobotsTests
    {
        private const int capacity = 10;
        private const string robotName = "Robot";
        private const int battery = 10;

        private Robot robot;
        private RobotManager robotManager;
        [SetUp]
        public void SetUp()
        {
            this.robot = new Robot(robotName, battery);
            this.robotManager = new RobotManager(capacity);
        }
        [Test]
        public void TestRobotConstructorWorksCorrectly()
        {
            var name = "Robot";
            var maximumBattery = 10;

            var robot = new Robot(name, maximumBattery);

            var expectedName = name;
            var expectedMaxBat = maximumBattery;
            var expectedBat = maximumBattery;

            var actualName = robot.Name;
            var actualMaxBat = robot.MaximumBattery;
            var actualBat = robot.Battery;

            Assert.AreEqual(expectedName, actualName);
            Assert.AreEqual(expectedMaxBat, actualMaxBat);
            Assert.AreEqual(expectedBat, actualBat);
        }
        [Test]
        public void TestRobotManagerConstructorWorksCorrectly()
        {
            var capacity = 10;
            var robotManager = new RobotManager(capacity);

            var expectedCapacity = capacity;
            var expectedRobotManagerCount = 0;

            var actualCapacity = robotManager.Capacity;
            var actualRobotManagerCount = robotManager.Count;

            Assert.AreEqual(expectedCapacity, actualCapacity);
            Assert.AreEqual(expectedRobotManagerCount, actualRobotManagerCount);
        }
        [Test]
        public void CapacityShouldThrowExceptionWithNegativeNumber()
        {
            var capacity = -10;

            Assert.Throws<ArgumentException>(() =>
            {
                var robotManager = new RobotManager(capacity);
            });
        }
        [Test]
        public void AddShouldAddingRobotInCollection()
        {
            this.robotManager.Add(this.robot);

            var expectedCount = 1;
            var actualCount = robotManager.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }
        [Test]
        public void AddShouldThrowExceptionWhenRobotNameExist()
        {
            var secondRobot = new Robot(robotName, 20);
            this.robotManager.Add(this.robot);

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.robotManager.Add(secondRobot);
            });
        }
        [Test]
        public void AddShouldThrowExceptionWhenCountIsEqualToCapacity()
        {
            var secondRobot = new Robot("New robot", 12);
            var robotManager = new RobotManager(1);

            robotManager.Add(this.robot);

            Assert.Throws<InvalidOperationException>(() =>
            {
                robotManager.Add(secondRobot);
            });
        }
        [Test]
        public void RemoveShouldThrowExceptionWhenRemoveNotExistingRobotByName()
        {
            this.robotManager.Add(this.robot);

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.robotManager.Remove("Test");
            });
        }
        [Test]
        public void RemoveShouldRemoveRobotFromCollection()
        {
            this.robotManager.Add(this.robot);

            this.robotManager.Remove(this.robot.Name);

            var expectedCount = 0;
            var actualCount = this.robotManager.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }
        [Test]
        public void WorkShouldThrowExceptionWhenRobotByNameNotExist()
        {
            this.robotManager.Add(this.robot);

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.robotManager.Work("Test", "Work", 5);
            });
        }
        [Test]
        public void WorkShouldThrowExceptionWhenRobotBatteryIsLow()
        {
            this.robotManager.Add(this.robot);

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.robotManager.Work(robotName, "Work", 15);
            });
        }
        [Test]
        public void WorkShouldDecreasRobotBaterry()
        {
            this.robotManager.Add(this.robot);

            this.robotManager.Work(robotName, "Work", 5);

            var expectedBattery = 5;
            var actualBattery = this.robot.Battery;

            Assert.AreEqual(expectedBattery, actualBattery);
        }
        [Test]
        public void ChargeSouldThrowExceptionWhenRobotByNameNotExist()
        {
            this.robotManager.Add(this.robot);

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.robotManager.Charge("Test");
            });
        }
        [Test]
        public void ChargeShouldFullRestoreRobotBattery()
        {
            this.robotManager.Add(this.robot);
            this.robotManager.Work(robotName, "Work", 5);
            this.robotManager.Charge(robotName);

            var expectedBattery = this.robot.MaximumBattery;
            var actualBattery = this.robot.Battery;

            Assert.AreEqual(expectedBattery, actualBattery);
        }
    }
}
