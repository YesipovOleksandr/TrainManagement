using System.ComponentModel.DataAnnotations;

namespace TrainManagement.API.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [MaxLength(100)]
        public string Login { get; set; }

        [Required]
        [StringLength(18, MinimumLength = 4)]
        public string Password { get; set; }
    }
}
