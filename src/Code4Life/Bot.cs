using System;
using System.Collections.Generic;
using System.Linq;
using Code4Life.States;

namespace Code4Life {
public class Bot {
    List<Sample> _samples;
    public Sample nextSample;
    int[] _storage;
    int[] _expertise;
    int _eta;
    int _nextRank = 0;
    BotState _state;
    private int _sampleCount;
    public bool CanGetSamples => _sampleCount < 3;
    public bool CanGetMolecules => _storage.Sum()<10;
    Project project;

    public Bot(Project p) {
        Uninitialized = true;
        _storage = new int[5];
        _expertise = new int[5];
        _sampleCount = 0;
        _samples = new List<Sample>();
        _state = AtStart.Instance;
        project = p;
    }

    public BotState State {
        get => _state;
        set {
            _state = value;
            Console.Error.WriteLine($"State: {_state?.GetType().Name ?? "NULL"}");
        }
    }
    public int ETA {
        get => _eta;
        set {
            _eta = value;
            Console.Error.WriteLine($"ETA: {_eta}");
        }
    }

    public int[] Storage => _storage;
    public int[] Expertise => _expertise;
    
    public bool Uninitialized { get; set; }
    
    public bool CanResearch(Sample sample) {
        bool result = true;
        for (int i=0; i<5; i++)
            result &= sample.Costs[i]<=(_storage[i] + _expertise[i]);
        
        return result;
    }

    public bool CouldResearch(Sample sample) {
        bool result = true;
        for (int i=0; i<5; i++)
            result &= sample.Costs[i]<=(_storage[i] + _expertise[i] + Molecule.Available[i]);
        
        return result;
    }

    public void SelectBestSample(List<Sample> samples) {
        nextSample = null;
        nextSample = samples.FirstOrDefault(s => CouldResearch(s) && ProjectGains(s)) ?? samples.FirstOrDefault(s => CouldResearch(s));
    }

        private bool ProjectGains(Sample s)
        {
            int index = s.Gain[0]-65;
            return project.Costs[index] > Expertise[index];
        }

        public string Request(List<Sample> samples) {
        return _state.Handle(this, samples);
    }
    
    public void Update(string target, int eta, int score, int storageA, int storageB, int storageC, int storageD, int storageE, int expertiseA, int expertiseB, int expertiseC, int expertiseD, int expertiseE) {
        ETA = eta;
        _storage[0] = storageA;
        _storage[1] = storageB;
        _storage[2] = storageC;
        _storage[3] = storageD;
        _storage[4] = storageE;
        _expertise[0] = expertiseA;
        _expertise[1] = expertiseB;
        _expertise[2] = expertiseC;
        _expertise[3] = expertiseD;
        _expertise[4] = expertiseE;
        switch (target) {
            case "START_POS":
                State = eta==0 ? AtStart.Instance : ToStart.Instance;
                break;
            case "SAMPLES":
                State = eta==0? AtSamples.Instance : ToSamples.Instance;
                break;
            case "DIAGNOSIS":
                State = eta==0 ? AtDiagnosis.Instance : ToDiagnosis.Instance;
                break;
            case "MOLECULES":
                State = eta == 0 ? AtMolecules.Instance : ToMolecules.Instance;
                break;
            case "LABORATORY":
                State = eta==0 ? AtLaboratory.Instance : ToLaboratory.Instance;
                break;
        }
        Uninitialized = false;
    }
    
    public void ClearStorage() {
        for (int i=0; i<_storage.Length; i++)
            _storage[i] = 0;
    }
    
    public string Connect(string request) {
        string result = $"CONNECT {request}";
        Console.Error.WriteLine(result);
        
        if (_state is AtSamples) {
            _sampleCount++;
        } else if (_state is AtDiagnosis) {
        } else if (_state is AtMolecules) {
        } else if (_state is AtLaboratory) {
            _sampleCount--;
        }

        return result;
    }
    public string Move(string target) {
        string result = $"GOTO {target}";
        Console.Error.WriteLine(result);
        return result;
    }
    public string Wait() {
        Console.Error.WriteLine("WAIT");
        return "WAIT";
    }
    
    public int NextRank() {
        int result = ++_nextRank;
        if (result > 3) {
            result = _nextRank = 1;
        }
        return result;
    }
    public void Update(List<Sample> samples) {
        foreach (var sample in samples.Where(s=>s.Carrier==0)) {
            _samples.Add(sample);
        }
    }
}
}