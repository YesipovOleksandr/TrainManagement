using AutoMapper;
using TrainManagement.Common.Abstract.Repository;
using TrainManagement.Common.Models;
using TrainManagement.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace TrainManagement.DAL.Repository
{
    public class UserRepository : IUserRepository
    {
        protected readonly ApplicationDbContext _context;
        protected readonly IMapper _mapper;

        public UserRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<User> Create(User model)
        {
            var newUser = _mapper.Map<Entities.User>(model);
            await _context.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return _mapper.Map<User>(newUser);
        }

        public async Task<User> GetByLogin(string login)
        {
            return _mapper.Map<User>(await _context.Users.FirstOrDefaultAsync(x => x.Login == login));
        }

        public async Task Update(User item)
        {
            await _context.Database.ExecuteSqlRawAsync(@"UPDATE Users SET  
            Login={1}, Password={2},RefreshToken={3},RefreshTokenExpiryTime={4}
            WHERE Id={0}",
            item.Id, item.Login, item.Password, item.RefreshToken, item.RefreshTokenExpiryTime);
            await _context.SaveChangesAsync();
        }
    } 
}
