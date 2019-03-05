using System;

public class ArrayStack<T>
{
    private T[] elements;
    public int Count { get; private set; }

    const int InitialCapacity = 16;

    public ArrayStack(int capacity = InitialCapacity)
    {
        elements = new T[InitialCapacity];
        Count = 0;
    }

    public ArrayStack(byte jCount)
    {
        elements = new T[jCount];
    }

    public void Push(T element)
    {
        if (Count == elements.Length - 1)
        {
            Grow();
        }

        elements[Count] = element;
        Count++;
    }

    public T Pop()
    {
        T thisElement = elements[Count - 1];
        elements[Count - 1] = default(T);
        Count--;
        return thisElement;
    }

    public T[] ToArray()
    {
        T[] newElements = new T[elements.Length];

        Array.Copy(elements, newElements, Count);

        return newElements;
    }

    private void Grow()
    {
        T[] newArr = new T[elements.Length * 2];

        for (int i = 0; i < elements.Length; i++)
        {
            newArr[i] = elements[i];
        }

        elements = newArr;
    }
}
