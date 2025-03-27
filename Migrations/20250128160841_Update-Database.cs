using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetLife.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drugs_Treatments_TreatmentId",
                table: "Drugs");

            migrationBuilder.DropForeignKey(
                name: "FK_Operations_Treatments_TreatmentId",
                table: "Operations");

            migrationBuilder.DropIndex(
                name: "IX_Operations_TreatmentId",
                table: "Operations");

            migrationBuilder.DropIndex(
                name: "IX_Drugs_TreatmentId",
                table: "Drugs");

            migrationBuilder.DropColumn(
                name: "TreatmentId",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "TreatmentId",
                table: "Drugs");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Pets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Pets",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Owners",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ProfilePictureURL",
                table: "Owners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Owners",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Treatment_Drug",
                columns: table => new
                {
                    TreatmentId = table.Column<int>(type: "int", nullable: false),
                    DrugId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treatment_Drug", x => new { x.TreatmentId, x.DrugId });
                    table.ForeignKey(
                        name: "FK_Treatment_Drug_Drugs_DrugId",
                        column: x => x.DrugId,
                        principalTable: "Drugs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Treatment_Drug_Treatments_TreatmentId",
                        column: x => x.TreatmentId,
                        principalTable: "Treatments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Treatment_Drug_DrugId",
                table: "Treatment_Drug",
                column: "DrugId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Treatment_Drug");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Pets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Pets",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Owners",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "ProfilePictureURL",
                table: "Owners",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Owners",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<int>(
                name: "TreatmentId",
                table: "Operations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TreatmentId",
                table: "Drugs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Operations_TreatmentId",
                table: "Operations",
                column: "TreatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Drugs_TreatmentId",
                table: "Drugs",
                column: "TreatmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drugs_Treatments_TreatmentId",
                table: "Drugs",
                column: "TreatmentId",
                principalTable: "Treatments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_Treatments_TreatmentId",
                table: "Operations",
                column: "TreatmentId",
                principalTable: "Treatments",
                principalColumn: "Id");
        }
    }
}
