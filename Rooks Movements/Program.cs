using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Solution
{
    static void Main(string[] args)
    {
        List<char> columns = new List<char>{'a','b','c','d','e','f','g','h'};
        char[,] board = new char[8,8];
        for (int i=0; i<8; i++) {
            for (int j=0; j<8; j++) {
                board[i,j] = ' ';
            }
        }

        string rookPosition = Console.ReadLine();
        int myR = int.Parse(rookPosition[1].ToString())-1;
        int myC = columns.IndexOf(rookPosition[0]);
        board[myR, myC] = 'R';
        int nbPieces = int.Parse(Console.ReadLine());
        for (int i = 0; i < nbPieces; i++)
        {
            string[] inputs = Console.ReadLine().Split(' ');
            char colour = int.Parse(inputs[0]) == 0 ? 'W' : 'B';
            string onePiece = inputs[1];
            int r = int.Parse(onePiece[1].ToString())-1;
            int c = columns.IndexOf(onePiece[0]);
            board[r,c] = colour;
        }

        string result = BuildMoves("Rd5", board, columns);
        Console.WriteLine(" ABCDEFGH");
        for (int r=0; r<8; r++) {
            Console.Write($"{8-r}");
            for (int c=0; c<8; c++) {
                Console.Write($"{board[r,c]}");
            }
            Console.WriteLine();
        }

        // Write an action using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");

        Console.WriteLine(result);
    }

    static private string BuildMoves(string preFix, char[,] board, List<char> columns) {

    }
}