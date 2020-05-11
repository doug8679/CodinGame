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
        char[,] map = new char[height, width];
        for (int i = 0; i < height; i++)
        {
            var line = Console.ReadLine();
            for (int j=0; j < width; j++) {
                map[i, j] = line[j];
            }
        }

        // game loop
        while (true)
        {
            inputs = Console.ReadLine().Split(' ');
            int myScore = int.Parse(inputs[0]);
            int opponentScore = int.Parse(inputs[1]);
            int visiblePacCount = int.Parse(Console.ReadLine()); // all your pacs and enemy pacs in sight
            List<Pac> myPacs = new List<Pac>();
            for (int i = 0; i < visiblePacCount; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                int pacId = int.Parse(inputs[0]); // pac number (unique within a team)
                bool mine = inputs[1] != "0"; // true if this pac is yours
                int x = int.Parse(inputs[2]);
                int y = int.Parse(inputs[3]);
                if (mine) {
                    myPacs.Add(new Pac(pacId, x, y));
                }
                string typeId = inputs[4]; // unused in wood leagues
                int speedTurnsLeft = int.Parse(inputs[5]); // unused in wood leagues
                int abilityCooldown = int.Parse(inputs[6]); // unused in wood leagues
            }
            int visiblePelletCount = int.Parse(Console.ReadLine()); // all pellets in sight
            List<Tuple<int, int>> pellets = new List<Tuple<int, int>>();
            List<Tuple<int, int>> superPellets = new List<Tuple<int, int>>();
            double dist = double.MaxValue;
            for (int i = 0; i < visiblePelletCount; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                int x = int.Parse(inputs[0]);
                int y = int.Parse(inputs[1]);
                pellets.Add(new Tuple<int,int>(x, y));
                
                int value = int.Parse(inputs[2]); // amount of points this pellet is worth
                if (value > 1) {
                    superPellets.Add(new Tuple<int, int>(x, y));
                }
            }

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");
            List<string> commands = new List<string>();
            foreach (var pac in myPacs) {
                Tuple<int, int> pos = pac.GetClosestTarget(superPellets, pellets);
                commands.Add($"MOVE {pac.Id} {pos.Item1} {pos.Item2}");
            }
            double superDist = double.MaxValue;
            /*foreach (var pellet in superPellets) {
                if (GetDistance(myX, myY, pellet.Item1, pellet.Item2) < superDist) {
                    nextPos = pellet;
                }
            }*/
            Console.WriteLine(string.Join("|", commands)); // MOVE <pacId> <x> <y>
            //Console.WriteLine("MOVE 0 0 0");

        }
    }

    static double GetDistance(int x1, int y1, int x2, int y2) {
        var x = Math.Pow(x2 - x1, 2);
        var y = Math.Pow(y2 - y1, 2);
        return Math.Sqrt(x + y);
    }
}

class Pac {
    public int Id {get;set;}
    public int X {get;set;}
    public int Y {get;set;}

    public Pac(int id, int x, int y) {
        Id = id;
        X = x;
        Y = y;
    }

    public override string ToString() {
        return $"Pac {Id} @ ({X}, {Y})";
    }

    public Tuple<int, int> GetClosestTarget(List<Tuple<int, int>> super, List<Tuple<int, int>> reg) {
        var minDist = double.MaxValue;
        Tuple<int, int> result = null;
        if (super.Any()) {
            foreach (var targ in super) {
                var dist = Math.Sqrt(Math.Pow(X-targ.Item1,2) + Math.Pow(Y-targ.Item2,2));
                if (dist < minDist) {
                    result = targ;
                    minDist = dist;
                }
            }
            super.Remove(result);
        } else {
            foreach(var targ in reg) {
                var dist = Math.Sqrt(Math.Pow(X-targ.Item1,2) + Math.Pow(Y-targ.Item2,2));
                if (dist < minDist) {
                    result = targ;
                    minDist = dist;
                }
            }
            reg.Remove(result);
        }
        return result;
    }
}