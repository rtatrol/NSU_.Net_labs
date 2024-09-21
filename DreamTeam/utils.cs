using System.IO;
namespace DreamTeam
{
    class FileParser
    {
        public static List<string> Parse(string filePath)
        {
            List<string> personList = new List<string>();
            using (StreamReader reader = new StreamReader(filePath))
            {
                string headerLine = reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    personList.Add(line);
                }
            }
            return personList;
        }
    }
}
