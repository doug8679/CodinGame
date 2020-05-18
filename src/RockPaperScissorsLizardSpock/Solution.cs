using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RockPaperScissorsLizardSpock
{
    /**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
    public class Solution
    {
        List<Node> _players = new List<Node>();

        public Solution(string[] players)
        {
            foreach (var player in players)
            {
                _players.Add(new Node(new Player(player.Split(' '))));
            }
        }

        public string Solve()
        {
            while (_players.Count > 1)
            {
                var temp = new List<Node>();
                for (int i = 0; i < _players.Count; i += 2)
                {
                    var p1 = _players[i];
                    var p2 = _players[i+1];
                    temp.Add(Game.PlayRound(p1, p2));
                }
                _players = temp;
            }

            Stack<Player> opponents = new Stack<Player>();
            BuildSolutionTree(_players[0], _players[0].Value, ref opponents);

            return $"{_players[0].Value.Id}\n{string.Join(" ", opponents.Select(p => p.Id))}";
        }

        private void WriteTree(Node player)
        {
            if (player.Left != null)
                WriteTree(player.Left);
            Console.Write($"{player.Value.Id} ");
            if (player.Right != null)
                WriteTree(player.Right);

        }

        private void BuildSolutionTree(Node node, Player player, ref Stack<Player> opponents)
        {
            if (!node.Value.Equals(player))
            {
                opponents.Push(node.Value);
            }

            if (!node.IsLeaf)
            {
                if (node.Left != null && node.Left.Value.Equals(player))
                {
                    BuildSolutionTree(node.Left, player, ref opponents);
                }
                else
                {
                    opponents.Push(node.Left.Value);
                }

                if (node.Right != null && node.Right.Value.Equals(player))
                {
                    BuildSolutionTree(node.Right, player, ref opponents);
                }
                else
                {
                    opponents.Push(node.Right.Value);
                }
            }
        }

        private string DetermineWinner()
        {
            var winner = _players[0];
            StringBuilder b = new StringBuilder();
            b.AppendLine($"{winner.Value.Id}");
            List<int> opponents = new List<int>();


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

        #region Overrides of Object

        public override string ToString()
        {
            return $"{Id} {Sign}";
        }

        #endregion
    }

    class Game
    {
        private static IComparer<Sign> COMPARER = new SignComparer();

        public static Node PlayRound(Node p1, Node p2)
        {
            Console.Error.Write($"Game between ({p1.Value}) and ({p2.Value}): ");
            Node result = new Node(p1, null, p2);
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
            Console.Error.WriteLine($"{result.Value}");
            return result;
        }
    }

    class Node
    {
        public Node(Player value)
        {
            Value = value;
        }

        public Node(Node left, Player value, Node right): this(value)
        {
            Left = left;
            Right = right;
        }

        public Player Value { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public bool IsLeaf => Left == null && Right == null;
    }
}