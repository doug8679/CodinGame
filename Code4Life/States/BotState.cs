
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
            return bot.Connect("2");
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
            var smp = samples.FirstOrDefault(s=>s.Costs[0]==-1);
            if (smp == null) {
                //smp = samples.OrderByDescending(s=>s.Health).First(s=>s.Carrier==0);
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
            return bot.Move("MOLECULES");
        }
    }
    public class AtMolecules : BotState {
        private static Lazy<BotState> _instance = new Lazy<BotState>(()=>new AtMolecules());
        public static BotState Instance => _instance.Value;
        public override string Handle(Bot bot, List<Sample> samples) {
            string result;
            var smp = samples.First(s=>s.Carrier==0);
            var request = string.Empty;
            int i;
            for (i=0; i<5; i++) {
                if (smp.Costs[i] > bot.Storage[i]) {
                    request = ((char)(i + 65)).ToString();
                    break;
                }
            }
            Console.Error.WriteLine($"Request string at molecule module: {request}");
            if (request == string.Empty) {
                result = bot.Move("LABORATORY");
                //bot.State = new AtLaboratory();
            } else {
                result = bot.Connect(request);
                smp.Costs[i]--;
                bot.Storage[i]++;
            }
            return result;
        }
    }
    public class ToLaboratory : BotState {
        private static Lazy<BotState> _instance = new Lazy<BotState>(()=>new ToLaboratory());
        public static BotState Instance => _instance.Value;
        public override string Handle(Bot bot, List<Sample> samples) {
            return bot.Move("LABORATORY");
        }
    }
    public class AtLaboratory : BotState {
        private static Lazy<BotState> _instance = new Lazy<BotState>(()=>new AtLaboratory());
        public static BotState Instance => _instance.Value;
        public override string Handle(Bot bot, List<Sample> samples) {
            var smp = samples.First(s => s.Carrier == 0);
            string result = bot.Connect(smp.Id.ToString());
            bot.ClearStorage();
            return result;
        }
    }
}