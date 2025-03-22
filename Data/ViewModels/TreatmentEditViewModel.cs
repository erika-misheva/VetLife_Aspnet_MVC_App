using VetLife.Models;

namespace VetLife.Data.ViewModels
{
    public class TreatmentEditViewModel
    {
        public int Id { get; set; }
        public int PetId { get; set; }
        public Disease Disease { get; set; }
        public List<int> SelectedDrugIds { get; set; } = new List<int>();
        public List<Drug> AvailableDrugs { get; set; } = new List<Drug>();

        public Treatment Treatment { get; set; } = new Treatment();
    }
}
