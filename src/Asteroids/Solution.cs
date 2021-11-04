using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace Asteroids
{
    /**
     * Auto-generated code below aims at helping you parse
     * the standard input according to the problem statement.
     **/
    public class Solution
    {
        public static void Main(string[] args)
        {
            Func<int, int, int, int, float> velocity = (t1, p1, t2, p2) => (float)((p2 - p1)) / (float)((t2 - t1));
            Func<int, int, float, int> position = (dt, s0, v) => (int)Math.Floor(s0 + (v * dt));
            Dictionary<char, Dictionary<int, Tuple<int, int>>> positions = new Dictionary<char, Dictionary<int, Tuple<int, int>>>();

            string[] inputs;
            inputs = Console.ReadLine().Split(' ');
            int W = int.Parse(inputs[0]);
            int H = int.Parse(inputs[1]);
            int T1 = int.Parse(inputs[2]);
            int T2 = int.Parse(inputs[3]);
            int T3 = int.Parse(inputs[4]);

            Dictionary<char, int> time1 = new Dictionary<char, int>(), time2 = new Dictionary<char, int>();
            char[] time3 = Enumerable.Repeat('.', W*H).ToArray();
            for (int i = 0; i < H; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                for (int j=0; j<W; j++)
                {
                    if (inputs[0][j] != '.') time1.Add(inputs[0][j], W*i + j);
                    if (inputs[1][j] != '.') time2.Add(inputs[1][j], W*i + j);
                }
            }

            foreach (var item in time2)
            {
                var x2 = item.Value % W;
                var y2 = item.Value / W;
                if (time1.TryGetValue(item.Key, out var start))
                {
                    var x1 = start % W;
                    var y1 = start / W;

                    var vx = velocity(T1, x1, T2, x2);
                    var vy = velocity(T1, y1, T2, y2);

                    var x3 = position(T3 - T2, x2, vx);
                    var y3 = position(T3 - T2, y2, vy);

                    var pos = y3*W + x3;
                    if (x3 >= 0 && x3 < W && y3 >= 0 && y3 < H)
                    {
                         time3[pos] = (time3[pos] == '.' ? item.Key : (item.Key < time3[pos] ? item.Key : time3[pos]));
                    }
                }
            }
            // Write an answer using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");

            for (int i=0; i<H; i++)
            {
                for (int j=0; j<W; j++)
                {
                    Console.Write($"{time3[W*i + j]}");
                }
                Console.WriteLine();
            }
        }
    }
}