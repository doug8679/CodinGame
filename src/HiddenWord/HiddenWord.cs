using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
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
            grid.SetRow("BAC", 0);
            grid.SetRow("EOO", 1);
            grid.SetRow("CKB", 2);
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
            Assert.AreEqual("OK", finder.Find());
        }
    }

    public class WordGrid {
        public char[,] grid;

        public WordGrid(int w, int h, string[] rows)
        {
            grid = new char[w, h];
        }

        public void SetRow(string cols, int row)
        {
            for (int c=0; c<cols.Length; c++) {
                grid[c, row] = cols[c];
            }
        }
    }

    public class WordFinder {
        private List<string> _words;
        private WordGrid _grid;
        public WordFinder(List<string> list, WordGrid grid)
        {
            _grid = grid;
            _words = list;
        }

        public string Find()
        {
            return string.Empty;
        }
    }
}