
namespace DreamTeam.Data;

class Program
{
    static void Main(string[] args)
    {
        using (var context = new HackatonDbContext())
        {
            context.Database.EnsureCreated();
            var hackatonService = new HackatonService();

            //hackatonService.RemoveHackaton(12);

            int newId = hackatonService.CreateHackaton();

            var hackatonPrinter = new HackatonPrinter();
            hackatonPrinter.PrintHackatonDetails(newId);

            hackatonPrinter.PrintAverageHarmonyAcrossHackatons();
        }
    }
}
