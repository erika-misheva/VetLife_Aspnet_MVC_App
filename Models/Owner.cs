using System.ComponentModel.DataAnnotations;
using VetLife.Data.Base;

namespace VetLife.Models
{
    public class Owner : IEntityBase
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "The first name is required")]
        [StringLength(50, MinimumLength = 2,
            ErrorMessage = "The first name must be at least 2 charachters long. Maximum length is 50")]
        public string Name { get; set; }


        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "The last name is required")]
        [StringLength(50, MinimumLength = 2,
            ErrorMessage = "The last name must be at least 2 charachters long. Maximum length is 50")]
        public string Surname { get; set; }

        [Key]
        public int Id { get; set; }

        [Display(Name = "Age")]
        [Required(ErrorMessage = "The age is required")]
        [Range(18, 100, ErrorMessage = "The age must be between 18 and 100")]
        public int Age { get; set; }

        [Display(Name = "Profile Picture")]
        [Required(ErrorMessage = "Profile Picture is required")]
        public string? ProfilePictureURL { get; set; }

        public ICollection<Pet> Pets { get; set; } = [];
    }
}
