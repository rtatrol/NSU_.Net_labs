
namespace DreamTeam
{
    public class HRManager(ITeamsGenStrategy teamsGen)
    {
        ITeamsGenStrategy _teamsGen = teamsGen;
        public List<(Junior, TeamLead)> StrategyRun(List<Junior> juniors, List<TeamLead> teamLeaders)
        {
            return _teamsGen.MakeTeams(juniors, teamLeaders);
        }
    }
}
