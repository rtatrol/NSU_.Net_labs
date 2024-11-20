namespace DreamTeam
{

    public class HRDirector
    {
        public double CalculateHarmony(List<(Junior, TeamLead)> teams)
        {
            List<double> values = new();
            int junior_prefer_len = teams[0].Item1.preferences.Count;
            int team_leaders_prefer_len = teams[0].Item2.preferences.Count;
            foreach (var (jun, lead) in teams)
            {
                double jun_score = junior_prefer_len - jun.preferences.IndexOf(lead);
                double lead_score = team_leaders_prefer_len - lead.preferences.IndexOf(jun);
                values.Add(jun_score);
                values.Add(lead_score);
            }
            return CalculateTotalValue(values);
        }

        public double CalculateTotalValue(List<double> values)
        {
            if (values == null || values.Count == 0)
                throw new ArgumentException("Values cannot be null or empty");
            return values.Count / values.Sum(v => 1 / v);
        }
    }
}
