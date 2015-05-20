using AutoMapper;
using ImpulseApp.Models.AdModels;
using ImpulseApp.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImpulseApp.MapUtils
{
    public class AdMapUtils
    {
        public static SimpleAdModelDTO GetAdDTO(SimpleAdModel ad)
        {
            SimpleAdModelDTO adDto = Mapper.Map<SimpleAdModel, SimpleAdModelDTO>(ad);
            foreach (var AdState in ad.AdStates)
            {
                AdStateDTO adStateDto = Mapper.Map<AdState, AdStateDTO>(AdState);
                adStateDto.UserElements = new HashSet<UserElementDTO>();
                foreach (var UserElement in AdState.UserElements)
                {
                    UserElementDTO userElementDTO = Mapper.Map<UserElement, UserElementDTO>(UserElement);
                    userElementDTO.HtmlTags = new HashSet<HtmlTagDTO>();
                    foreach (var HtmlTag in UserElement.HtmlTags)
                    {
                        HtmlTagDTO htmlTagDTO = Mapper.Map<HtmlTag, HtmlTagDTO>(HtmlTag);
                        userElementDTO.HtmlTags.Add(htmlTagDTO);
                    }
                    adStateDto.UserElements.Add(userElementDTO);
                }
                adDto.AdStates.Add(adStateDto);

            }
            return adDto;
        }
    }
}