using System;
using System.Collections.Generic;
using System.Linq;

namespace Code4Life {
    public class Project {
        public static List<Project> Projects = new List<Project>();
        public int[] Costs;

        public Project(int costA, int costB, int costC, int costD, int costE) {
            costs = new int[]{costA,costB,costC,costD,costE};
        }

        public int Score() {
            return costs.Sum() / costs.Length;
        }

        public static Project GetEasiestProject()
        {
            Project result = null;
            int minScore = int.MaxValue;
            foreach (var project in Projects) {
                var score = project.Score();
                if (score < minScore) {
                    result = project;
                    minScore = score;
                }
            }
            return result;
        }
    }
}