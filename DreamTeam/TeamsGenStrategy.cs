namespace DreamTeam
{
    public interface ITeamsGenStrategy
    {
        public List<(Junior, TeamLead)> MakeTeams(List<Junior> juniors, List<TeamLead> teamLeaders);
    }
    public class FirstPriorityStrategy : ITeamsGenStrategy
    {
        public List<(Junior, TeamLead)> MakeTeams(List<Junior> juniors, List<TeamLead> teamLeaders)
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
