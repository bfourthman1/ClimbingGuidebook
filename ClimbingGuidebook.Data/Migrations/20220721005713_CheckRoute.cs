using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClimbingGuidebook.Data.Migrations
{
    public partial class CheckRoute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ascents_Users_OwnerId",
                table: "Ascents");

            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Users_OwnerId",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_Routes_OwnerId",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_Ascents_OwnerId",
                table: "Ascents");

            migrationBuilder.AddColumn<Guid>(
                name: "UserOwnerId",
                table: "Routes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserOwnerId",
                table: "Ascents",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Routes_UserOwnerId",
                table: "Routes",
                column: "UserOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Ascents_UserOwnerId",
                table: "Ascents",
                column: "UserOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ascents_Users_UserOwnerId",
                table: "Ascents",
                column: "UserOwnerId",
                principalTable: "Users",
                principalColumn: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Users_UserOwnerId",
                table: "Routes",
                column: "UserOwnerId",
                principalTable: "Users",
                principalColumn: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ascents_Users_UserOwnerId",
                table: "Ascents");

            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Users_UserOwnerId",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_Routes_UserOwnerId",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_Ascents_UserOwnerId",
                table: "Ascents");

            migrationBuilder.DropColumn(
                name: "UserOwnerId",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "UserOwnerId",
                table: "Ascents");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_OwnerId",
                table: "Routes",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Ascents_OwnerId",
                table: "Ascents",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ascents_Users_OwnerId",
                table: "Ascents",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "OwnerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Users_OwnerId",
                table: "Routes",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "OwnerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
