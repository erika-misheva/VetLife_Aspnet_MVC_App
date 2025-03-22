using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VetLife.Data.ViewModels
{
    public class CreateOperationVM
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "The name is required")]
        public string Name { get; set; }

        [Display(Name = "Date")]
        [Required(ErrorMessage = "The date is required")]
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public string? Surgeon { get; set; }

        [ForeignKey("PetId")]
        public int PetId { get; set; }
    }
}
