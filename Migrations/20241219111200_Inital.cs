using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetLife.Migrations
{
    /// <inheritdoc />
    public partial class Inital : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Diseases",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Severity = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        Symptoms = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Diseases", x => x.Id);
            //    });

            //    migrationBuilder.CreateTable(
            //        name: "Owners",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(type: "int", nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //            Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //            Age = table.Column<int>(type: "int", nullable: false),
            //            ProfilePictureURL = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Owners", x => x.Id);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "Vaccines",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(type: "int", nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //            Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //            DateProduced = table.Column<DateTime>(type: "datetime2", nullable: false),
            //            ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Vaccines", x => x.Id);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "Pets",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(type: "int", nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //            OwnerId = table.Column<int>(type: "int", nullable: false),
            //            PetType = table.Column<int>(type: "int", nullable: false),
            //            ProfilePictureURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //            BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
            //            Race = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //            IsNeutered = table.Column<bool>(type: "bit", nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Pets", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_Pets_Owners_OwnerId",
            //                column: x => x.OwnerId,
            //                principalTable: "Owners",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "PetVaccine",
            //        columns: table => new
            //        {
            //            PetsId = table.Column<int>(type: "int", nullable: false),
            //            VaccinesId = table.Column<int>(type: "int", nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_PetVaccine", x => new { x.PetsId, x.VaccinesId });
            //            table.ForeignKey(
            //                name: "FK_PetVaccine_Pets_PetsId",
            //                column: x => x.PetsId,
            //                principalTable: "Pets",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //            table.ForeignKey(
            //                name: "FK_PetVaccine_Vaccines_VaccinesId",
            //                column: x => x.VaccinesId,
            //                principalTable: "Vaccines",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "Treatments",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(type: "int", nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            DiseaseId = table.Column<int>(type: "int", nullable: false),
            //            PetId = table.Column<int>(type: "int", nullable: false),
            //            Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //            Recommendations = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Treatments", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_Treatments_Diseases_DiseaseId",
            //                column: x => x.DiseaseId,
            //                principalTable: "Diseases",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //            table.ForeignKey(
            //                name: "FK_Treatments_Pets_PetId",
            //                column: x => x.PetId,
            //                principalTable: "Pets",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "Drugs",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(type: "int", nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //            Dosage = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //            Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //            ManufacturedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //            ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //            TreatmentId = table.Column<int>(type: "int", nullable: true)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Drugs", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_Drugs_Treatments_TreatmentId",
            //                column: x => x.TreatmentId,
            //                principalTable: "Treatments",
            //                principalColumn: "Id");
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "Operations",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(type: "int", nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //            Date = table.Column<DateTime>(type: "datetime2", nullable: false),
            //            Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //            Surgeon = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //            PetId = table.Column<int>(type: "int", nullable: false),
            //            TreatmentId = table.Column<int>(type: "int", nullable: true)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Operations", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_Operations_Pets_PetId",
            //                column: x => x.PetId,
            //                principalTable: "Pets",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //            table.ForeignKey(
            //                name: "FK_Operations_Treatments_TreatmentId",
            //                column: x => x.TreatmentId,
            //                principalTable: "Treatments",
            //                principalColumn: "Id");
            //        });

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Drugs_TreatmentId",
            //        table: "Drugs",
            //        column: "TreatmentId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Operations_PetId",
            //        table: "Operations",
            //        column: "PetId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Operations_TreatmentId",
            //        table: "Operations",
            //        column: "TreatmentId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Pets_OwnerId",
            //        table: "Pets",
            //        column: "OwnerId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_PetVaccine_VaccinesId",
            //        table: "PetVaccine",
            //        column: "VaccinesId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Treatments_DiseaseId",
            //        table: "Treatments",
            //        column: "DiseaseId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Treatments_PetId",
            //        table: "Treatments",
            //        column: "PetId");
            //}
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Drugs");

            migrationBuilder.DropTable(
                name: "Operations");

            migrationBuilder.DropTable(
                name: "PetVaccine");

            migrationBuilder.DropTable(
                name: "Treatments");

            migrationBuilder.DropTable(
                name: "Vaccines");

            migrationBuilder.DropTable(
                name: "Diseases");

            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "Owners");
        }
    }
}
