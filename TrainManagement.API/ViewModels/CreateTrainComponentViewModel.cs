using System.ComponentModel.DataAnnotations;

namespace TrainManagement.API.ViewModels
{
    public class CreateTrainComponentViewModel
    {
        [Required]
        [MaxLength(100)]

        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(10)]
        public string UniqueNumber { get; set; } = string.Empty;

        [Required]
        public bool CanAssignQuantity { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive integer.")]
        public int? Quantity { get; set; }
    }
}
