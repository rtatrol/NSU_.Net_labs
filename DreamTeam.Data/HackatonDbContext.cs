using Microsoft.EntityFrameworkCore;

namespace DreamTeam.Data;

public class HackatonDbContext : DbContext
{
    public DbSet<Hackaton> Hackatons { get; set; }
    public DbSet<HackatonJunior> HackatonJuniors { get; set; }
    public DbSet<Junior> Juniors { get; set; }
    public DbSet<JuniorPreference> JuniorPreferences { get; set; }
    public DbSet<HackatonTeamLead> HackatonTeamLeads { get; set; }
    public DbSet<TeamLead> TeamLeads { get; set; }
    public DbSet<TeamLeadPreference> TeamLeadPreferences { get; set; }
    public DbSet<Team> Teams { get; set; }

    public HackatonDbContext() { }
    public HackatonDbContext(DbContextOptions<HackatonDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = "Host=localhost;Database=hackaton_db;Username=postgres;Password=postgres";
            optionsBuilder.UseNpgsql(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // modelBuilder.Entity<Junior>().HasKey(j => j.Id);
        // modelBuilder.Entity<Junior>().Property(j=> j.Id).ValueGeneratedOnAdd();

        // modelBuilder.Entity<TeamLead>().HasKey(tl => tl.Id);
        // modelBuilder.Entity<TeamLead>().Property(tl=> tl.Id).ValueGeneratedOnAdd();

        // modelBuilder.Entity<Hackaton>()
        // .HasKey(h => h.Id);

        modelBuilder.Entity<Hackaton>()
        .HasMany(h => h.Juniors)
        .WithMany(j => j.Hackatons)
        .UsingEntity<HackatonJunior>(
            hj => hj.HasOne(hj => hj.Junior).WithMany().HasForeignKey(hj => hj.JuniorId),
            hj => hj.HasOne(hj => hj.Hackaton).WithMany().HasForeignKey(hj => hj.HackatonId)
        );

        modelBuilder.Entity<Hackaton>()
        .HasMany(h => h.TeamLeaders)
        .WithMany(j => j.Hackatons)
        .UsingEntity<HackatonTeamLead>(
            htl => htl.HasOne(htl => htl.TeamLead).WithMany().HasForeignKey(htl => htl.TeamLeadId),
            htl => htl.HasOne(htl => htl.Hackaton).WithMany().HasForeignKey(htl => htl.HackatonId)
        );

        modelBuilder.Entity<Hackaton>()
        .HasMany(h => h.Teams)
        .WithOne(t => t.Hackaton)
        .HasForeignKey(t => t.HackatonId)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Hackaton>()
        .HasMany(h => h.JuniorPreferences)
        .WithOne(jp => jp.Hackaton)
        .HasForeignKey(jp => jp.HackatonId)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Hackaton>()
        .HasMany(h => h.TeamLeadPreferences)
        .WithOne(tlp => tlp.Hackaton)
        .HasForeignKey(tlp => tlp.HackatonId)
        .OnDelete(DeleteBehavior.Cascade);

    }
}
