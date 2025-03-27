using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VetLife.Models;

namespace VetLife.Data.ViewModels
{
    public class TreatmentCreateViewModel
    {
        [Required]
        public int PetId { get; set; }

        public Treatment Treatment { get; set; } = new Treatment();

        public Disease Disease { get; set; } = new Disease();

        public List<Drug> AvailableDrugs { get; set; } = new List<Drug>();

        public List<int> SelectedDrugIds { get; set; } = new List<int>();
    }
}
