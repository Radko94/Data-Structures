using System;
using System.Collections.Generic;

public class Playground
{
    // Fast Review Here
    private static char[,] map =
    {
                    { '-', '-', '-', '-', '-', '-', '-', '-' },
                    { '-', '-', '*', '-', '-', '-', '-', '-' },
                    { 'W', 'W', 'W', 'W', 'W', '-', '-', '-' },
                    { '-', '-', '-', '-', 'W', 'W', '-', '-' },
                    { '-', '-', '-', 'P', 'W', '-', '-', '-' },
                    { '-', '-', '-', '-', '-', '-', '-', '-' }
    };

    // See ReadMap Below
    // There must be a problem
    static void Main()
    {
        map = ReadMap();

        var start = FindGoal('P');
        var goal = FindGoal('*');

        var aStar = new AStar(map);
        var path = aStar.GetPath(start, goal);

        foreach (var node in path)
        {
            var row = node.Row;
            var col = node.Col;
            map[row, col] = '@';
        }

        PrintMap();

    }

    private static char[,] ReadMap()
    {
        List<string> getMaze = new List<string>();

        string input = Console.ReadLine();
        getMaze.Add(input);

        while (string.IsNullOrEmpty(input))
        {
            input = Console.ReadLine();
            getMaze.Add(input);
        }

        var map = new char[getMaze.Count, getMaze[0].Length];

        for (int i = 0; i < map.GetLength(0); i++)
        {
            char[] line = getMaze[i].ToCharArray();
            for (int j = 0; j < map.GetLength(1); j++)
            {
                map[i, j] = line[j];
            }
        }

        return map;
    }

    static Node FindGoal(char goal)
    {
        for (int row = 0; row < map.GetLength(0); row++)
        {
            for (int col = 0; col < map.GetLength(1); col++)
            {
                if (map[row, col] == goal)
                {
                    return new Node(row, col);
                }
            }
        }

        throw new ArgumentException("Object not present on map");
    }

    static void PrintMap()
    {
        //what the hell man, why do you need colors here, just WTF

        for (int row = 0; row < map.GetLength(0); row++)
        {
            for (int col = 0; col < map.GetLength(1); col++)
            {
                if (map[row, col] == '@')
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }

                Console.Write(map[row, col]);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" ");
            }

            Console.WriteLine();
        }
    }
}
