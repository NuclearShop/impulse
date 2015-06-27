using AutoMapper;
using ImpulseApp.Models.AdModels;
using ImpulseApp.Models.DTO;
using ImpulseApp.Models.StatModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImpulseApp.App_Start
{
    public class MapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<AdState, AdStateDTO>()
                .ForMember(a=>a.VideoUnit, from=>from.MapFrom(d=>d.VideoUnit));
            Mapper.CreateMap<AdStateDTO, AdState>()
                .ForMember(a => a.VideoUnit, from => from.MapFrom(d => d.VideoUnit));

            Mapper.CreateMap<VideoUnit, VideoUnitDTO>();
            Mapper.CreateMap<VideoUnitDTO, VideoUnit>();

            Mapper.CreateMap<UserElement, UserElementDTO>()
                .ForMember(a=>a.HtmlTags, from=>from.MapFrom(d=>d.HtmlTags));
            Mapper.CreateMap<UserElementDTO, UserElement>()
                .ForMember(a => a.HtmlTags, from => from.MapFrom(d => d.HtmlTags));

            Mapper.CreateMap<SimpleAdModel, SimpleAdModelDTO>()
                .ForMember(a => a.AdStates, from => from.MapFrom(d => d.AdStates))
                .ForMember(a => a.StateGraph, from => from.MapFrom(d => d.StateGraph));
            Mapper.CreateMap<SimpleAdModelDTO, SimpleAdModel>()
                .ForMember(a => a.AdStates, from => from.MapFrom(d => d.AdStates))
                .ForMember(a => a.StateGraph, from => from.MapFrom(d => d.StateGraph))
                .ForMember(a => a.AdSessions, from => new HashSet<AdSession>());

            Mapper.CreateMap<HtmlTag, HtmlTagDTO>();
            Mapper.CreateMap<HtmlTagDTO, HtmlTag>();

            Mapper.CreateMap<NodeLink, NodeLinkDTO>();
            Mapper.CreateMap<NodeLinkDTO, NodeLink>();

            Mapper.CreateMap<UserRequest, UserRequestDTO>();
            Mapper.CreateMap<UserRequestDTO, UserRequest>();
        }
    }
}