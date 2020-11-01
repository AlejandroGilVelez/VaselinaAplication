using AutoMapper;
using Framework.Dtos;
using Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaselinaWeb.API.Profiles
{
    public class SupplieProfile : Profile
    {
        public SupplieProfile()
        {
            CreateMap<Supplie, SupplieDto>();
            CreateMap<SupplieDto, Supplie>();
        }
    }
}
