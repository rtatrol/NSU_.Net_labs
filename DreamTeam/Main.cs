
namespace DreamTeam
{
    class Program
    {
        static void Main(string[] args)
        {
            FileParser parser = new FileParser();

            List<Junior> juniors = parser.ParseJuniors("data/Juniors20.csv");
            List<TeamLead> teamLeaders = parser.ParseTeamLeads("data/Teamleads20.csv");

            Hackaton hackaton = new Hackaton(juniors, teamLeaders);

            int hackatonsNum = 1000;
            double summaryResult = 0;
            double[] harmonyResults = new double[hackatonsNum];

            for (int i = 0; i < hackatonsNum; i++)
            {
                double harmony = hackaton.Run();
                summaryResult += harmony;
                harmonyResults[i] = harmony;
            }

            for (int i = 0; i < hackatonsNum; i++)
            {
                Console.WriteLine($"Hackaton№ {i+1}, result {harmonyResults[i]}");
            }
            Console.WriteLine($"MEAN Harmony {summaryResult / hackatonsNum}");
        }
    }
}
