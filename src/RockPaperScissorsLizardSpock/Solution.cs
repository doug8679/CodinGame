using System;
using System.Collections.Generic;
using System.Linq;

namespace RockPaperScissorsLizardSpock
{
    /**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
    public class Solution
    {
        List<Player> _players = new List<Player>();

        public Solution(string[] players)
        {
            foreach (var player in players)
            {
                _players.Add(new Player(player.Split(' ')));
            }
        }

        public string Solve()
        {
            while (_players.Count > 1)
            {
                var temp = new List<Player>();
                for (int i = 0; i < _players.Count; i += 2)
                {
                    var game = _players.Skip(i).Take(2).ToArray();
                    temp.Add(Test(game));
                }
                _players = temp;
            }
            return string.Empty;
        }

        static void Main(string[] args)
        {
            int N = int.Parse(Console.ReadLine());
            string[] players = new string[N];
            for (int i = 0; i < N; i++)
            {
                players[i] = Console.ReadLine();
            }

            var sln = new Solution(players);

            // Write an answer using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");

            Console.WriteLine(sln.Solve());
        }
    }

    enum Sign
    {
        ROCK,
        PAPER,
        SCISSORS,
        LIZARD,
        SPOCK
    }

    class SignComparer : IComparer<Sign>
    {
        #region Implementation of IComparer<in Sign>

        public int Compare(Sign x, Sign y)
        {
            if (x == y)
            {
                return 0;
            }
            if ( (x == Sign.SCISSORS && y == Sign.PAPER) ||
                 (x == Sign.PAPER && y == Sign.ROCK) ||
                 (x == Sign.ROCK && y == Sign.LIZARD) ||
                 (x == Sign.LIZARD && y == Sign.SPOCK) ||
                 (x == Sign.SPOCK && y == Sign.SCISSORS) ||
                 (x == Sign.SCISSORS && y == Sign.LIZARD) ||
                 (x == Sign.LIZARD && y == Sign.PAPER) ||
                 (x == Sign.PAPER && y == Sign.SPOCK) ||
                 (x == Sign.SPOCK && y == Sign.ROCK) ||
                 (x == Sign.ROCK && y == Sign.SCISSORS))
            {
                return 1;
            }

            return -1;
        }

        #endregion
    }

    class Player
    {
        public Player(string[] info)
        {
            Id = Convert.ToInt32(info[0]);
            switch (info[1].Trim())
            {
                case "R":
                    Sign = Sign.ROCK;
                    break;
                case "P":
                    Sign = Sign.PAPER;
                    break;
                case "C":
                    Sign = Sign.SCISSORS;
                    break;
                case "L":
                    Sign = Sign.LIZARD;
                    break;
                case "S":
                    Sign = Sign.SPOCK;
                    break;
            }
        }
        public int Id { get; set; }
        public Sign Sign { get; set; }
    }

    class Game
    {
        private static IComparer<Sign> COMPARER = new SignComparer();

        public Game(Player l, Player r)
        {
            Left = l;
            Right = r;
        }

        public Game Left { get; set; }
        public Game Right { get; set; }
        public Game Winner => Test();

        private Player Test()
        {
            switch (COMPARER.Compare(Left.Sign, Right.Sign))
            {
                case -1:
                    return Right;
                case 0:
                    return (Left.Id < Right.Id) ? Left : Right;
                default:
                    return Left;
            }
        }
    }
}