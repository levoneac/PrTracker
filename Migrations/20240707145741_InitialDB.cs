﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrTracker.Migrations
{
    /// <inheritdoc />
    public partial class InitialDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LiftTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LiftName = table.Column<string>(type: "TEXT", nullable: false),
                    LiftType = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiftTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    DateOfRegistration = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecordedLifts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LiftId = table.Column<int>(type: "INTEGER", nullable: false),
                    Reps = table.Column<int>(type: "INTEGER", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(6, 2)", nullable: false),
                    DayOfLift = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LifterIdId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordedLifts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecordedLifts_LiftTypes_LiftId",
                        column: x => x.LiftId,
                        principalTable: "LiftTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecordedLifts_People_LifterIdId",
                        column: x => x.LifterIdId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecordedLifts_LifterIdId",
                table: "RecordedLifts",
                column: "LifterIdId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordedLifts_LiftId",
                table: "RecordedLifts",
                column: "LiftId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecordedLifts");

            migrationBuilder.DropTable(
                name: "LiftTypes");

            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
