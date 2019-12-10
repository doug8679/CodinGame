using System;
using System.Collections.Generic;
using Code4Life;

class Player
{
    static void Run(string[] args)
    {
        string[] inputs;
        string line;
        int projectCount = int.Parse(Console.ReadLine());
        List<Project> projects = new List<Project>();
        for (int i = 0; i < projectCount; i++)
        {
            line = Console.ReadLine();
            Console.Error.WriteLine(line);
            inputs = line.Split(' ');
            int a = int.Parse(inputs[0]);
            int b = int.Parse(inputs[1]);
            int c = int.Parse(inputs[2]);
            int d = int.Parse(inputs[3]);
            int e = int.Parse(inputs[4]);
            Project.Projects.Add(new Project(a,b,c,d,e));
        }

        Bot bot = new Bot(Project.GetEasiestProject());
        // game loop
        while (true)
        {
            List<Sample> samples = new List<Sample>();
            int[] available = new int[5];
            
            line = Console.ReadLine();
            Console.Error.WriteLine(line);
            inputs = line.Split(' ');
            bot.Update(inputs[0], int.Parse(inputs[1]), int.Parse(inputs[2]), int.Parse(inputs[3]), int.Parse(inputs[4]), int.Parse(inputs[5]), int.Parse(inputs[6]), int.Parse(inputs[7]), int.Parse(inputs[8]), int.Parse(inputs[9]), int.Parse(inputs[10]), int.Parse(inputs[11]), int.Parse(inputs[12]));
            
            Console.ReadLine();
            line = Console.ReadLine();
            Console.Error.WriteLine(line);
            inputs = line.Split(' ');
            int availableA = int.Parse(inputs[0]);
            int availableB = int.Parse(inputs[1]);
            int availableC = int.Parse(inputs[2]);
            int availableD = int.Parse(inputs[3]);
            int availableE = int.Parse(inputs[4]);
            Molecule.Available = new int[]{availableA, availableB, availableC, availableD, availableE};
            int sampleCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < sampleCount; i++)
            {
                line = Console.ReadLine();
                Console.Error.WriteLine(line);
                inputs = line.Split(' ');
                int sampleId = int.Parse(inputs[0]);
                int carriedBy = int.Parse(inputs[1]);
                int rank = int.Parse(inputs[2]);
                string expertiseGain = inputs[3];
                int health = int.Parse(inputs[4]);
                int costA = int.Parse(inputs[5]);
                int costB = int.Parse(inputs[6]);
                int costC = int.Parse(inputs[7]);
                int costD = int.Parse(inputs[8]);
                int costE = int.Parse(inputs[9]);
                samples.Add(new Sample(sampleId, carriedBy, rank, expertiseGain, health, costA, costB, costC, costD, costE));
            }
            bot.Update(samples);

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");

            Console.WriteLine(bot.Request(samples));
        }
    }
}
