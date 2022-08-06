using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClimbingGuidebook.Data.Migrations
{
    public partial class Test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boulders_Users_UserOwnerId",
                table: "Boulders");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserOwnerId",
                table: "Boulders",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Boulders_Users_UserOwnerId",
                table: "Boulders",
                column: "UserOwnerId",
                principalTable: "Users",
                principalColumn: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boulders_Users_UserOwnerId",
                table: "Boulders");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserOwnerId",
                table: "Boulders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Boulders_Users_UserOwnerId",
                table: "Boulders",
                column: "UserOwnerId",
                principalTable: "Users",
                principalColumn: "OwnerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
