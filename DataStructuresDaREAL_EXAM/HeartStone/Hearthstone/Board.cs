using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Board : IBoard
{
    Dictionary<string, Card> cards;

    public Board()
    {
        cards = new Dictionary<string, Card>();
    }

    public bool Contains(string name)
    {
        return cards.ContainsKey(name);
    }

    public int Count()
    {
        return cards.Count;
    }

    public void Draw(Card card)
    {
        cards.Add(card.Name, card);
    }

    public IEnumerable<Card> GetBestInRange(int start, int end)
    {
        List<Card> returnThis = new List<Card>();

        foreach (var item in cards)
        {
            if (item.Value.Score >= start && item.Value.Score <= end)
                returnThis.Add(item.Value);
        }

        return returnThis.OrderByDescending(x => x.Level);
    }

    public void Heal(int health)
    {
        var kur = cards.Select(x => x.Value).OrderByDescending(x => x.Health).Last();
        kur.Health += health;
            //= cards.Select(x => x.Value).OrderByDescending(x => x.Health).TakeLast(cards.Count - 1).Select(x => x.Health += health);
    }

    public IEnumerable<Card> ListCardsByPrefix(string prefix)
    {
        List<Card> card = new List<Card>();

        foreach (var item in cards)
        {
            if (item.Key.StartsWith(prefix))
                card.Add(item.Value);
        }

        return card.OrderBy(x => x.Name.Reverse()).ThenBy(x => x.Level);
        //return card.OrderBy(x => x.Name.Reverse()/*.ToCharArray().Reverse().Sum(y => y)*/).OrderBy(x => x.Level);
    }

    public void Play(string attackerCardName, string attackedCardName)
    {
        if (!cards.ContainsKey(attackerCardName) || !cards.ContainsKey(attackedCardName))
            throw new ArgumentException();

        if (cards[attackerCardName].Level != cards[attackedCardName].Level)
            throw new ArgumentException();

        var kur = cards[attackerCardName];
        var kur2 = cards[attackedCardName];

        if (cards[attackerCardName].Health > 0 && cards[attackedCardName].Health > 0)
        {
            cards[attackedCardName].Health -= cards[attackerCardName].Damage;

            if (cards[attackedCardName].Health <= 0)
            {
                cards[attackerCardName].Score += cards[attackedCardName].Level;
            }
        }
    }

    public void Remove(string name)
    {
        if (!cards.ContainsKey(name))
            throw new ArgumentException();

        cards.Remove(name);
    }

    public void RemoveDeath()
    {
        var well = cards.Select(x => x.Value).Where(x => x.Health <= 0).Select(x => x.Name).ToList();

        for (int i = 0; i < well.Count; i++)
        {
            cards.Remove(well[i]);
        }
    }

    public IEnumerable<Card> SearchByLevel(int level)
    {
        return cards.Select(x => x.Value).Where(x => x.Level == level).OrderByDescending(x => x.Score);
    }
}