using System;
using System.Collections.Generic;
using System.Linq;

namespace PlagueJunior
{
    /**
     * Auto-generated code below aims at helping you parse
     * the standard input according to the problem statement.
     **/
    public class Solution
    {
        public static void Main(string[] args)
        {
            HashSet<Pad> pads = new HashSet<Pad>();
            int N = int.Parse(Console.ReadLine());
            for (int i = 0; i < N; i++)
            {
                string[] inputs = Console.ReadLine().Split(' ');
                int A = int.Parse(inputs[0]);
                int B = int.Parse(inputs[1]);
                var a = LocatePad(A, pads);
                var b = LocatePad(B, pads);
                a.AddNeighbor(b);
                b.AddNeighbor(a);
            }

            var target = pads.OrderByDescending(p => p.Neighbors.Count).ToArray()[0];
            target.Infected = true;
            WritePads(pads);

            int nights = 0;
            while (pads.Any(p => !p.Infected))
            {
                foreach (var pad in pads.Where(p=> p.Infected).SelectMany(p=> p.Neighbors.Where(n=> !n.Infected)))
                {
                    pad.Infected = true;
                }

                nights++;

                Console.Error.WriteLine($"After {nights} nights:");
                WritePads(pads);
            }

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");

            Console.WriteLine(nights);
        }

        private static void WritePads(HashSet<Pad> pads)
        {
            foreach (var pad in pads)
            {
                Console.Error.WriteLine($"Pad {pad}: {string.Join(", ", pad.Neighbors)}");
            }
        }

        private static Pad LocatePad(int id, HashSet<Pad> pads)
        {
            var result = pads.FirstOrDefault(p => p.Id.Equals(id));
            if (result == null)
            {
                result = new Pad(id);
                pads.Add(result);
            }
            return result;
        }
    }

    public class Pad : IComparable<Pad>, IEquatable<Pad>
    {
        public Pad(int id)
        {
            Id = id;
            Neighbors = new HashSet<Pad>();
        }

        public int Id { get; }
        public HashSet<Pad> Neighbors { get; set; }
        public bool Infected { get; set; }

        public void AddNeighbor(Pad neighbor)
        {
            Neighbors.Add(neighbor);
        }

        #region Overrides of Object

        public override bool Equals(object obj)
        {
            return Equals((Pad) obj);
        }

        #region Equality members

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"{(Infected ? "*" : "")}{Id}";
        }

        #endregion

        #endregion

        #region Implementation of IComparable<in Pad>

        public int CompareTo(Pad other)
        {
            return Id.CompareTo(other.Id);
        }

        #endregion

        #region Implementation of IEquatable<Pad>

        public bool Equals(Pad other)
        {
            return Id.Equals(other?.Id ?? -1);
        }

        #endregion
    }
}