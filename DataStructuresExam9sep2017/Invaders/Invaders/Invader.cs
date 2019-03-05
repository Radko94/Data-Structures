using System;

public class Invader : IInvader
{
    public Invader(int damage, int distance)
    {
        Damage = damage;
        Distance = distance;
    }
    
    public int Damage { get; set; }
    public int Distance { get; set; }

    public int CompareTo(IInvader other)
    {
        if (this.Damage == other.Damage)
        {
            if (this.Distance == other.Distance) return 0;
            else if (this.Distance >= other.Distance) return 1;
            else return -1;
        }
        else if (this.Damage >= other.Damage) return 1;
        else return -1;
    }
}
