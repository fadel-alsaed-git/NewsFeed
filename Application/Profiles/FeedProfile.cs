using Application.ViewModels;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Profiles
{
    public class FeedProfile : Profile
    {
        public FeedProfile() { 
        
        
        CreateMap<FeedConvertDTO, Feed>()
               .ForMember(src => src.ImageUrl,dst => dst.MapFrom(s => s.UrlToImage))
                ;
            CreateMap<Feed, FeedDTO>();


        }
    }
}
