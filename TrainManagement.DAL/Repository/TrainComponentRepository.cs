using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TrainManagement.Common.Abstract.Repository;
using TrainManagement.Common.Models;
using TrainManagement.DAL.Context;

namespace TrainManagement.DAL.Repository
{
    public class TrainComponentRepository : ITrainComponentRepository
    {
        protected readonly ApplicationDbContext _context;
        protected readonly IMapper _mapper;

        public TrainComponentRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TrainComponentList> GetAll(int page, int number, string? search = null)
        {
            var query = _context.TrainComponents.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(c => c.Name.Contains(search) || c.UniqueNumber.Contains(search));
            }

            int totalCount = await query.CountAsync();

            var items = await query
                .Skip((page - 1) * number)
                .Take(number)
                .ToListAsync();

            return new TrainComponentList(_mapper.Map<List<TrainComponent>>(items), totalCount);
        }

        public async Task<TrainComponent> GetById(long id)
        {
            return _mapper.Map<TrainComponent>(await _context.TrainComponents.FirstOrDefaultAsync(x => x.Id == id));
        }

        public async Task<TrainComponent> Create(TrainComponent model)
        {
            var newTrainComponent = _mapper.Map<Entities.TrainComponent>(model);
            await _context.AddAsync(newTrainComponent);
            await _context.SaveChangesAsync();
            return _mapper.Map<TrainComponent>(newTrainComponent);
        }

        public async Task<TrainComponent> Update(TrainComponent model)
        {
            var trainComponent = _mapper.Map<Entities.TrainComponent>(model);
            _context.Entry(trainComponent).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task DeleteById(long id)
        {
            var model = await _context.TrainComponents.FindAsync(id);
            if (model != null)
            {
                _context.TrainComponents.Remove(model);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateQuantity(long id, int quantity)
        {
            var model = await _context.TrainComponents.FindAsync(id);
            if (model != null && model.CanAssignQuantity)
            {
                model.Quantity = quantity;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Quantity assignment is not allowed.");
            }
        }

        public async Task<bool> IsUniqueNumberExists(string uniqueNumber)
        {
            return await _context.TrainComponents
                                 .AnyAsync(tc => tc.UniqueNumber == uniqueNumber);
        }

    }
}
