using DreamTeam;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
namespace DreamTeam.Data;

class HackatonService
{
    public int CreateHackaton()
    {
        using (var context = new HackatonDbContext())
        {
            var hackaton = new Hackaton
            {
                Juniors = new List<Junior>(),
                JuniorPreferences = new List<JuniorPreference>(),
                TeamLeaders = new List<TeamLead>(),
                TeamLeadPreferences = new List<TeamLeadPreference>(),
                Teams = new List<Team>(),
                AverageHarmony = 0
            };

            context.Hackatons.Add(hackaton);

            var parser = new Utils.FileParser();
            var juniors = parseJuniors(parser.FileRead("../../../../DreamTeam/bin/Debug/net8.0/data/Juniors20.csv"));
            var teamLeads = parseTeamLeads(parser.FileRead("../../../../DreamTeam/bin/Debug/net8.0/data/Teamleads20.csv"));

            hackaton.Juniors.AddRange(juniors);
            hackaton.TeamLeaders.AddRange(teamLeads);
            
            context.SaveChanges();

            List<int> numbers = Enumerable.Range(1, 20).ToList();
            Utils.Shaffle<int>(numbers);

            foreach (var junior in juniors)
            {
                int numbPos = 0;
                foreach (var teamLead in teamLeads)
                {
                    hackaton.JuniorPreferences.Add(new JuniorPreference
                    {
                        HackatonId = hackaton.Id,
                        JuniorId = junior.Id,
                        TeamLeadId = teamLead.Id,
                        Rank = numbers[numbPos++]
                    });
                }
                Utils.Shaffle<int>(numbers);
            }

            foreach (var teamLead in teamLeads)
            {
                int numbPos = 0;
                foreach (var junior in juniors)
                {
                    hackaton.TeamLeadPreferences.Add(new TeamLeadPreference
                    {
                        HackatonId = hackaton.Id,
                        TeamLeadId = teamLead.Id,
                        JuniorId = junior.Id,
                        Rank = numbers[numbPos++]
                    });
                }
                Utils.Shaffle<int>(numbers);
            }

            for (int i = 0; i < Math.Min(juniors.Count, teamLeads.Count); i++)
            {
                var team = new Team
                {
                    HackatonId = hackaton.Id,
                    JuniorId = juniors[i].Id,
                    TeamLeadId = teamLeads[i].Id
                };

                hackaton.Teams.Add(team);
            }

            hackaton.AverageHarmony = CalculateHarmony(hackaton);

            context.SaveChanges();
            return hackaton.Id;
        }
    }

    public void RemoveHackaton(int hackatonId)
    {
        using (var context = new HackatonDbContext())
        {
            var hackatonToDelete = context.Hackatons.FirstOrDefault(h => h.Id == hackatonId);
            if (hackatonToDelete == null)
            {
                Console.WriteLine("Hackaton not found!");
                return;
            }

            var hacJuneToDelete = context.HackatonJuniors.Where(hj => hj.HackatonId == hackatonId);
            context.HackatonJuniors.RemoveRange(hacJuneToDelete);

            var hacTLToDelete = context.HackatonTeamLeads.Where(htl => htl.HackatonId == hackatonId);
            context.HackatonTeamLeads.RemoveRange(hacTLToDelete);

            context.Hackatons.Remove(hackatonToDelete);

            context.SaveChanges();
        }
    }

    private decimal CalculateHarmony(Hackaton hackaton)
    {
        var values = new List<double>();

        using (var context = new HackatonDbContext())
        {
            foreach (var team in hackaton.Teams)
            {
                var juniorPreference = hackaton.JuniorPreferences
                    .FirstOrDefault(jp => jp.JuniorId == team.JuniorId && jp.TeamLeadId == team.TeamLeadId);

                var teamLeadPreference = hackaton.TeamLeadPreferences
                    .FirstOrDefault(tlp => tlp.TeamLeadId == team.TeamLeadId && tlp.JuniorId == team.JuniorId);

                if (juniorPreference != null && teamLeadPreference != null)
                {
                    values.Add(juniorPreference.Rank);
                    values.Add(teamLeadPreference.Rank);
                }
            }
        }
        double result = values.Count / values.Sum(v => 1 / v);
        return (decimal)result;
    }

    private List<Junior> parseJuniors(List<string> lines)
    {
        var juniors = new List<Junior>();
        foreach (var line in lines)
        {
            string[] columns = line.Split(';');
            int id = int.Parse(columns[0]);
            string name = columns[1];
            var june = new Junior() { Name = $"{id}_{name}" };
            juniors.Add(june);
        }
        return juniors;
    }
    private List<TeamLead> parseTeamLeads(List<string> lines)
    {
        var teamLeads = new List<TeamLead>();
        foreach (var line in lines)
        {
            string[] columns = line.Split(';');
            int id = int.Parse(columns[0]);
            string name = columns[1];
            teamLeads.Add(new TeamLead() { Name = $"{id}_{name}" });
        }
        return teamLeads;
    }
}
