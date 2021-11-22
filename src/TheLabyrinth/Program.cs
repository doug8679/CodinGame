using System;
using System.Collections.Generic;
using System.Linq;

namespace TheLabyrinth
{
    /**
     * Auto-generated code below aims at helping you parse
     * the standard input according to the problem statement.
     **/
    public class Player
    {
        // Direction vectors
        static int[] dRow = { -1, 0, 1, 0 };
        static int[] dCol = { 0, 1, 0, -1 };
        static int R, C;
        static Queue<int> rq = new Queue<int>(), cq = new Queue<int>();
        static bool exploring = true;
        static int nodes_left_in_layer = 1, nodes_in_next_layer = 0;
        static int sr, sc;
        static bool[][] visited;
        static Queue<string> returnPath = new Queue<string>();

        public static void Main(string[] args)
        {
            string[] inputs;
            inputs = Console.ReadLine().Split(' ');
            R = int.Parse(inputs[0]); // number of rows.
            C = int.Parse(inputs[1]); // number of columns.
            int A = int.Parse(inputs[2]); // number of rounds between the time the alarm countdown is activated and the time the alarm goes off.

            char[][] grid = Enumerable.Repeat(Enumerable.Repeat('', C).ToArray(), R).ToArray();
            visited = Enumerable.Repeat(Enumerable.Repeat(false, C).ToArray(), R).ToArray();

            Stack<string> previousCells = new Stack<string>();

            // game loop
            while (true)
            {
                inputs = Console.ReadLine().Split(' ');
                int KR = int.Parse(inputs[0]); // row where Kirk is located.
                int KC = int.Parse(inputs[1]); // column where Kirk is located.
                vis[KR, KC] = true;
                for (int i = 0; i < R; i++)
                {
                    string ROW = Console.ReadLine(); // C of the characters in '#.TC?' (i.e. one line of the ASCII maze).
                    for (int j = 0; j < ROW.Length; j++)
                    {
                        grid[i][j] = ROW[j];
                        if (ROW[j] == 'T')
                        {
                            sr = i; sc = j;
                        }
                    }
                }

                if (exploring)
                {
                    Console.Error.WriteLine("EXPLORING");
                    Console.WriteLine(getNextDirection(grid, vis, KR, KC, previousCells));
                }
                else
                {
                    Console.Error.WriteLine("!! ESCAPING !!");
                    Console.WriteLine(returnPath.Dequeue());
                }
            }
        }

        private static string getNextDirection(char[,] grid, bool[,] vis, int kR, int kC, Stack<string> previousCells)
        {
            for (int i = 0; i < 4; i++)
            {
                int r = kR + dRow[i];
                int c = kC + dCol[i];
                char cell = grid[r, c];
                Console.Error.WriteLine($"Cell ({c}, {r}) = {cell}");
                if ((cell == '.' || cell == 'C') && !vis[r, c])
                {
                    if (cell == 'C')
                    {
                        exploring = false;
                        BuildReturnPath(grid, r, c);
                        Console.Error.WriteLine("Found the control room, go home!");
                    }
                    return DirectionName(i, previousCells);
                }
            }
            return previousCells.Pop();
        }

        private static void BuildReturnPath(char[,] grid, int r, int c)
        {
            int curX = c, curY = r, dx, dy;
            string dir = "";
            var endNode = AStar(grid, startX, startY, c, r);
            while (endNode.x != startX || endNode.y != startY)
            {
                dir = "";
                dx = curX - endNode.x;
                dy = curY - endNode.y;

                if (dx == 0 && dy == 0)
                {
                    Console.Error.WriteLine($"Nodes are equal at ({c}, {r}), do nothing.");
                    endNode = endNode.parent;
                    continue;
                }

                if (dx < 0 && dy == 0)
                {
                    dir = "RIGHT";
                }
                else if (dx > 0 && dy == 0)
                {
                    dir = "LEFT";
                }
                else if (dx == 0 && dy < 0)
                {
                    dir = "DOWN";
                }
                else if (dx == 0 && dy > 0)
                {
                    dir = "UP";
                }
                returnPath.Enqueue(dir);
                curX = endNode.x;
                curY = endNode.y;
                endNode = endNode.parent;
            }
            dx = curX - endNode.x;
            dy = curY - endNode.y;
            dir = "";
            if (dx < 0 && dy == 0)
            {
                dir = "RIGHT";
            }
            else if (dx > 0 && dy == 0)
            {
                dir = "LEFT";
            }
            else if (dx == 0 && dy < 0)
            {
                dir = "DOWN";
            }
            else if (dx == 0 && dy > 0)
            {
                dir = "UP";
            }
            returnPath.Enqueue(dir);

            Console.Error.WriteLine($"RETURN PATH: {string.Join(" -> ", returnPath.ToList())}");
        }

