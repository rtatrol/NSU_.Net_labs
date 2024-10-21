using System.Collections.Generic;

namespace DreamTeam
{
    public interface IHackaton
    {
        public static Random rand = new Random();
        public double Run(List<Junior> juniors, List<TeamLead> teamLeaders);
        public void MakePrefer(List<Junior> juniors, List<TeamLead> teamLeaders);

    }
    class Hackaton : IHackaton
    {
        IHRManager _hrManager;
        IHRDirector _hrDirector;
        public Hackaton(IHRManager hRManager, IHRDirector hRDirector)
        {
            _hrManager = hRManager;
            _hrDirector = hRDirector;
        }
        public double Run(List<Junior> juniors, List<TeamLead> teamLeaders)
        {
            //HRManager hrManager = new HRManager();
            MakePrefer(juniors, teamLeaders);
            List<(Junior, TeamLead)> teams = _hrManager.MakeTeams(juniors, teamLeaders);
            //HRDirector hrDirector = new HRDirector();
            double harmony = _hrDirector.CalculateHarmony(teams);
            return harmony;
        }

        public void MakePrefer(List<Junior> juniors, List<TeamLead> teamLeaders)
        {
            var shaffle_jun = new List<Junior>(juniors);
            var shaffle_lead = new List<TeamLead>(teamLeaders);
            foreach (var jun in juniors)
            {
                Shaffle<TeamLead>(shaffle_lead);
                jun.preferences = new List<TeamLead>(shaffle_lead);
            }
            foreach (var lead in teamLeaders)
            {
                Shaffle<Junior>(shaffle_jun);
                lead.preferences = new List<Junior>(shaffle_jun);
            }
        }

        static void Shaffle<T>(List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = IHackaton.rand.Next(n + 1);
                (list[n], list[k]) = (list[k], list[n]);
            }
        }
    }
}
