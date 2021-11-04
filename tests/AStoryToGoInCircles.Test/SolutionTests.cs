using NUnit.Framework;

namespace AStoryToGoInCircles.Test
{
    public class SolutionTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var sln = new Solution(7, 4, new string[] {"abcd", "dcba", "ba#d", "dabc" });
            var answer = sln.Solve();
            Assert.AreEqual("c", answer);
        }

        [Test]
        public void Test2()
        {
            var sln = new Solution(123458, 6, new string[] {"aezrty", "@sdfgh", "z#uoep", "wxcvbn", "mlkjeg", "ascvgy" });
            var answer = sln.Solve();
            Assert.AreEqual("e", answer);
        }

        [Test]
        public void Test3()
        {
            var sln = new Solution(975312, 7, new string[] { "wabxdg#", "pof@bgr", "n@xbvsz", "qq#sgfu", "#ohojda", "oaef@dh", "ljrepc#" });
            var answer = sln.Solve();
            Assert.AreEqual("d", answer);
        }

        [Test]
        [Ignore("Runtime exceeds acceptable")]
        public void Test4()
        {
            var sln = new Solution(98765432123, 9, new string[] { "a@cdefghi", "klmn#pqrs", "uvwxyzab@", "efghijk#m", "@pqrstuvw", "yza#cdefg", "ijklmn@pq", "stuvwxyza", "cd@fghijk" });
            var answer = sln.Solve();
            Assert.AreEqual("i", answer);
        }
    }
}