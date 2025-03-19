using TrainManagement.Common.Enums;

namespace TrainManagement.DAL.Entities
{
    public class User : Entity<long>
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public UserRole Role { get; set; }
    }
}
