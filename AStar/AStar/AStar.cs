using System;
using System.Collections.Generic;

public class AStar
{
    private char[,] map;

    public AStar(char[,] map)
    {
        this.map = map;
    }

    public static int GetH(Node current, Node goal)
    {
        int deltaX = Math.Abs(current.Col - goal.Col);
        int deltaY = Math.Abs(current.Row - goal.Row);

        return deltaX + deltaY;
    }

    public IEnumerable<Node> GetPath(Node start, Node goal)
    {

        PriorityQueue<Node> frontier = new PriorityQueue<Node>();
        Dictionary<Node, Node> parents = new Dictionary<Node, Node>();
        Dictionary<Node, int> gCost = new Dictionary<Node, int>();
        // 4-3 4-4 3-4 2-4 1-4 1-3 1-2
        frontier.Enqueue(start);
        parents.Add(start, null);
        gCost.Add(start, 0);

        List<Node> visited = new List<Node>();

        while (frontier.Count != 0)
        {
            var current = frontier.Dequeue();

            if (current.Equals(goal))
            {
                return RecostructPath(parents, start, goal);
            }
            
            foreach (var neighbor in Neighbors(current))
            {
                int newCost = gCost[current] + 1;

                if (!gCost.ContainsKey(neighbor) || newCost < gCost[neighbor])
                {
                    neighbor.HCost = newCost;
                    parents[neighbor] = current;
                    gCost[neighbor] = newCost;
                    frontier.Enqueue(neighbor);

                }
            }
        }
        return RecostructPath(parents, start, goal);

    }

    private IEnumerable<Node> RecostructPath(Dictionary<Node, Node> parents, Node start, Node goal)
    {
        if (!parents.ContainsKey(goal))
        {
            return new List<Node>() { start };
        }
        else
        {
            Node current = parents[goal];
            Stack<Node> path = new Stack<Node>();

            path.Push(goal);

            while (!current.Equals(start))
            {
                path.Push(current);
                current = parents[current];
            }

            path.Push(start);
            return path;
        }
    }

    public List<Node> Neighbors(Node thisNode)
    {
        List<Node> neighbor = new List<Node>(4);

        int row = map.GetLength(0);
        int col = map.GetLength(1);

        if (thisNode.Row >= 0)
        {
            if (thisNode.Row != 0 && map[thisNode.Row - 1, thisNode.Col] != 'W')
            {
                Node down = new Node(thisNode.Row - 1, thisNode.Col);
                neighbor.Add(down);
            }
        }

        if (thisNode.Row < row)
        {
            if (thisNode.Row != row - 1 && map[thisNode.Row + 1, thisNode.Col] != 'W')
            {
                Node up = new Node(thisNode.Row + 1, thisNode.Col);
                neighbor.Add(up);
            }
        }

        if (thisNode.Col >= 0)
        {
            if (thisNode.Col != 0 && map[thisNode.Row, thisNode.Col - 1] != 'W')
            {
                Node left = new Node(thisNode.Row, thisNode.Col - 1);
                neighbor.Add(left);
            }
        }

        if (thisNode.Col < col)
        {
            if (thisNode.Col != col - 1 && map[thisNode.Row, thisNode.Col + 1] != 'W')
            {
                Node right = new Node(thisNode.Row, thisNode.Col + 1);
                neighbor.Add(right);
            }
        }

        return neighbor;
    }
}

