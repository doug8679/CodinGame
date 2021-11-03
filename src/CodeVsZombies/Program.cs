using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Save humans, destroy zombies!
 **/
class Player
{
    static void Main(string[] args)
    {
        string[] inputs;

        // game loop
        while (true)
        {
            inputs = Console.ReadLine().Split(' ');
            int x = int.Parse(inputs[0]);
            int y = int.Parse(inputs[1]);
            int humanCount = int.Parse(Console.ReadLine());
            int [][] humans = new int[humanCount][];
            for (int i = 0; i < humanCount; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                humans[i] = new int[] {
                    int.Parse(inputs[0]),
                    int.Parse(inputs[1]),
                    int.Parse(inputs[2])
                };
            }
            int zombieCount = int.Parse(Console.ReadLine());
            int[][] zombies = new int[zombieCount][];
            for (int i = 0; i < zombieCount; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                zombies[i] = new int[] {
                    int.Parse(inputs[0]),
                    int.Parse(inputs[1]),
                    int.Parse(inputs[2]),
                    int.Parse(inputs[3]),
                    int.Parse(inputs[4])
                };
            }

            int[] nearestHuman = getNearestSavableHuman(x, y, humans, zombies);
            int[] nearestZombie = getNearestZombie(x, y, zombies);

            int[] target = getBestTarget(x, y, nearestHuman, nearestZombie);

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");

            Console.WriteLine($"{target[0]} {target[1]}"); // Your destination coordinates

        }
    }

    private static int[] getBestTarget(int x, int y, int[] h, int[] z) {
        
        var hd = Math.Sqrt(Math.Pow(x-h[0], 2) + Math.Pow(y-h[1], 2));
        var zd = Math.Sqrt(Math.Pow(x-z[0], 2) + Math.Pow(y-z[1], 2));

        return (hd < zd) ? new int[] { h[0], h[1] } : new int[] { z[0], z[1] };
    }

    private static int[] getNearestSavableHuman(int x, int y, int[][] humans, int[][] zombies) {
        int minDist = int.MaxValue;
        int mindex = 0;
        for (int i=0; i<humans.Length; i++)
        {
            bool canSave=true;
            int j=0;
            while (canSave && j < zombies.Length)
            {
                var zd = (int)Math.Sqrt(Math.Pow(zombies[j][1]-humans[i][1], 2) + Math.Pow(zombies[j][2]-humans[i][2], 2));
                canSave = zd > 400;
                j++;
            }

            if (!canSave) continue;

            var d = (int)Math.Sqrt(Math.Pow(x-humans[i][1], 2) + Math.Pow(y-humans[i][2], 2));
            if (d < minDist) {
                mindex = i;
                minDist = d;
            }
        }

        return new int[] {humans[mindex][1], humans[mindex][2]};
    }

    private static int[] getNearestZombie(int x, int y, int[][] zombies) {
        int minDist = int.MaxValue;
        int mindex = 0;
        for (int i=0; i<zombies.Length; i++)
        {
            var d = (int)Math.Sqrt(Math.Pow(x-zombies[i][3], 2) + Math.Pow(y-zombies[i][4], 2));
            if (d < minDist) {
                mindex = i;
                minDist = d;
            }
        }

        return new int[] {zombies[mindex][3], zombies[mindex][4]};
    }
}