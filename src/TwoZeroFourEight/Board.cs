using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace TwoZeroFourEight
{
    public class Board {
        public const int SIZE = 4;
        private long seed;
        private int[] grid = new int[SIZE*SIZE];

        public Board[] Next = new Board[4];

        public Board(int seed) {
            this.seed = seed;
            SpawnTile();
            SpawnTile();
        }

        public Board(long seed, int score, int[] cells) {
            this.seed = seed;
            this.grid = cells;
            Score = score;
        }

        private Board(Board source, int iDir)
        {
            Array.Copy(source.grid, grid, SIZE*SIZE);
            seed = source.seed;
            Score = source.Score + ApplyMove(iDir);
            Moves = source.Moves + 1;
            SpawnTile();
        }

        public Board CloneWithMove(int iDir, int lookAhead=0)
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
            for (int x = 0; x < SIZE; x++) {
                for (int y = 0; y < SIZE; y++) {
                    if (grid[y*SIZE+x] == 0) freeCells.Add(y*SIZE + x);
                }
            }

            int spawnIndex = freeCells[(int) seed % freeCells.Count];
            int value = (seed & 0x10) == 0 ? 2 : 4;

            grid[spawnIndex] = value;

            seed = seed * seed % 50515093L;
        }

        public int Score { get; set; }

        public long Seed => seed;

        public int Moves { get; }

        public bool canMove() {
            for (int i = 0; i < 4; i++) {
                if (TestMove(i, out var _)) return true;
            }
            return false;
        }

        public bool TestMove(int dir, out int score) {
            int[] backup = new int[SIZE*SIZE];
            for (int x = 0; x < SIZE; x++) {
                for (int y = 0; y < SIZE; y++) backup[y*SIZE+x] = grid[y*SIZE+x];
            }

            score = ApplyMove(dir);
            bool changed = false;
            for (int x = 0; x < SIZE; x++) {
                for (int y = 0; y < SIZE; y++) {
                    changed |= grid[y*SIZE+x] != backup[y*SIZE+x];
                    grid[y*SIZE+x] = backup[y*SIZE+x];
                }
            }

            Console.Error.WriteLine($"TestMove :: dir = {dir}, score = {score}, changed = {changed}");
            return changed;
        }

        public int ApplyMove(int dir) {
            int turnScore = 0;
            bool[] merged = new bool[SIZE*SIZE];
            int targetStart = new int[]{0, SIZE - 1, SIZE * (SIZE - 1), 0}[dir];
            int targetStep = new int[]{1, SIZE, 1, SIZE}[dir];
            int sourceStep = new int[]{SIZE, -1, -SIZE, 1}[dir];

            for (int i = 0; i < SIZE; i++) {
                int finalTarget = targetStart + i * targetStep;
                for (int j = 1; j < SIZE; j++) {
                    int source = finalTarget + j * sourceStep;
                    int sourceX = source % SIZE;
                    int sourceY = source / SIZE;
                    if (grid[sourceY*SIZE+sourceX] == 0) continue;
                    for (int k = j - 1; k >= 0; k--) {
                        int intermediate = finalTarget + k * sourceStep;

                        int intermediateX = intermediate % SIZE;
                        int intermediateY = intermediate / SIZE;
                        if (grid[intermediateY*SIZE+intermediateX] == 0) {
                            grid[intermediateY*SIZE+intermediateX] = grid[sourceY*SIZE+sourceX];
                            grid[sourceY*SIZE+sourceX] = 0;
                            source = intermediate;
                            sourceX = source % SIZE;
                            sourceY = source / SIZE;
                        } else {
                            if (!merged[intermediateY*SIZE+intermediateX] && grid[intermediateY*SIZE+intermediateX] == grid[sourceY*SIZE+sourceX]) {
                                grid[sourceY*SIZE+sourceX] = 0;
                                grid[intermediateY*SIZE+intermediateX] *= 2;
                                merged[intermediateY*SIZE+intermediateX] = true;
                                turnScore += grid[intermediateY*SIZE+intermediateX];
                            }
                            break;
                        }
                    }
                }
            }
            return turnScore;
        }

        public override string ToString()
        {
            var b = new StringBuilder();
            b.AppendLine($"SEED: {Seed}");
            b.AppendLine($"SCORE: {Score}");
            b.AppendLine($"MOVES: {Moves}");
            for (int x = 0; x < 4; x++)
            {
                b.AppendLine(string.Join(" ", grid.Skip(x * SIZE).Take(SIZE)));
            }

            b.AppendLine();
            return b.ToString();
        }

        public void LookAhead(int turns)
        {
            // Do look ahead
            for (int i = 0; i < 4; i++)
            {
                Next[i] = CloneWithMove(i, turns - 1);
            }
        }

        public static Board CreateWithLookAhead(long seed, int score, int[] cells, int turnsAhead)
        {
            var b = new Board(seed, score, cells);
            for (int i = 0; i < 4; i++)
            {
                b.Next[i] = b.CloneWithMove(i, turnsAhead - 1);
            }
            return b;
        }
    }
}