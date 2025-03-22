
namespace VetLife.Models
{
public class PetVaccine
    {
        public int PetId { get; set; }
        public Pet Pet { get; set; }

        public int VaccineId { get; set; }
        public Vaccine Vaccine { get; set; }

        public DateTime AdministeredDate { get; set; }
        public DateTime? NextDoseDate { get; set; }
    }
}
