using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;

namespace AStoryToGoInCircles
{

    public class Solution
    {
        static string pos = "abcdefghijklmnopqrstuvwxyz";
        private long ii;
        private int nb;
        private char[,] grid0, grid90, grid180, grid270;
        private List<char[,]> grids;
        private int rotation = 0;

        public Solution(long ii, int nb, string[] lines)
        {
            this.ii = ii;
            this.nb = nb;
            grid0 = new char[nb,nb];
            grid90 = new char[nb, nb];
            grid180 = new char[nb, nb];
            grid270 = new char[nb, nb];
            for (int i=0; i< nb; i++)
            {
                for (int j=0; j<nb; j++)
                {
                    grid0[i,j] = lines[i][j];
                }
            }
            for (int i = 0; i < nb; i++)
            {
                for (int j = 0; j < nb; j++)
                {
                    grid90[j, nb - (i + 1)] = grid0[i, j];
                    grid270[nb - (j + 1), i] = grid0[i, j];
                    grid180[nb - (i + 1), nb - (j + 1)] = grid0[i,j];
                }
            }
            grids = new List<char[,]> { grid0, grid90, grid180, grid270};
        }

        public string Solve()
        {
            int myR=0, myC=0, r=0, c=0, gridIndex = 0;
            var sw = Stopwatch.StartNew();
            long i=1;
            while (i < ii) {
                char test = grids[gridIndex][myR, myC];
                while (test == '#' || test == '@')
                {
                    if (test == '#') gridIndex++;
                    if (test == '@') gridIndex--;
                    if (gridIndex < 0) gridIndex += 4;
                    if (gridIndex >=4) gridIndex -= 4;
                    test = grids[gridIndex][myR, myC];
                }
                myC += pos.IndexOf(test) + 1;
                while (myC >= nb) {
                    myR++;
                    if (myR >= nb) {
                        myR = 0;
                    }
                    myC -= nb;
                }
                i++;
            }
            sw.Stop();
            Console.Error.WriteLine($"Full solution time was {sw.ElapsedMilliseconds} ms.");
            return $"{grids[gridIndex][myR, myC]}";
        }

        private void DumpGrid(char[,] grid)
        {
            StringBuilder b = new StringBuilder();
            for (int i = 0; i < nb; i++)
            {
                for (int j = 0; j < nb; j++)
                {
                    b.Append(grid[i, j]);
                }
                b.AppendLine();
            }
            Console.Error.WriteLine(b.ToString());
        }

        static void Main(string[] args)
        {
            long ii = long.Parse(Console.ReadLine());
            int nb = int.Parse(Console.ReadLine());
            string[] lines = new string[nb];
            for (int i = 0; i < nb; i++)
            {
                lines[i] = Console.ReadLine();
            }

            var sln = new Solution(ii, nb, lines);
            Console.WriteLine(sln.Solve());
        }
    }
}