using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

/// <summary>
/// The ThreadExecutor is the concrete implementation of the IScheduler.
/// You can send any class to the judge system as long as it implements
/// the IScheduler interface. The Tests do not contain any <e>Reflection</e>!
/// </summary>
public class ThreadExecutor : IScheduler
{
    private int count;
    private Task[] tasks;

    public ThreadExecutor()
    {
        tasks = new Task[4];
        count = 0;
    }

    public int Count
    {
        get => count;
    }


    public void ChangePriority(int id, Priority newPriority)
    {
        if (count == 0 || tasks.FirstOrDefault(x => x.Id == id) == null)
            throw new ArgumentException();
        tasks.First(x => x.Id == id).TaskPriority = newPriority;
        //jack.TaskPriority = newPriority;
    }

    public bool Contains(Task task)
    {
        bool isTrue = false;
        for (int i = 0; i < count; i++)
        {
            if (tasks[i].Equals(task))
            {
                isTrue = true;
                break;
            }
        }
        return isTrue;
    }

    public int Cycle(int cycles)
    {
        if (count == 0)
            throw new InvalidOperationException();

        int taskCompleated = 0;

        for (int i = 0; i < cycles; i++)
        {
            for (int j = 0; j < count; j++)
            {
                tasks[j].Consumption--;

                if (tasks[j].Consumption <= 0)
                {
                    Task[] newKur = new Task[tasks.Length];
                    for (int h = 0; h < tasks.Length; h++)
                    {
                        if (h == j)
                        {
                        }
                        else if (h < j)
                        {
                            newKur[h] = tasks[h];
                        }
                        else if (h > j)
                        {
                            newKur[h - 1] = tasks[h];
                        }
                    }
                    j--;
                    count--;
                    tasks = newKur;
                    taskCompleated++;
                }
            }
        }

        return taskCompleated;
    }

    public void Execute(Task task)
    {
        for (int i = 0; i < count; i++)
        {
            if (tasks[i].Id.CompareTo(task.Id) == 0)
                throw new ArgumentException();
        }

        if (tasks.Length == count)
        {
            Task[] newTasks = new Task[tasks.Length * 2];
            Array.Copy(tasks, newTasks, tasks.Length);
            tasks = newTasks;
        }
        tasks[count] = task;
        count++;

    }

    public IEnumerable<Task> GetByConsumptionRange(int lo, int hi, bool inclusive)
    {
        List<Task> thisRange = new List<Task>();

        for (int i = 0; i < count; i++)
        {
            if (inclusive == true)
            {
                if (tasks[i].Consumption.CompareTo(lo) >= 0 && tasks[i].Consumption.CompareTo(hi) <= 0)
                {
                    thisRange.Add(tasks[i]);
                }
            }
            else
            {
                if (tasks[i].Consumption.CompareTo(lo) > 0 && tasks[i].Consumption.CompareTo(hi) < 0)
                {
                    thisRange.Add(tasks[i]);
                }
            }
        }


        return thisRange.OrderBy(x => x.Consumption).ThenByDescending(x => x.TaskPriority);
    }

    public Task GetById(int id)
    {
        for (int i = 0; i < count; i++)
        {
            if (tasks[i].Id.CompareTo(id) == 0)
            {
                return tasks[i];
            }
        }

        throw new ArgumentException();
    }

    public Task GetByIndex(int index)
    {
        if (index > count - 1 || index < 0)
            throw new ArgumentOutOfRangeException();

        return tasks[index];
    }

    public IEnumerable<Task> GetByPriority(Priority type)
    {
        List<Task> thisKur = new List<Task>();

        for (int i = 0; i < count; i++)
        {
            if (tasks[i].TaskPriority == type)
                thisKur.Add(tasks[i]);
        }

        return thisKur.OrderByDescending(x => x.Id);
    }

    public IEnumerable<Task> GetByPriorityAndMinimumConsumption(Priority priority, int lo)
    {
        List<Task> thisKur = new List<Task>();

        foreach (var item in tasks)
        {
            if (item == null)
                break;
            if (item.TaskPriority == priority)
            {
                if (item.Consumption >= lo)
                    thisKur.Add(item);
                else if (count == 1)
                    thisKur.Add(item);
            }
        }

        return thisKur.OrderByDescending(x => x.Id);
    }


    public IEnumerator<Task> GetEnumerator()
    {
        foreach (var item in tasks)
        {
            if (item == null)
                yield break;
            yield return item;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
