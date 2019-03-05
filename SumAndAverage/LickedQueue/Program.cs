using System;

public class Program
{
    static void Main(string[] args)
    {
        LinkedQueue<int> linkedQueue = new LinkedQueue<int>(15);
    }
}

public class LinkedQueue<Tk>
{
    public QueueNode<Tk> Head { get; set; }

    public QueueNode<Tk> Tail { get; set; }

    public int Count { get; private set; }

    public LinkedQueue()
    {
    }

    public LinkedQueue(Tk kur)
    {
        Head = new QueueNode<Tk>(kur);
        Head.NextNode = null;
        Head.PrevNode = null;
        Count = 1;
    }


    public class QueueNode<T>
    {
        public T Element { get; set; }
        public QueueNode<T> NextNode { get; set; }
        public QueueNode<T> PrevNode { get; set; }

        public QueueNode()
        {
        }
        public QueueNode(T element)
        {
            Element = element;
            NextNode = null;
            PrevNode = null;
        }

        public void Add(T element)
        {
            if (NextNode == null)
            {
                NextNode = new QueueNode<T>(element);
            }
            else
            {
                NextNode.Add(element);
            }
        }

        public override string ToString()
        {
            return Element.ToString();
        }
    }

    public void Enqueue(Tk element)
    {
        if (Head == null)
        {
            Head = new QueueNode<Tk>(element);
            Count++;
        }
        else if (Tail == null)
        {
            Tail = this.Head;
            Head = new QueueNode<Tk>(element);
            Count++;

            Tail.NextNode = Head;
            Head.PrevNode = Tail;
        }
        else
        {
            Head.NextNode = new QueueNode<Tk>(element);
            Head.NextNode.PrevNode = Head;
            Count++;

            Head = Head.NextNode;
        }
    }

    public Tk Dequeue()
    {
        if (Tail == null)
        {
            throw new InvalidOperationException();
        }

        var returnThis = Tail.Element;
        Tail = Tail.NextNode;
        Count--;

        return returnThis;
    }

    public Tk[] ToArray()
    {
        Tk[] andNowThisIsArray = new Tk[Count];

        int count = 0;

        while (Tail != null)
        {
            andNowThisIsArray[count] = Tail.Element;
            count++;
            Tail = Tail.NextNode;
        }

        return andNowThisIsArray;
    }
}

//public class DoublyLinkedList<TValue>  /* : ILinkedList<TValue> */
//{
//    public Node<TValue> Head;
//    public Node<TValue> Tail;

//    public DoublyLinkedList(TValue kur)
//    {
//        Head = new Node<TValue>(kur);
//    }

//    public class Node<T>
//    {
//        public T Element { get; set; }
//        public Node<T> Next { get; set; }
//        public Node<T> Prev { get; set; }
//        public Node(T kur)
//        {
//            Element = kur;
//            Next = null;
//            Prev = null;
//        }
//    }

//    public void Add(TValue kur)
//    {
//        if (Head == null)
//        {
//            Head = new Node<TValue>(kur);
//        }
//        else if (this.Tail == null)
//        {
//            this.Tail = this.Head;
//            this.Head = new Node<TValue>(kur);

//            this.Tail.Next = this.Head;
//            this.Head.Prev = this.Tail;
//        }
//        else
//        {
//            this.Head.Next = new Node<TValue>(kur);
//            this.Head.Next.Prev = this.Head;

//            this.Head = this.Head.Next;
//        }
//    }

//    public bool Remove(Node<TValue> node)
//    {
//        //TODO(RApostolov): Implement
//        return false;
//    }
//}