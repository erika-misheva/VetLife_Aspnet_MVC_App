using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VetLife.Models
{
    public class Treatment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int DiseaseId { get; set; }
        public Disease? Disease { get; set; }

        [Required]
        public int PetId { get; set; }
        public Pet? Pet { get; set; }

        public string? Notes { get; set; }
        public string? Recommendations { get; set; }

        public ICollection<Treatment_Drug> Treatment_Drug { get; set; } = new List<Treatment_Drug>();
    }
}
