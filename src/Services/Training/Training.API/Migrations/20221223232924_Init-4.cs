using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Training.API.Migrations
{
    public partial class Init4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EnrollmentId",
                table: "Trainings",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsHistoric",
                table: "Trainings",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnrollmentId",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "IsHistoric",
                table: "Trainings");
        }
    }
}
