using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetLife.Migrations
{
    /// <inheritdoc />
    public partial class Pet_Vaccine_Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PetVaccine_Pets_PetsId",
                table: "PetVaccine");

            migrationBuilder.DropForeignKey(
                name: "FK_PetVaccine_Vaccines_VaccinesId",
                table: "PetVaccine");

            migrationBuilder.RenameColumn(
                name: "VaccinesId",
                table: "PetVaccine",
                newName: "VaccineId");

            migrationBuilder.RenameColumn(
                name: "PetsId",
                table: "PetVaccine",
                newName: "PetId");

            migrationBuilder.RenameIndex(
                name: "IX_PetVaccine_VaccinesId",
                table: "PetVaccine",
                newName: "IX_PetVaccine_VaccineId");

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "Vaccines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "AdministeredDate",
                table: "PetVaccine",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NextDoseDate",
                table: "PetVaccine",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Pets",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddForeignKey(
                name: "FK_PetVaccine_Pets_PetId",
                table: "PetVaccine",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PetVaccine_Vaccines_VaccineId",
                table: "PetVaccine",
                column: "VaccineId",
                principalTable: "Vaccines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PetVaccine_Pets_PetId",
                table: "PetVaccine");

            migrationBuilder.DropForeignKey(
                name: "FK_PetVaccine_Vaccines_VaccineId",
                table: "PetVaccine");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "Vaccines");

            migrationBuilder.DropColumn(
                name: "AdministeredDate",
                table: "PetVaccine");

            migrationBuilder.DropColumn(
                name: "NextDoseDate",
                table: "PetVaccine");

            migrationBuilder.RenameColumn(
                name: "VaccineId",
                table: "PetVaccine",
                newName: "VaccinesId");

            migrationBuilder.RenameColumn(
                name: "PetId",
                table: "PetVaccine",
                newName: "PetsId");

            migrationBuilder.RenameIndex(
                name: "IX_PetVaccine_VaccineId",
                table: "PetVaccine",
                newName: "IX_PetVaccine_VaccinesId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Pets",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_PetVaccine_Pets_PetsId",
                table: "PetVaccine",
                column: "PetsId",
                principalTable: "Pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PetVaccine_Vaccines_VaccinesId",
                table: "PetVaccine",
                column: "VaccinesId",
                principalTable: "Vaccines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
