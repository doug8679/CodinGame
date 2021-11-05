using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwoZeroFourEight
{
    /**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
    public class Player
    {
        private const int SIZE = 4;
        public static string[] DIRECTIONS = new string[] {"U", "R", "L"};

        static void Main(string[] args){
            long seed = long.Parse(Console.ReadLine());
            var b = Board.CreateWithLookAhead(seed, 3);
            Console.WriteLine(b);
        }

        static void OldMain(string[] args)
        {
            // game loop
            while (true)
            {
                long seed = long.Parse(Console.ReadLine()); // needed to predict the next spawns
                int score = int.Parse(Console.ReadLine());

                int[] cells = new int[16];
                for (int i = 0; i < 4; i++)
                {
                    string[] inputs = Console.ReadLine().Split(' ');
                    for (int j = 0; j < 4; j++)
                    {
                        cells[i * 4 + j] = int.Parse(inputs[j]);
                    }
                }

                var board = new Board(seed, score, cells);
                var root = new MoveNode(board, 5);
                string moves = Evaluate(root);
                Console.WriteLine(moves);
            }
        }

        private static string Evaluate(MoveNode root)
        {
            SortedSet<MoveNode> nodes = new SortedSet<MoveNode>(new MoveNodeComparer());
            root.Traverse(nodes);
            return nodes.FirstOrDefault()?.ToString();
        }
    }

    public class MoveNodeComparer : IComparer<MoveNode>
    {
        public int Compare(MoveNode x, MoveNode y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (ReferenceEquals(null, y)) return 1;
            if (ReferenceEquals(null, x)) return -1;
            int result = x.Board.Score.CompareTo(y.Board.Score) * -1;
            if (result == 0)
            {
                result = x.Board.Moves.CompareTo(y.Board.Moves);
            }

            return result;
        }
    }

    public class MoveNode
    {
        public Board Board;
        public string Dir = "";
        public MoveNode Parent = null;
        public readonly List<MoveNode> Children = new List<MoveNode>();

        public MoveNode(Board board, int moveCount)
        {
            Board = board;
            if (moveCount > 0)
                CreateChildNodes(moveCount);
        }

        private void CreateChildNodes(int moveCount)
        {
            for (int i = 0; i < 3; i++)
            {
                Children.Add(new MoveNode(Board.CloneWithMove(i), moveCount - 1)
                    {Dir = Player.DIRECTIONS[i], Parent = this});
            }
        }

        public string Print(string prev = "")
        {
            StringBuilder b = new StringBuilder();
            var path = $"{prev}{Dir}";
            b.AppendLine($"Current Board Score ({path}, {Board.Moves} moves) = {Board.Score}");
            foreach (var child in Children)
            {
                b.Append(child.Print(path));
            }

            return b.ToString();
        }

        public void Traverse(SortedSet<MoveNode> set)
        {
            set.Add(this);
            foreach (var child in Children)
            {
                child.Traverse(set);
            }
        }

        #region Overrides of Object

        public override string ToString()
        {
            return $"{Parent?.ToString() ?? ""}{Dir}";
        }

        #endregion
    }

    public class Board
    {
        public const int SIZE = 4;
        private long seed;
        private int[] grid = new int[SIZE * SIZE];

        public Board[] Next = new Board[4];

        public Board(int seed)
        {
            this.seed = seed;
            SpawnTile();
            SpawnTile();
        }

        public Board(long seed, int score, int[] cells)
        {
            this.seed = seed;
            this.grid = cells;
            Score = score;
        }

        private Board(Board source, int iDir)
        {
            Array.Copy(source.grid, grid, SIZE * SIZE);
            seed = source.seed;
            Score = source.Score + ApplyMove(iDir);
            Moves = source.Moves + 1;
            SpawnTile();
        }

        public Board CloneWithMove(int iDir, int lookAhead = 0)
        {
            var b = new Board(this, iDir);
            if (lookAhead > 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    b.Next[i] = b.CloneWithMove(i, lookAhead - 1);
                }
            }

            return b;
        }

        public void SpawnTile()
        {
            List<int> freeCells = new List<int>();
            for (int x = 0; x < SIZE; x++)
            {
                for (int y = 0; y < SIZE; y++)
                {
                    if (grid[y * SIZE + x] == 0) freeCells.Add(y * SIZE + x);
                }
            }

            if (freeCells.Count > 0)
            {
                int spawnIndex = freeCells[(int) seed % freeCells.Count];
                int value = (seed & 0x10) == 0 ? 2 : 4;

                grid[spawnIndex] = value;

                seed = seed * seed % 50515093L;
            }
        }

        public int Score { get; set; }

        public long Seed => seed;

        public int Moves { get; }

        public int ApplyMove(int dir)
        {
            int turnScore = 0;
            bool[] merged = new bool[SIZE * SIZE];
            int targetStart = new int[] {0, SIZE - 1, SIZE * (SIZE - 1), 0}[dir];
            int targetStep = new int[] {1, SIZE, 1, SIZE}[dir];
            int sourceStep = new int[] {SIZE, -1, -SIZE, 1}[dir];

            for (int i = 0; i < SIZE; i++)
            {
                int finalTarget = targetStart + i * targetStep;
                for (int j = 1; j < SIZE; j++)
                {
                    int source = finalTarget + j * sourceStep;
                    int sourceX = source % SIZE;
                    int sourceY = source / SIZE;
                    if (grid[sourceY * SIZE + sourceX] == 0) continue;
                    for (int k = j - 1; k >= 0; k--)
                    {
                        int intermediate = finalTarget + k * sourceStep;

                        int intermediateX = intermediate % SIZE;
                        int intermediateY = intermediate / SIZE;
                        if (grid[intermediateY * SIZE + intermediateX] == 0)
                        {
                            grid[intermediateY * SIZE + intermediateX] = grid[sourceY * SIZE + sourceX];
                            grid[sourceY * SIZE + sourceX] = 0;
                            source = intermediate;
                            sourceX = source % SIZE;
                            sourceY = source / SIZE;
                        }
                        else
                        {
                            if (!merged[intermediateY * SIZE + intermediateX] &&
                                grid[intermediateY * SIZE + intermediateX] == grid[sourceY * SIZE + sourceX])
                            {
                                grid[sourceY * SIZE + sourceX] = 0;
                                grid[intermediateY * SIZE + intermediateX] *= 2;
                                merged[intermediateY * SIZE + intermediateX] = true;
                                turnScore += grid[intermediateY * SIZE + intermediateX];
                            }

                            break;
                        }
                    }
                }
            }

            return turnScore;
        }
    }
}