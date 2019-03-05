using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseNumbersWithStack
{
    class Program
    {
        static void Main(string[] args)
        {
            Stack<int> well = new Stack<int>();

            string ok = Console.ReadLine();

            if (String.IsNullOrEmpty(ok))
            {
                Console.WriteLine("(empty)");
            }
            else
            {
                string[] ohWell = ok.Split();
                for (int i = 0; i < ohWell.Length; i++)
                {
                    well.Push(int.Parse(ohWell[i]));
                }

                for (int i = 0; i < ohWell.Length; i++)
                {
                    Console.Write(well.Pop() + " ");
                }
            }
        }
    }
}
