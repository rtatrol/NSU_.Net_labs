
using System.ComponentModel.DataAnnotations;

namespace DreamTeam.Data;

public class Hackaton
{
    [Key]
    public int Id { get; set; }
    public List<Junior> Juniors { get; set; }
    public List<JuniorPreference> JuniorPreferences { get; set; }
    public List<TeamLead> TeamLeaders { get; set; }
    public List<TeamLeadPreference> TeamLeadPreferences { get; set; }
    public List<Team> Teams { get; set; }
    public decimal AverageHarmony { get; set; }
}

public class HackatonJunior
{
    public int HackatonId { get; set; }
    public Hackaton Hackaton { get; set; }
    public int JuniorId { get; set; }
    public Junior Junior { get; set; }
}
public class Junior
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Hackaton> Hackatons { get; set; }
}
public class JuniorPreference
{
    [Key]
    public int Id { get; set; }
    public int HackatonId { get; set; }
    public Hackaton Hackaton { get; set; }
    public int JuniorId { get; set; }
    public int TeamLeadId { get; set; }
    public int Rank { get; set; }
}
public class HackatonTeamLead
{
    public int HackatonId { get; set; }
    public Hackaton Hackaton { get; set; }
    public int TeamLeadId { get; set; }
    public TeamLead TeamLead { get; set; }
}
public class TeamLead
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Hackaton> Hackatons { get; set; }
}
public class TeamLeadPreference
{
    [Key]
    public int Id { get; set; }
    public int HackatonId { get; set; }
    public Hackaton Hackaton { get; set; }
    public int TeamLeadId { get; set; }
    public int JuniorId { get; set; }
    public int Rank { get; set; }
}
public class Team
{
    [Key]
    public int Id { get; set; }
    public int HackatonId { get; set; }
    public Hackaton Hackaton { get; set; }
    public int JuniorId { get; set; }
    public int TeamLeadId { get; set; }
}
