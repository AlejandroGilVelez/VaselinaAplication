using AutoMapper;
using Framework.Dtos;
using Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaselinaWeb.API.Utilidades;

namespace VaselinaWeb.API.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.Imagen, opt => opt.MapFrom(src => ConvertImagen.ImagenToString(src.Imagen)));

        }
    
    }
}
