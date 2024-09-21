
namespace DreamTeam
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Junior> juniors = Junior.Make_list(FileParser.Parse("data/Juniors20.csv"));
            List<TeamLead> team_leaders = TeamLead.Make_list(FileParser.Parse("data/Teamleads20.csv"));

            Hackaton hackaton = new Hackaton(juniors, team_leaders);

            int hackatonsNum = 1000;
            double summaryResult = 0;
            double[] harmony_results = new double[hackatonsNum];

            for (int i = 0; i < hackatonsNum; i++)
            {
                double harmony = hackaton.Run();
                summaryResult += harmony;
                harmony_results[i] = harmony;
            }

            for (int i = 0; i < hackatonsNum; i++)
            {
                Console.WriteLine($"Hackaton№ {i+1}, result {harmony_results[i]}");
            }
            Console.WriteLine($"MEAN Harmony {summaryResult / hackatonsNum}");
        }
    }
}
