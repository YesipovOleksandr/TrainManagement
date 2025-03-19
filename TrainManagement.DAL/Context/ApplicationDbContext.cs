using TrainManagement.Common.Enums;
using TrainManagement.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;


namespace TrainManagement.DAL.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<TrainComponent> TrainComponents { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion(
                v => v.ToString(),
                v => (UserRole)Enum.Parse(typeof(UserRole), v)
            );

            builder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Login = "admin",
                Password = "Zf4+0X+GmroJiOFR/PBEjfIoXZurBtcrPHI0i9YxNJv9yfYkNujc+lXVhNLlRWDw",
                Role = UserRole.Admin
            });

            builder.Entity<TrainComponent>()
                   .HasIndex(tc => tc.Name)
                   .IsUnique();

            builder.Entity<TrainComponent>()
                   .HasIndex(tc => tc.UniqueNumber)
                   .IsUnique();


            base.OnModelCreating(builder);
        }
    }
}

