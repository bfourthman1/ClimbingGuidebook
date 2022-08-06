using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClimbingGuidebook.Data.Migrations
{
    public partial class TestLinkage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boulders_Users_OwnerId",
                table: "Boulders");

            migrationBuilder.DropIndex(
                name: "IX_Boulders_OwnerId",
                table: "Boulders");

            migrationBuilder.AddColumn<Guid>(
                name: "UserOwnerId",
                table: "Boulders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Boulders_UserOwnerId",
                table: "Boulders",
                column: "UserOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Boulders_Users_UserOwnerId",
                table: "Boulders",
                column: "UserOwnerId",
                principalTable: "Users",
                principalColumn: "OwnerId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boulders_Users_UserOwnerId",
                table: "Boulders");

            migrationBuilder.DropIndex(
                name: "IX_Boulders_UserOwnerId",
                table: "Boulders");

            migrationBuilder.DropColumn(
                name: "UserOwnerId",
                table: "Boulders");

            migrationBuilder.CreateIndex(
                name: "IX_Boulders_OwnerId",
                table: "Boulders",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Boulders_Users_OwnerId",
                table: "Boulders",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "OwnerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
