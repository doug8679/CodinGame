using System.Collections.Generic;

public interface WeightedGraph<L>
{
    double Cost(L a, L b);
    IEnumerable<L> Neighbors(L id);
}
