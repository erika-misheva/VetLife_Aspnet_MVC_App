using VetLife.Models;

namespace VetLife.Data.ViewModels
{
    public class ManagePetVaccinesViewModel
    {
        public int PetId { get; set; }
        public List<Vaccine> AvailableVaccines { get; set; }
        public List<Vaccine> SelectedVaccines { get; set; }
        public int SelectedVaccineId { get; set; }  
        public DateTime AdministeredDate { get; set; } = DateTime.Now;
        public DateTime? NextDoseDate { get; set; }
    }

}
