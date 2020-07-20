using AutoMapper;
using Framework.Dtos;
using Framework.Models;

namespace VaselinaWeb.API.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
