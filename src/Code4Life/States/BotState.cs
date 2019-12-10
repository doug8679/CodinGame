
using System;
using System.Collections.Generic;
using System.Linq;

namespace Code4Life.States {
    public abstract class BotState {
        public abstract string Handle(Bot bot, List<Sample> samples);
    }

    public class ToStart : BotState {
        private static Lazy<BotState> _instance = new Lazy<BotState>(()=>new ToStart());
        internal static BotState Instance => _instance.Value;

        public override string Handle(Bot bot, List<Sample> samples) {
            return bot.Move("SAMPLES");
        }
    }
    public class AtStart : BotState {
        private static Lazy<BotState> _instance = new Lazy<BotState>(()=>new AtStart());
        internal static BotState Instance => _instance.Value;

        public override string Handle(Bot bot, List<Sample> samples) {
            return bot.Move("SAMPLES");
        }
    }

    public class ToSamples : BotState {
        private static Lazy<BotState> _instance = new Lazy<BotState>(()=>new ToSamples());
        public static BotState Instance => _instance.Value;
        public override string Handle(Bot bot, List<Sample> samples) {
            return bot.Wait();
        }
    }

    public class AtSamples : BotState {
        private static Lazy<BotState> _instance = new Lazy<BotState>(()=>new AtSamples());
        public static BotState Instance => _instance.Value;

        public override string Handle(Bot bot, List<Sample> samples) {
            return bot.CanGetSamples ? bot.Connect(bot.NextRank().ToString()) : bot.Move("DIAGNOSIS");
        }
        
        private string DetermineRankRequest(Bot bot) {
            string result = "1";
            if (bot.Expertise.Any(e=> e>=3)) {
                result = "3";
            } else if (bot.Expertise.Any(e=>e>=2)) {
                result = "2";
            }
            return result;
        }
    }
    public class ToDiagnosis : BotState {

        private static Lazy<BotState> _instance = new Lazy<BotState>(()=>new ToDiagnosis());
        public static BotState Instance => _instance.Value;
        public override string Handle(Bot bot, List<Sample> samples) {
            return bot.Wait();
        }
    }
    public class AtDiagnosis : BotState {
        private static Lazy<BotState> _instance = new Lazy<BotState>(()=>new AtDiagnosis());
        public static BotState Instance => _instance.Value;
        public override string Handle(Bot bot, List<Sample> samples) {
            string result = "WAIT";
            var smp = samples.FirstOrDefault(s=>s.Costs[0]==-1 && s.Carrier==0);
            if (smp == null) {
                bot.SelectBestSample(samples);
                result = bot.Move("MOLECULES");
            } else {
                result = bot.Connect(smp.Id.ToString());
            }
            return result;
        }
    }
    public class ToMolecules : BotState {

        private static Lazy<BotState> _instance = new Lazy<BotState>(()=>new ToMolecules());
        public static BotState Instance => _instance.Value;
        public override string Handle(Bot bot, List<Sample> samples) {
            return bot.Wait();
        }
    }
    public class AtMolecules : BotState {
        private static Lazy<BotState> _instance = new Lazy<BotState>(()=>new AtMolecules());
        public static BotState Instance => _instance.Value;
        public override string Handle(Bot bot, List<Sample> samples) {
            string result;
            //var smp = samples.OrderByDescending(s=>s.Health).Where(s=>s.Carrier==0 && CanSatisfy(bot, s)).FirstOrDefault();
            var smp = bot.nextSample;
            var request = string.Empty;
            int i;
            for (i=0; i<5; i++) {
                if (smp.Costs[i] > (bot.Storage[i] + bot.Expertise[i])) {
                    request = ((char)(i + 65)).ToString();
                    break;
                }
            }
            if (request == string.Empty) {
                result = bot.Move("LABORATORY");
            } else {
                result = bot.Connect(request);
                smp.Costs[i]--;
                bot.Storage[i]++;
            }
            Console.Error.WriteLine($"Request string at molecule module: {result}");
            return result;
        }
        
        private bool CanSatisfy(Bot b, Sample s) {
            bool result = true;
            for (int i=0; i<5; i++) {
                result &= s.Costs[i] <= (b.Storage[i] + b.Expertise[i] + Molecule.Available[i]);
            }
            return result;
        }
    }
    public class ToLaboratory : BotState {
        private static Lazy<BotState> _instance = new Lazy<BotState>(()=>new ToLaboratory());
        public static BotState Instance => _instance.Value;
        public override string Handle(Bot bot, List<Sample> samples) {
            return bot.Wait();
        }
    }
    public class AtLaboratory : BotState {
        private static Lazy<BotState> _instance = new Lazy<BotState>(()=>new AtLaboratory());
        public static BotState Instance => _instance.Value;
        public override string Handle(Bot bot, List<Sample> samples) {
            string result = string.Empty;
            //var smp = samples.OrderByDescending(s=>s.Health).Where(s=>s.Carrier==0).FirstOrDefault(bot.CanResearch);
            var smp = bot.nextSample;
            if (smp == null) {
                bot.SelectBestSample(samples);
                if (smp == null) {
                    result = bot.Move("SAMPLES");
                } else {
                    result = bot.Move("MOLECULES");
                }
            } else {
                result = bot.Connect(smp.Id.ToString());
            }
            return result;
        }
    }
}