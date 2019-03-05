using System;
using System.Collections.Generic;
using System.Linq;

public class Olympics : IOlympics
{
    Dictionary<int, Competitor> competitors;
    Dictionary<int, Competition> competition;

    public Olympics()
    {
        competition = new Dictionary<int, Competition>();
        competitors = new Dictionary<int, Competitor>();
    }

    public void AddCompetition(int id, string name, int participantsLimit)
    {
        if (competition.ContainsKey(id))
            throw new ArgumentException();

        competition.Add(id, new Competition(name, id, participantsLimit));
    }

    public void AddCompetitor(int id, string name)
    {
        if (competitors.ContainsKey(id))
            throw new ArgumentException();

        competitors.Add(id, new Competitor(id, name));
    }

    public void Compete(int competitorId, int competitionId)
    {
        if (!competition.ContainsKey(competitionId) || !competitors.ContainsKey(competitorId))
            throw new ArgumentException();

        competition[competitionId].Competitors.Add(competitors[competitorId]);
        competitors[competitorId].TotalScore += competition[competitionId].Score;
    }

    public int CompetitionsCount()
    {
        return competition.Count;
    }

    public int CompetitorsCount()
    {
        return competitors.Count;
    }

    public bool Contains(int competitionId, Competitor comp)
    {
        if (!competition.ContainsKey(competitionId))
            throw new ArgumentException();

        foreach (var item in competition[competitionId].Competitors)
        {
            if (comp.Equals(item))
                return true;
        }

        return false;
    }

    public void Disqualify(int competitionId, int competitorId)
    {
        if (!competitors.ContainsKey(competitorId) || !competition.ContainsKey(competitionId))
            throw new ArgumentException();

        competition[competitionId].Competitors.Remove(competitors[competitorId]);
        competitors[competitorId].TotalScore -= competition[competitionId].Score;
    }

    public IEnumerable<Competitor> FindCompetitorsInRange(long min, long max)
    {
        List<Competitor> competitor = new List<Competitor>();

        foreach (var item in competitors.Select(x => x.Value))
        {
            if (item.TotalScore > min && item.TotalScore <= max)
                competitor.Add(item);
        }

        return competitor.OrderBy(x => x.Id);
    }

    public IEnumerable<Competitor> GetByName(string name)
    {
        List<Competitor> competitor = new List<Competitor>();

        foreach (var item in competitors)
        {
            if (item.Value.Name == name)
                competitor.Add(item.Value);
        }

        if (competitor.Count == 0)
            throw new ArgumentException();

        return competitor.OrderBy(x => x.Id);
    }

    public Competition GetCompetition(int id)
    {
        if (!competition.ContainsKey(id))
            throw new ArgumentException();

        return competition[id];
    }

    public IEnumerable<Competitor> SearchWithNameLength(int min, int max)
    {
        List<Competitor> competitor = new List<Competitor>();

        foreach (var item in competitors)
        {
            if (item.Value.Name.Length >= min && item.Value.Name.Length <= max)
                competitor.Add(item.Value);
        }

        return competitor.OrderBy(x => x.Id);
    }
}