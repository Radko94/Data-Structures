using System;
using System.Collections.Generic;

public class BinaryHeap<T> where T : IComparable<T>
{
    private T[] heap;

    public BinaryHeap()
    {
        heap = new T[4];
        Count = 0;
    }

    public int Count
    {
        get;
        private set;
    }

    public void Insert(T item)
    {
        if (Count == heap.Length - 1)
        {
            T[] newHeap = new T[heap.Length * 2];
            Array.Copy(heap, newHeap, heap.Length);
            heap = newHeap;
        }

        this.heap[Count] = item;
        this.HeapifyUp(Count);
        Count++;
    }

    private void HeapifyUp(int index)
    {
        while (index > 0 && IsLess(Parent(index), index))
        {
            this.Swap(index, Parent(index));
            index = Parent(index);
        }
    }

    private void Swap(int index, int parentIndex)
    {
        T children = this.heap[index];

        heap[index] = heap[parentIndex];
        heap[parentIndex] = children;
    }

    private bool IsLess(int parent, int children)
    {
        int compareThis = heap[parent].CompareTo(heap[children]);

        if (compareThis < 0)
            return true;
        else
            return false;
    }

    private int Parent(int index)
    {
        int parent = 0;

        if (index > 0)
        {
            parent = (index - 1) / 2;
        }
        return parent;
    }

    public T Peek()
    {
        if (Count == 0)
            throw new InvalidOperationException();
        return this.heap[0];
    }

    public T Pull()
    {
        if (Count == 0)
            throw new InvalidOperationException();

        T item = heap[0];

        Swap(Count - 1, 0);
        heap[Count - 1] = default(T);
        Count--;
        HeapifyDown(0);

        return item;
    }

    private void HeapifyDown(int thisIndex)
    {
        while (thisIndex < Count / 2)
        {
            int child = LeftChild(thisIndex);

            if (child + 1 < Count && IsLess(child, child + 1))
            {
                child = child + 1;
            }

            if (IsLess(child, thisIndex))
                break;

            this.Swap(thisIndex, child);
            thisIndex = child;
        }
    }

    private int LeftChild(int index)
    {
        int returnThis = 2 * index + 1;
        return returnThis;
    }
}
