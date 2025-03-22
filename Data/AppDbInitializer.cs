using VetLife.Models;
using VetLife.Data.Enums;
using Microsoft.AspNetCore.Identity;
using VetLife.Data.Static;

namespace VetLife.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                if (!context.Owners.Any())
                {
                    var owner = new Owner
                    {
                        Name = "John",
                        Surname = "Doe",
                        Age = 35,
                        ProfilePictureURL = "data:image/jpeg;base64,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"
                    };

                    context.Owners.Add(owner);
                    context.SaveChanges();

                    var pet = new Pet
                    {
                        Name = "Buddy",
                        BirthDate = new DateTime(2020, 5, 1),
                        Race = "Golden Retriever",
                        IsNeutered = true,
                        PetType = PetType.Dog,
                        OwnerId = owner.Id, 
                        Owner = owner,
                        ProfilePictureURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTOUVLfarv2Yfa3Smgda8ISxQHI4jaK5DIDug&s"
                    };

                    context.Pets.Add(pet);
                    context.SaveChanges();

                    var vaccine = new Vaccine
                    {
                        Name = "Rabies Vaccine",
                        Manufacturer = "VaccineCo",
                        DateProduced = new DateTime(2023, 1, 1),
                        ExpiryDate = new DateTime(2025, 1, 1)
                    };

                    context.Vaccines.Add(vaccine);
                    context.SaveChanges();

                   // pet.Vaccines.Add(vaccine);
                    context.SaveChanges();

                    var disease = new Disease
                    {
                        Name = "Parvovirus",
                        Severity = "Severe",
                        StartTime = new DateOnly(2022, 12, 1),
                        EndTime = new DateOnly(2023, 1, 15),
                        Symptoms = "Vomiting, Diarrhea"
                    };

                    context.Diseases.Add(disease);
                    context.SaveChanges();

                    var drug = new Drug
                    {
                        Name = "Antibiotic A",
                        Dosage = "5mg/kg",
                        Manufacturer = "PharmaPlus",
                        ManufacturedDate = new DateTime(2022, 6, 1),
                        ExpiryDate = new DateTime(2024, 6, 1)
                    };

                    context.Drugs.Add(drug);
                    context.SaveChanges();

                    var operation = new Operation
                    {
                        Name = "Neutering Surgery",
                        Date = new DateTime(2021, 7, 15),
                        Description = "Routine neutering procedure",
                        Surgeon = "Dr. Smith",
                        PetId= pet.Id,
                        Pet = pet 
                    };

                    context.Operations.Add(operation);
                    context.SaveChanges();

                    var treatment = new Treatment
                    {
                        Disease = disease,
                        PetId= pet.Id,
                        Pet = pet, 
                        Notes = "Recovery was smooth",
                        Recommendations = "Regular check-ups required"
                    };


                    //treatment.Treatment_Drug.Add(drug);

                    context.Treatments.Add(treatment);
                    context.SaveChanges();

                    pet.MedicalHistory.Add(treatment);
                    context.SaveChanges();
                }
            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {

                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string adminUserEmail = "admin@vetlife.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FullName = "Admin User",
                        UserName = "admin-user",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }


                string appUserEmail = "user@vetlife.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FullName = "Application User",
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }

}
