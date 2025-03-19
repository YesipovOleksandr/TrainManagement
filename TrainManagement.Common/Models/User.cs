using TrainManagement.Common.Enums;

namespace TrainManagement.Common.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public UserRole Role { get; set; }
    }
}
