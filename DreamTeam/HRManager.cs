
namespace DreamTeam
{

    class HRManager
    {
        List<Junior> juniors;
        List<TeamLead> team_leaders;
        public HRManager(List<Junior> Juniors, List<TeamLead> Team_leaders)
        {
            juniors = Juniors;
            team_leaders = Team_leaders;
        }

        public List<(Junior, TeamLead)> make_teams()
        {
            List<(Junior, TeamLead)> teams = new List<(Junior, TeamLead)>();
            var stayed_leaders = new List<TeamLead>(team_leaders);
            foreach (var jun in juniors)
            {
                foreach (var lead in jun.preferences)
                {
                    if (stayed_leaders.Contains(lead))
                    {
                        stayed_leaders.Remove(lead);
                        teams.Add((jun, lead));
                        break;
                    }
                }
            }

            return teams;
        }
        public double calculate_harmony(List<(Junior, TeamLead)> teams)
        {
            double total = 0;
            int junior_prefer_len = teams[0].Item1.preferences.Count;
            int team_leaders_prefer_len = teams[0].Item2.preferences.Count;
            foreach (var (jun, lead) in teams)
            {
                double jun_score = junior_prefer_len - jun.preferences.IndexOf(lead);
                double lead_score = team_leaders_prefer_len - lead.preferences.IndexOf(jun);
                total += (jun_score + lead_score);
            }
            return total / (2 * teams.Count);
        }
    }
}
