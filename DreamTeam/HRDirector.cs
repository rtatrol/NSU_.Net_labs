namespace DreamTeam
{
    public class HRDirector
    {
        List<(Junior, TeamLead)> teams;
        public HRDirector(List<(Junior, TeamLead)> Teams)
        {
            teams = new List<(Junior, TeamLead)>(Teams);
        }

        public double CalculateHarmony()
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
