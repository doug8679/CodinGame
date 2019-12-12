using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace AStoryToGoInCircles
{

    public class Solution
    {
        static string pos = "abcdefghijklmnopqrstuvwxyz";
        private long ii;
        private int nb;
        private char[,] grid;

        public Solution(long ii, int nb, string[] lines)
        {
            this.ii = ii;
            this.nb = nb;
            grid = new char[nb,nb];
            for (int i=0; i< nb; i++)
            {
                for (int j=0; j<nb; j++)
                {
                    grid[i,j] = lines[i][j];
                }
            }
        }

        public string Solve()
        {
            DumpGrid();
            int myR=0, myC=0;
            for (long i=1; i < ii; i++) {
                char test = grid[myR, myC];
                Console.Error.WriteLine($"Row: {myR}, Column: {myC}, Current Character: {test}, ii = {i}");
                if (test == '#')
                {
                    RotateGridCW();
                    DumpGrid();
                    i--;
                } 
                else if (test == '@')
                {
                    RotateGridCCW();
                    DumpGrid();
                    i--;
                } 
                else
                { 
                    myC += pos.IndexOf(test) + 1;
                    while (myC >= nb)
                    {
                        myR++;
                        if (myR >= nb)
                        {
                            myR = 0;
                        }
                        myC -= nb;
                    }
                }
            }
            return $"{grid[myR, myC]}";
        }

        private void DumpGrid()
        {
            StringBuilder b = new StringBuilder();
            for(int i=0; i< nb; i++)
            {
                for (int j=0; j<nb; j++)
                {
                    b.Append(grid[i,j]);
                }
                b.AppendLine();
            }
            Console.Error.WriteLine(b.ToString());
        }

        private void RotateGridCW()
        {
            char[,] rot = new char[nb, nb];
            for (int i=0; i<nb; i++)
            {
                for (int j=0; j<nb; j++)
                {
                    rot[i, j] = grid[nb - (j + 1), i];
                }
            }
            grid = rot;
        }

        private void RotateGridCCW()
        {
            char[,] rot = new char[nb, nb];
            for (int i = 0; i < nb; i++)
            {
                for (int j = 0; j < nb; j++)
                {
                    rot[i, j] = grid[j, nb - (i + 1)];
                }
            }
            grid = rot;
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