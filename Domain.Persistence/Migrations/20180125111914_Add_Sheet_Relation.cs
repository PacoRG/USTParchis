using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Infraestructure.Persistence.Migrations
{
    public partial class Add_Sheet_Relation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SongId",
                table: "Sheets",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sheets_SongId",
                table: "Sheets",
                column: "SongId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sheets_Songs_SongId",
                table: "Sheets",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sheets_Songs_SongId",
                table: "Sheets");

            migrationBuilder.DropIndex(
                name: "IX_Sheets_SongId",
                table: "Sheets");

            migrationBuilder.DropColumn(
                name: "SongId",
                table: "Sheets");
        }
    }
}
