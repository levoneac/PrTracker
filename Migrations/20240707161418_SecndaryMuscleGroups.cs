using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrTracker.Migrations
{
    /// <inheritdoc />
    public partial class SecndaryMuscleGroups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LiftTypes_MuscleGroups_MuscleGroupIdId",
                table: "LiftTypes");

            migrationBuilder.RenameColumn(
                name: "MuscleGroup",
                table: "MuscleGroups",
                newName: "PrimaryMuscleGroup");

            migrationBuilder.RenameColumn(
                name: "MuscleGroupIdId",
                table: "LiftTypes",
                newName: "PrimaryMuscleGroupIdId");

            migrationBuilder.RenameIndex(
                name: "IX_LiftTypes_MuscleGroupIdId",
                table: "LiftTypes",
                newName: "IX_LiftTypes_PrimaryMuscleGroupIdId");

            migrationBuilder.AddColumn<int>(
                name: "SecondaryMuscleGroupIdId",
                table: "LiftTypes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LiftTypes_SecondaryMuscleGroupIdId",
                table: "LiftTypes",
                column: "SecondaryMuscleGroupIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_LiftTypes_MuscleGroups_PrimaryMuscleGroupIdId",
                table: "LiftTypes",
                column: "PrimaryMuscleGroupIdId",
                principalTable: "MuscleGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LiftTypes_MuscleGroups_SecondaryMuscleGroupIdId",
                table: "LiftTypes",
                column: "SecondaryMuscleGroupIdId",
                principalTable: "MuscleGroups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LiftTypes_MuscleGroups_PrimaryMuscleGroupIdId",
                table: "LiftTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_LiftTypes_MuscleGroups_SecondaryMuscleGroupIdId",
                table: "LiftTypes");

            migrationBuilder.DropIndex(
                name: "IX_LiftTypes_SecondaryMuscleGroupIdId",
                table: "LiftTypes");

            migrationBuilder.DropColumn(
                name: "SecondaryMuscleGroupIdId",
                table: "LiftTypes");

            migrationBuilder.RenameColumn(
                name: "PrimaryMuscleGroup",
                table: "MuscleGroups",
                newName: "MuscleGroup");

            migrationBuilder.RenameColumn(
                name: "PrimaryMuscleGroupIdId",
                table: "LiftTypes",
                newName: "MuscleGroupIdId");

            migrationBuilder.RenameIndex(
                name: "IX_LiftTypes_PrimaryMuscleGroupIdId",
                table: "LiftTypes",
                newName: "IX_LiftTypes_MuscleGroupIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_LiftTypes_MuscleGroups_MuscleGroupIdId",
                table: "LiftTypes",
                column: "MuscleGroupIdId",
                principalTable: "MuscleGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
