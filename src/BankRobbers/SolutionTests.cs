using System.Collections.Generic;
using NUnit.Framework;

namespace BankRobbers
{
    public class SolutionTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void OneRobberOneEasyVault()
        {
            var sln = new Solution(1, 1, new List<int[]> {new int[]{3, 1}});
            Assert.AreEqual("250", sln.Solve());
        }

        [Test]
        public void MoreRobbersMoreVaults() {
            var sln = new Solution(4, 4, new List<int[]> {
                new int[]{3, 2},
                new int[]{4, 1},
                new int[]{7, 6},
                new int[]{7, 1}
            });
            Assert.AreEqual("5000000", sln.Solve());
        }

        [Test]
        public void FewerRobbersThanVaults() {
            var sln = new Solution(2, 4, new List<int[]> {
                new int[]{3, 1},
                new int[]{3, 2},
                new int[]{4, 0},
                new int[]{4, 0}
            });
            Assert.AreEqual("1125", sln.Solve());
        }

        [Test]
        public void BigHeist() {
            var sln = new Solution(5, 20, new List<int[]> {
                new int[]{6, 3},
                new int[]{7, 1},
                new int[]{4, 4},
                new int[]{8, 4},
                new int[]{3, 0},
                new int[]{4, 3},
                new int[]{8, 1},
                new int[]{3, 3},
                new int[]{5, 5},
                new int[]{7, 6},
                new int[]{6, 2},
                new int[]{5, 3},
                new int[]{5, 4},
                new int[]{7, 0},
                new int[]{7, 0},
                new int[]{8, 4},
                new int[]{6, 0},
                new int[]{6, 5},
                new int[]{3, 2},
                new int[]{4, 2}
            });
            Assert.AreEqual("6515625", sln.Solve());
        }
    }
}