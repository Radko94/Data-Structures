using System;

class Program
{
    static void Main(string[] args)
    {
        ArrayStack<int> kur = new ArrayStack<int>();

        kur.Push(3123);

        Console.WriteLine(kur.Count);
        Console.Write(kur.Pop());
    }
}
