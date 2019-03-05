using System;
using System.Collections.Generic;
using System.Linq;

public class Computer : IComputer
{
    int energy;
    List<Invader> invaders;

    public Computer(int energy)
    {
        if (energy <= 0)
            throw new ArgumentException();

        Energy = energy;
        invaders = new List<Invader>();
    }

    public int Energy
    {
        get
        {
            if (energy <= 0)
                energy = 0;

            return energy;
        }
        private set { energy = value; }
    }

    public int Count => invaders.Count;

    public void Skip(int turns)
    {
        for (int i = 0; i < turns; i++)
        {
            for (int j = 0; j < invaders.Count; j++)
            {
                invaders[j].Distance--;
                if (invaders[j].Distance < 1)
                {
                    Energy -= invaders[j].Damage;
                    invaders.RemoveAt(j);
                    j--;
                }
            }
        }
    }

    public void AddInvader(Invader invader)
    {
        invaders.Add(invader);
    }

    public void DestroyHighestPriorityTargets(int count)
    {
        int kur = 0;
        foreach (var item in invaders.OrderBy(x => x.Distance).OrderByDescending(x => x.Damage))
        {
            if (kur == count)
                break;
            invaders.Remove(item);
        }
    }

    public void DestroyTargetsInRadius(int radius)
    {
        for (int i = 0; i < invaders.Count; i++)
        {
            if (invaders[i].Distance <= radius)
            {
                invaders.RemoveAt(i);
                i--;
            }
        }
    }

    public IEnumerable<Invader> Invaders()
    {
        return invaders;
    }
}
