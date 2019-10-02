using AutoMapper;
using CoolPhotosAPI.BL.ViewModels;
using CoolPhotosAPI.Data.Entities;
using System;

namespace CoolPhotosAPI.BL
{
    public class CoolEntitiesMappingProfile: Profile
    {
        public CoolEntitiesMappingProfile()
        {
            CreateMap<Photo, PhotoViewModel>()
                .ForMember(dest => dest.Guid, source => source.MapFrom(src => src.Guid.ToString()))
                .ForMember(dest => dest.DateCreated, source => source.MapFrom(src => src.DateCreated.ToLongDateString()))
                .ForMember(dest => dest.Path, source => source.MapFrom(p => p.Path.Substring(p.Path.LastIndexOf('\\'))));
        }
    }
}
