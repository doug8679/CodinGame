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
            var sln = new Solution("01.01.2016", "01.08.2016");
            var answer = sln.Solve();
            Assert.AreEqual("7 months, total 213 days", answer);
        }
        [Test]
        public void YearsAndMonths()
        {
            var sln = new Solution("01.11.2015", "01.02.2017");
            var answer = sln.Solve();
            Assert.AreEqual("1 year, 3 months, total 458 days", answer);
        }
        [Test]
        public void DaysOnly()
        {
            var sln = new Solution("17.12.2016", "16.01.2017");
            var answer = sln.Solve();
            Assert.AreEqual("total 30 days", answer);
        }
        [Test]
        public void SameDate()
        {
            var sln = new Solution("01.01.2016", "01.01.2016");
            var answer = sln.Solve();
            Assert.AreEqual("total 0 days", answer);
        }
        [Test]
        public void Complex()
        {
            var sln = new Solution("28.02.2015", "13.04.2018");
            var answer = sln.Solve();
            Assert.AreEqual("3 years, 1 month, total 1140 days", answer);
        }
        [Test]
        public void OneMonth()
        {
            var sln = new Solution("28.01.2015", "28.02.2015");
            var answer = sln.Solve();
            Assert.AreEqual("1 month, total 31 days", answer);
        }
        [Test]
        public void OneYear()
        {
            var sln = new Solution("17.03.2022", "17.03.2023");
            var answer = sln.Solve();
            Assert.AreEqual("1 year, total 365 days", answer);
        }
        [Test]
        public void LeapYear()
        {
            var sln = new Solution("17.02.2024", "17.02.2025");
            var answer = sln.Solve();
            Assert.AreEqual("1 year, total 366 days", answer);
        }
    }
}