        private static Node AStar(char[,] matrix, int fromX, int fromY, int toX, int toY)
        {
            Dictionary<string, Node> greens = new Dictionary<string, Node>();
            Dictionary<string, Node> reds = new Dictionary<string, Node>();

            Node startNode = new Node { x = fromX, y = fromY, sum = 0 };
            string key = startNode.x.ToString() + startNode.y.ToString();
            greens.Add(key, startNode);

            Func<KeyValuePair<string, Node>> smallestGreen = () =>
            {
                KeyValuePair<string, Node> smallest = greens.ElementAt(0);

                foreach (KeyValuePair<string, Node> item in greens)
                {
                    if (item.Value.sum < smallest.Value.sum)
                        smallest = item;
                    else if (item.Value.sum == smallest.Value.sum
                            && item.Value.to < smallest.Value.to)
                        smallest = item;
                }

                return smallest;
            };


            List<KeyValuePair<int, int>> fourNeighbors = new List<KeyValuePair<int, int>>()
                                            { new KeyValuePair<int, int>(-1,0),
                                              new KeyValuePair<int, int>(0,1),
                                              new KeyValuePair<int, int>(1, 0),
                                              new KeyValuePair<int, int>(0,-1) };

            int maxX = C;
            if (maxX == 0)
                return null;
            int maxY = R;

            while (true)
            {
                if (greens.Count == 0)
                    return null;

                KeyValuePair<string, Node> current = smallestGreen();
                if (current.Value.x == toX && current.Value.y == toY)
                    return current.Value;

                greens.Remove(current.Key);
                reds.Add(current.Key, current.Value);

                foreach (KeyValuePair<int, int> plusXY in fourNeighbors)
                {
                    int nbrX = current.Value.x + plusXY.Key;
                    int nbrY = current.Value.y + plusXY.Value;
                    string nbrKey = nbrX.ToString() + nbrY.ToString();
                    if (nbrX < 0 || nbrY < 0 || nbrX >= maxX || nbrY >= maxY
                        || matrix[nbrY, nbrX] == '#'
                        || reds.ContainsKey(nbrKey))
                        continue;

                    if (greens.ContainsKey(nbrKey))
                    {
                        Node curNbr = greens[nbrKey];
                        int from = Math.Abs(nbrX - fromX) + Math.Abs(nbrY - fromY);
                        if (from < curNbr.fr)
                        {
                            curNbr.fr = from;
                            curNbr.sum = current.Value.sum + 1;
                            curNbr.parent = current.Value;
                        }
                    }
                    else
                    {
                        Node curNbr = new Node { x = nbrX, y = nbrY };
                        curNbr.fr = Math.Abs(nbrX - fromX) + Math.Abs(nbrY - fromY);
                        curNbr.to = Math.Abs(nbrX - toX) + Math.Abs(nbrY - toY);
                        curNbr.sum = current.Value.sum + 1;
                        curNbr.parent = current.Value;
                        greens.Add(nbrKey, curNbr);
                    }
                }
            }
        }

        private static string DirectionName(int i, Stack<string> returnPath)
        {
            switch (i)
            {
                case 0:
                    returnPath.Push("DOWN");
                    return "UP";
                case 1:
                    returnPath.Push("LEFT");
                    return "RIGHT";
                case 2:
                    returnPath.Push("UP");
                    return "DOWN";
                default:
                    returnPath.Push("RIGHT");
                    return "LEFT";
            };
        }
    }

    class Node
    {
        public int x, y;
        public int fr = 0, to = 0, sum = 0;
        public Node parent;
    }
}