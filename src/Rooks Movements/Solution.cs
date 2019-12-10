using System;
using System.Collections.Generic;

namespace RookMovements
{
    public class Solution
    {
        private static List<char> columns = new List<char>{'a','b','c','d','e','f','g','h'};
        private char[,] board = new char[8,8];
        private int myR, myC;
        private string preFix;
        public Solution(string rookPos, int pieceCount, string[] piecePositions) {
            preFix = $"R{rookPos}";
            for (int i=0; i<8; i++) {
                for (int j=0; j<8; j++) {
                    board[i,j] = ' ';
                }
            }

            myR = int.Parse(rookPos[1].ToString())-1;
            myC = columns.IndexOf(rookPos[0]);
            board[myR, myC] = 'R';

            foreach(var pos in piecePositions) {
                string[] inputs = pos.Split(' ');
                char colour = int.Parse(inputs[0]) == 0 ? 'W' : 'B';
                string onePiece = inputs[1];
                int r = int.Parse(onePiece[1].ToString())-1;
                int c = columns.IndexOf(onePiece[0]);
                board[r,c] = colour;
            }
        }

        public string Solve() {
            List<string> result = new List<string>();
            // Test my row first
            for (int i=myC-1; i>=0; i--) {
                if (board[myR, i] == ' ') {
                    result.Add($"{preFix}-{columns[i]}{myR+1}");
                }
                if (board[myR, i] == 'W') {
                    break;
                }
                if (board[myR, i] == 'B') {
                    result.Add($"{preFix}x{columns[i]}{myR+1}");
                    break;
                }
            }
            for (int i=myC+1; i<8; i++) {
                if (board[myR, i] == ' ') {
                    result.Add($"{preFix}-{columns[i]}{myR+1}");
                }
                if (board[myR, i] == 'W') {
                    break;
                }
                if (board[myR, i] == 'B') {
                    result.Add($"{preFix}x{columns[i]}{myR+1}");
                    break;
                }
            }

            // Test my column
            for (int i=myR-1; i>=0; i--) {
                if (board[i, myC] == ' ') {
                    result.Add($"{preFix}-{columns[myC]}{i+1}");
                }
                if (board[i, myC] == 'W') {
                    break;
                }
                if (board[i, myC] == 'B') {
                    result.Add($"{preFix}x{columns[myC]}{i+1}");
                    break;
                }
            }
            for (int i=myR+1; i<8; i++) {
                if (board[i, myC] == ' ') {
                    result.Add($"{preFix}-{columns[myC]}{i+1}");
                }
                if (board[i, myC] == 'W') {
                    break;
                }
                if (board[i, myC] == 'B') {
                    result.Add($"{preFix}x{columns[myC]}{i+1}");
                    break;
                }
            }
            result.Sort();
            return string.Join(Environment.NewLine, result);
        }

        static void Main(string[] args)
        {   
            string rookPosition = Console.ReadLine();
            int nbPieces = int.Parse(Console.ReadLine());
            List<string> pieces = new List<string>();
            for (int i = 0; i < nbPieces; i++)
            {
                pieces.Add(Console.ReadLine());
            }

            var sln = new Solution(rookPosition, nbPieces, pieces.ToArray());
            
            Console.WriteLine(sln.Solve());
        }
    }
}