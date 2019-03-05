using System;

public class Transaction : IComparable<Transaction>
{
    public int Id { get; set; }
    public TransactionStatus Status { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public double Amount { get; set; }

    public Transaction(int id, TransactionStatus st, string from, string to, double amount)
    {
        this.Id = id;
        this.Status = st;
        this.From = from;
        this.To = to;
        this.Amount = amount;
    }

    public int CompareTo(Transaction other)
    {
        if (this.Id == other.Id)
        {
            if (this.Status == other.Status)
            {
                if (this.From == other.From)
                {
                    if (this.To == other.To)
                    {
                        if (this.Amount == other.Amount) return 0;
                        else if (this.Amount > other.Amount) return 1;
                        else return -1;
                    }
                    else if (this.To.Length > other.To.Length) return 1;
                    else return -1;
                }
                else if (this.To.Length > other.To.Length) return 1;
                else return -1;
            }
            else if (this.Status >= other.Status) return 1;
            else return -1;
        }
        else if (this.Id > other.Id) return 1;
        else return -1;
    }

    public override bool Equals(object obj)
    {
        if (!(obj is Transaction))
        {
            return false;
        }

        Transaction well = (Transaction)obj;

        if (this.Id == well.Id && this.Status == well.Status && this.From == well.From && this.To == well.To && this.Amount == well.Amount)
            return true;
        else
            return false;
    }
}