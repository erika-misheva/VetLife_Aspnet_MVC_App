using System.ComponentModel.DataAnnotations;
using VetLife.Data.Base;

namespace VetLife.Models
{
    public class Vaccine : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage = "The name is required")]
        public string Name { get; set; }

        [Display(Name = "Manufacturer")]
        [Required(ErrorMessage = "The Manufacturer is required")]
        public string Manufacturer { get; set; }

        [Display(Name = "Date of Production")]
        [Required(ErrorMessage = "The Date of Production is required")]
        public DateTime DateProduced { get; set; }

        [Display(Name = "Date of Expiration")]
        [Required(ErrorMessage = "The Date of Expiration is required")]
        public DateTime ExpiryDate { get; set; }

        [Display(Name = "Stock")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be less than zero.")]
        public int Stock { get; set; }  
        public ICollection<PetVaccine> PetVaccines { get; set; } = new List<PetVaccine>();
    }
}
