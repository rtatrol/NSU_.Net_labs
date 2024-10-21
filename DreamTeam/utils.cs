using System.Collections.Generic;
namespace DreamTeam
{
    class Utils
    {
        public class FileParser
        {
            public List<string> FileRead(string filePath)
            {
                List<string> personList = [];
                using (StreamReader reader = new StreamReader(filePath))
                {
                    if (reader != null)
                    {
                        string headerLine = reader.ReadLine()!;
                        while (!reader.EndOfStream)
                        {
                            string line = reader.ReadLine()!;
                            personList.Add(line);
                        }
                    }
                }
                return personList;
            }

            public List<Junior> ParseJuniors(string filePath)
            {
                return Junior.MakeList(FileRead(filePath));
            }

            public List<TeamLead> ParseTeamLeads(string filePath)
            {
                return TeamLead.MakeList(FileRead(filePath));
            }
        };

        public static Random rand = new Random();
        public static void Shaffle<T>(List<T> list)
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
