using AutoMapper;
using Portal.DAL.Entities;
using Portal.Web.ViewModels;

namespace Portal.Web
{
    public class MappingConfig :Profile
    {

        public MappingConfig()
        {
            CreateMap<Post,PostViewModel>().ReverseMap();
            CreateMap<PostContent,PostViewModel>().ReverseMap();
            CreateMap<PostComment, PostViewModel>().ReverseMap();
            CreateMap<User, PostViewModel>().ReverseMap();

        }
    }
}
