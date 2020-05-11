using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Grab the pellets as fast as you can!
 **/
class Player
{
    static void Main(string[] args)
    {
        string[] inputs;
        inputs = Console.ReadLine().Split(' ');
        int width = int.Parse(inputs[0]); // size of the grid
        int height = int.Parse(inputs[1]); // top left corner is (x=0, y=0)
        for (int i = 0; i < height; i++)
        {
            string row = Console.ReadLine(); // one line of the grid: space " " is floor, pound "#" is wall
        }

        // game loop
        while (true)
        {
            inputs = Console.ReadLine().Split(' ');
            int myScore = int.Parse(inputs[0]);
            int opponentScore = int.Parse(inputs[1]);
            int visiblePacCount = int.Parse(Console.ReadLine()); // all your pacs and enemy pacs in sight
            int myX=0, myY=0;
            for (int i = 0; i < visiblePacCount; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                int pacId = int.Parse(inputs[0]); // pac number (unique within a team)
                bool mine = inputs[1] != "0"; // true if this pac is yours
                if (mine) {
                   myX = int.Parse(inputs[2]); // position in the grid
                   myY = int.Parse(inputs[3]); // position in the grid
                }
                string typeId = inputs[4]; // unused in wood leagues
                int speedTurnsLeft = int.Parse(inputs[5]); // unused in wood leagues
                int abilityCooldown = int.Parse(inputs[6]); // unused in wood leagues
            }
            int visiblePelletCount = int.Parse(Console.ReadLine()); // all pellets in sight
            List<Tuple<int, int>> superPellets = new List<Tuple<int, int>>();
            Tuple<int, int> nextPos = null;
            double dist = double.MaxValue;
            for (int i = 0; i < visiblePelletCount; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                int x = int.Parse(inputs[0]);
                int y = int.Parse(inputs[1]);
                if (GetDistance(myX, myY, x, y) < dist) {
                    nextPos = new Tuple<int,int>(x, y);
                }
                int value = int.Parse(inputs[2]); // amount of points this pellet is worth
                if (value > 1) {
                    superPellets.Add(new Tuple<int, int>(x, y));
                }
            }

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");
            double superDist = double.MaxValue;
            foreach (var pellet in superPellets) {
                if (GetDistance(myX, myY, pellet.Item1, pellet.Item2) < superDist) {
                    nextPos = pellet;
                }
            }
            Console.WriteLine($"MOVE 0 {nextPos.Item1} {nextPos.Item2}"); // MOVE <pacId> <x> <y>

        }
    }

    static double GetDistance(int x1, int y1, int x2, int y2) {
        var x = Math.Pow(x2 - x1, 2);
        var y = Math.Pow(y2 - y1, 2);
        return Math.Sqrt(x + y);
    }
}