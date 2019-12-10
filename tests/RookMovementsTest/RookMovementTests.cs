using NUnit.Framework;

using RookMovements;

using System;

namespace Tests
{
    public class Tests
    {
        private static string MOVING_FREELY = "Rd5-a5" + Environment.NewLine +
                                             "Rd5-b5" + Environment.NewLine +
                                             "Rd5-c5" + Environment.NewLine +
                                             "Rd5-d1" + Environment.NewLine +
                                             "Rd5-d2" + Environment.NewLine +
                                             "Rd5-d3" + Environment.NewLine +
                                             "Rd5-d4" + Environment.NewLine +
                                             "Rd5-d6" + Environment.NewLine +
                                             "Rd5-d7" + Environment.NewLine +
                                             "Rd5-d8" + Environment.NewLine +
                                             "Rd5-e5" + Environment.NewLine +
                                             "Rd5-f5" + Environment.NewLine +
                                             "Rd5-g5" + Environment.NewLine +
                                             "Rd5-h5";

        private static string CLOSE_TO_THE_EDGE = "Ra8-a5" + Environment.NewLine +
                                                  "Ra8-a6" + Environment.NewLine +
                                                  "Ra8-a7" + Environment.NewLine +
                                                  "Ra8-b8" + Environment.NewLine +
                                                  "Ra8-c8" + Environment.NewLine +
                                                  "Ra8-d8";
        private static string ONLY_ALLIES = "Rd5-a5" + Environment.NewLine +
                                            "Rd5-b5" + Environment.NewLine +
                                            "Rd5-c5" + Environment.NewLine +
                                            "Rd5-d3" + Environment.NewLine +
                                            "Rd5-d4" + Environment.NewLine +
                                            "Rd5-d6" + Environment.NewLine +
                                            "Rd5-d7" + Environment.NewLine +
                                            "Rd5-d8" + Environment.NewLine +
                                            "Rd5-e5" + Environment.NewLine +
                                            "Rd5-f5";
        private static string FOR_FRODOOO = "Rd5-a5" + Environment.NewLine +
                                            "Rd5-b5" + Environment.NewLine +
                                            "Rd5-c5" + Environment.NewLine +
                                            "Rd5-d3" + Environment.NewLine +
                                            "Rd5-d4" + Environment.NewLine +
                                            "Rd5-d6" + Environment.NewLine +
                                            "Rd5-e5" + Environment.NewLine +
                                            "Rd5-f5" + Environment.NewLine +
                                            "Rd5xd7";
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void MovingFreely()
        {
            var sln = new Solution("d5", 2, new string[]{"0 c1", "1 e8"});
            Assert.AreEqual(MOVING_FREELY, sln.Solve());
        }

        [Test]
        public void CloseToTheEdge() {
            var sln = new Solution("a8", 5, new string[] {"0 e8", "1 d7", "0 c6", "1 b5", "0 a4"});
            Assert.AreEqual(CLOSE_TO_THE_EDGE, sln.Solve());
        }

        [Test]
        public void OnlyAllies() {
            var sln = new Solution("d5", 2, new string[]{"0 g5", "0 d2"});
            Assert.AreEqual(ONLY_ALLIES, sln.Solve());
        }

        [Test]
        public void ForFrodooo() {
            var sln = new Solution("d5", 3, new string[]{"0 g5", "0 d2", "1 d7"});
            Assert.AreEqual(FOR_FRODOOO, sln.Solve());
        }
    }
}