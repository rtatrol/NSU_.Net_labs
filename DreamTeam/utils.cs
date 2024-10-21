
namespace DreamTeam
{
    class FileParser
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
    }
}
