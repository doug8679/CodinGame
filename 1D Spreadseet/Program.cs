using System;

namespace _1D_Spreadseet
{
    class Program
    {
        static void Main(string[] args)
        {
            int N = int.Parse(Console.ReadLine());
            Cell[] cells = new Cell[N];
            for (int i = 0; i < N; i++)
            {
                string[] inputs = Console.ReadLine().Split(' ');
                string operation = inputs[0];
                string arg1 = inputs[1];
                string arg2 = inputs[2];
                var cell = new Cell{ Operation=operation, Arg1=arg1, Arg2=arg2};
                Console.Error.WriteLine(cell);
                cells[i] = cell;
            }
            for (int i = 0; i < N; i++)
            {
                // Write an action using Console.WriteLine()
                // To debug: Console.Error.WriteLine("Debug messages...");
                Console.WriteLine(cells[i].Evaluate(cells));
            }
        }
    }

    class Cell {
        public string Operation { get; set; }
        public string Arg1 { get; set; }
        public string Arg2 { get; set; }

        private double _value = double.NaN;

        public double Evaluate(Cell[] cells) {
            if (_value.Equals(double.NaN)) {
                var val1 = GetValue(Arg1, cells);
                var val2 = GetValue(Arg2, cells);
                
                switch (Operation) {
                    case "VALUE":
                        _value = val1;
                        break;
                    case "ADD":
                        _value = val1 + val2;
                        break;
                    case "SUB":
                        _value = val1 - val2;
                        break;
                    case "MULT":
                        _value = val1 * val2;
                        break;
                }
            }

            return _value;
        }

        private double GetValue(string arg, Cell[] cells) {
            if (arg.StartsWith("$")) {
                // Lookup reference
                return cells[int.Parse(arg.Replace("$", ""))].Evaluate(cells);
            } else if (arg.Equals("_")) {
                return 0;
            } else {
                return double.Parse(arg);
            }
        }

        public override string ToString() {
            return $"{Operation} {Arg1} {Arg2}";
        }
    }
}
