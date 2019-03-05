using System;
using System.Collections.Generic;

public class IntervalTree
{
    private class Node
    {
        internal Interval interval;
        internal double max;
        internal Node right;
        internal Node left;

        public Node(Interval interval)
        {
            this.interval = interval;
            this.max = interval.Hi;
        }
    }

    private Node root;

    public void Insert(double lo, double hi)
    {
        this.root = this.Insert(this.root, lo, hi);
    }

    public void EachInOrder(Action<Interval> action)
    {
        EachInOrder(this.root, action);
    }

    public Interval SearchAny(double lo, double hi)
    {
        var currentInterval = root;

        while (currentInterval != null && !currentInterval.interval.Intersects(lo, hi))
        {
            if (currentInterval.left != null && currentInterval.left.max > lo)
            {
                currentInterval = currentInterval.left;
            }
            else
            {
                currentInterval = currentInterval.right;
            }
        }

        if (currentInterval == null)
            return null;

        return currentInterval.interval;
    }

    public IEnumerable<Interval> SearchAll(double lo, double hi)
    {
        var result = new List<Interval>();
        SearchAll(root, lo, hi, result);
        return result;
    }

    private void SearchAll(Node root, double lo, double hi, List<Interval> result)
    {
        if (root == null)
            return;

        var goLeft = root.left != null && root.left.max > lo;
        var goRight = root.right != null && root.right.interval.Lo < hi;

        if (goLeft)
            SearchAll(root.left, lo, hi, result);

        if (root.interval.Intersects(lo, hi))
            result.Add(root.interval);

        if (goRight)
            SearchAll(root.right, lo, hi, result);
    }

    private void EachInOrder(Node node, Action<Interval> action)
    {
        if (node == null)
        {
            return;
        }

        EachInOrder(node.left, action);
        action(node.interval);
        EachInOrder(node.right, action);
    }

    private Node Insert(Node node, double lo, double hi)
    {
        if (node == null)
        {
            return new Node(new Interval(lo, hi));
        }

        int cmp = lo.CompareTo(node.interval.Lo);
        if (cmp < 0)
        {
            node.left = Insert(node.left, lo, hi);
        }
        else if (cmp > 0)
        {
            node.right = Insert(node.right, lo, hi);
        }
        
        return node;
    }
}
