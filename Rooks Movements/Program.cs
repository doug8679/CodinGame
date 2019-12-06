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
        string rookPosition = Console.ReadLine();
        int nbPieces = int.Parse(Console.ReadLine());
        for (int i = 0; i < nbPieces; i++)
        {
            string[] inputs = Console.ReadLine().Split(' ');
            int colour = int.Parse(inputs[0]);
            string onePiece = inputs[1];
        }

        // Write an action using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");

        Console.WriteLine("ANSWER");
    }
}