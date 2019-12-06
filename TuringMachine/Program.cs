using System;
using System.Collections.Generic;

namespace TuringMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] inputs = Console.ReadLine().Split(' ');
            int S = int.Parse(inputs[0]);
            int T = int.Parse(inputs[1]);
            string[] tape = new string[T];
            Array.Fill(tape, "0");
            int X = int.Parse(inputs[2]);
            string START = Console.ReadLine();
            int N = int.Parse(Console.ReadLine());
            Dictionary<string, State> states = new Dictionary<string, State> { 
                {"HALT", new State{Name="HALT"}},
                {"OOB_LEFT", new State{Name="OOB_LEFT"}},
                {"OOB_RIGHT", new State{Name="OOB_RIGHT"}},
            };
            for (int i = 0; i < N; i++)
            {
                string STATEACTIONS = Console.ReadLine();
                var s = new State(STATEACTIONS);
                states.Add(s.Name, s);
            }
            State curState = states[START];
            foreach(var state in states.Values) {
                Console.WriteLine($"State: {state}");
            }
            
            int actions = 0;
            while (!curState.Name.Equals("HALT")) {
                curState = curState.DoAction(ref X, tape, states);
                actions++;
                if (curState.Name.Contains("OOB")) {
                    //Do someting here too...
                    break;
                }
            }
            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");
            
            Console.WriteLine(actions);
            Console.WriteLine(X);
            Console.WriteLine(string.Join("", tape));
        }
    }

    class State {
        public string Name{get;set;}
        public List<Action> Actions {get;set;}

        public State() {
            Actions = new List<Action>();
        }
        public State(string stateActions)  {
            Actions = new List<Action>();
            var pieces = stateActions.Split(":".ToCharArray());
            Name = pieces[0];
            var actions = pieces[1].Split(";".ToCharArray());
            foreach (var action in actions) {
                Actions.Add(new Action(action));
            }
        }

        public State DoAction(ref int cell, string[] tape, Dictionary<string, State> states) {
            Console.Error.WriteLine($"At cell {cell}, current value is: {tape[cell]}...");
            var action = Actions[int.Parse(tape[cell])];
            tape[cell] = action.Write;
            cell += action.Move;
            Console.Error.WriteLine($"After action, new cell is {cell}, next state is '{action.Next}'.");
            return states[action.Next];
        }

        public override string ToString() {
            return $"{Name}:{string.Join(";", Actions)}";
        }
    }

    class Action {
        public string Write {get;set;}
        public int Move {get;set;}
        public string Next{get;set;}

        public Action(string action) {
            var pieces = action.Split(" ".ToCharArray());
            Write = pieces[0];
            switch (pieces[1]) {
                case "L":
                    Move = -1;
                    break;
                case "R":
                    Move = 1;
                    break;
            }
            Next = pieces[2];
        }

        public override string ToString() {
            return $"{Write} {(Move < 0 ? "L" : "R")} {Next}";
        }
    }
}
