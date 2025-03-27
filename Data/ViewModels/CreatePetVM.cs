using System.ComponentModel.DataAnnotations;
using VetLife.Data.Enums;
using VetLife.Models;

namespace VetLife.Data.ViewModels
{
    public class CreatePetVM
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "The Name is required")]
        public string Name { get; set; }

        [Display(Name = "Owner")]
        [Required(ErrorMessage = "You must select an owner")]
        public int OwnerId { get; set; } 

        [Display(Name = "Type of pet")]
        [Required(ErrorMessage = "You must select a pet type")]
        public PetType PetType { get; set; }

        [Display(Name = "Profile Picture")]
        public string? ProfilePictureURL { get; set; }

        [Display(Name = "Birthday")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Race")]
        public string? Race { get; set; }

        [Display(Name = "Neutered")]
        [Required(ErrorMessage = "Please provide if the animal is neutered")]
        public bool IsNeutered { get; set; }

        public List<int> SelectedVaccines { get; set; }
        public ICollection<Treatment> MedicalHistory { get; set; } = new List<Treatment>();
        public ICollection<Operation> Operations { get; set; } = new List<Operation>();
    }

}
