using TrainManagement.Common.Abstract.Repository;
using TrainManagement.Common.Abstract.Services;
using TrainManagement.Common.Models;

namespace TrainManagement.BLL.Services
{
    public class TrainComponentService : ITrainComponentService
    {
        private readonly ITrainComponentRepository _trainComponentRepository;

        public TrainComponentService(ITrainComponentRepository trainComponentRepository)
        {
            _trainComponentRepository = trainComponentRepository;
        }

        public async Task<TrainComponent> Create(TrainComponent model)
        {
            return await _trainComponentRepository.Create(model);
        }

        public async Task DeleteById(long id)
        {
            await _trainComponentRepository.DeleteById(id);
        }

        public async Task<TrainComponentList> GetAll(int page, int number, string? search = null)
        {
            return await _trainComponentRepository.GetAll(page,number,search);
        }

        public async Task<TrainComponent> GetById(long id)
        {
            return await _trainComponentRepository.GetById(id);
        }

        public async Task<TrainComponent> Update(TrainComponent model)
        {
            return await _trainComponentRepository.Update(model);
        }

        public async Task UpdateQuantity(long id, int quantity)
        {
             await _trainComponentRepository.UpdateQuantity(id, quantity);
        }

        public async Task<bool> IsUniqueNumberExists(string uniqueNumber)
        {
            return await _trainComponentRepository.IsUniqueNumberExists(uniqueNumber);
        }
    }
}
