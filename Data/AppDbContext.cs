using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VetLife.Models;

namespace VetLife.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Drug> Drugs { get; set; }
        public DbSet<Disease> Diseases { get; set; }
        public DbSet<Treatment> Treatments { get; set; }
        public DbSet<Vaccine> Vaccines { get; set; }
        public DbSet<PetVaccine> PetVaccine { get; set; }
        public DbSet<Treatment_Drug> Treatment_Drug { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Owner>()
                .HasMany(o => o.Pets)
                .WithOne(p => p.Owner)
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Pet>()
                .HasMany(p => p.MedicalHistory)
                .WithOne(t => t.Pet)
                .HasForeignKey(t => t.PetId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Pet>()
             .HasMany(p => p.Operations)
             .WithOne(o => o.Pet)
             .HasForeignKey(o => o.PetId)
             .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PetVaccine>().HasKey(am => new
            {
                am.PetId,
                am.VaccineId
            });

            modelBuilder.Entity<PetVaccine>().HasOne(p => p.Pet).WithMany(pv => pv.PetVaccines).HasForeignKey(p => p.PetId);

            modelBuilder.Entity<PetVaccine>().HasOne(v => v.Vaccine).WithMany(pv => pv.PetVaccines).HasForeignKey(v => v.VaccineId);


            modelBuilder.Entity<Treatment_Drug>().HasKey(am => new
            {
                am.TreatmentId,
                am.DrugId
            });

            modelBuilder.Entity<Treatment_Drug>().HasOne(m => m.Treatment).WithMany(td => td.Treatment_Drug).HasForeignKey(m => m.TreatmentId);

            modelBuilder.Entity<Treatment_Drug>().HasOne(a => a.Drug).WithMany(am => am.Treatment_Drug).HasForeignKey(a => a.DrugId);

            base.OnModelCreating(modelBuilder);
        }
    }


}
