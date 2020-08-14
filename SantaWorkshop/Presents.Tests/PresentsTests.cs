namespace Presents.Tests
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;

    [TestFixture]
    public class PresentsTests
    {
        private const string presentName = "Present";
        private const double presentMagic = 10;

        private Present present;
        private Bag bag;
        [SetUp]
        public void SetUp()
        {
            this.present = new Present(presentName, presentMagic);
            this.bag = new Bag();
        }
        [Test]
        public void TestPresentConstructorWorksCorrectly()
        {
            var name = "Present";
            var magic = 10.0;

            var present = new Present(name, magic);

            Assert.AreEqual(name, present.Name);
            Assert.AreEqual(magic, present.Magic);
        }

        [Test]
        public void TestBagConstructorWorksCorrectly()
        {
            var bag = new Bag();

            Assert.IsNotNull(bag.GetPresents());
        }
        [Test]
        public void CreateShouldThrowExceptionWithNullPresent()
        {
            Present present = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                this.bag.Create(present);
            });
        }
        [Test]
        public void CreateShouldThrowExceptionIfPresentExist()
        {
            this.bag.Create(this.present);

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.bag.Create(this.present);
            });
        }
        [Test]
        public void CreateShouldAddedPresentInBag()
        {
            this.bag.Create(this.present);

            IReadOnlyCollection<Present> expected = new List<Present> { this.present };
            IReadOnlyCollection<Present> actual = this.bag.GetPresents();

            CollectionAssert.AreEqual(expected, actual);
        }
        [Test]
        public void RemoveShouldReturnTrueIfPresentRemoved()
        {
            this.bag.Create(this.present);

            var result = this.bag.Remove(this.present);

            Assert.IsTrue(result);
        }
        [Test]
        public void RemoveShouldRemovePresentFromCollection()
        {
            var secondPresent = new Present("Test Present", 5);

            this.bag.Create(this.present);
            this.bag.Create(secondPresent);

            this.bag.Remove(secondPresent);

            IReadOnlyCollection<Present> expected = new List<Present> { this.present };
            IReadOnlyCollection<Present> actual = this.bag.GetPresents();

            CollectionAssert.AreEqual(expected, actual);
        }
        [Test]
        public void TestGetPresentWithLeastMagicReturnedCorrectPresent()
        {
            var secondPresent = new Present("Test Present", 15);

            this.bag.Create(secondPresent);
            this.bag.Create(this.present);

            var expectedPresent = this.present;
            var actualPresent = this.bag.GetPresentWithLeastMagic();

            Assert.AreEqual(expectedPresent, actualPresent);
        }
        [Test]
        public void GetPresentShouldReturnPresentByName()
        {
            this.bag.Create(this.present);

            var expectedPresent = this.present;
            var actualPresent = this.bag.GetPresent(this.present.Name);

            Assert.AreEqual(expectedPresent, actualPresent);
        }
    }
}
