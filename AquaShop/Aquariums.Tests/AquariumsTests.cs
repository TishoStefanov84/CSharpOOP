namespace Aquariums.Tests
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class AquariumsTests
    {
        [Test]
        public void TestConstruktorWorksCorrectly()
        {
            var name = "Aquarium";
            var capacity = 10;

            var aquarium = new Aquarium(name, capacity);

            Assert.AreEqual(name, aquarium.Name);
            Assert.AreEqual(capacity, aquarium.Capacity);
        }
        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void AquariumNameShouldThrowExceptionIfIsNullOrEmpty(string name)
        {
            var capacity = 10;

            Assert.Throws<ArgumentNullException>(() =>
            {
                var aquarium = new Aquarium(name, capacity);
            });
        }
        [Test]
        public void AquariumCapacityShouldThrowExceptionWithNegativeNumber()
        {
            var name = "Aquarium";
            var capacity = -10;

            Assert.Throws<ArgumentException>(() =>
            {
                var aquarium = new Aquarium(name, capacity);
            });
        }
        [Test]
        public void TestCountWorksCorrectly()
        {
            var fish = new Fish("Fish");
            var aquarium = new Aquarium("Aquarium", 10);

            aquarium.Add(fish);

            var expectedCount = 1;
            var actualCount = aquarium.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }
        [Test]
        public void AddShouldThrowExceptionWhenCapacityIsEqualToCount()
        {
            var aquarium = new Aquarium("Aquarium", 2);
            var fish = new Fish("Fish1");
            var secondFish = new Fish("Fish2");

            aquarium.Add(fish);
            aquarium.Add(secondFish);

            Assert.Throws<InvalidOperationException>(() =>
            {
                aquarium.Add(new Fish("Fish"));
            });
        }
        [Test]
        public void RemoveShouldRemoveFishFromCollection()
        {
            var aquarium = new Aquarium("Aquarium", 10);
            var fish = new Fish("Fish1");
            var secondFish = new Fish("Fish2");

            aquarium.Add(fish);
            aquarium.Add(secondFish);

            aquarium.RemoveFish(fish.Name);

            var expectedCount = 1;
            var actualCount = aquarium.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }
        [Test]
        public void RemoveShouldThrowExceptionWhenFishIsNotFound()
        {
            var aquarium = new Aquarium("Aquarium", 10);
            var fish = new Fish("Fish");

            aquarium.Add(fish);

            Assert.Throws<InvalidOperationException>(() =>
            {
                aquarium.RemoveFish("TestFish");
            });
        }
        [Test]
        public void SellFishShouldChangeFishAvailableToFalse()
        {
            var aquarium = new Aquarium("Aquarium", 10);
            var fish = new Fish("Fish");

            aquarium.Add(fish);
            aquarium.SellFish(fish.Name);

            Assert.IsFalse(fish.Available);
            
        }
        [Test]
        public void SellFishShouldThrowExceptionWhenFishIsNotFound()
        {
            var aquarium = new Aquarium("Aquarium", 10);
            var fish = new Fish("Fish");

            aquarium.Add(fish);

            Assert.Throws<InvalidOperationException>(() =>
            {
                aquarium.SellFish("TestFish");
            });
        }
        [Test]
        public void TestReportReturnCorrectInfo()
        {
            var aquarium = new Aquarium("Aquarium", 10);
            var fish = new Fish("Fish");

            aquarium.Add(fish);

            var expectedMessage = $"Fish available at {aquarium.Name}: {fish.Name}";
            var actualMessage = aquarium.Report();

            Assert.AreEqual(expectedMessage, actualMessage);
        }
    }
}
