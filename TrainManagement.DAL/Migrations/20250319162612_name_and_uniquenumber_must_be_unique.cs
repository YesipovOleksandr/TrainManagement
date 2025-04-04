﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainManagement.DAL.Migrations
{
    public partial class name_and_uniquenumber_must_be_unique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UniqueNumber",
                table: "TrainComponents",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TrainComponents",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_TrainComponents_Name",
                table: "TrainComponents",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainComponents_UniqueNumber",
                table: "TrainComponents",
                column: "UniqueNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TrainComponents_Name",
                table: "TrainComponents");

            migrationBuilder.DropIndex(
                name: "IX_TrainComponents_UniqueNumber",
                table: "TrainComponents");

            migrationBuilder.AlterColumn<string>(
                name: "UniqueNumber",
                table: "TrainComponents",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TrainComponents",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
