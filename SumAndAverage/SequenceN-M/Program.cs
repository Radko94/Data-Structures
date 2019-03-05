using System;
using System.Collections.Generic;
using System.Linq;

namespace SequenceN_M
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] input = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
            Queue<int> thatQueue = new Queue<int>();

            if (input[0] < input[1])
                thatQueue.Enqueue(input[0]);

            while (thatQueue.Count > 0)
            {
                var item = thatQueue.Peek();

                if (item < input[1])
                {
                    thatQueue.Enqueue(item + 1);
                    thatQueue.Enqueue(item + 2);
                    thatQueue.Enqueue(item * 2);
                }
            }

            Console.WriteLine(String.Join(" -> ", thatQueue.Dequeue()));
        }
    }
}
