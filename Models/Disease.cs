using System.ComponentModel.DataAnnotations;

namespace VetLife.Models
{
    public class Disease
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "The name is required")]
        public string Name { get; set; }

        [Display(Name = "Severity")]
        [Required(ErrorMessage = "The severity is required")]
        public string Severity { get; set; }

        [Display(Name = "Start Time")]
        [Required(ErrorMessage = "The time the disese starts is required")]
        public DateOnly StartTime { get; set; }

        [Display(Name = "End Time")]
        public DateOnly? EndTime { get; set; } 
        public string? Symptoms { get; set; }
    }
}
