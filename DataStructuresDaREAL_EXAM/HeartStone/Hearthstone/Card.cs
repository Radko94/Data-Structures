using System;
using System.Collections.Generic;
using System.Text;

public class Card
{

    public Card(string name, int damage, int score, int level)
    {
        this.Name = name;
        this.Damage = damage;
        this.Score = score;
        this.Level = level;
        this.Health = 20;
    }
    public string Name { get; set; }

    public int Damage { get; set; }

    public int Score { get; set; }

    public int Health { get; set; }

    public int Level { get; set; }

    public override bool Equals(object obj)
    {
        if (!(obj is Card))
            return false;

        Card otherCard = (Card)obj;

        if (Name == otherCard.Name)
        {
            if (Damage == otherCard.Damage)
            {
                if (Score == otherCard.Score)
                {
                    if (Health == otherCard.Health)
                    {
                        if (Level == otherCard.Level) return true;
                        else return false;
                    }
                    else return false;
                }
                else return false;
            }
            else return false;
        }
        else return false;
    }
}