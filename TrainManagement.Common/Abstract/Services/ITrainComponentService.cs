using TrainManagement.Common.Models;

namespace TrainManagement.Common.Abstract.Services
{
    public interface ITrainComponentService
    {
        Task<IEnumerable<TrainComponent>> GetAll();
        Task<TrainComponent> GetById(long id);
        Task<TrainComponent> Create(TrainComponent model);
        Task<TrainComponent> Update(TrainComponent model);
        Task DeleteById(long id);
        Task UpdateQuantity(long id, int quantity);
    }
}
