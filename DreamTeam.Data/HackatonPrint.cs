using Microsoft.EntityFrameworkCore;
namespace DreamTeam.Data;

class HackatonPrinter
{
    public void PrintHackatonDetails(int hackatonId)
    {
        using (var context = new HackatonDbContext())
        {
            var hackaton = context.Hackatons
                .Include(h => h.Teams)
                .FirstOrDefault(h => h.Id == hackatonId);

            if (hackaton == null)
            {
                Console.WriteLine("Hackaton not found!");
                return;
            }

            Console.WriteLine($"Hackaton ID: {hackaton.Id}");
            Console.WriteLine($"Average Harmony: {hackaton.AverageHarmony}");

            foreach (var team in hackaton.Teams)
            {
                var junior = context.Juniors.Find(team.JuniorId);
                var teamLead = context.TeamLeads.Find(team.TeamLeadId);

                Console.WriteLine($"Team: Junior: {junior.Name}, TeamLead: {teamLead.Name}");
            }
        }
    }
    public void PrintAverageHarmonyAcrossHackatons()
    {
        using (var context = new HackatonDbContext())
        {
            var averageHarmony = context.Hackatons.Average(h => h.AverageHarmony);
            Console.WriteLine($"Average Harmony across all hackatons: {averageHarmony}");
        }
    }
}
