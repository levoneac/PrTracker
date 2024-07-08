using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrTracker.Migrations
{
    /// <inheritdoc />
    public partial class MuscleGroups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LiftType",
                table: "LiftTypes");

            migrationBuilder.AddColumn<int>(
                name: "MuscleGroupIdId",
                table: "LiftTypes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MuscleGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MuscleGroup = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MuscleGroups", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LiftTypes_MuscleGroupIdId",
                table: "LiftTypes",
                column: "MuscleGroupIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_LiftTypes_MuscleGroups_MuscleGroupIdId",
                table: "LiftTypes",
                column: "MuscleGroupIdId",
                principalTable: "MuscleGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LiftTypes_MuscleGroups_MuscleGroupIdId",
                table: "LiftTypes");

            migrationBuilder.DropTable(
                name: "MuscleGroups");

            migrationBuilder.DropIndex(
                name: "IX_LiftTypes_MuscleGroupIdId",
                table: "LiftTypes");

            migrationBuilder.DropColumn(
                name: "MuscleGroupIdId",
                table: "LiftTypes");

            migrationBuilder.AddColumn<string>(
                name: "LiftType",
                table: "LiftTypes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
