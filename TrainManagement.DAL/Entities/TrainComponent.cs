using System.ComponentModel.DataAnnotations;

namespace TrainManagement.DAL.Entities
{
    public class TrainComponent : Entity<long>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string UniqueNumber { get; set; }
        [Required]
        public bool CanAssignQuantity { get; set; }
        public int? Quantity { get; set; }
    }
}
