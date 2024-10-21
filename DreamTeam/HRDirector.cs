namespace DreamTeam
{
    public interface IHRDirector
    {
        public double CalculateHarmony(List<(Junior, TeamLead)> teams);
    }
    public class HRDirector : IHRDirector
    {

        public double CalculateHarmony(List<(Junior, TeamLead)> teams)
        {
            double total = 0;
            int junior_prefer_len = teams[0].Item1.preferences.Count;
            int team_leaders_prefer_len = teams[0].Item2.preferences.Count;
            foreach (var (jun, lead) in teams)
            {
                double jun_score = junior_prefer_len - jun.preferences.IndexOf(lead);
                double lead_score = team_leaders_prefer_len - lead.preferences.IndexOf(jun);
                total += (1 / jun_score + 1 / lead_score);
            }
            return (2 * teams.Count) / total;
        }
    }
}
