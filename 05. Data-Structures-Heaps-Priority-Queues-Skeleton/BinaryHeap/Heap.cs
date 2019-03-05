using System;

public static class Heap<T> where T : IComparable<T>
{
    public static void Sort(T[] arr)
    {
        if (arr.Length == 0 || arr == null)
            return;

        for (int i = arr.Length / 2; i >= 0; i--)
        {
            HeapifyDown(arr, i, arr.Length);
        }

        for (int i = arr.Length - 1; i >= 0; i--)
        {
            Swap(arr, 0, i);
            HeapifyDown(arr, 0, i);
        }
    }
    private static void HeapifyDown(T[] array, int index, int lenght)
    {
        while (index < lenght / 2)
        {
            int children = index * 2 + 1;

            if (children + 1 < lenght && array[children].CompareTo(array[children + 1]) < 0)
            {
                children = children + 1;
            }

            if (array[children].CompareTo(array[index]) < 0)
                break;

            //if (IsGreater(array , children, index))
            //    break;

            Swap(array, index, children);
            index = children;
        }
    }

    //private static bool IsGreater(T[] arr, int child, int otherChild)
    //{
    //    int comparer = arr[child].CompareTo(arr[otherChild]);

    //    if (comparer < 0)
    //        return true;
    //    else
    //        return false;
    //}

    private static void Swap(T[] arr, int index, int parentIndex)
    {
        T children = arr[index];

        arr[index] = arr[parentIndex];
        arr[parentIndex] = children;
    }
}
