namespace VetLife.Models
{
    public class Treatment_Drug
    {

        public int TreatmentId { get; set; }

        public Treatment Treatment { get; set; }

        public int DrugId { get; set; }

        public Drug Drug { get; set; }

    }
}
