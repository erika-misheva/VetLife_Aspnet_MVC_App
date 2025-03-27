using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VetLife.Data.Base;
using VetLife.Data.Enums;

namespace VetLife.Models
{
    public class Pet : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("OwnerId")]
        public int OwnerId { get; set; }
        public Owner? Owner { get; set; }
        public PetType PetType { get; set; }
        public string? ProfilePictureURL { get; set; }
        public ICollection<PetVaccine> PetVaccines { get; set; } = [];
        public ICollection<Treatment> MedicalHistory { get; set; } = [];
        public ICollection<Operation> Operations { get; set; } = [];
        public DateTime? BirthDate { get; set; } 
        public string? Race { get; set; } 
        public bool IsNeutered { get; set; }

        //[Range(0, 50, ErrorMessage = "The age must be between 18 and 100")] 
        //public int? Age { get; set; }
    }
}

