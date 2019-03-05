using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    static void Main(string[] args)
    {
        LinkedStack<DateTime> kur = new LinkedStack<DateTime>();
        kur.Push(new DateTime(0, 0, 321));
        kur.Push(new DateTime(2, 3, 22));
        kur.Push(new DateTime(1995, 11, 23));
        kur.Push(new DateTime(34322, 7, 12, 13, 34, 13));

        kur.ToArray().ToList().ForEach(x => Console.WriteLine(x));
    }
}

public class LinkedStack<T>
{
    private Node<T> firstNode;

    public int Count { get; private set; }



    public void Push(T element)
    {
        if (Count == 0)
        {
            firstNode = new Node<T>(element);
            Count++;
        }
        else
        {
            firstNode.AddToEnd(element);
        }
    }

    public T Pop()
    {
        if (Count == 0)
        {
            throw new InvalidOperationException();
        }

        return firstNode.RemoveEnd();
    }

    public T[] ToArray()
    {
        return firstNode.ToArray();
    }


    private class Node<Tk>
    {
        public Tk Value;
        public Node<Tk> NextNode { get; set; }

        private List<Tk> thisIsTOArray;

        public Node(Tk value, Node<Tk> nextNode = null)
        {
            this.Value = value;
            NextNode = nextNode;
        }

        public void AddToEnd(Tk data)
        {
            if (NextNode == null)
            {
                NextNode = new Node<Tk>(data);
            }
            else
            {
                NextNode.AddToEnd(data);
            }
        }

        public Tk RemoveEnd()
        {
            if (NextNode.NextNode == null)
            {
                Tk returnThis = NextNode.Value;

                NextNode = null;

                return returnThis;
            }
            else
            {
                return NextNode.RemoveEnd();
            }
        }

        public Tk[] ToArray()
        {
            if (NextNode != null)
            {
                thisIsTOArray.Add(Value);
            }
            else
            {
                NextNode.ToArray();
            }

            return thisIsTOArray.ToArray();
        }
    }
}

