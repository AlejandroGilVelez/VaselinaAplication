using AutoMapper;
using Framework.Dtos;
using Framework.Models;

namespace VaselinaWeb.API.Profiles
{
    public class ProviderProfile : Profile
    {
        public ProviderProfile()
        {
            CreateMap<Provider, ProviderDto>();
            CreateMap<ProviderDto, Provider>();
        }
    }
}
