using System.ComponentModel.DataAnnotations;
using VetLife.Data.Base;

namespace VetLife.Models
{
    public class Drug : IEntityBase
    {
        public int Id { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage = "The name is required")]
        public string Name { get; set; }

        [Display(Name = "Dosage")]
        [Required(ErrorMessage = "The Dosage is required")]
        public string Dosage { get; set; }

        [Display(Name = "Manufacturer")]
        [Required(ErrorMessage = "The Manufacturer is required")]
        public string Manufacturer { get; set; }


        [Display(Name = "Date of Production")]
        [Required(ErrorMessage = "The Date of Production is required")]
        public DateTime ManufacturedDate { get; set; }

        [Display(Name = "Date of Expiration")]
        [Required(ErrorMessage = "The Date of Expiration is required")]
        public DateTime ExpiryDate { get; set; }
        public ICollection<Treatment_Drug> Treatment_Drug { get; set; } = [];
    }
}
