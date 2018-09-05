public class Sample {
    public Sample(int id, int carrier, int rank, string gain, int health, int costA, int costB, int costC, int costD, int costE){
        Id = id;
        Carrier = carrier;
        Rank = rank;
        Gain = gain;
        Health = health;
        Costs[0] = costA;
        Costs[1] = costB;
        Costs[2] = costC;
        Costs[3] = costD;
        Costs[4] = costE;
    }
    
    public int Id;
    public int Carrier;
    public int Rank;
    public string Gain;
    public int Health;
    public int[] Costs = new int[5];
}