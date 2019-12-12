using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HungerGames
{

    /**
     * Auto-generated code below aims at helping you parse
     * the standard input according to the problem statement.
     **/
    public class HungerGame
    {
        private string[] _turns;

        List<Tribute> _tributes;

        public HungerGame(string[] tributes, string[] turns)
        {
            _tributes = new List<Tribute>();
            foreach (var trib in tributes)
            {
                _tributes.Add(new Tribute(trib));
            }
            _turns = turns;
        }

        public string Solve()
        {
            foreach (var turn in _turns)
            {
                var pieces = Regex.Split(turn, " killed ");
                var trib = _tributes.FirstOrDefault(t => t.name.Equals(pieces[0]));
                var victims = Regex.Split(pieces[1], ", ").Select(t => _tributes.FirstOrDefault(p => p.name.Equals(t))).ToList();
                trib.AddVictims(victims);
            }
            _tributes.Sort();

            var answer = string.Join(Environment.NewLine + Environment.NewLine, _tributes.Select(t => t.ToString()));
            Console.Error.WriteLine(answer);
            return answer;
        }

        static void Main(string[] args)
        {
            string[] tributes = new string[int.Parse(Console.ReadLine())];
            for (int i = 0; i < tributes.Length; i++)
            {
                tributes[i] = Console.ReadLine();
            }
            string[] turns = new string[int.Parse(Console.ReadLine())];
            for (int i = 0; i < turns.Length; i++)
            {
                turns[i] = Console.ReadLine();
            }

            var game = new HungerGame(tributes, turns);
            Console.WriteLine(game.Solve());
        }
    }

    public class Tribute : IComparable<Tribute>
    {
        public string name;
        List<string> _victims;
        public string killer;

        public Tribute(string name)
        {
            this.name = name;
            _victims = new List<string>();
        }

        public void AddVictims(IEnumerable<Tribute> victims)
        {
            foreach (var victim in victims)
            {
                victim.killer = name;
                _victims.Add(victim.name);
            }
        }

        public override string ToString()
        {
            StringBuilder b = new StringBuilder();
            _victims.Sort();
            b.AppendLine($"Name: {name}");
            var victimString = (_victims.Count == 0) ? "None" : string.Join(", ", _victims);
            b.AppendLine($"Killed: {victimString}");
            var killerString = (string.IsNullOrEmpty(killer)) ? "Winner" : killer;
            b.Append($"Killer: {killerString}");
            return b.ToString();
        }

        public int CompareTo(Tribute other)
        {
            return name.CompareTo(other.name);
        }
    }
}