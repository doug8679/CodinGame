using NUnit.Framework;

using System.Collections.Generic;

namespace HiddenWord.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CanBuildWordGrid() {
            var grid = new WordGrid(1,1, new string[] {"A"});
            Assert.AreEqual(1, grid.Size);
        }

        [Test]
        public void HorizontalWords()
        {
            var grid = new WordGrid(3,3, new string[]
            {
                "BAC", "BOB", "RED"
            });
            var finder = new WordFinder(new List<string>{"BAC","BOB"}, grid);
            Assert.AreEqual("RED", finder.Find());
        }

        [Test]
        public void VerticalWords()
        {
            var grid = new WordGrid(4, 4, new string[]
            { "GTRI", "LMAL", "UAGO", "ECML"});
            var finder = new WordFinder(new List<string> { "GLUE", "RAG", "LOL", "MAC" }, grid);
            Assert.AreEqual("TIM", finder.Find());
        }

        [Test]
        public void AllDirectionsSmall()
        {
            var grid = new WordGrid(3, 3, new string[] {"BAC", "EOO", "CKB"});
            var finder = new WordFinder(new List<string> { "BAC", "BOB", "BEC", "COC" }, grid);
            Assert.AreEqual("OK", finder.Find());
        }

        [Test]
        public void AllDirectionsBig()
        {
            var grid = new WordGrid(12, 12, new string[]
            {
                "VCENJUGUENTO",
                "OCORCSEERRAN",
                "SOCHASSIGNET",
                "SDRAHCUAFDND",
                "AENMALMENEES",
                "BSETINMUIDEM",
                "VOLLEYBALLTG",
                "COLATURESAUM",
                "ISEMAEGROGER",
                "SSURSITESERE",
                "TEGNITRIFIAT",
                "ENILECOUSAIS"
            });
            var finder = new WordFinder(new List<string>
            {
                "BASSOV",
                "CELINE",
                "CHASSIGNET",
                "CISTE",
                "COLATURES",
                "COUSAIS",
                "EAMES",
                "ENJUGUENT",
                "ERES",
                "ESCROC",
                "FAUCHARDS",
                "GRAUBUNDEN",
                "INSERAIENT",
                "MALMENEES",
                "MEDIUMNITES",
                "NARREES",
                "NITRIFIAT",
                "ODES",
                "REGORGEA",
                "SURSITES",
                "TUERA",
                "VOLLEYBALL"
            }, grid);
            Assert.AreEqual("CODNGAME", finder.Find());
        }
    }
}