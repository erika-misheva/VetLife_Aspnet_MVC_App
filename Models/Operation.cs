using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VetLife.Data.Base;

namespace VetLife.Models
{
    public class Operation : IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public string? Surgeon { get; set; }

        public int PetId { get; set; }
        public Pet Pet { get; set; }

    }
}