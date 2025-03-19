using AutoMapper;
using TrainManagement.API.ViewModels;

namespace TrainManagement.API.MappingProfile
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<LoginViewModel, Common.Models.User>().ReverseMap();
            CreateMap<AuthViewModel, Common.Models.User>().ReverseMap();
            CreateMap<CreateTrainComponentViewModel, Common.Models.TrainComponent>().ReverseMap();
        }
    }
}
