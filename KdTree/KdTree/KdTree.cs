using System;

public class KdTree
{
    private Node root;

    public class Node
    {
        public Node(Point2D point)
        {
            this.Point = point;
        }

        public Point2D Point { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
    }

    public Node Root => this.root;

    public bool Contains(Point2D point)
    {
        return Contains(root, point);
    }

    private bool Contains(Node root, Point2D point)
    {
        //if (root.Equals(point))
        //    return true;

        //return Contains(root.Left, point);
        //return Contains(root.Right, point);

        return true;
    }

    public void Insert(Point2D point)
    {
        root = Insert(root, point);
    }

    private Node Insert(Node root, Point2D point)
    {
        if (root == null)
            root = new Node(point);

        int compare = root.Point.CompareTo(point);

        if (compare < 0)
            root.Left = Insert(root.Left, point);
        if (compare > 0)
            root.Right = Insert(root.Right, point);

        return root;
    }

    public void EachInOrder(Action<Point2D> action)
    {
        this.EachInOrder(this.root, action);
    }

    private void EachInOrder(Node node, Action<Point2D> action)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrder(node.Left, action);
        action(node.Point);
        this.EachInOrder(node.Right, action);
    }
}
