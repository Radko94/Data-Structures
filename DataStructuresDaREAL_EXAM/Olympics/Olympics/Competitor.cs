public class Competitor
{
    public Competitor(int id, string name)
    {
        this.Id = id;
        this.Name = name;
        this.TotalScore = 0;
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public long TotalScore { get; set; }

    public override bool Equals(object obj)
    {
        if (!(obj is Competitor))
            return false;

        Competitor otherCompetitor = (Competitor)obj;

        if (Id == otherCompetitor.Id)
        {
            if (Name == otherCompetitor.Name) return true;
            else return false;
        }
        else return false;
    }
}
