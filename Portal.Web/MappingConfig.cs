using AutoMapper;
using Portal.DAL.Entities;
using Portal.Web.ViewModels;

namespace Portal.Web
{
    public class MappingConfig : Profile
    {

        public MappingConfig()
        {
            CreateMap<Post, PostViewModel>();
            CreateMap<PostViewModel, Post>();

            CreateMap<PostContent, PostViewModel>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.Post.CreatedAt));


            CreateMap<PostViewModel, PostContent>();


            CreateMap<PostComment, PostViewModel>();
            CreateMap<PostViewModel, PostComment>();

            CreateMap<User, PostViewModel>();
            CreateMap<PostViewModel, User>();

        }
    }
}
