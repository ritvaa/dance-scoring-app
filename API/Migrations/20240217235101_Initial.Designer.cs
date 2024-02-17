﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(DancerScoringAppDbContext))]
    [Migration("20240217235101_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DancerScoringApp.Entities.AgeCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AgeCategoryType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("AgeCategories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AgeCategoryType = 1
                        },
                        new
                        {
                            Id = 2,
                            AgeCategoryType = 2
                        },
                        new
                        {
                            Id = 3,
                            AgeCategoryType = 3
                        },
                        new
                        {
                            Id = 4,
                            AgeCategoryType = 4
                        });
                });

            modelBuilder.Entity("DancerScoringApp.Entities.Bonus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Name")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Bonuses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = 0
                        },
                        new
                        {
                            Id = 2,
                            Name = 1
                        },
                        new
                        {
                            Id = 3,
                            Name = 2
                        },
                        new
                        {
                            Id = 4,
                            Name = 3
                        });
                });

            modelBuilder.Entity("DancerScoringApp.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AgeCategoryId")
                        .HasColumnType("integer");

                    b.Property<int>("CategoryType")
                        .HasColumnType("integer");

                    b.Property<int>("Prop")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AgeCategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("DancerScoringApp.Entities.Coach", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Coaches");
                });

            modelBuilder.Entity("DancerScoringApp.Entities.Competition", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Competitions");
                });

            modelBuilder.Entity("DancerScoringApp.Entities.Dancer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LicenceId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("TeamId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("Dancers");
                });

            modelBuilder.Entity("DancerScoringApp.Entities.JudgeRating", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("BodyTechniquePoints")
                        .HasColumnType("numeric");

                    b.Property<decimal>("ChoreographyPoints")
                        .HasColumnType("numeric");

                    b.Property<bool>("HasBonus")
                        .HasColumnType("boolean");

                    b.Property<decimal>("PropWorkPoints")
                        .HasColumnType("numeric");

                    b.Property<Guid>("RoutineId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RoutineId");

                    b.HasIndex("UserId");

                    b.ToTable("JudgeRatings");
                });

            modelBuilder.Entity("DancerScoringApp.Entities.PenaltyPoints", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Name")
                        .HasColumnType("integer");

                    b.Property<decimal>("PenaltyScore")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("PenaltyPoints");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = 0,
                            PenaltyScore = -0.05m
                        },
                        new
                        {
                            Id = 2,
                            Name = 1,
                            PenaltyScore = -0.05m
                        },
                        new
                        {
                            Id = 3,
                            Name = 2,
                            PenaltyScore = -0.05m
                        },
                        new
                        {
                            Id = 4,
                            Name = 3,
                            PenaltyScore = -0.05m
                        },
                        new
                        {
                            Id = 5,
                            Name = 4,
                            PenaltyScore = -0.1m
                        },
                        new
                        {
                            Id = 6,
                            Name = 5,
                            PenaltyScore = -0.1m
                        },
                        new
                        {
                            Id = 7,
                            Name = 6,
                            PenaltyScore = -0.1m
                        },
                        new
                        {
                            Id = 8,
                            Name = 7,
                            PenaltyScore = -0.1m
                        });
                });

            modelBuilder.Entity("DancerScoringApp.Entities.PenaltyPointsRating", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("PenaltyPointsId")
                        .HasColumnType("uuid");

                    b.Property<int>("PenaltyPointsId1")
                        .HasColumnType("integer");

                    b.Property<Guid>("TechJudgeRatingId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PenaltyPointsId1");

                    b.HasIndex("TechJudgeRatingId");

                    b.ToTable("PenaltyPointsRatings");
                });

            modelBuilder.Entity("DancerScoringApp.Entities.RatingBonus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("BonusId1")
                        .HasColumnType("integer");

                    b.Property<Guid>("JudgeRatingId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("BonusId1");

                    b.HasIndex("JudgeRatingId");

                    b.ToTable("RatingBonuses");
                });

            modelBuilder.Entity("DancerScoringApp.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Name")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = 0
                        },
                        new
                        {
                            Id = 2,
                            Name = 1
                        },
                        new
                        {
                            Id = 3,
                            Name = 2
                        },
                        new
                        {
                            Id = 4,
                            Name = 3
                        });
                });

            modelBuilder.Entity("DancerScoringApp.Entities.Routine", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<Guid?>("CompetitionId")
                        .HasColumnType("uuid");

                    b.Property<bool?>("GrandPrix")
                        .HasColumnType("boolean");

                    b.Property<string>("RoutineName")
                        .HasColumnType("text");

                    b.Property<decimal>("Score")
                        .HasColumnType("numeric");

                    b.Property<Guid>("SquadId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CompetitionId");

                    b.HasIndex("SquadId");

                    b.ToTable("Routines");
                });

            modelBuilder.Entity("DancerScoringApp.Entities.Squad", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("PerformanceType")
                        .HasColumnType("integer");

                    b.Property<Guid>("TeamId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("Squads");
                });

            modelBuilder.Entity("DancerScoringApp.Entities.SquadDancer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("DancerId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SquadId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("DancerId");

                    b.HasIndex("SquadId");

                    b.ToTable("SquadDancers");
                });

            modelBuilder.Entity("DancerScoringApp.Entities.Team", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("DancerScoringApp.Entities.TeamCoach", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CoachId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TeamId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CoachId");

                    b.HasIndex("TeamId");

                    b.ToTable("TeamCoaches");
                });

            modelBuilder.Entity("DancerScoringApp.Entities.TechJudgeRating", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("PenaltyPointId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoutineId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RoutineId")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("TechJudgeRatings");
                });

            modelBuilder.Entity("DancerScoringApp.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DancerScoringApp.Entities.UserCompetition", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CompetitionId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CompetitionId");

                    b.HasIndex("UserId");

                    b.ToTable("UserCompetitions");
                });

            modelBuilder.Entity("DancerScoringApp.Entities.UserRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.Property<int>("RoleId1")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RoleId1");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("DancerScoringApp.Entities.Category", b =>
                {
                    b.HasOne("DancerScoringApp.Entities.AgeCategory", "AgeCategory")
                        .WithMany("Categories")
                        .HasForeignKey("AgeCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AgeCategory");
                });

            modelBuilder.Entity("DancerScoringApp.Entities.Dancer", b =>
                {
                    b.HasOne("DancerScoringApp.Entities.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Team");
                });

            modelBuilder.Entity("DancerScoringApp.Entities.JudgeRating", b =>
                {
                    b.HasOne("DancerScoringApp.Entities.Routine", "Routine")
                        .WithMany("JudgeRating")
                        .HasForeignKey("RoutineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DancerScoringApp.Entities.User", "User")
                        .WithMany("JudgeRating")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Routine");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DancerScoringApp.Entities.PenaltyPointsRating", b =>
                {
                    b.HasOne("DancerScoringApp.Entities.PenaltyPoints", "PenaltyPoints")
                        .WithMany("PenaltyPointsRating")
                        .HasForeignKey("PenaltyPointsId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DancerScoringApp.Entities.TechJudgeRating", "TechJudgeRating")
                        .WithMany("PenaltyPoints")
                        .HasForeignKey("TechJudgeRatingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PenaltyPoints");

                    b.Navigation("TechJudgeRating");
                });

            modelBuilder.Entity("DancerScoringApp.Entities.RatingBonus", b =>
                {
                    b.HasOne("DancerScoringApp.Entities.Bonus", "Bonus")
                        .WithMany("RatingBonus")
                        .HasForeignKey("BonusId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DancerScoringApp.Entities.JudgeRating", "JudgeRating")
                        .WithMany("RatingBonus")
                        .HasForeignKey("JudgeRatingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bonus");

                    b.Navigation("JudgeRating");
                });

            modelBuilder.Entity("DancerScoringApp.Entities.Routine", b =>
                {
                    b.HasOne("DancerScoringApp.Entities.Category", "Category")
                        .WithMany("Routines")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DancerScoringApp.Entities.Competition", null)
                        .WithMany("Routinines")
                        .HasForeignKey("CompetitionId");

                    b.HasOne("DancerScoringApp.Entities.Squad", "Squad")
                        .WithMany()
                        .HasForeignKey("SquadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Squad");
                });

            modelBuilder.Entity("DancerScoringApp.Entities.Squad", b =>
                {
                    b.HasOne("DancerScoringApp.Entities.Team", "Team")
                        .WithMany("Squads")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Team");
                });

            modelBuilder.Entity("DancerScoringApp.Entities.SquadDancer", b =>
                {
                    b.HasOne("DancerScoringApp.Entities.Dancer", "Dancer")
                        .WithMany("SquadDancers")
                        .HasForeignKey("DancerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DancerScoringApp.Entities.Squad", "Squad")
                        .WithMany("Dancers")
                        .HasForeignKey("SquadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dancer");

                    b.Navigation("Squad");
                });

            modelBuilder.Entity("DancerScoringApp.Entities.TeamCoach", b =>
                {
                    b.HasOne("DancerScoringApp.Entities.Coach", "Coach")
                        .WithMany("TeamCoaches")
                        .HasForeignKey("CoachId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DancerScoringApp.Entities.Team", "Team")
                        .WithMany("Coaches")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Coach");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("DancerScoringApp.Entities.TechJudgeRating", b =>
                {
                    b.HasOne("DancerScoringApp.Entities.Routine", "Routine")
                        .WithOne("TechJudgeRating")
                        .HasForeignKey("DancerScoringApp.Entities.TechJudgeRating", "RoutineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DancerScoringApp.Entities.User", "User")
                        .WithMany("TechJudgeRating")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Routine");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DancerScoringApp.Entities.UserCompetition", b =>
                {
                    b.HasOne("DancerScoringApp.Entities.Competition", "Competition")
                        .WithMany("Users")
                        .HasForeignKey("CompetitionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DancerScoringApp.Entities.User", "User")
                        .WithMany("UserCompetitions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Competition");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DancerScoringApp.Entities.UserRole", b =>
                {
                    b.HasOne("DancerScoringApp.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DancerScoringApp.Entities.User", "User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DancerScoringApp.Entities.AgeCategory", b =>
                {
                    b.Navigation("Categories");
                });

            modelBuilder.Entity("DancerScoringApp.Entities.Bonus", b =>
                {
                    b.Navigation("RatingBonus");
                });

            modelBuilder.Entity("DancerScoringApp.Entities.Category", b =>
                {
                    b.Navigation("Routines");
                });

            modelBuilder.Entity("DancerScoringApp.Entities.Coach", b =>
                {
                    b.Navigation("TeamCoaches");
                });

            modelBuilder.Entity("DancerScoringApp.Entities.Competition", b =>
                {
                    b.Navigation("Routinines");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("DancerScoringApp.Entities.Dancer", b =>
                {
                    b.Navigation("SquadDancers");
                });

            modelBuilder.Entity("DancerScoringApp.Entities.JudgeRating", b =>
                {
                    b.Navigation("RatingBonus");
                });

            modelBuilder.Entity("DancerScoringApp.Entities.PenaltyPoints", b =>
                {
                    b.Navigation("PenaltyPointsRating");
                });

            modelBuilder.Entity("DancerScoringApp.Entities.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("DancerScoringApp.Entities.Routine", b =>
                {
                    b.Navigation("JudgeRating");

                    b.Navigation("TechJudgeRating")
                        .IsRequired();
                });

            modelBuilder.Entity("DancerScoringApp.Entities.Squad", b =>
                {
                    b.Navigation("Dancers");
                });

            modelBuilder.Entity("DancerScoringApp.Entities.Team", b =>
                {
                    b.Navigation("Coaches");

                    b.Navigation("Squads");
                });

            modelBuilder.Entity("DancerScoringApp.Entities.TechJudgeRating", b =>
                {
                    b.Navigation("PenaltyPoints");
                });

            modelBuilder.Entity("DancerScoringApp.Entities.User", b =>
                {
                    b.Navigation("JudgeRating");

                    b.Navigation("Roles");

                    b.Navigation("TechJudgeRating");

                    b.Navigation("UserCompetitions");
                });
#pragma warning restore 612, 618
        }
    }
}
