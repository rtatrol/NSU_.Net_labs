using System.Linq.Expressions;

namespace DreamTeam
{
    public class Person
    {
        public string id, Name;

        public Person(string csvLine)
        {
            string[] columns = csvLine.Split(';');
            id = columns[0];
            Name = columns[1];
        }
    }
    public class Junior : Person
    {
        public List<TeamLead> preferences;
        public Junior(string name) : base(name)
        {
            preferences = new List<TeamLead>();
        }
        public static List<Junior> Make_list(List<string> list)
        {
            var jun_list = new List<Junior>();
            foreach (var line in list)
            {
                var new_june = new Junior(line);
                jun_list.Add(new_june);
            }
            return jun_list;
        }
    }
    public class TeamLead : Person
    {
        public List<Junior> preferences;
        public TeamLead(string name) : base(name)
        {
            preferences = new List<Junior>();
        }
        public static List<TeamLead> Make_list(List<string> list)
        {
            var lead_list = new List<TeamLead>();
            foreach (var line in list)
            {
                var new_lead = new TeamLead(line);
                lead_list.Add(new_lead);
            }
            return lead_list;
        }
    }

}
