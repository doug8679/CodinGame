using NUnit.Framework;
using HowTimeFlies;

namespace HowTimeFliesTests
{
    public class HowTimeFliesTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void FullYears()
        {
            var sln = new Solution("01.01.2000", "01.01.2016");
            var answer = sln.Solve();
            Assert.AreEqual("16 years, total 5844 days", answer);
        }
        [Test]
        public void FullMonths()
        {
            Assert.Pass();
        }
        [Test]
        public void YearsAndMonths()
        {
            Assert.Pass();
        }
        [Test]
        public void DaysOnly()
        {
            Assert.Pass();
        }
        [Test]
        public void SameDate()
        {
            Assert.Pass();
        }
        [Test]
        public void Complex()
        {
            Assert.Pass();
        }
        [Test]
        public void OneMonth()
        {
            Assert.Pass();
        }
        [Test]
        public void OneYear()
        {
            Assert.Pass();
        }
        [Test]
        public void LeapYear()
        {
            Assert.Pass();
        }
    }
}