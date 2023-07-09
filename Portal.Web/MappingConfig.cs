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

            CreateMap<ProfileViewModel, User>();
            CreateMap<User, ProfileViewModel>();

            CreateMap<ProfileViewModel, UserProfile>();
            CreateMap<UserProfile, ProfileViewModel>();


      
            CreateMap<ChatUser, ChatProfileViewModel>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id))
                .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.User.Profile.Firstname))
                .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.User.Profile.Lastname))
                .ForMember(dest => dest.RegistrationDate, opt => opt.MapFrom(src => src.User.Profile.RegistrationDate))
                .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.User.Profile.Birthday))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Profile.Email))
                .ForMember(dest => dest.AvatarImg, opt => opt.MapFrom(src => src.User.Profile.AvatarImg))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.User.Role))


;
        }
    }
}
