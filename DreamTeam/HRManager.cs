
namespace DreamTeam
{
    class HRManager
    {
        readonly List<Junior> juniors;
        readonly List<TeamLead> teamLeaders;
        public HRManager(List<Junior> Juniors, List<TeamLead> TeamLeaders)
        {
            juniors = Juniors;
            teamLeaders = TeamLeaders;
        }

        public List<(Junior, TeamLead)> MakeTeams()
        {
            var teams = new List<(Junior, TeamLead)>();
            var stayedLeaders = new List<TeamLead>(teamLeaders);
            foreach (var jun in juniors)
            {
                foreach (var lead in jun.preferences)
                {
                    if (stayedLeaders.Contains(lead))
                    {
                        stayedLeaders.Remove(lead);
                        teams.Add((jun, lead));
                        break;
                    }
                }
            }
            return teams;
        }
    }
}
