using System;
class Interval
{

    private _2DPosition[] objects;
    public int Count { get; private set; }

    public Interval()
    {
        objects = new _2DPosition[4];
    }

    public void Add(string name, int x, int y)
    {
        if (objects.Length == Count + 1)
        {
            _2DPosition[] temp = new _2DPosition[objects.Length * 2];
            Array.Copy(objects, temp, objects.Length);
            objects = temp;
        }

        objects[Count] = new _2DPosition(name, x, y);
        Count++;
        objects = PositionSort(objects);
    }

    private _2DPosition[] PositionSort(_2DPosition[] objects)
    {
        if (Count > 0)
        {
            for (int i = 1; i < Count - 1; i++)
            {
                for (int j = i; j >= 0; j--)
                {
                    if (objects[i].CompareTo(objects[j]) == 0)
                    {
                        objects[i].
                    }
                }
            }
        }
    }

    public class _2DPosition : IComparable
    {
        public _2DPosition(string name, int x, int y)
        {
            Name = name;
            X = x;
            Y = y;
        }
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public int CompareTo(object obj)
        {
            _2DPosition comp = (_2DPosition)obj;

            if (this.X > comp.X) return 1;
            else if (this.X == comp.X) return 0;
            else return -1;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is _2DPosition)) return false;

            _2DPosition otherInterval = (_2DPosition)obj;

            if (this.X >= otherInterval.X && this.X <= otherInterval.X + 10) return true;
            else if (this.Y >= otherInterval.Y && this.Y <= otherInterval.Y + 10) return true;
            else return false;
        }
    }
}

