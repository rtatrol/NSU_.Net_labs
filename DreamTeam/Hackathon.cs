namespace DreamTeam
{
    public class Hackaton(HRManager hRManager, HRDirector hRDirector, IWishlistGenStrategy wishlistGen)
    {
        HRManager _hrManager = hRManager;
        HRDirector _hrDirector = hRDirector;
        IWishlistGenStrategy _wishlistGen = wishlistGen;

        public double Run(List<Junior> juniors, List<TeamLead> teamLeaders)
        {
            _wishlistGen.MakePrefer(juniors, teamLeaders);
            List<(Junior, TeamLead)> teams = _hrManager.StrategyRun(juniors, teamLeaders);
            double harmony = _hrDirector.CalculateHarmony(teams);
            return harmony;
        }
    }
}
