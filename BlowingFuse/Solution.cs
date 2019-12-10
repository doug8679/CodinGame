using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace BlowingFuse {

    public class Solution
    {
        private const string BLOWN_FUSE = "Fuse was blown.";
        private string NOT_BLOWN_TEMPLATE = "Fuse was not blown." + Environment.NewLine + "Maximal consumed current was {0} A.";

        private int N, M, C;
        private int[] amps, clicks;
        private Device[] state;

        public Solution(string line1, string line2, string line3) {
            string[] inputs;
            inputs = line1.Split(' ');
            N = int.Parse(inputs[0]);
            M = int.Parse(inputs[1]);
            C = int.Parse(inputs[2]);
            inputs = line2.Split(' ');
            state = new Device[N];
            amps = new int[N];
            clicks = new int[M];
            for (int i = 0; i < N; i++)
            {
                state[i] = new BlowingFuse.Device(int.Parse(inputs[i]));
            }
            inputs = line3.Split(' ');
            for (int i = 0; i < M; i++)
            {
                clicks[i] = int.Parse(inputs[i]);
            }
        }

        public string Solve() {
            int maxConsume = 0;
            for (int i=0; i<M; i++) {
                state[clicks[i]-1].Toggle();

                var consume = state.Where(d => d.On).Sum(d=>d.Consumption);
                if (consume > C) {
                    return BLOWN_FUSE;
                }

                maxConsume = Math.Max(maxConsume, consume);
            }
            return string.Format(NOT_BLOWN_TEMPLATE, maxConsume);
        }

        static void Main(string[] args)
        {
            var sln = new Solution(Console.ReadLine(), Console.ReadLine(), Console.ReadLine());
            Console.WriteLine(sln.Solve());

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");

            Console.WriteLine("Fuse was not blown.");
            Console.WriteLine("Maximal consumed current was XX A.");
        }
    }

    class Device {

        public Device(int amps) {
            Consumption = amps;
            On = false;
        }

        public int Consumption {get; set;}
        public bool On {get;set;}

        public void Toggle() {
            On = !On;
        }
    }
}