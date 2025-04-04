﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VetLife.Data;

#nullable disable

namespace VetLife.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250128160841_Update-Database")]
    partial class UpdateDatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PetVaccine", b =>
                {
                    b.Property<int>("PetsId")
                        .HasColumnType("int");

                    b.Property<int>("VaccinesId")
                        .HasColumnType("int");

                    b.HasKey("PetsId", "VaccinesId");

                    b.HasIndex("VaccinesId");

                    b.ToTable("PetVaccine");
                });

            modelBuilder.Entity("VetLife.Models.Disease", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Severity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Symptoms")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Diseases");
                });

            modelBuilder.Entity("VetLife.Models.Drug", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Dosage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ManufacturedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Drugs");
                });

            modelBuilder.Entity("VetLife.Models.Operation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PetId")
                        .HasColumnType("int");

                    b.Property<string>("Surgeon")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PetId");

                    b.ToTable("Operations");
                });

            modelBuilder.Entity("VetLife.Models.Owner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ProfilePictureURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Owners");
                });

            modelBuilder.Entity("VetLife.Models.Pet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsNeutered")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("OwnerId")
                        .HasColumnType("int");

                    b.Property<int>("PetType")
                        .HasColumnType("int");

                    b.Property<string>("ProfilePictureURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Race")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Pets");
                });

            modelBuilder.Entity("VetLife.Models.Treatment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DiseaseId")
                        .HasColumnType("int");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PetId")
                        .HasColumnType("int");

                    b.Property<string>("Recommendations")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DiseaseId");

                    b.HasIndex("PetId");

                    b.ToTable("Treatments");
                });

            modelBuilder.Entity("VetLife.Models.Treatment_Drug", b =>
                {
                    b.Property<int>("TreatmentId")
                        .HasColumnType("int");

                    b.Property<int>("DrugId")
                        .HasColumnType("int");

                    b.HasKey("TreatmentId", "DrugId");

                    b.HasIndex("DrugId");

                    b.ToTable("Treatment_Drug");
                });

            modelBuilder.Entity("VetLife.Models.Vaccine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateProduced")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Vaccines");
                });

            modelBuilder.Entity("PetVaccine", b =>
                {
                    b.HasOne("VetLife.Models.Pet", null)
                        .WithMany()
                        .HasForeignKey("PetsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VetLife.Models.Vaccine", null)
                        .WithMany()
                        .HasForeignKey("VaccinesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VetLife.Models.Operation", b =>
                {
                    b.HasOne("VetLife.Models.Pet", "Pet")
                        .WithMany("Operations")
                        .HasForeignKey("PetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pet");
                });

            modelBuilder.Entity("VetLife.Models.Pet", b =>
                {
                    b.HasOne("VetLife.Models.Owner", "Owner")
                        .WithMany("Pets")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("VetLife.Models.Treatment", b =>
                {
                    b.HasOne("VetLife.Models.Disease", "Disease")
                        .WithMany()
                        .HasForeignKey("DiseaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VetLife.Models.Pet", "Pet")
                        .WithMany("MedicalHistory")
                        .HasForeignKey("PetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Disease");

                    b.Navigation("Pet");
                });

            modelBuilder.Entity("VetLife.Models.Treatment_Drug", b =>
                {
                    b.HasOne("VetLife.Models.Drug", "Drug")
                        .WithMany("Treatment_Drug")
                        .HasForeignKey("DrugId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VetLife.Models.Treatment", "Treatment")
                        .WithMany("Treatment_Drug")
                        .HasForeignKey("TreatmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Drug");

                    b.Navigation("Treatment");
                });

            modelBuilder.Entity("VetLife.Models.Drug", b =>
                {
                    b.Navigation("Treatment_Drug");
                });

            modelBuilder.Entity("VetLife.Models.Owner", b =>
                {
                    b.Navigation("Pets");
                });

            modelBuilder.Entity("VetLife.Models.Pet", b =>
                {
                    b.Navigation("MedicalHistory");

                    b.Navigation("Operations");
                });

            modelBuilder.Entity("VetLife.Models.Treatment", b =>
                {
                    b.Navigation("Treatment_Drug");
                });
#pragma warning restore 612, 618
        }
    }
}
