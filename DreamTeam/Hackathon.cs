using System.Collections.Generic;

namespace DreamTeam
{
    class Hackaton
    {
        List<Junior> juniors;
        List<TeamLead> teamLeaders;
        static Random rand = new Random();
        public Hackaton(List<Junior> Juniors, List<TeamLead> TeamLeaders)
        {
            juniors = Juniors;
            teamLeaders = TeamLeaders;
        }

        public double Run()
        {
            HRManager hrManager = new HRManager(juniors, teamLeaders);
            MakePrefer();
            List<(Junior, TeamLead)> teams = hrManager.MakeTeams();
            HRDirector hrDirector = new HRDirector(teams);
            double harmony = hrDirector.CalculateHarmony();
            return harmony;
        }

        private void MakePrefer()
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
                int k = rand.Next(n + 1);
                (list[n], list[k]) = (list[k], list[n]);
            }
        }
    }
}
