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
        public void Test1()
        {
            var grid = new WordGrid(3,3);
                grid.SetRow("BAC", 0);
                grid.SetRow("BOB", 1);
                grid.SetRow("RED", 2);
            var finder = new WordFinder(new List<string>{"BAC","BOB"}, grid);
            Assert.Pass();
        }
    }

    public class WordGrid {
        public char[,] grid;

        public WordGrid(int w, int h)
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
    }
}