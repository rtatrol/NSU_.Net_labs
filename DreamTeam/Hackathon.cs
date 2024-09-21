using System.Collections.Generic;

namespace DreamTeam
{
    class Hackaton
    {

        List<Junior> juniors;
        List<TeamLead> team_leaders;
        static Random rand = new Random();
        public Hackaton(List<Junior> Juniors, List<TeamLead> Team_leaders)
        {
            juniors = Juniors;
            team_leaders = Team_leaders;
        }

        public double Run()
        {
            HRManager hrmanager = new HRManager(juniors, team_leaders);
            MakePrefer();
            List<(Junior, TeamLead)> teams = hrmanager.make_teams();
            double harmony = hrmanager.calculate_harmony(teams);
            return harmony;
        }

        private void MakePrefer()
        {
            var shaffle_jun = new List<Junior>(juniors);
            var shaffle_lead = new List<TeamLead>(team_leaders);
            foreach (var jun in juniors)
            {
                Shaffle<TeamLead>(shaffle_lead);
                jun.preferences = new List<TeamLead>(shaffle_lead);
            }
            foreach (var lead in team_leaders)
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
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
