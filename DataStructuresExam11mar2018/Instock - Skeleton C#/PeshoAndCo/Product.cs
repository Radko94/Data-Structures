using System;

/// <summary>
/// <para>Product is the entity which your stock data structure
/// will consist of. Please, do not make any modifications as
/// it might lead to unexpected results</para>
/// </summary>
public class Product : IComparable<Product>
{

    public Product(string label, double price, int quantity)
    {
        this.Label = label;
        this.Price = price;
        this.Quantity = quantity;
    }

    public string Label { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }

    public int CompareTo(Product other)
    {
        if (this.Label == other.Label)
        {
            if (this.Price == other.Price)
            {
                if (this.Quantity == other.Quantity) return 0;
                else if (this.Quantity <= other.Quantity) return -1;
                else return 1;
            }
            else if (this.Price <= other.Price) return -1;
            else return 1;
        }
        else return -1;
    }

    public override bool Equals(object obj)
    {
        if (obj is Product)
        {
            Product kur = (Product)obj;
            if (this.Label == kur.Label)
            {
                if (this.Price == kur.Price)
                {
                    if (this.Quantity == kur.Quantity)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;
        }
        else
            return false;
    }
}