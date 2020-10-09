using AutoMapper;
using CoreCommandEntities.Models;
using CoreCommandEntities.DTO;

namespace CoreCommandAPI.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Command,CommandReadDTO>().ReverseMap();
            CreateMap<Command,CommandCreateDTO>().ReverseMap();
            CreateMap<Command,CommandUpdateDTO>().ReverseMap();
            CreateMap<CommandImage,CommandImageReadDTO>().ReverseMap();
            CreateMap<CommandImage,CommandImageCreateDTO>().ReverseMap();

        }
    }
}