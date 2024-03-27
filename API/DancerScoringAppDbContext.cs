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

        modelBuilder.Entity<Category>()
            .HasKey(c => c.Id);

        modelBuilder.Entity<Category>().HasData(
            new Category
            {
                Id = 1,
                AgeCategory = AgeCategory.MiniCadet,
                Requisite = RequisiteType.Baton,
                CategoryType = CategoryType.Sport,
                SquadType = SquadType.Formation
            },
            new Category
            {
                Id = 2,
                AgeCategory = AgeCategory.Cadet,
                Requisite = RequisiteType.Baton,
                CategoryType = CategoryType.Sport,
                SquadType = SquadType.Formation
            },
            new Category
            {
                Id = 3,
                AgeCategory = AgeCategory.Junior,
                Requisite = RequisiteType.Baton,
                CategoryType = CategoryType.Sport,
                SquadType = SquadType.Formation
            },
            new Category
            {
                Id = 4,
                AgeCategory = AgeCategory.Senior,
                Requisite = RequisiteType.Baton,
                CategoryType = CategoryType.Sport,
                SquadType = SquadType.Formation
            },

            // Baton Sport - Mini formacja
            new Category
            {
                Id = 5,
                AgeCategory = AgeCategory.Cadet,
                Requisite = RequisiteType.Baton,
                CategoryType = CategoryType.Sport,
                SquadType = SquadType.MiniFormation
            },
            new Category
            {
                Id = 6,
                AgeCategory = AgeCategory.Junior,
                Requisite = RequisiteType.Baton,
                CategoryType = CategoryType.Sport,
                SquadType = SquadType.MiniFormation
            },
            new Category
            {
                Id = 7,
                AgeCategory = AgeCategory.Senior,
                Requisite = RequisiteType.Baton,
                CategoryType = CategoryType.Sport,
                SquadType = SquadType.MiniFormation
            },

            // Baton Sport - Duo/trio
            new Category
            {
                Id = 8,
                AgeCategory = AgeCategory.Cadet,
                Requisite = RequisiteType.Baton,
                CategoryType = CategoryType.Sport,
                SquadType = SquadType.DuoTrio
            },
            new Category
            {
                Id = 9,
                AgeCategory = AgeCategory.Junior,
                Requisite = RequisiteType.Baton,
                CategoryType = CategoryType.Sport,
                SquadType = SquadType.DuoTrio
            },
            new Category
            {
                Id = 10,
                AgeCategory = AgeCategory.Senior,
                Requisite = RequisiteType.Baton,
                CategoryType = CategoryType.Sport,
                SquadType = SquadType.DuoTrio
            },

            // Baton Sport - Solo
            new Category
            {
                Id = 11,
                AgeCategory = AgeCategory.Cadet,
                Requisite = RequisiteType.Baton,
                CategoryType = CategoryType.Sport,
                SquadType = SquadType.Solo
            },
            new Category
            {
                Id = 12,
                AgeCategory = AgeCategory.Junior,
                Requisite = RequisiteType.Baton,
                CategoryType = CategoryType.Sport,
                SquadType = SquadType.Solo
            },
            new Category
            {
                Id = 13,
                AgeCategory = AgeCategory.Senior,
                Requisite = RequisiteType.Baton,
                CategoryType = CategoryType.Sport,
                SquadType = SquadType.Solo
            },

            // 2x Baton - Solo
            new Category
            {
                Id = 14,
                AgeCategory = AgeCategory.Cadet,
                Requisite = RequisiteType.Baton,
                CategoryType = CategoryType.TwoBatons,
                SquadType = SquadType.Solo
            },
            new Category
            {
                Id = 15,
                AgeCategory = AgeCategory.Junior,
                Requisite = RequisiteType.Baton,
                CategoryType = CategoryType.TwoBatons,
                SquadType = SquadType.Solo
            },
            new Category
            {
                Id = 16,
                AgeCategory = AgeCategory.Senior,
                Requisite = RequisiteType.Baton,
                CategoryType = CategoryType.TwoBatons,
                SquadType = SquadType.Solo
            },
            // 2x Baton - Duo/trio
            new Category
            {
                Id = 17,
                AgeCategory = AgeCategory.Cadet,
                Requisite = RequisiteType.Baton,
                CategoryType = CategoryType.TwoBatons,
                SquadType = SquadType.DuoTrio
            },
            new Category
            {
                Id = 18,
                AgeCategory = AgeCategory.Junior,
                Requisite = RequisiteType.Baton,
                CategoryType = CategoryType.TwoBatons,
                SquadType = SquadType.DuoTrio
            },
            new Category
            {
                Id = 19,
                AgeCategory = AgeCategory.Senior,
                Requisite = RequisiteType.Baton,
                CategoryType = CategoryType.TwoBatons,
                SquadType = SquadType.DuoTrio
            },

            // Baton Acrobatic - Duo/trio
            new Category
            {
                Id = 20,
                AgeCategory = AgeCategory.Cadet,
                Requisite = RequisiteType.Baton,
                CategoryType = CategoryType.Acrobatic,
                SquadType = SquadType.DuoTrio
            },
            new Category
            {
                Id = 21,
                AgeCategory = AgeCategory.Junior,
                Requisite = RequisiteType.Baton,
                CategoryType = CategoryType.Acrobatic,
                SquadType = SquadType.DuoTrio
            },
            new Category
            {
                Id = 22,
                AgeCategory = AgeCategory.Senior,
                Requisite = RequisiteType.Baton,
                CategoryType = CategoryType.Acrobatic,
                SquadType = SquadType.DuoTrio
            },

            // Baton Acrobatic - Solo
            new Category
            {
                Id = 23,
                AgeCategory = AgeCategory.Cadet,
                Requisite = RequisiteType.Baton,
                CategoryType = CategoryType.Acrobatic,
                SquadType = SquadType.Solo
            },
            new Category
            {
                Id = 24,
                AgeCategory = AgeCategory.Junior,
                Requisite = RequisiteType.Baton,
                CategoryType = CategoryType.Acrobatic,
                SquadType = SquadType.Solo
            },
            new Category
            {
                Id = 25,
                AgeCategory = AgeCategory.Senior,
                Requisite = RequisiteType.Baton,
                CategoryType = CategoryType.Acrobatic,
                SquadType = SquadType.Solo
            },
            // Pompon Classic - Prezentacja sceniczna
            new Category
            {
                Id = 26,
                AgeCategory = AgeCategory.Cadet,
                Requisite = RequisiteType.Pompon,
                CategoryType = CategoryType.Classic,
                SquadType = SquadType.Formation
            },
            new Category
            {
                Id = 27,
                AgeCategory = AgeCategory.Junior,
                Requisite = RequisiteType.Pompon,
                CategoryType = CategoryType.Classic,
                SquadType = SquadType.Formation
            },
            new Category
            {
                Id = 28,
                AgeCategory = AgeCategory.Senior,
                Requisite = RequisiteType.Pompon,
                CategoryType = CategoryType.Classic,
                SquadType = SquadType.Formation
            },

            // Pompon Basic - Prezentacja sceniczna
            new Category
            {
                Id = 29,
                AgeCategory = AgeCategory.Cadet,
                Requisite = RequisiteType.Pompon,
                CategoryType = CategoryType.Basic,
                SquadType = SquadType.Formation
            },
            new Category
            {
                Id = 30,
                AgeCategory = AgeCategory.Junior,
                Requisite = RequisiteType.Pompon,
                CategoryType = CategoryType.Basic,
                SquadType = SquadType.Formation
            },
            new Category
            {
                Id = 31,
                AgeCategory = AgeCategory.Senior,
                Requisite = RequisiteType.Pompon,
                CategoryType = CategoryType.Basic,
                SquadType = SquadType.Formation
            },

            // Pompon Sport - Prezentacja sceniczna
            new Category
            {
                Id = 32,
                AgeCategory = AgeCategory.MiniCadet,
                Requisite = RequisiteType.Pompon,
                CategoryType = CategoryType.Sport,
                SquadType = SquadType.Formation
            },
            new Category
            {
                Id = 33,
                AgeCategory = AgeCategory.Cadet,
                Requisite = RequisiteType.Pompon,
                CategoryType = CategoryType.Sport,
                SquadType = SquadType.Formation
            },
            // Pompon Sport - Mini formacja, Duo/trio, Solo
            new Category
            {
                Id = 34,
                AgeCategory = AgeCategory.MiniCadet,
                Requisite = RequisiteType.Pompon,
                CategoryType = CategoryType.Sport,
                SquadType = SquadType.MiniFormation
            },
            new Category
            {
                Id = 35,
                AgeCategory = AgeCategory.Cadet,
                Requisite = RequisiteType.Pompon,
                CategoryType = CategoryType.Sport,
                SquadType = SquadType.MiniFormation
            },
            new Category
            {
                Id = 36,
                AgeCategory = AgeCategory.Junior,
                Requisite = RequisiteType.Pompon,
                CategoryType = CategoryType.Sport,
                SquadType = SquadType.MiniFormation
            },
            new Category
            {
                Id = 37,
                AgeCategory = AgeCategory.Senior,
                Requisite = RequisiteType.Pompon,
                CategoryType = CategoryType.Sport,
                SquadType = SquadType.MiniFormation
            },
            new Category
            {
                Id = 38,
                AgeCategory = AgeCategory.MiniCadet,
                Requisite = RequisiteType.Pompon,
                CategoryType = CategoryType.Sport,
                SquadType = SquadType.DuoTrio
            },
            new Category
            {
                Id = 39,
                AgeCategory = AgeCategory.Cadet,
                Requisite = RequisiteType.Pompon,
                CategoryType = CategoryType.Sport,
                SquadType = SquadType.DuoTrio
            },
            new Category
            {
                Id = 40,
                AgeCategory = AgeCategory.Junior,
                Requisite = RequisiteType.Pompon,
                CategoryType = CategoryType.Sport,
                SquadType = SquadType.DuoTrio
            },
            new Category
            {
                Id = 41,
                AgeCategory = AgeCategory.Senior,
                Requisite = RequisiteType.Pompon,
                CategoryType = CategoryType.Sport,
                SquadType = SquadType.DuoTrio
            },
            new Category
            {
                Id = 42,
                AgeCategory = AgeCategory.MiniCadet,
                Requisite = RequisiteType.Pompon,
                CategoryType = CategoryType.Sport,
                SquadType = SquadType.Solo
            },
            new Category
            {
                Id = 43,
                AgeCategory = AgeCategory.Cadet,
                Requisite = RequisiteType.Pompon,
                CategoryType = CategoryType.Sport,
                SquadType = SquadType.Solo
            },
            new Category
            {
                Id = 44,
                AgeCategory = AgeCategory.Junior,
                Requisite = RequisiteType.Pompon,
                CategoryType = CategoryType.Sport,
                SquadType = SquadType.Solo
            },
            new Category
            {
                Id = 45,
                AgeCategory = AgeCategory.Senior,
                Requisite = RequisiteType.Pompon,
                CategoryType = CategoryType.Sport,
                SquadType = SquadType.Solo
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


        modelBuilder.Entity<PenaltyPoint>()
            .Property(c => c.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<PenaltyPoint>().HasData(
            new PenaltyPoint
            {
                Id = (int)PenaltyType.MissingGreeting,
                Name = "Missing greeting",
                PenaltyScore = -0.05m
            },
            new PenaltyPoint
            {
                Id = (int)PenaltyType.PropOrCostumePieceDrop,
                Name = "Prop or costume piece drop",
                PenaltyScore = -0.05m
            },
            new PenaltyPoint
            {
                Id = (int)PenaltyType.LeavingPropOnStageAfterDrop,
                Name = "Leaving prop on stage after drop",
                PenaltyScore = -0.05m
            },
            new PenaltyPoint
            {
                Id = (int)PenaltyType.TimeOverrun,
                Name = "For every second time overrun",
                PenaltyScore = -0.05m
            },
            new PenaltyPoint
            {
                Id = (int)PenaltyType.NotReadyAfterAnnouncement,
                Name = "Team or solist is not ready after announcement",
                PenaltyScore = -0.1m
            },
            new PenaltyPoint
            {
                Id = (int)PenaltyType.CrossingLine,
                Name = "Crossing a line",
                PenaltyScore = -0.1m
            },
            new PenaltyPoint
            {
                Id = (int)PenaltyType.IncorrectlyMixedMusic,
                Name = "Incorrectyly mixed music",
                PenaltyScore = -0.1m
            },
            new PenaltyPoint
            {
                Id = (int)PenaltyType.DancerSupport,
                Name = "The dancer supports themselves",
                PenaltyScore = -0.1m
            },
            new PenaltyPoint
            {
                Id = (int)PenaltyType.EarlyEntrance,
                Name = "Too early entrance",
                PenaltyScore = -0.2m
            },
            new PenaltyPoint
            {
                Id = (int)PenaltyType.MissingStopFigure,
                Name = "Missing stop figure at the end",
                PenaltyScore = -0.2m
            },
            new PenaltyPoint
            {
                Id = (int)PenaltyType.MissingSingleRequiredCostumeElement,
                Name = "Missing single required costume element",
                PenaltyScore = -0.3m
            },
            new PenaltyPoint
            {
                Id = (int)PenaltyType.CommunicationDuringPresentation,
                Name = "Communication between dancers or dancers and coach during presentation",
                PenaltyScore = -0.3m
            },
            new PenaltyPoint
            {
                Id = (int)PenaltyType.MissingPropContact,
                Name = "Missing prop contact",
                PenaltyScore = -0.3m
            },
            new PenaltyPoint
            {
                Id = (int)PenaltyType.MissingLiftAssurance,
                Name = "Missing lift assurance",
                PenaltyScore = -0.3m
            },
            new PenaltyPoint
            {
                Id = (int)PenaltyType.MissingSingleRequiredElement,
                Name = "Missing single required element",
                PenaltyScore = -0.4m
            },
            new PenaltyPoint
            {
                Id = (int)PenaltyType.CompetitorFall,
                Name = "Dancer fall", PenaltyScore = -0.4m
            },
            new PenaltyPoint
            {
                Id = (int)PenaltyType.PuttingDownPropForMoreThan16MarchingSteps,
                Name = "Putting down prop for more than 16 matching steps",
                PenaltyScore = -0.4m
            },
            new PenaltyPoint
            {
                Id = (int)PenaltyType.NonRegulationMusic,
                Name = "Non regulation music",
                PenaltyScore = -0.4m
            },
            new PenaltyPoint
            {
                Id = (int)PenaltyType.DifferentChoreographyFromPreviousQualifications,
                Name = "Different choreography from previous qualifications",
                PenaltyScore = -0.4m
            },
            new PenaltyPoint
            {
                Id = (int)PenaltyType.NonRegulationPropOrCostumeElement,
                Name = "Non regulation prop or costume element",
                PenaltyScore = -0.4m
            },
            new PenaltyPoint
            {
                Id = (int)PenaltyType.ThreeTieredPyramids,
                Name = "Three tiered pyramids",
                PenaltyScore = -3m
            },
            new PenaltyPoint
            {
                Id = (int)PenaltyType.CategoryProhibitedElements,
                Name = "Category prohibited elements",
                PenaltyScore = -3m
            },
            new PenaltyPoint
            {
                Id = (int)PenaltyType.UnjustifiedLiftsAndThrows,
                Name = "Unjustified lifts and throws",
                PenaltyScore = -3m
            },
            new PenaltyPoint
            {
                Id = (int)PenaltyType.PlacingNonDedicatedPropsOnStage,
                Name = "Placing non dedicated props on stage",
                PenaltyScore = -3m
            },
            new PenaltyPoint
            {
                Id = (int)PenaltyType.Disqualification,
                Name = "Disqualification",
                PenaltyScore = -100m
            }
        );
    }

    #region Enitites

    public DbSet<Bonus> Bonuses { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Coach> Coaches { get; set; }
    public DbSet<Competition> Competitions { get; set; }
    public DbSet<Dancer> Dancers { get; set; }
    public DbSet<JudgeRating> JudgeRatings { get; set; }
    public DbSet<PenaltyPoint> PenaltyPoints { get; set; }

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