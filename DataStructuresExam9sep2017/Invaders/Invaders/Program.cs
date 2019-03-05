using System.Collections.Generic;

public class Program
{
    static void Main(string[] args)
    {
        Computer computer = new Computer(100);
        List<Invader> actual = new List<Invader>();

        for (int i = 1; i <= 100; i++)
        {
            Invader invader = new Invader(i, i);
            computer.AddInvader(invader);
            if (i > 50)
            {
                actual.Add(invader);
            }
        }

        computer.DestroyTargetsInRadius(50);

        //CollectionAssert.AreEqual(actual, computer.Invaders().ToList(), "Collections not equal");
    }
}
