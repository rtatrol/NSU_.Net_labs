namespace DreamTeam
{
    public interface IWishlistGenStrategy
    {
        public void MakePrefer(List<Junior> juniors, List<TeamLead> teamLeaders);
    }
    public class RandomGenStrategy : IWishlistGenStrategy
    {
        public void MakePrefer(List<Junior> juniors, List<TeamLead> teamLeaders)
        {
            var shaffle_jun = new List<Junior>(juniors);
            var shaffle_lead = new List<TeamLead>(teamLeaders);
            foreach (var jun in juniors)
            {
                Utils.Shaffle<TeamLead>(shaffle_lead);
                jun.preferences = new List<TeamLead>(shaffle_lead);
            }
            foreach (var lead in teamLeaders)
            {
                Utils.Shaffle<Junior>(shaffle_jun);
                lead.preferences = new List<Junior>(shaffle_jun);
            }
        }
    }
}
