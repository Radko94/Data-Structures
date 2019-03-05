using System;
using System.Collections.Generic;
using System.Linq;

//using Wintellect.PowerCollections;

public class FirstLastList<T> : IFirstLastList<T> where T : IComparable<T>
{
    private Node thisList;

    public FirstLastList()
    {
        Count = 0;
    }

    public int Count { get; private set; }

    public void Add(T element)
    {
        thisList = Add(element, thisList);
    }

    private Node Add(T element, Node childNode)
    {
        if (childNode != null)
        {
            childNode.Child = Add(element, childNode.Child);
            childNode.Child.First = childNode.First;
            childNode.Last = childNode.Child.Last;
            childNode.Child.Parent = childNode;
        }
        else
        {
            childNode = new Node(null, element, null);
            childNode.First = childNode;
            childNode.Last = childNode;

            Count++;
        }

        //if (childNode == null)
        //{
        //    childNode = new Node(null, element, null);
        //    if (childNode.First == null)
        //        childNode.First = childNode;
        //    Count++;
        //}
        //else if (childNode.Child != null)
        //{
        //    childNode.Child = Add(element, childNode.Child);
        //    childNode.Child.First = childNode.First;
        //    childNode.Child.Parent = childNode;

        //}

        return childNode;
    }

    public void Clear()
    {
        thisList = null;
        Count = 0;
    }

    public IEnumerable<T> First(int count)
    {
        if (count > Count)
            throw new ArgumentOutOfRangeException();

        List<T> firstNElements = new List<T>(count);
        Node temp = thisList;

        while (firstNElements.Count < firstNElements.Capacity)
        {
            firstNElements.Add(temp.Value);
            temp = temp.Child;
        }

        return firstNElements;
    }

    public IEnumerable<T> Last(int count)
    {
        if (count > Count)
            throw new ArgumentOutOfRangeException();

        List<T> lastNElements = new List<T>(count);
        Node temp = thisList;

        while (lastNElements.Count < lastNElements.Capacity)
        {
            lastNElements.Add(temp.Last.Value);
            temp.Last = temp.Last.Parent;
        }

        return lastNElements;
    }

    public IEnumerable<T> Max(int count)
    {
        if (count > Count)
            throw new ArgumentOutOfRangeException();

        return Max(count, thisList);
    }

    private IEnumerable<T> Max(int count, Node childNode)
    {
        List<T> sortedList = new List<T>();

        while (childNode != null)
        {
            sortedList.Add(childNode.Value);
            childNode = childNode.Child;
        }

        sortedList.Sort();

        List<T> returnThis = new List<T>();

        for (int i = 0; i < count; i++)
        {
            returnThis.Add(sortedList.Last());
            sortedList.Remove(sortedList.Last());
        }

        return returnThis;
    }

    public IEnumerable<T> Min(int count)
    {
        if (count > Count)
            throw new ArgumentOutOfRangeException();

        return Min(count, thisList);
    }

    private IEnumerable<T> Min(int count, Node childNode)
    {
        List<T> sortedList = new List<T>();

        while (childNode != null)
        {
            sortedList.Add(childNode.Value);
            childNode = childNode.Child;
        }

        sortedList.Sort();
        sortedList.Reverse();

        List<T> returnThis = new List<T>();

        for (int i = 0; i < count; i++)
        {
            returnThis.Add(sortedList.Last());
            sortedList.Remove(sortedList.Last());
        }

        return returnThis;
    }

    public int RemoveAll(T element)
    {
        return RemoveAll(element, thisList);
    }

    //items.Add(new Product(1.11m, "first"));
    //items.Add(new Product(0.50m, "coffee"));
    //items.Add(new Product(2.50m, "chocolate"));
    //items.Add(new Product(1.20m, "mint drops"));
    //items.Add(new Product(1.20m, "beer"));
    //items.Add(new Product(0.50m, "candy"));
    //items.Add(new Product(1.20m, "cola"));

    private int RemoveAll(T element, Node childNode)
    {
        int count = 0;

        while (childNode != null)
        {
            if (childNode.Value.CompareTo(element) == 0)
            {
                if (childNode.Child != null)
                {
                    childNode.Child.Parent = childNode.Parent;
                    childNode.Parent.Child = childNode.Child;
                    count++;
                }
                else
                {
                    childNode.Parent.Child = null;
                    childNode.Last = childNode.Parent;
                    childNode = null;
                    count++;
                }
            }
            
            if (childNode == null)
                break;
            else
                childNode = childNode.Child;
        }

        Count -= count;

        return count;
    }

    private class Node
    {
        public Node First { get; set; }

        public Node Parent { get; set; }

        public T Value { get; set; }

        public Node Child { get; set; }

        public Node Last { get; set; }

        public Node(Node parent, T value, Node child)
        {
            Parent = parent;
            Value = value;
            Child = child;
        }

        public override string ToString()
        {
            return $"First: {First.Value}, Parent: {Parent.Value}, Child: {Child.Value}, Last: {Last.Value}";
        }
    }
}
