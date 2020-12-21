using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TwoZeroFourEight
{
    /**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
    public class Player
    {
        private const int SIZE = 4;
        public static string[] DIRECTIONS = new string[]{ "U", "R", "D", "L"};

        static void Main(string[] args)
        {
            // game loop
            //while (true)
            //{
                long seed = long.Parse(Console.ReadLine()); // needed to predict the next spawns
                int score = int.Parse(Console.ReadLine());

                int[] cells = new int[16];
                for (int i = 0; i < 4; i++)
                {
                    string[] inputs = Console.ReadLine().Split(' ');
                    for (int j = 0; j < 4; j++)
                    {
                        cells[i*4 + j] = int.Parse(inputs[j]);
                    }
                }

                var board = new Board(seed, score, cells);

                var sim = new Simulator(board);

                sim.DoSimulation(15);

                int maxScore = score;
                var dir = "U";
                for (int i = 0; i < DIRECTIONS.Length; i++)
                {
                    if (board.TestMove(i, out var s))
                    {
                        if (s > maxScore)
                        {
                            maxScore = s;
                            dir = DIRECTIONS[i];
                        }
                    }
                }

                // Write an action using Console.WriteLine()
                // To debug: Console.Error.WriteLine("Debug messages...");

                //Console.WriteLine(dir);
            //}
        }
    }

    class Simulator
    {
        static readonly List<string> DIRECTIONS = new List<string>{ "U", "R", "D", "L"};
        private Board _board;
        
        public Simulator(Board b)
        {
            _board = b;
        }

        public void DoSimulation(int maxTurns)
        {
            for (int iTurn = 0; iTurn < maxTurns; iTurn++)
            {
                int maxScore = _board.Score;
                var dir = "U";
                for (int iDir = 0; iDir < DIRECTIONS.Count; iDir++)
                {
                    if (_board.TestMove(iDir, out var s))
                    {
                        if (s > maxScore)
                        {
                            maxScore = s;
                            dir = DIRECTIONS[iDir];
                        }
                    }
                }
                _board.Score += _board.ApplyMove(DIRECTIONS.IndexOf(dir));
                _board.SpawnTile();
                Console.WriteLine($"Turn #: {iTurn+1}, Direction: {dir}, Score: {_board.Score}, Board:{Environment.NewLine}{_board}{Environment.NewLine}");
            }
        }
    }
}