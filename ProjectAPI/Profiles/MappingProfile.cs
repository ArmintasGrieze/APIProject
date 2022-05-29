using APIdemo.Models;
using AutoMapper;
using ProjectAPI.Models;
using ProjectAPI.Services.CarCommands;
using ProjectAPI.Services.UserCommands;

namespace APIdemo.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Car, CarDTO>().ReverseMap();
            CreateMap<AddCarCommand, Car>().ReverseMap();
            CreateMap<UpdateCarCommand, Car>().ReverseMap();
            CreateMap<AddUserCommand, User>().ReverseMap();
            CreateMap<LoginUserCommand, User>().ReverseMap();
            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<User, AuthResponse>().ReverseMap();
            CreateMap<UserDTO, AuthResponse>().ReverseMap();
        }
    }
}
