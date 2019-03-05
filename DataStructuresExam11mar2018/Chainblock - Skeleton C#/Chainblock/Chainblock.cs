using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class Chainblock : IChainblock
{
    Dictionary<int, Transaction> jack;

    public Chainblock()
    {
        jack = new Dictionary<int, Transaction>();
    }

    public int Count => jack.Count;

    public void Add(Transaction tx)
    {
        if (!jack.ContainsKey(tx.Id))
            jack.Add(tx.Id, tx);
    }

    public void ChangeTransactionStatus(int id, TransactionStatus newStatus)
    {
        if (!jack.ContainsKey(id))
            throw new ArgumentException();

        jack[id].Status = newStatus;
    }

    public bool Contains(Transaction tx)
    {
        bool isTrue = false;

        foreach (var item in jack)
        {
            if (tx.Equals(item))
                isTrue = true;
        }

        return isTrue;
    }

    public bool Contains(int id)
    {
        return jack.ContainsKey(id);
    }

    public IEnumerable<Transaction> GetAllInAmountRange(double lo, double hi)
    {
        List<Transaction> kur = new List<Transaction>();

        foreach (var item in jack)
        {
            if (item.Value.Amount >= lo && item.Value.Amount <= hi)
                kur.Add(item.Value);
        }

        return kur;
    }

    public IEnumerable<Transaction> GetAllOrderedByAmountDescendingThenById()
    {
        return jack.Values.OrderByDescending(x => x.Amount).ThenBy(x => x.Id);
    }

    public IEnumerable<string> GetAllReceiversWithTransactionStatus(TransactionStatus status)
    {
        List<Transaction> kur = new List<Transaction>();

        foreach (var item in jack)
        {
            if (item.Value.Status == status)
                kur.Add(item.Value);
        }

        if (kur.Count == 0)
            throw new InvalidOperationException();

        kur = kur.OrderByDescending(x => x.Amount).ToList();

        return kur.Select(x => x.To);
    }

    public IEnumerable<string> GetAllSendersWithTransactionStatus(TransactionStatus status)
    {
        List<Transaction> kur = new List<Transaction>();

        foreach (var item in jack)
        {
            if (item.Value.Status == status)
                kur.Add(item.Value);
        }

        if (kur.Count == 0)
            throw new InvalidOperationException();

        kur = kur.OrderByDescending(x => x.Amount).ToList();

        return kur.Select(x => x.From);
    }

    public Transaction GetById(int id)
    {
        if (!jack.ContainsKey(id))
            throw new InvalidOperationException();

        return jack[id];
    }

    public IEnumerable<Transaction> GetByReceiverAndAmountRange(string receiver, double lo, double hi)
    {
        List<Transaction> kur = new List<Transaction>();

        foreach (var item in jack)
        {
            if ((item.Value.To == receiver && item.Value.Amount >= lo) && (item.Value.To == receiver && item.Value.Amount < hi))
                kur.Add(item.Value);
        }

        if (kur.Count == 0)
            throw new InvalidOperationException();

        return kur.OrderByDescending(x => x.Amount).ThenBy(x => x.Id);
    }

    public IEnumerable<Transaction> GetByReceiverOrderedByAmountThenById(string receiver)
    {
        List<Transaction> transactions = new List<Transaction>();

        foreach (var item in jack)
        {
            if (item.Value.To == receiver)
                transactions.Add(item.Value);
        }

        if (transactions.Count == 0)
            throw new InvalidOperationException();

        return transactions.OrderByDescending(x => x.Amount).OrderBy(x => x.Id);
    }

    public IEnumerable<Transaction> GetBySenderAndMinimumAmountDescending(string sender, double amount)
    {
        List<Transaction> kur = new List<Transaction>();

        foreach (var item in jack)
        {
            if (item.Value.From == sender && item.Value.Amount > amount)
                kur.Add(item.Value);
        }

        if (kur.Count == 0)
            throw new InvalidOperationException();

        return kur.OrderByDescending(x => x.Amount);
    }

    public IEnumerable<Transaction> GetBySenderOrderedByAmountDescending(string sender)
    {
        List<Transaction> transactions = new List<Transaction>();

        foreach (var item in jack)
        {
            if (item.Value.From == sender)
                transactions.Add(item.Value);
        }

        if (transactions.Count == 0)
            throw new InvalidOperationException();

        return transactions.OrderByDescending(x => x.Amount);
    }

    public IEnumerable<Transaction> GetByTransactionStatus(TransactionStatus status)
    {
        List<Transaction> returnThis = new List<Transaction>();

        foreach (var item in jack)
        {
            if (item.Value.Status == status)
                returnThis.Add(item.Value);
        }

        if (returnThis.Count == 0)
            throw new InvalidOperationException();

        return returnThis.OrderByDescending(x => x.Amount);
    }

    public IEnumerable<Transaction> GetByTransactionStatusAndMaximumAmount(TransactionStatus status, double amount)
    {
        List<Transaction> transactions = new List<Transaction>();

        foreach (var item in jack)
        {
            if (item.Value.Status == status && item.Value.Amount <= amount)
                transactions.Add(item.Value);
        }

        return transactions.OrderByDescending(x => x.Amount);
    }

    public void RemoveTransactionById(int id)
    {
        if (!jack.ContainsKey(id))
            throw new InvalidOperationException();

        jack.Remove(id);
    }

    public IEnumerator<Transaction> GetEnumerator()
    {
        foreach (var item in jack)
        {
            yield return item.Value;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return (IEnumerator)GetEnumerator();
    }
}

