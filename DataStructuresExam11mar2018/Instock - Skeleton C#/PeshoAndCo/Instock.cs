using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Instock : IProductStock
{
    Dictionary<string, Product> store;

    private bool isUpdated = false;
    private Product[] updateFind { get; set; }

    public Instock()
    {
        store = new Dictionary<string, Product>();
    }

    public int Count => store.Count;

    public void Add(Product product)
    {
        if (!store.ContainsKey(product.Label))
        {
            store.Add(product.Label, product);
            isUpdated = true;
        }
    }

    public void ChangeQuantity(string product, int quantity)
    {
        if (!store.ContainsKey(product))
            throw new ArgumentException();

        store[product].Quantity = quantity;
    }

    public bool Contains(Product product)
    {
        return store.ContainsKey(product.Label);
    }

    public Product Find(int index)
    {
        if (index > Count - 1)
            throw new IndexOutOfRangeException();

        if (isUpdated)
        {
            updateFind = store.Select(x => x.Value).ToArray();
            isUpdated = false;
        }

        return updateFind[index];
    }

    public IEnumerable<Product> FindAllByPrice(double price)
    {
        List<Product> products = new List<Product>();

        foreach (var item in store)
        {
            if (item.Value.Price.CompareTo(price) == 0)
                products.Add(item.Value);
        }

        return products;
    }

    public IEnumerable<Product> FindAllByQuantity(int quantity)
    {
        List<Product> products = new List<Product>();

        foreach (var item in store)
        {
            if (item.Value.Quantity == quantity)
                products.Add(item.Value);
        }

        return products;
    }

    public IEnumerable<Product> FindAllInRange(double lo, double hi)
    {
        List<Product> products = new List<Product>();

        foreach (var item in store)
        {
            if ((item.Value.Price.CompareTo(lo) > 0) && (item.Value.Price.CompareTo(hi) <= 0))
                products.Add(item.Value);
        }

        return products.OrderByDescending(x => x.Price);
    }

    public Product FindByLabel(string label)
    {
        if (!store.ContainsKey(label))
            throw new ArgumentException();

        return store[label];
    }

    public IEnumerable<Product> FindFirstByAlphabeticalOrder(int count)
    {
        if (count > Count)
            throw new ArgumentException();

        List<Product> hoho = new List<Product>();
        int currCount = 0;

        foreach (var item in store.OrderBy(x => x.Key))
        {
            if (item.Value.Equals(null))
            {
            }
            else if (currCount <= count)
                hoho.Add(item.Value);

            currCount++;
        }
        return hoho;
    }

    public IEnumerable<Product> FindFirstMostExpensiveProducts(int count)
    {
        if (count > Count)
            throw new ArgumentException();

        List<Product> hoho = new List<Product>();
        int currCount = 0;

        foreach (var item in store.OrderByDescending(x => x.Value.Price))
        {
            if (currCount < count)
                hoho.Add(item.Value);

            currCount++;
        }

        return hoho.OrderByDescending(x => x.Price);
    }

    public IEnumerator<Product> GetEnumerator()
    {
        foreach (var item in store)
        {
            if (item.Value == null)
                yield break;
            yield return item.Value;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return (IEnumerator)GetEnumerator();
    }
}
