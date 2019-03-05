using System;
public class Person : IComparable
{
    public Person(string name, double salary)
    {
        this.Name = name;
        this.Salary = salary;
    }

    public string Name { get; set; }
    public double Salary { get; set; }

    public int CompareTo(object obj)
    {
        Person other = (Person)obj;

        if (Name == other.Name)
        {
            if (Salary == other.Salary) return 0;
            else if (Salary > other.Salary) return 1;
            else return -1;
        }
        else if (Name.Length > other.Name.Length) return 1;
        else return -1;
    }

    public override bool Equals(object obj)
    {
        if (!(obj is Person))
            return false;

        Person other = (Person)obj;

        if (Name == other.Name)
        {
            if (Salary == other.Salary) return true;
            else return false;
        }
        else return false;
    }
}
