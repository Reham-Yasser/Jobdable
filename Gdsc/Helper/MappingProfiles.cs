using AutoMapper;
using DAL.Entities;
using Gdsc.Dto;

namespace Gdsc.Helper
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Jop, JopDto>().ReverseMap();
            CreateMap<JopForm, JobFormDto>().ReverseMap();


        }
    }
}
