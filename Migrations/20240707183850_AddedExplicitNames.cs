using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddedExplicitNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LiftTypes_MuscleGroups_PrimaryMuscleGroupIdId",
                table: "LiftTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_LiftTypes_MuscleGroups_SecondaryMuscleGroupIdId",
                table: "LiftTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_RecordedLifts_LiftTypes_LiftId",
                table: "RecordedLifts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LiftTypes",
                table: "LiftTypes");

            migrationBuilder.RenameTable(
                name: "LiftTypes",
                newName: "Lifts");

            migrationBuilder.RenameIndex(
                name: "IX_LiftTypes_SecondaryMuscleGroupIdId",
                table: "Lifts",
                newName: "IX_Lifts_SecondaryMuscleGroupIdId");

            migrationBuilder.RenameIndex(
                name: "IX_LiftTypes_PrimaryMuscleGroupIdId",
                table: "Lifts",
                newName: "IX_Lifts_PrimaryMuscleGroupIdId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lifts",
                table: "Lifts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lifts_MuscleGroups_PrimaryMuscleGroupIdId",
                table: "Lifts",
                column: "PrimaryMuscleGroupIdId",
                principalTable: "MuscleGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lifts_MuscleGroups_SecondaryMuscleGroupIdId",
                table: "Lifts",
                column: "SecondaryMuscleGroupIdId",
                principalTable: "MuscleGroups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecordedLifts_Lifts_LiftId",
                table: "RecordedLifts",
                column: "LiftId",
                principalTable: "Lifts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lifts_MuscleGroups_PrimaryMuscleGroupIdId",
                table: "Lifts");

            migrationBuilder.DropForeignKey(
                name: "FK_Lifts_MuscleGroups_SecondaryMuscleGroupIdId",
                table: "Lifts");

            migrationBuilder.DropForeignKey(
                name: "FK_RecordedLifts_Lifts_LiftId",
                table: "RecordedLifts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lifts",
                table: "Lifts");

            migrationBuilder.RenameTable(
                name: "Lifts",
                newName: "LiftTypes");

            migrationBuilder.RenameIndex(
                name: "IX_Lifts_SecondaryMuscleGroupIdId",
                table: "LiftTypes",
                newName: "IX_LiftTypes_SecondaryMuscleGroupIdId");

            migrationBuilder.RenameIndex(
                name: "IX_Lifts_PrimaryMuscleGroupIdId",
                table: "LiftTypes",
                newName: "IX_LiftTypes_PrimaryMuscleGroupIdId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LiftTypes",
                table: "LiftTypes",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_RecordedLifts_LiftTypes_LiftId",
                table: "RecordedLifts",
                column: "LiftId",
                principalTable: "LiftTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
