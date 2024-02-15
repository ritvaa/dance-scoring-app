using DancerScoringApp.Entities;
using Microsoft.EntityFrameworkCore;

public class DancerScoringDbContext : DbContext
{
    public DbSet<AgeCategory> AgeCategories { get; set; }
    public DbSet<Bonus> Bonuses { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Coach> Coaches { get; set; }
    public DbSet<Competition> Competitions { get; set; }
    public DbSet<Dancer> Dancers { get; set; }
    public DbSet<JudgeRating> JudgeRatings { get; set; }
    public DbSet<PenaltyPoints> PenaltyPoints { get; set; }
    public DbSet<PenaltyPointsRating> PenaltyPointsRatings { get; set; }
    public DbSet<RatingBonus> RatingBonuses { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Routine> Routines { get; set; }
    public DbSet<Squad> Squads { get; set; }
    public DbSet<SquadDancer> SquadDancers { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<TeamCoach> TeamCoaches { get; set; }
    public DbSet<TechJudgeRating> TechJudgeRatings { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserCompetition> UserCompetitions { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(
            "Host=localhost;Port=5432;Database=DanceScoringApp;Username=postgres;Password=P@ssw0rd");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}