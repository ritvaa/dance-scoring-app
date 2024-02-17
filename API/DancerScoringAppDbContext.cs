using DancerScoringApp.Entities;
using Microsoft.EntityFrameworkCore;

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
            new AgeCategory { Id = (int)AgeCategoryType.Cadet, AgeCategoryType = AgeCategoryType.Cadet },
            new AgeCategory { Id = (int)AgeCategoryType.Junior, AgeCategoryType = AgeCategoryType.Junior },
            new AgeCategory { Id = (int)AgeCategoryType.Senior, AgeCategoryType = AgeCategoryType.Senior },
            new AgeCategory { Id = (int)AgeCategoryType.GrandSenior, AgeCategoryType = AgeCategoryType.GrandSenior }
        );                              

        //modelBuilder.Entity<Category>().HasData(

        //    new Category { Prop = PropType.Baton, CategoryType = CategoryType.Sport, AgeCategoryId = (int)AgeCategoryType.Cadet},
        //    new Category { Prop = PropType.Baton, CategoryType = CategoryType.Sport, AgeCategoryId = (int)AgeCategoryType.Junior },
        //    new Category { Prop = PropType.Baton, CategoryType = CategoryType.Sport, AgeCategoryId = (int)AgeCategoryType.Senior },
        //    new Category { Prop = PropType.Baton, CategoryType = CategoryType.Sport, AgeCategoryId = (int)AgeCategoryType.GrandSenior },

        //    new Category { Prop = PropType.Baton, CategoryType = CategoryType.Classic, AgeCategoryId = (int)AgeCategoryType.Cadet },
        //    new Category { Prop = PropType.Baton, CategoryType = CategoryType.Classic, AgeCategoryId = (int)AgeCategoryType.Junior},
        //    new Category { Prop = PropType.Baton, CategoryType = CategoryType.Classic, AgeCategoryId = (int)AgeCategoryType.Senior},

        //    new Category { Prop = PropType.Baton, CategoryType = CategoryType.Acrobatic, AgeCategoryId = (int)AgeCategoryType.Cadet },
        //    new Category { Prop = PropType.Baton, CategoryType = CategoryType.Acrobatic, AgeCategoryId = (int)AgeCategoryType.Junior },
        //    new Category { Prop = PropType.Baton, CategoryType = CategoryType.Acrobatic, AgeCategoryId = (int)AgeCategoryType.Senior },

        //    new Category { Prop = PropType.Baton, CategoryType = CategoryType.Basic, AgeCategoryId = (int)AgeCategoryType.Cadet },
        //    new Category { Prop = PropType.Baton, CategoryType = CategoryType.Basic, AgeCategoryId = (int)AgeCategoryType.Junior },
        //    new Category { Prop = PropType.Baton, CategoryType = CategoryType.Basic, AgeCategoryId = (int)AgeCategoryType.Senior },

        //    new Category { Prop = PropType.Baton, CategoryType = CategoryType.TwoBatons, AgeCategoryId = (int)AgeCategoryType.Cadet },
        //    new Category { Prop = PropType.Baton, CategoryType = CategoryType.TwoBatons, AgeCategoryId = (int)AgeCategoryType.Junior },
        //    new Category { Prop = PropType.Baton, CategoryType = CategoryType.TwoBatons, AgeCategoryId = (int)AgeCategoryType.Senior },

        //    new Category { Prop = PropType.Pompon, CategoryType = CategoryType.Sport, AgeCategoryId = (int)AgeCategoryType.Cadet},
        //    new Category { Prop = PropType.Pompon, CategoryType = CategoryType.Sport, AgeCategoryId = (int)AgeCategoryType.Junior },
        //    new Category { Prop = PropType.Pompon, CategoryType = CategoryType.Sport, AgeCategoryId = (int)AgeCategoryType.Senior },
        //    new Category { Prop = PropType.Pompon, CategoryType = CategoryType.Sport, AgeCategoryId = (int)AgeCategoryType.GrandSenior },

        //    new Category { Prop = PropType.Pompon, CategoryType = CategoryType.Classic, AgeCategoryId = (int)AgeCategoryType.Cadet },
        //    new Category { Prop = PropType.Pompon, CategoryType = CategoryType.Classic, AgeCategoryId = (int)AgeCategoryType.Junior },
        //    new Category { Prop = PropType.Pompon, CategoryType = CategoryType.Classic, AgeCategoryId = (int)AgeCategoryType.Senior },

        //    new Category { Prop = PropType.Pompon, CategoryType = CategoryType.Basic, AgeCategoryId = (int)AgeCategoryType.Cadet },
        //    new Category { Prop = PropType.Pompon, CategoryType = CategoryType.Basic, AgeCategoryId = (int)AgeCategoryType.Junior },
        //    new Category { Prop = PropType.Pompon, CategoryType = CategoryType.Basic, AgeCategoryId = (int)AgeCategoryType.Senior }
        //);


        modelBuilder.Entity<Bonus>().HasData(
            new Bonus { Id = 1, Name = BonusType.Originality },
            new Bonus { Id = 2, Name = BonusType.Synchronization },
            new Bonus { Id = 3, Name = BonusType.PerfectSynchronization },
            new Bonus { Id = 4,  Name = BonusType.PresenceAndElegance }
        );

        modelBuilder.Entity<Role>()
            .Property(c => c.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, Name = RoleType.SuperAdmin },
            new Role { Id = 2, Name = RoleType.Judge },
            new Role { Id = 3, Name = RoleType.TechnicalJudge },
            new Role { Id = 4, Name = RoleType.Scrutineer }
        );


        modelBuilder.Entity<PenaltyPoints>()
            .Property(c => c.Id)
            .ValueGeneratedOnAdd();


        modelBuilder.Entity<PenaltyPoints>().HasData(
            new PenaltyPoints {Id = 1, Name = PenaltyType.MissingGreeting, PenaltyScore = -0.05m},
            new PenaltyPoints {Id = 2, Name = PenaltyType.PropOrCostumePieceDrop, PenaltyScore = -0.05m},
            new PenaltyPoints {Id = 3, Name = PenaltyType.LeavingPropOnStageAfterDrop, PenaltyScore = -0.05m },
            new PenaltyPoints {Id = 4, Name = PenaltyType.TimeOverrun, PenaltyScore = -0.05m },

            new PenaltyPoints {Id = 5, Name = PenaltyType.NotReadyAfterAnnouncement, PenaltyScore = -0.1m },
            new PenaltyPoints {Id = 6, Name = PenaltyType.CrossingLine, PenaltyScore = -0.1m },
            new PenaltyPoints {Id = 7, Name = PenaltyType.IncorrectlyMixedMusic, PenaltyScore = -0.1m },
            new PenaltyPoints {Id = 8, Name = PenaltyType.DancerSupport, PenaltyScore = -0.1m }

            //new PenaltyPoints {Name = PenaltyType.EarlyEntrance, PenaltyScore = -0.2m },
            //new PenaltyPoints {Name = PenaltyType.MissingStopFigure, PenaltyScore = -0.2m },

            //new PenaltyPoints {Name = PenaltyType.MissingSingleRequiredCostumeElement, PenaltyScore = -0.3m },
            //new PenaltyPoints {Name = PenaltyType.CommunicationDuringPresentation, PenaltyScore = -0.3m },
            //new PenaltyPoints {Name = PenaltyType.MissingPropContact, PenaltyScore = -0.3m },
            //new PenaltyPoints {Name = PenaltyType.MissingLiftAssurance, PenaltyScore = -0.3m },

            //new PenaltyPoints {Name = PenaltyType.MissingSingleRequiredElement, PenaltyScore = -0.4m },
            //new PenaltyPoints {Name = PenaltyType.CompetitorFall, PenaltyScore = -0.4m },
            //new PenaltyPoints {Name = PenaltyType.PuttingDownPropForMoreThan16MarchingSteps, PenaltyScore = -0.4m },
            //new PenaltyPoints {Name = PenaltyType.NonRegulationMusic, PenaltyScore = -0.4m },
            //new PenaltyPoints {Name = PenaltyType.DifferentChoreographyFromPreviousQualifications, PenaltyScore = -0.4m },
            //new PenaltyPoints {Name = PenaltyType.NonRegulationPropOrCostumeElement, PenaltyScore = -0.4m },

            //new PenaltyPoints {Name = PenaltyType.ThreeTieredPyramids, PenaltyScore = -3m },
            //new PenaltyPoints {Name = PenaltyType.CategoryProhibitedElements, PenaltyScore = -3m },
            //new PenaltyPoints {Name = PenaltyType.UnjustifiedLiftsAndThrows, PenaltyScore = -3m },
            //new PenaltyPoints {Name = PenaltyType.PlacingNonDedicatedPropsOnStage, PenaltyScore = -3m },

            //new PenaltyPoints {Name = PenaltyType.Disqualification, PenaltyScore = -100m}

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