using VetLife.Models;

namespace VetLife.Data.ViewModels
{
    public class TreatmentDetailsViewModel
    {
        public int TreatmentId { get; set; }
        public int PetId { get; set; }
        public string PetName { get; set; }
        public Disease Disease { get; set; }
        public Treatment Treatment { get; set; }
        public List<Drug> PrescribedDrugs { get; set; }
    }
}
