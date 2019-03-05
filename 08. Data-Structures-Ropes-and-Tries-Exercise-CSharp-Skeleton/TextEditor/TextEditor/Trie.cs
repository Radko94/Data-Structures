using System;
using System.Collections.Generic;

public class Trie<T>
{
    // изключително неясна структура
    // -- еднобуквени параметри, някакви ключове
    // все пак това е задача за структури от данни, не задача за рефактор

    private Node root;
    private bool logOut = false;

    private class Node
    {
        public T Value;
        public bool IsTerminal;
        public Dictionary<char, Node> Next = new Dictionary<char, Node>();
    }

    public int GetLenth() => root.Next.Count;

    public bool LogOut {
        get => logOut;

        set
        {
            logOut = value;
        }
    }

    public T GetValue(string key)
    {
        Node thisNode = this.GetNode(this.root, key, 0);
        if (thisNode == null || !thisNode.IsTerminal)
        {
            throw new InvalidOperationException();
        }

        return thisNode.Value;
    }

    public bool Contains(string key)
    {
        Node node = this.GetNode(this.root, key, 0);
        return node != null && node.IsTerminal;
    }

    public void Insert(string input, T value)
    {
        this.root = this.Insert(this.root, input, value, 0);
    }

    public IEnumerable<string> GetByPrefix(string prefix)
    {
        Queue<string> results = new Queue<string>();
        Node x = this.GetNode(this.root, prefix, 0);

        this.Collect(x, prefix, results);

        return results;
    }

    private Node GetNode(Node current, string key, int index)
    {
        if (current == null)
        {
            return null;
        }

        if (index == key.Length)
        {
            return current;
        }

        Node node = null;
        char charAtIndex = key[index];

        if (current.Next.ContainsKey(charAtIndex))
        {
            node = current.Next[charAtIndex];
        }

        return this.GetNode(node, key, index + 1);
    }

    private Node Insert(Node current, string input, T value, int index)
    {
        if (current == null)
        {
            current = new Node();
        }

        if (index == input.Length)
        {
            current.Value = value;
            current.IsTerminal = true;
            return current;
        }

        Node node = null;
        char currentChar = input[index];

        if (current.Next.ContainsKey(currentChar))
        {
            node = current.Next[currentChar];
        }

        current.Next[currentChar] = this.Insert(node, input, value, index + 1);
        return current;
    }

    private void Collect(Node current, string prefix, Queue<string> results)
    {
        if (current == null)
        {
            return;
        }

        if (current.Value != null && current.IsTerminal)
        {
            results.Enqueue(prefix);
        }

        foreach (char c in current.Next.Keys)
        {
            this.Collect(current.Next[c], prefix + c, results);
        }
    }
}