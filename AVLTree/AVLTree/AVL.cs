using System;

public class AVL<T> where T : IComparable<T>
{
    private Node<T> root;

    public Node<T> Root
    {
        get
        {
            return this.root;
        }
    }

    public bool Contains(T item)
    {
        var node = this.Search(this.root, item);
        return node != null;
    }

    public void Insert(T item)
    {
        this.root = this.Insert(this.root, item);
    }

    public void Delete(T value)
    {
        if (root != null)
        {
            root = Delete(root, value);
        }

        if (root != null)
        {
            UpdateHeight(root);
            root = Balance(root);
        }
    }

    private Node<T> Delete(Node<T> current, T value)
    {
        if (current == null)
        {
            return null;
        }

        current.Left = Delete(current.Left, value);

        if (current.Value.CompareTo(value) == 0)
        {
            if (current.Left != null && current.Right != null)
            {
                var rightReference = current.Right;
                var leftReference = current.Left;

                current = leftReference;

                if (current.Right != null)
                {
                    current.Right.Right = rightReference;
                    current.Right.Right.Height = 1;
                    current.Right.Height = 2;
                    current.Height = 3;
                }
                else
                {
                    current.Right = rightReference;
                    current.Right.Height = 1;
                    current.Height = 2;
                }

                return current;
            }
            else if (current.Left != null && current.Right == null)
            {
                current = current.Left;
                return current;
            }
            else if (current.Left == null && current.Right != null)
            {
                current = current.Right;
                return current;
            }
            else if (current.Left == null && current.Right == null)
            {
                return null;
            }
        }

        current.Right = Delete(current.Right, value);

        return current;
    }

    public void DeleteMin()
    {
        if (root != null)
        {
            root = DeleteMin(root);
        }
    }

    private Node<T> DeleteMin(Node<T> root)
    {
        if (root.Right == null && root.Left == null)
            return null;

        if (root.Value.CompareTo(root.Right.Value) < 0 && root.Value.CompareTo(root.Left.Value) < 0)
        {
            var rightReference = root.Right;
            root = root.Left;
            root.Right = rightReference;
        }
        else if (root.Left.Value.CompareTo(root.Value) < 0 && root.Left.Value.CompareTo(root.Right.Value) < 0)
        {
            root.Left = null;
        }
        else if (root.Right.Value.CompareTo(root.Value) < 0 && root.Right.Value.CompareTo(root.Left.Value) < 0)
        {
            root.Right = null;
        }

        return root;
    }

    public void EachInOrder(Action<T> action)
    {
        this.EachInOrder(this.root, action);
    }

    private Node<T> Insert(Node<T> node, T item)
    {
        if (node == null)
        {
            return new Node<T>(item);
        }

        int cmp = item.CompareTo(node.Value);
        if (cmp < 0)
        {
            node.Left = this.Insert(node.Left, item);
        }
        else if (cmp > 0)
        {
            node.Right = this.Insert(node.Right, item);
        }

        node = Balance(node);
        UpdateHeight(node);
        return node;
    }

    private Node<T> Balance(Node<T> node)
    {
        var balance = Height(node.Left) - Height(node.Right);
        if (balance > 1)
        {
            var childBalance = Height(node.Left.Left) - Height(node.Left.Right);
            if (childBalance < 0)
            {
                node.Left = RotateLeft(node.Left);
            }

            node = RotateRight(node);
        }
        else if (balance < -1)
        {
            var childBalance = Height(node.Right.Left) - Height(node.Right.Right);
            if (childBalance > 0)
            {
                node.Right = RotateRight(node.Right);
            }

            node = RotateLeft(node);
        }

        return node;
    }

    private void UpdateHeight(Node<T> node)
    {
        node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;
    }

    private Node<T> Search(Node<T> node, T item)
    {
        if (node == null)
        {
            return null;
        }

        int cmp = item.CompareTo(node.Value);
        if (cmp < 0)
        {
            return Search(node.Left, item);
        }
        else if (cmp > 0)
        {
            return Search(node.Right, item);
        }

        return node;
    }

    private void EachInOrder(Node<T> node, Action<T> action)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrder(node.Left, action);
        action(node.Value);
        this.EachInOrder(node.Right, action);
    }

    private int Height(Node<T> node)
    {
        if (node == null)
        {
            return 0;
        }

        return node.Height;
    }

    private Node<T> RotateRight(Node<T> node)
    {
        var left = node.Left;
        node.Left = left.Right;
        left.Right = node;

        UpdateHeight(node);

        return left;
    }

    private Node<T> RotateLeft(Node<T> node)
    {
        var right = node.Right;
        node.Right = right.Left;
        right.Left = node;

        UpdateHeight(node);

        return right;
    }
}
