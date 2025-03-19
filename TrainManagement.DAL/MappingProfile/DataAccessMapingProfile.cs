using AutoMapper;
using TrainManagement.Common.Models;

namespace TrainManagement.DAL.MappingProfile
{
    public class DataAccessMapingProfile : Profile
    {
        public DataAccessMapingProfile()
        {
            CreateMap<Entities.User, User>().ReverseMap();
        }
    }
}
