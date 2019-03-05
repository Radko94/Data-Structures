using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RoyaleArena : IArena
{
    private Dictionary<int, Battlecard> royals;

    public RoyaleArena()
    {
        royals = new Dictionary<int, Battlecard>();
    }

    public int Count => royals.Count;

    public void Add(Battlecard card)
    {
        if (!royals.ContainsKey(card.Id))
            royals.Add(card.Id, card);
    }

    public void ChangeCardType(int id, CardType type)
    {
        if (royals.ContainsKey(id))
            royals[id].Type = type;
        else
            throw new ArgumentException();
    }

    public bool Contains(Battlecard card)
    {
        return royals.ContainsKey(card.Id);
    }

    public IEnumerable<Battlecard> FindFirstLeastSwag(int n)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Battlecard> GetAllByNameAndSwag()
    {
        List<Battlecard> thisKur = new List<Battlecard>();

        foreach (var item in royals.GroupBy(x => x.Value.Name).OrderByDescending(x => x.Max(y => y.Value.Swag)).Take(1))
        {
            Console.WriteLine(item);
        }

        return thisKur;
    }

    public IEnumerable<Battlecard> GetAllInSwagRange(double lo, double hi)
    {
        List<Battlecard> thisKur = new List<Battlecard>();

        foreach (var item in royals)
        {
            if ((item.Value.Swag.CompareTo(lo) >= 0) || (item.Value.Swag.CompareTo(hi) <= 0))
                thisKur.Add(item.Value);
        }

        return thisKur.OrderBy(x => x.Swag);
    }

    public IEnumerable<Battlecard> GetByCardType(CardType type)
    {
        List<Battlecard> thisKur = new List<Battlecard>();

        foreach (var item in royals)
        {
            if (item.Value.Type == type)
                thisKur.Add(item.Value);
        }

        if (thisKur.Count == 0)
            throw new InvalidOperationException();

        return thisKur.OrderByDescending(x => x.Damage).ThenBy(x => x.Id);
    }

    public IEnumerable<Battlecard> GetByCardTypeAndMaximumDamage(CardType type, double damage)
    {
        List<Battlecard> thisKur = new List<Battlecard>();

        foreach (var item in royals)
        {
            if (item.Value.Type == type && item.Value.Damage <= damage)
                thisKur.Add(item.Value);
        }

        if (thisKur.Count == 0)
            throw new InvalidOperationException();

        return thisKur.OrderByDescending(x => x.Damage).ThenBy(x => x.Id);
    }

    public Battlecard GetById(int id)
    {
        if (royals.ContainsKey(id))
            return royals[id];
        else
            throw new InvalidOperationException();
    }

    public IEnumerable<Battlecard> GetByNameAndSwagRange(string name, double lo, double hi)
    {
        List<Battlecard> thisKur = new List<Battlecard>();

        foreach (var item in royals)
        {
            if ((item.Value.Name == name && item.Value.Swag.CompareTo(lo) >= 0) || (item.Value.Name == name && item.Value.Swag.CompareTo(hi) < 0))
                thisKur.Add(item.Value);
        }

        if (thisKur.Count == 0)
            throw new InvalidOperationException();

        return thisKur;
    }

    public IEnumerable<Battlecard> GetByNameOrderedBySwagDescending(string name)
    {
        List<Battlecard> thisKur = new List<Battlecard>();

        foreach (var item in royals)
        {
            if (item.Value.Name == name)
                thisKur.Add(item.Value);
        }

        if (thisKur.Count == 0)
            throw new InvalidOperationException();

        return thisKur.OrderByDescending(x => x.Swag).ThenBy(x => x.Id);
    }

    public IEnumerable<Battlecard> GetByTypeAndDamageRangeOrderedByDamageThenById(CardType type, int lo, int hi)
    {
        List<Battlecard> thisKur = new List<Battlecard>();

        foreach (var item in royals)
        {
            if ((item.Value.Type == type && item.Value.Damage.CompareTo(lo) >= 0) || (item.Value.Type == type && item.Value.Damage.CompareTo(hi) <= 0))
                thisKur.Add(item.Value);
        }

        if (thisKur.Count == 0)
            throw new InvalidOperationException();

        return thisKur.OrderByDescending(x => x.Damage).ThenBy(x => x.Id);
    }

    public void RemoveById(int id)
    {
        if (royals.ContainsKey(id))
            royals.Remove(id);
        else
            throw new InvalidOperationException();
    }

    public IEnumerator<Battlecard> GetEnumerator()
    {
        foreach (var item in royals)
        {
            yield return item.Value;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return (IEnumerator)GetEnumerator();
    }
}
