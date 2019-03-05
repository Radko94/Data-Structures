using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class Hierarchy<T> : IHierarchy<T>
{
    private Dictionary<T, Node> hierarchy;

    public Hierarchy(T root)
    {
        hierarchy = new Dictionary<T, Node>() { { root, new Node(null, root) } };
    }

    public int Count => this.hierarchy.Count;

    public void Add(T element, T child)
    {
        if (!hierarchy.ContainsKey(element))
        {
            throw new ArgumentException();
        }
        else
        {
            Node newNode = new Node(hierarchy[element], child);

            if (hierarchy[element].Children.Contains(newNode))
                throw new ArgumentException();

            hierarchy[element].Children.Add(newNode);
            hierarchy.Add(newNode.Value, newNode);
            hierarchy[newNode.Value].Parent = hierarchy[element];
        }
    }

    public void Remove(T element)
    {
        if (hierarchy.ContainsKey(element))
        {
            if (hierarchy[element].Parent == null)
            {
                throw new InvalidOperationException();
            }

            if (hierarchy[element].Children.Count > 0)
            {
                T parentValue = hierarchy[element].Parent.Value;

                List<Node> children = new List<Node>();

                hierarchy[element].Children.ForEach(x => children.Add(x));
                children.ForEach(x => x.Parent = hierarchy[parentValue]);
                children.ForEach(x => hierarchy[parentValue].Children.Add(x));

                int index = hierarchy[parentValue].Children.FindIndex(x => x.Value.Equals(element));
                hierarchy[parentValue].Children.RemoveAt(index);
            }
            hierarchy.Remove(element);
        }
        else
        {
            throw new ArgumentException();
        }
    }

    public IEnumerable<T> GetChildren(T item)
    {
        if (!hierarchy.ContainsKey(item))
        {
            throw new ArgumentException();
        }

        List<T> thatsMyChildren = new List<T>();

        hierarchy[item].Children.ForEach(x => thatsMyChildren.Add(x.Value));

        return thatsMyChildren;
    }

    public T GetParent(T item)
    {
        if (!hierarchy.ContainsKey(item))
        {
            throw new ArgumentException();
        }
        else if (hierarchy[item].Parent == null)
        {
            return default(T);
        }
        else
        {
            return hierarchy[item].Parent.Value;
        }
    }

    public bool Contains(T value) => hierarchy.ContainsKey(value);

    public IEnumerable<T> GetCommonElements(Hierarchy<T> other)
    {
        List<T> commonElements = new List<T>();

        foreach (var first in hierarchy)
        {
            foreach (var second in other.hierarchy)
            {

                if (first.Key.Equals(second.Key))
                    commonElements.Add(first.Key);
            }
        }

        return commonElements;
    }

    public IEnumerator<T> GetEnumerator()
    {
        yield return hierarchy.ElementAt(0).Key;

        foreach (var item in hierarchy.Values)
        {
            foreach (var itemChild in item.Children)
            {
                yield return itemChild.Value;
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    public class Node
    {
        public Node Parent { get; set; }

        public T Value { get; set; }

        public List<Node> Children { get; set; }

        public Node(Node parent, T value)
        {
            this.Parent = parent;
            this.Value = value;
            this.Children = new List<Node>();
        }

        public override string ToString()
        {
            return $"Parent: {this.Parent.Value}, Value: {Value}";
        }
    }
}
