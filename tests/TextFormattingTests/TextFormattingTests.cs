using NUnit.Framework;

using TextFormatting;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void OneSentenceWithoutSpaces()
        {
            var sln = new Solution("one,two,three.");
            var answer = sln.Solve();
            Assert.AreEqual("One, two, three.", answer);
        }

        [Test]
        public void TwoSentences()
        {
            var sln = new Solution("one,two,three.four,five, six.");
            var answer = sln.Solve();
            Assert.AreEqual("One, two, three. Four, five, six.", answer);
        }

        [Test]
        public void ExtraSpaces()
        {
            var sln = new Solution("one , two , three . four , five , six .");
            var answer = sln.Solve();
            Assert.AreEqual("One, two, three. Four, five, six.", answer);
        }

        [Test]
        public void MoreErrors()
        {
            var sln = new Solution("one , TWO  ,,  three  ..  four,fivE , six .");
            var answer = sln.Solve();
            Assert.AreEqual("One, two, three. Four, five, six.", answer);
        }

        [Test]
        public void Shakespeare()
        {
            var sln = new Solution("when a father gives to his son,,, Both laugh; When a son gives to his father, , , Both cry...shakespeare");
            var answer = sln.Solve();
            Assert.AreEqual("When a father gives to his son, both laugh; when a son gives to his father, both cry. Shakespeare", answer);
        }
    }
}