using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bonuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bonuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AgeCategory = table.Column<int>(type: "integer", nullable: false),
                    Requisite = table.Column<int>(type: "integer", nullable: false),
                    CategoryType = table.Column<int>(type: "integer", nullable: false),
                    SquadType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Coaches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coaches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Competitions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PenaltyPoints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PenaltyScore = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PenaltyPoints", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dancers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    LicenceId = table.Column<string>(type: "text", nullable: false),
                    TeamId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dancers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dancers_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Squads",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SquadType = table.Column<int>(type: "integer", nullable: false),
                    TeamId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Squads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Squads_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamCoaches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CoachId = table.Column<Guid>(type: "uuid", nullable: false),
                    TeamId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamCoaches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamCoaches_Coaches_CoachId",
                        column: x => x.CoachId,
                        principalTable: "Coaches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamCoaches_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCompetitions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompetitionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCompetitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCompetitions_Competitions_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "Competitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCompetitions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Routines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoutineName = table.Column<string>(type: "text", nullable: true),
                    Score = table.Column<decimal>(type: "numeric", nullable: false),
                    GrandPrix = table.Column<bool>(type: "boolean", nullable: true),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    SquadId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompetitionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Routines_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Routines_Competitions_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "Competitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Routines_Squads_SquadId",
                        column: x => x.SquadId,
                        principalTable: "Squads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SquadDancers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SquadId = table.Column<Guid>(type: "uuid", nullable: false),
                    DancerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SquadDancers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SquadDancers_Dancers_DancerId",
                        column: x => x.DancerId,
                        principalTable: "Dancers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SquadDancers_Squads_SquadId",
                        column: x => x.SquadId,
                        principalTable: "Squads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JudgeRatings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ChoreographyPoints = table.Column<decimal>(type: "numeric", nullable: false),
                    BodyTechniquePoints = table.Column<decimal>(type: "numeric", nullable: false),
                    RequisiteWorkPoints = table.Column<decimal>(type: "numeric", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: false),
                    HasBonus = table.Column<bool>(type: "boolean", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoutineId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JudgeRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JudgeRatings_Routines_RoutineId",
                        column: x => x.RoutineId,
                        principalTable: "Routines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JudgeRatings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TechJudgeRatings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoutineId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    PenaltyPointId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechJudgeRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechJudgeRatings_PenaltyPoints_PenaltyPointId",
                        column: x => x.PenaltyPointId,
                        principalTable: "PenaltyPoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TechJudgeRatings_Routines_RoutineId",
                        column: x => x.RoutineId,
                        principalTable: "Routines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TechJudgeRatings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RatingBonuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    JudgeRatingId = table.Column<Guid>(type: "uuid", nullable: false),
                    BonusId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingBonuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RatingBonuses_Bonuses_BonusId",
                        column: x => x.BonusId,
                        principalTable: "Bonuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RatingBonuses_JudgeRatings_JudgeRatingId",
                        column: x => x.JudgeRatingId,
                        principalTable: "JudgeRatings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Bonuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Orginality" },
                    { 2, "Sychronization" },
                    { 3, "Perfect Synchronization" },
                    { 4, "Presence and Elegance" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "AgeCategory", "CategoryType", "Requisite", "SquadType" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, 1 },
                    { 2, 2, 1, 1, 1 },
                    { 3, 3, 1, 1, 1 },
                    { 4, 4, 1, 1, 1 },
                    { 5, 2, 1, 1, 2 },
                    { 6, 3, 1, 1, 2 },
                    { 7, 4, 1, 1, 2 },
                    { 8, 2, 1, 1, 3 },
                    { 9, 3, 1, 1, 3 },
                    { 10, 4, 1, 1, 3 },
                    { 11, 2, 1, 1, 4 },
                    { 12, 3, 1, 1, 4 },
                    { 13, 4, 1, 1, 4 },
                    { 14, 2, 5, 1, 4 },
                    { 15, 3, 5, 1, 4 },
                    { 16, 4, 5, 1, 4 },
                    { 17, 2, 5, 1, 3 },
                    { 18, 3, 5, 1, 3 },
                    { 19, 4, 5, 1, 3 },
                    { 20, 2, 4, 1, 3 },
                    { 21, 3, 4, 1, 3 },
                    { 22, 4, 4, 1, 3 },
                    { 23, 2, 4, 1, 4 },
                    { 24, 3, 4, 1, 4 },
                    { 25, 4, 4, 1, 4 },
                    { 26, 2, 2, 2, 1 },
                    { 27, 3, 2, 2, 1 },
                    { 28, 4, 2, 2, 1 },
                    { 29, 2, 3, 2, 1 },
                    { 30, 3, 3, 2, 1 },
                    { 31, 4, 3, 2, 1 },
                    { 32, 1, 1, 2, 1 },
                    { 33, 2, 1, 2, 1 },
                    { 34, 1, 1, 2, 2 },
                    { 35, 2, 1, 2, 2 },
                    { 36, 3, 1, 2, 2 },
                    { 37, 4, 1, 2, 2 },
                    { 38, 1, 1, 2, 3 },
                    { 39, 2, 1, 2, 3 },
                    { 40, 3, 1, 2, 3 },
                    { 41, 4, 1, 2, 3 },
                    { 42, 1, 1, 2, 4 },
                    { 43, 2, 1, 2, 4 },
                    { 44, 3, 1, 2, 4 },
                    { 45, 4, 1, 2, 4 }
                });

            migrationBuilder.InsertData(
                table: "PenaltyPoints",
                columns: new[] { "Id", "Name", "PenaltyScore" },
                values: new object[,]
                {
                    { 1, "Missing greeting", -0.05m },
                    { 2, "Prop or costume piece drop", -0.05m },
                    { 3, "Leaving prop on stage after drop", -0.05m },
                    { 4, "For every second time overrun", -0.05m },
                    { 5, "Team or solist is not ready after announcement", -0.1m },
                    { 6, "Crossing a line", -0.1m },
                    { 7, "Incorrectyly mixed music", -0.1m },
                    { 8, "The dancer supports themselves", -0.1m },
                    { 9, "Too early entrance", -0.2m },
                    { 10, "Missing stop figure at the end", -0.2m },
                    { 11, "Missing single required costume element", -0.3m },
                    { 12, "Communication between dancers or dancers and coach during presentation", -0.3m },
                    { 13, "Missing prop contact", -0.3m },
                    { 14, "Missing lift assurance", -0.3m },
                    { 15, "Missing single required element", -0.4m },
                    { 16, "Dancer fall", -0.4m },
                    { 17, "Putting down prop for more than 16 matching steps", -0.4m },
                    { 18, "Non regulation music", -0.4m },
                    { 19, "Different choreography from previous qualifications", -0.4m },
                    { 20, "Non regulation prop or costume element", -0.4m },
                    { 21, "Three tiered pyramids", -3m },
                    { 22, "Category prohibited elements", -3m },
                    { 23, "Unjustified lifts and throws", -3m },
                    { 24, "Placing non dedicated props on stage", -3m },
                    { 25, "Disqualification", -100m }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Super Admin" },
                    { 2, "Judge" },
                    { 3, "Technical Judge" },
                    { 4, "Scrutineer" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dancers_TeamId",
                table: "Dancers",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_JudgeRatings_RoutineId",
                table: "JudgeRatings",
                column: "RoutineId");

            migrationBuilder.CreateIndex(
                name: "IX_JudgeRatings_UserId",
                table: "JudgeRatings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingBonuses_BonusId",
                table: "RatingBonuses",
                column: "BonusId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingBonuses_JudgeRatingId",
                table: "RatingBonuses",
                column: "JudgeRatingId");

            migrationBuilder.CreateIndex(
                name: "IX_Routines_CategoryId",
                table: "Routines",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Routines_CompetitionId",
                table: "Routines",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Routines_SquadId",
                table: "Routines",
                column: "SquadId");

            migrationBuilder.CreateIndex(
                name: "IX_SquadDancers_DancerId",
                table: "SquadDancers",
                column: "DancerId");

            migrationBuilder.CreateIndex(
                name: "IX_SquadDancers_SquadId",
                table: "SquadDancers",
                column: "SquadId");

            migrationBuilder.CreateIndex(
                name: "IX_Squads_TeamId",
                table: "Squads",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamCoaches_CoachId",
                table: "TeamCoaches",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamCoaches_TeamId",
                table: "TeamCoaches",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TechJudgeRatings_PenaltyPointId",
                table: "TechJudgeRatings",
                column: "PenaltyPointId");

            migrationBuilder.CreateIndex(
                name: "IX_TechJudgeRatings_RoutineId",
                table: "TechJudgeRatings",
                column: "RoutineId");

            migrationBuilder.CreateIndex(
                name: "IX_TechJudgeRatings_UserId",
                table: "TechJudgeRatings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCompetitions_CompetitionId",
                table: "UserCompetitions",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCompetitions_UserId",
                table: "UserCompetitions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RatingBonuses");

            migrationBuilder.DropTable(
                name: "SquadDancers");

            migrationBuilder.DropTable(
                name: "TeamCoaches");

            migrationBuilder.DropTable(
                name: "TechJudgeRatings");

            migrationBuilder.DropTable(
                name: "UserCompetitions");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Bonuses");

            migrationBuilder.DropTable(
                name: "JudgeRatings");

            migrationBuilder.DropTable(
                name: "Dancers");

            migrationBuilder.DropTable(
                name: "Coaches");

            migrationBuilder.DropTable(
                name: "PenaltyPoints");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Routines");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Competitions");

            migrationBuilder.DropTable(
                name: "Squads");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
