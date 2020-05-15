using System;
using System.Collections.Generic;
using System.Drawing;

namespace HiddenWord
{
    class Solution
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            List<string> words = new List<string>();
            for (int i = 0; i < n; i++)
            {
                words.Add(Console.ReadLine());
            }
            string[] inputs = Console.ReadLine().Split(' ');
            int h = int.Parse(inputs[0]);
            int w = int.Parse(inputs[1]);
            string[] lines = new string[h];
            for (int i = 0; i < h; i++)
            {
                lines[i] = Console.ReadLine();
            }

            WordGrid grid = new WordGrid(w, h, lines);
            WordFinder finder = new WordFinder(words, grid);

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");

            Console.WriteLine(finder.Find());
        }
    }

    public class GridCell
    {
        public GridCell(char c)
        {
            Value = c;
            Used = false;
        }

        public char Value { get; set; }
        public bool Used { get; set; }
    }

    public class WordGrid {
        public int Rows { get; set; }
        public int Cols { get; set; }
        public int Size => Rows * Cols;

        public GridCell[,] grid;

        public WordGrid(int w, int h, string[] rows)
        {
            Rows = h;
            Cols = w;
            grid = new GridCell[Cols, Rows];
            for (int j = 0; j < Rows; j++)
            {
                for (int i = 0; i < Cols; i++)
                {
                    grid[j, i] = new GridCell(rows[j][i]);
                }
            }
        }

        public void SetRow(string cols, int row)
        {
            for (int c=0; c<cols.Length; c++) {
                grid[row, c] = new GridCell(cols[c]);
            }
        }

        private static int[] x = {-1, -1, -1, 0, 0, 1, 1, 1};
        private static int[] y = {-1, 0, 1, -1, 1, -1, 0, 1};
        private void Strike(List<Point> path)
        {
            foreach (var point in path)
            {
                grid[point.X, point.Y].Used = true;
            }
        }

        public void Search2D(int row, int col, string word)
        {
            if (grid[row, col].Value != word[0])
            {
                return;
            }

            int len = word.Length;

            for (int dir = 0; dir < 8; dir++)
            {
                List<Point> path = new List<Point>{new Point(row, col)};
                int k, rd = row + x[dir], cd = col + y[dir];

                for (k = 1; k < len; k++)
                {
                    if (rd >= Rows || rd < 0 || cd >= Cols || cd < 0)
                    {
                        break;
                    }

                    if (grid[rd, cd].Value != word[k])
                    {
                        break;
                    }

                    path.Add(new Point(rd, cd));
                    rd += x[dir];
                    cd += y[dir];
                }

                if (k == len)
                {
                    Strike(path);
                    return;
                }
            }
        }

        public string DecodeRemaining()
        {
            string result = "";
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Cols; c++)
                {
                    if (!grid[r, c].Used)
                    {
                        result += $"{grid[r, c].Value}";
                    }
                }
            }

            return result;
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
            foreach (var word in _words)
            {
                for (int r = 0; r < _grid.Rows; r++)
                {
                    for (int c = 0; c < _grid.Cols; c++)
                    {
                        _grid.Search2D(r, c, word);
                    }
                }
            }

            return _grid.DecodeRemaining();
        }
    }
}