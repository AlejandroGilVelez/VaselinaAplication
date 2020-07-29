using AutoMapper;
using Framework.Dtos;
using Framework.Models;

namespace VaselinaWeb.API.Profiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<Client, ClientDto>();
            CreateMap<ClientDto, Client>();
        }
    }
}
