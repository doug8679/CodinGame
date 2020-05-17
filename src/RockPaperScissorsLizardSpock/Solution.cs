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
        private Tree<Player> _tourney;

        List<Tree<Player>> _players = new List<Tree<Player>>();

        public Solution(string[] players)
        {
            foreach (var player in players)
            {
                _players.Add(new Leaf<Player>(new Player(player.Split(' '))));new Player(player.Split(' ')));
            }
        }

        public string Solve()
        {
            while (_players.Count > 1)
            {
                var temp = new List<Tree<Player>>();
                for (int i = 0; i < _players.Count; i += 2)
                {
                    var p1 = _players[i];
                    var p2 = _players[i+1];
                    temp.Add(Game.PlayRound(p1, p2));
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

        public static Tree<Player> PlayRound(Tree<Player> p1, Tree<Player> p2)
        {
            Tree<Player> result = new Node<Player>(p1, null, p2);
            switch (COMPARER.Compare(p1.Value.Sign, p2.Value.Sign))
            {
                case -1:
                    result.Value = p2.Value;
                    break;
                case 0:
                    result.Value = (p1.Value.Id < p2.Value.Id) ? p1.Value : p2.Value;
                    break;
                default:
                    result.Value = p1.Value;
                    break;
            }
            return result;
        }
    }

    abstract class Tree<T>
    {
        public T Value { get; set; }
    }

    class Node<T> : Tree<T>
    {
        public Node(Tree<T> left, T value, Tree<T> right)
        {
            Left = left;
            Value = value;
            Right = right;
        }

        public Tree<T> Left { get; set; }
        public Tree<T> Right { get; set; }
    }

    class Leaf<T> : Tree<T>
    {
        public Leaf(T value)
        {
            Value = value;
        }
    }
}