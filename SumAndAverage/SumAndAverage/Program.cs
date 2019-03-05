using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace SumAndAverage
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] well = Console.ReadLine().Split();

            int count = 0;
            string elements = "";

            elements += well[0];
            for (int i = 1; i < well.Length; i++)
            {
                if (well[i] == well[i - 1])
                {
                    count++;
                    elements += well[i];
                }
                else
                {
                    count = 0;
                    elements = "";
                }
            }

            Console.WriteLine(elements);
        }
    }
}
