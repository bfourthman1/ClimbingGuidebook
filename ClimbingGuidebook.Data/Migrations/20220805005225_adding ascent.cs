using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClimbingGuidebook.Data.Migrations
{
    public partial class addingascent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "FirstAscent",
                table: "Ascents",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<int>(
                name: "Grade",
                table: "Ascents",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstAscent",
                table: "Ascents");

            migrationBuilder.DropColumn(
                name: "Grade",
                table: "Ascents");
        }
    }
}
