using BlowingFuse;

using NUnit.Framework;

using System;

namespace Tests
{
    public class BlowingFuseTests
    {
        private const string BLOWN_FUSE = "Fuse was blown.";
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Blown()
        {
            var sln = new Solution("5 2 10", "11 6 11 10 10", "3 3");
            var answer = sln.Solve();
            Assert.AreEqual(BLOWN_FUSE, answer);
        }
        [Test]
        public void NotBlown()
        {
            var sln = new Solution("5 8 82", "18 20 3 1 20", "2 4 3 3 5 4 2 3");
            var answer = sln.Solve();
            Assert.AreEqual("Fuse was not blown." + Environment.NewLine + "Maximal consumed current was 41 A.", answer);
        }
        [Test]
        public void SingleDevice()
        {
            var sln = new Solution("1 10 1", "9", "1 1 1 1 1 1 1 1 1 1");
            var answer = sln.Solve();
            Assert.AreEqual(BLOWN_FUSE, answer);
        }
        [Test]
        public void MoreDevices()
        {
            var sln = new Solution("6 24 71", "10 10 14 14 14 15", "4 3 3 5 4 1 5 5 5 4 1 5 5 4 2 3 3 3 1 6 2 1 5 5");
            var answer = sln.Solve();
            Assert.AreEqual("Fuse was not blown." + Environment.NewLine + "Maximal consumed current was 49 A.", answer);
        }
        [Test]
        public void MoreClicksMoreDevices()
        {
            var sln = new Solution("11 20 72", "11 10 13 19 15 9 20 10 16 12 5", "6 8 3 4 8 6 10 3 6 5 2 4 10 2 6 6 4 2 4 5");
            var answer = sln.Solve();
            Assert.AreEqual("Fuse was not blown." + Environment.NewLine + "Maximal consumed current was 65 A.", answer);
        }
        [Test]
        public void PowerHungry()
        {
            var sln = new Solution("20 20 200", "3 12 5 8 15 12 12 10 11 16 10 19 17 15 11 9 17 6 14 5", "1 3 5 7 5 9 2 4 6 8 10 11 13 15 2 17 12 14 16 18");
            var answer = sln.Solve();
            Assert.AreEqual("Fuse was not blown." + Environment.NewLine + "Maximal consumed current was 181 A.", answer);
        }
    }
}