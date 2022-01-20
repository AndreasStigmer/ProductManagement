using AutoMapper;
using Model;
using ProdMan_WASM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdMan_WASM.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductDTO,CartItem>().ForMember(dest=>dest.ImageUrl,src=>src.MapFrom(s=>s.Images.FirstOrDefault().Url));
           
        }
    }
}
