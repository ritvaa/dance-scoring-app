using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API;

public class DancerScoringAppDbContext : DbContext
{
    public DancerScoringAppDbContext(DbContextOptions<DancerScoringAppDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "Host=localhost;Port=5432;Database=DanceScoringApp;Username=postgres;Password=P@ssw0rd";
        optionsBuilder.UseNpgsql(connectionString);
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<JudgeRating>()
            .HasOne(jr => jr.Routine)
            .WithMany(r => r.JudgeRating);


        modelBuilder.Entity<AgeCategory>().HasData(
            new AgeCategory { Id = (int)AgeCategoryType.Cadet, Name = "Cadet" },
            new AgeCategory { Id = (int)AgeCategoryType.Junior, Name = "Junior" },
            new AgeCategory { Id = (int)AgeCategoryType.Senior, Name = "Senior" },
            new AgeCategory { Id = (int)AgeCategoryType.GrandSenior, Name = "Grand Senior" }
        );

        modelBuilder.Entity<Category>().HasData(
            new Category
            {
                Id = 1, Prop = PropType.Baton, CategoryType = CategoryType.Sport,
                AgeCategoryId = (int)AgeCategoryType.Cadet
            },
            new Category
            {
                Id = 2, Prop = PropType.Baton, CategoryType = CategoryType.Sport,
                AgeCategoryId = (int)AgeCategoryType.Junior
            },
            new Category
            {
                Id = 3, Prop = PropType.Baton, CategoryType = CategoryType.Sport,
                AgeCategoryId = (int)AgeCategoryType.Senior
            },
            new Category
            {
                Id = 4, Prop = PropType.Baton, CategoryType = CategoryType.Sport,
                AgeCategoryId = (int)AgeCategoryType.GrandSenior
            },
            new Category
            {
                Id = 5, Prop = PropType.Baton, CategoryType = CategoryType.Classic,
                AgeCategoryId = (int)AgeCategoryType.Cadet
            },
            new Category
            {
                Id = 6, Prop = PropType.Baton, CategoryType = CategoryType.Classic,
                AgeCategoryId = (int)AgeCategoryType.Junior
            },
            new Category
            {
                Id = 7, Prop = PropType.Baton, CategoryType = CategoryType.Classic,
                AgeCategoryId = (int)AgeCategoryType.Senior
            },
            new Category
            {
                Id = 8, Prop = PropType.Baton, CategoryType = CategoryType.Acrobatic,
                AgeCategoryId = (int)AgeCategoryType.Cadet
            },
            new Category
            {
                Id = 9, Prop = PropType.Baton, CategoryType = CategoryType.Acrobatic,
                AgeCategoryId = (int)AgeCategoryType.Junior
            },
            new Category
            {
                Id = 10, Prop = PropType.Baton, CategoryType = CategoryType.Acrobatic,
                AgeCategoryId = (int)AgeCategoryType.Senior
            },
            new Category
            {
                Id = 11, Prop = PropType.Baton, CategoryType = CategoryType.Basic,
                AgeCategoryId = (int)AgeCategoryType.Cadet
            },
            new Category
            {
                Id = 12, Prop = PropType.Baton, CategoryType = CategoryType.Basic,
                AgeCategoryId = (int)AgeCategoryType.Junior
            },
            new Category
            {
                Id = 13, Prop = PropType.Baton, CategoryType = CategoryType.Basic,
                AgeCategoryId = (int)AgeCategoryType.Senior
            },
            new Category
            {
                Id = 14, Prop = PropType.Baton, CategoryType = CategoryType.TwoBatons,
                AgeCategoryId = (int)AgeCategoryType.Cadet
            },
            new Category
            {
                Id = 15, Prop = PropType.Baton, CategoryType = CategoryType.TwoBatons,
                AgeCategoryId = (int)AgeCategoryType.Junior
            },
            new Category
            {
                Id = 16, Prop = PropType.Baton, CategoryType = CategoryType.TwoBatons,
                AgeCategoryId = (int)AgeCategoryType.Senior
            },
            new Category
            {
                Id = 17, Prop = PropType.Pompon, CategoryType = CategoryType.Sport,
                AgeCategoryId = (int)AgeCategoryType.Cadet
            },
            new Category
            {
                Id = 18, Prop = PropType.Pompon, CategoryType = CategoryType.Sport,
                AgeCategoryId = (int)AgeCategoryType.Junior
            },
            new Category
            {
                Id = 19, Prop = PropType.Pompon, CategoryType = CategoryType.Sport,
                AgeCategoryId = (int)AgeCategoryType.Senior
            },
            new Category
            {
                Id = 20, Prop = PropType.Pompon, CategoryType = CategoryType.Sport,
                AgeCategoryId = (int)AgeCategoryType.GrandSenior
            },
            new Category
            {
                Id = 21, Prop = PropType.Pompon, CategoryType = CategoryType.Classic,
                AgeCategoryId = (int)AgeCategoryType.Cadet
            },
            new Category
            {
                Id = 22, Prop = PropType.Pompon, CategoryType = CategoryType.Classic,
                AgeCategoryId = (int)AgeCategoryType.Junior
            },
            new Category
            {
                Id = 23, Prop = PropType.Pompon, CategoryType = CategoryType.Classic,
                AgeCategoryId = (int)AgeCategoryType.Senior
            },
            new Category
            {
                Id = 24, Prop = PropType.Pompon, CategoryType = CategoryType.Basic,
                AgeCategoryId = (int)AgeCategoryType.Cadet
            },
            new Category
            {
                Id = 25, Prop = PropType.Pompon, CategoryType = CategoryType.Basic,
                AgeCategoryId = (int)AgeCategoryType.Junior
            },
            new Category
            {
                Id = 26, Prop = PropType.Pompon, CategoryType = CategoryType.Basic,
                AgeCategoryId = (int)AgeCategoryType.Senior
            }
        );


        modelBuilder.Entity<Bonus>().HasData(
            new Bonus { Id = (int)BonusType.Originality, Name = "Orginality" },
            new Bonus { Id = (int)BonusType.Synchronization, Name = "Sychronization" },
            new Bonus { Id = (int)BonusType.PerfectSynchronization, Name = "Perfect Synchronization" },
            new Bonus { Id = (int)BonusType.PresenceAndElegance, Name = "Presence and Elegance" }
        );

        modelBuilder.Entity<Role>()
            .Property(c => c.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Role>().HasData(
            new Role { Id = (int)RoleType.SuperAdmin, Name = "Super Admin" },
            new Role { Id = (int)RoleType.Judge, Name = "Judge" },
            new Role { Id = (int)RoleType.TechnicalJudge, Name = "Technical Judge" },
            new Role { Id = (int)RoleType.Scrutineer, Name = "Scrutineer" }
        );


        modelBuilder.Entity<PenaltyPoints>()
            .Property(c => c.Id)
            .ValueGeneratedOnAdd();


        modelBuilder.Entity<PenaltyPoints>().HasData(
            new PenaltyPoints
            {
                Id = (int)PenaltyType.MissingGreeting,
                Name = "Missing greeting",
                PenaltyScore = -0.05m
            },
            new PenaltyPoints
            {
                Id = (int)PenaltyType.PropOrCostumePieceDrop,
                Name = "Prop or costume piece drop",
                PenaltyScore = -0.05m
            },
            new PenaltyPoints
            {
                Id = (int)PenaltyType.LeavingPropOnStageAfterDrop,
                Name = "Leaving prop on stage after drop",
                PenaltyScore = -0.05m
            },
            new PenaltyPoints
            {
                Id = (int)PenaltyType.TimeOverrun,
                Name = "For every second time overrun",
                PenaltyScore = -0.05m
            },
            new PenaltyPoints
            {
                Id = (int)PenaltyType.NotReadyAfterAnnouncement,
                Name = "Team or solist is not ready after announcement",
                PenaltyScore = -0.1m
            },
            new PenaltyPoints
            {
                Id = (int)PenaltyType.CrossingLine,
                Name = "Crossing a line",
                PenaltyScore = -0.1m
            },
            new PenaltyPoints
            {
                Id = (int)PenaltyType.IncorrectlyMixedMusic,
                Name = "Incorrectyly mixed music",
                PenaltyScore = -0.1m
            },
            new PenaltyPoints
            {
                Id = (int)PenaltyType.DancerSupport,
                Name = "The dancer supports themselves",
                PenaltyScore = -0.1m
            },
            new PenaltyPoints
            {
                Id = (int)PenaltyType.EarlyEntrance,
                Name = "Too early entrance",
                PenaltyScore = -0.2m
            },
            new PenaltyPoints
            {
                Id = (int)PenaltyType.MissingStopFigure,
                Name = "Missing stop figure at the end",
                PenaltyScore = -0.2m
            },
            new PenaltyPoints
            {
                Id = (int)PenaltyType.MissingSingleRequiredCostumeElement,
                Name = "Missing single required costume element",
                PenaltyScore = -0.3m
            },
            new PenaltyPoints
            {
                Id = (int)PenaltyType.CommunicationDuringPresentation,
                Name = "Communication between dancers or dancers and coach during presentation",
                PenaltyScore = -0.3m
            },
            new PenaltyPoints
            {
                Id = (int)PenaltyType.MissingPropContact,
                Name = "Missing prop contact",
                PenaltyScore = -0.3m
            },
            new PenaltyPoints
            {
                Id = (int)PenaltyType.MissingLiftAssurance,
                Name = "Missing lift assurance",
                PenaltyScore = -0.3m
            },
            new PenaltyPoints
            {
                Id = (int)PenaltyType.MissingSingleRequiredElement,
                Name = "Missing single required element",
                PenaltyScore = -0.4m
            },
            new PenaltyPoints
            {
                Id = (int)PenaltyType.CompetitorFall,
                Name = "Dancer fall", PenaltyScore = -0.4m
            },
            new PenaltyPoints
            {
                Id = (int)PenaltyType.PuttingDownPropForMoreThan16MarchingSteps,
                Name = "Putting down prop for more than 16 matching steps",
                PenaltyScore = -0.4m
            },
            new PenaltyPoints
            {
                Id = (int)PenaltyType.NonRegulationMusic,
                Name = "Non regulation music",
                PenaltyScore = -0.4m
            },
            new PenaltyPoints
            {
                Id = (int)PenaltyType.DifferentChoreographyFromPreviousQualifications,
                Name = "Different choreography from previous qualifications",
                PenaltyScore = -0.4m
            },
            new PenaltyPoints
            {
                Id = (int)PenaltyType.NonRegulationPropOrCostumeElement,
                Name = "Non regulation prop or costume element",
                PenaltyScore = -0.4m
            },
            new PenaltyPoints
            {
                Id = (int)PenaltyType.ThreeTieredPyramids,
                Name = "Three tiered pyramids",
                PenaltyScore = -3m
            },
            new PenaltyPoints
            {
                Id = (int)PenaltyType.CategoryProhibitedElements,
                Name = "Category prohibited elements",
                PenaltyScore = -3m
            },
            new PenaltyPoints
            {
                Id = (int)PenaltyType.UnjustifiedLiftsAndThrows,
                Name = "Unjustified lifts and throws",
                PenaltyScore = -3m
            },
            new PenaltyPoints
            {
                Id = (int)PenaltyType.PlacingNonDedicatedPropsOnStage,
                Name = "Placing non dedicated props on stage",
                PenaltyScore = -3m
            },
            new PenaltyPoints
            {
                Id = (int)PenaltyType.Disqualification,
                Name = "Disqualification",
                PenaltyScore = -100m
            }
        );
    }

    #region Enitites

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

    #endregion
}