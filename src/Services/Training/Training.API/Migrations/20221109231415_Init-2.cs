using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Training.API.Migrations
{
    public partial class Init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ExternalExerciseId",
                table: "TrainingExercises",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalExerciseId",
                table: "TrainingExercises");
        }
    }
}
