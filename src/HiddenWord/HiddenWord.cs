using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Tests
{
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