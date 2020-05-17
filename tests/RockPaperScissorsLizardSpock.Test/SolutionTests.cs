using NUnit.Framework;

namespace RockPaperScissorsLizardSpock.Test
{
    public class SolutionTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SameAsExample()
        {
            string[] players = new[] {"4 R", "1 P", "8 P", "3 R", "7 C", "5 S", "6 L", "2 L"};
            var sol = new Solution(players);
            Assert.AreEqual("2\n6 5 1", sol.Solve());
        }

        [Test]
        public void WithTwoPlayers()
        {
            string[] players = new[] {"1 S", "2 S"};
            var sol = new Solution(players);
            Assert.AreEqual("1\n2", sol.Solve());
        }
    }
}