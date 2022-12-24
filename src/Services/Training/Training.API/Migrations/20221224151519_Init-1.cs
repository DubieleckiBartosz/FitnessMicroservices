using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Training.API.Migrations
{
    public partial class Init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trainings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    EnrollmentId = table.Column<Guid>(type: "uuid", nullable: true),
                    TrainerUniqueCode = table.Column<Guid>(type: "uuid", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsHistoric = table.Column<bool>(type: "boolean", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Availability = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: true),
                    DurationTrainingInMinutes = table.Column<int>(type: "integer", nullable: true),
                    BreakBetweenExercisesInMinutes = table.Column<int>(type: "integer", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainingExercises",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    ExternalExerciseId = table.Column<Guid>(type: "uuid", nullable: false),
                    NumberRepetitions = table.Column<int>(type: "integer", nullable: false),
                    BreakBetweenSetsInMinutes = table.Column<int>(type: "integer", nullable: false),
                    TrainingDetailsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingExercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingExercises_Trainings_TrainingDetailsId",
                        column: x => x.TrainingDetailsId,
                        principalTable: "Trainings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainingDetailsTrainingUser",
                columns: table => new
                {
                    TrainingUsersId = table.Column<Guid>(type: "uuid", nullable: false),
                    TrainingsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingDetailsTrainingUser", x => new { x.TrainingUsersId, x.TrainingsId });
                    table.ForeignKey(
                        name: "FK_TrainingDetailsTrainingUser_Trainings_TrainingsId",
                        column: x => x.TrainingsId,
                        principalTable: "Trainings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainingDetailsTrainingUser_Users_TrainingUsersId",
                        column: x => x.TrainingUsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrainingDetailsTrainingUser_TrainingsId",
                table: "TrainingDetailsTrainingUser",
                column: "TrainingsId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingExercises_TrainingDetailsId",
                table: "TrainingExercises",
                column: "TrainingDetailsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainingDetailsTrainingUser");

            migrationBuilder.DropTable(
                name: "TrainingExercises");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Trainings");
        }
    }
}
