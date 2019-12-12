using NUnit.Framework;

namespace HungerGames
{
    public class HungerGamesTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            HungerGame game = new HungerGame(new string[] {"Bowser", "Mario" }, new string[] {"Mario killed Bowser" });
            string answer = game.Solve();
            Assert.AreEqual("Name: Bowser" + System.Environment.NewLine +
                            "Killed: None" + System.Environment.NewLine +
                            "Killer: Mario" + System.Environment.NewLine +
                            "" + System.Environment.NewLine +
                            "Name: Mario" + System.Environment.NewLine +
                            "Killed: Bowser" + System.Environment.NewLine +
                            "Killer: Winner", answer);
        }
    }
}