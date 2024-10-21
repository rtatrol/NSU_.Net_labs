namespace DreamTeam
{
    public class Person
    {
        public string id, name;

        public Person(string csvLine)
        {
            string[] columns = csvLine.Split(';');
            id = columns[0];
            name = columns[1];
        }
    }
    public class Junior : Person
    {
        public List<TeamLead> preferences;

        public Junior(string name) : base(name)
        {
            preferences = [];
        }

        public static List<Junior> MakeList(List<string> list)
        {
            var junList = new List<Junior>();
            foreach (var line in list)
            {
                var newJune = new Junior(line);
                junList.Add(newJune);
            }
            return junList;
        }
    }
    public class TeamLead : Person
    {
        public List<Junior> preferences;
        public TeamLead(string name) : base(name)
        {
            preferences = [];
        }

        public static List<TeamLead> MakeList(List<string> list)
        {
            var leadList = new List<TeamLead>();
            foreach (var line in list)
            {
                var newLead = new TeamLead(line);
                leadList.Add(newLead);
            }
            return leadList;
        }
    }
}
