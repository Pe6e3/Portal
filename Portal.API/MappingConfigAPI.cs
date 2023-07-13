using AutoMapper;
using Portal.API.Entities.DTO;
using Portal.DAL.Entities;

namespace Portal.API
{
    public class MappingConfigAPI : Profile
    {

        public MappingConfigAPI()
        {


            CreateMap<Post, PostDTO>()
               .ForMember(dest => dest.PostId, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Content.Title))
               .ForMember(dest => dest.ImgUrl, opt => opt.MapFrom(src => src.Content.PostImage));


            CreateMap<PostDTO, Post>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PostId));

            CreateMap<PostDTO, PostContent>()
               .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
               .ForMember(dest => dest.PostImage, opt => opt.MapFrom(src => src.ImgUrl))
               .ForMember(dest => dest.CommentsNum, opt => opt.MapFrom(src => src.CommentsNum));
        }


    }
}
