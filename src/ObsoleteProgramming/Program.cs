using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
public class Solution
{
    private Queue<string> _instructions;
    private Stack<int> _stack;
    private Dictionary<string, List<string>> _customOperations;
    private StringBuilder _builder;

    public Solution()
    {
        _builder = new StringBuilder();
        _instructions = new Queue<string>();
        _stack = new Stack<int>();
        _customOperations = new Dictionary<string, List<string>>();
    }

    public void AddInstruction(string instruction)
    {
        _instructions.Enqueue(instruction);
    }

    public string Solve()
    {
        while (_instructions.Any())
        {
            var instr = _instructions.Dequeue();
            ProcessInstruction(instr, _instructions);
        }

        return _builder.ToString();
    }

    private void ProcessInstruction(string instr, Queue<string> instrQueue)
    {
        int a, b;
        if (int.TryParse(instr, out var num)) 
        {
            _stack.Push(num);
        } 
        else
        {
            switch (instr.ToUpper())
            {
                case "ADD":
                    Add();
                    break;
                case "SUB":
                    Subtract();
                    break;
                case "MUL":
                    Multiply();
                    break;
                case "DIV":
                    Divide();
                    break;
                case "MOD":
                    Modulo();
                    break;
                case "POP":
                    _stack.Pop();
                    break;
                case "DUP":
                    _stack.Push(_stack.Peek());
                    break;
                case "SWP":
                    Swap();
                    break;
                case "ROT":
                    a = _stack.Pop();
                    b = _stack.Pop();
                    var c = _stack.Pop();
                    _stack.Push(b);
                    _stack.Push(a);
                    _stack.Push(c);
                    break;
                case "OVR":
                    b = _stack.Pop();
                    a = _stack.Pop();
                    _stack.Push(a);
                    _stack.Push(b);
                    _stack.Push(a);
                    break;
                case "POS":
                    a = _stack.Pop();
                    _stack.Push(a >= 0 ? 1 : 0);
                    break;
                case "NOT":
                    a = _stack.Pop();
                    _stack.Push(a == 0 ? 1 : 0);
                    break;
                case "OUT":
                    _builder.AppendLine($"{_stack.Pop()}");
                    break;
                case "DEF":
                    // Build custom operation
                    DefineNewOperation(instrQueue);
                    break;
                case "IF":
                    // Process test, check top of stack for a non-zero value
                    ProcessFlowControl(instrQueue);
                    break;
                default:
                    HandleCustomOperation(instr);
                    break;
            }
        }
    }

    private void HandleCustomOperation(string name)
    {
        if (_customOperations.TryGetValue(name, out var list))
        {
            var q = new Queue<string>(list);
            while (q.Any()) {
                ProcessInstruction(q.Dequeue(), q);
            }
        } else {
            Console.Error.WriteLine($"UNKNOWN OPERATION: {name}");
        }
    }

    private void ProcessFlowControl(Queue<string> ops)
    {
        Queue<string> trueBranch = new Queue<string>(), falseBranch = new Queue<string>();
        HarvestBranches(trueBranch, falseBranch, ops);
        if (_stack.Pop() != 0)
        {
            while (trueBranch.Any())
            {
                ProcessInstruction(trueBranch.Dequeue(), trueBranch);
            }
        }
        else
        {
            while (falseBranch.Any())
            {
                ProcessInstruction(falseBranch.Dequeue(), falseBranch);
            }
        }
    }
    private void HarvestBranches(Queue<string> tb, Queue<string> fb, Queue<string> ops)
    {
        Queue<string> current = tb;
        string op = ops.Dequeue();
        int level = 0;
        bool stop = op.Equals("FI") && level == 0;
        while (!stop)
        {
            bool skip = false;
            switch (op) 
            {
                case "IF":
                    level++;
                    break;
                case "ELS":
                    if (level == 0) {
                        current = fb;
                        skip=true;
                    }
                    break;
                case "FI":
                    if (level > 0) level--;
                    break;
            }
            
            if (!skip)
            {
                current.Enqueue(op);
            }

            op = ops.Dequeue();
            stop = op.Equals("FI") && level == 0;
        }
    }

    private void ProcessUntilInstruction(string stop, Queue<string> ops)
    {
        var op = ops.Dequeue();
        while (!op.Equals(stop))
        {
            ProcessInstruction(op, ops);
        }
    }

    private void SkipToOperation(string opName, Queue<string> ops)
    {
        var op = ops.Dequeue();
        while (!op.Equals(opName))
            op = ops.Dequeue();
    }

    private void Add()
    {
        var b = _stack.Pop();
        var a = _stack.Pop();
        _stack.Push(a + b);
    }
    private void Subtract()
    {
        var b = _stack.Pop();
        var a = _stack.Pop();
        _stack.Push(a - b);
    }
    private void Multiply()
    {
        var b = _stack.Pop();
        var a = _stack.Pop();
        _stack.Push(a * b);
    }
    private void Divide()
    {
        var b = _stack.Pop();
        var a = _stack.Pop();
        _stack.Push(a / b);
    }
    private void Modulo()
    {
        var b = _stack.Pop();
        var a = _stack.Pop();
        _stack.Push(a % b);
    }
    private void Swap()
    {
        var b = _stack.Pop();
        var a = _stack.Pop();
        _stack.Push(b);
        _stack.Push(a);
    }

    private void DefineNewOperation(Queue<string> ops)
    {
        var name = ops.Dequeue().ToUpper();
        var func = new List<string>();
        var op = ops.Dequeue().ToUpper();
        while (!op.Equals("END")) {
            func.Add(op);
            op = ops.Dequeue().ToUpper();
        }
        _customOperations.Add(name, func);
    }

    public static void Main(string[] args)
    {
        var sln = new Solution();
        int N = int.Parse(Console.ReadLine());
        Console.Error.WriteLine($"N = {N}");
        for (int i = 0; i < N; i++)
        {
            var ops = Console.ReadLine().Trim().Split(' ');
            Console.Error.WriteLine($"Operation line = {string.Join(" ", ops)}");
            foreach (var op in ops)
                sln.AddInstruction(op);
        }

        // Write an answer using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");

        Console.Write(sln.Solve());
    }
}