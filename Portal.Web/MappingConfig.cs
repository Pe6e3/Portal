using AutoMapper;
using Portal.DAL.Entities;
using Portal.Web.Areas.Admin.Controllers.ViewModels;
using Portal.Web.ViewModels;

namespace Portal.Web
{
    public class MappingConfig : Profile
    {

        public MappingConfig()
        {


            CreateMap<Post, PostViewModel>()
                .ForMember(dest => dest.PostId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ContentId, opt => opt.MapFrom(src => src.Content.Id))
                .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => src.Slug))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Content.Title))
                .ForMember(dest => dest.PostBody, opt => opt.MapFrom(src => src.Content.PostBody))
                .ForMember(dest => dest.PostImage, opt => opt.MapFrom(src => src.Content.PostImage))
                .ForMember(dest => dest.CommentsNum, opt => opt.MapFrom(src => src.Content.CommentsNum))
                .ForMember(dest => dest.CommentsClosed, opt => opt.MapFrom(src => src.Content.CommentsClosed))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
                .ForMember(dest => dest.PostVideo, opt => opt.MapFrom(src => src.Content.PostVideo))
                .ForMember(dest => dest.CategoriesId, opt => opt.MapFrom(src => src.Categories.Select(c => c.Id).ToArray()))
                .ForMember(dest => dest.ImageFile, opt => opt.Ignore())
                .ForMember(dest => dest.Category, opt => opt.Ignore());

            CreateMap<PostViewModel, Post>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PostId))
                .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => src.Slug))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.Id))
                .ForMember(dest => dest.Categories, opt => opt.Ignore())
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments));



            CreateMap<PostContent, PostViewModel>()
              .ForMember(dest => dest.ContentId, opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
              .ForMember(dest => dest.PostBody, opt => opt.MapFrom(src => src.PostBody))
              .ForMember(dest => dest.PostImage, opt => opt.MapFrom(src => src.PostImage))
              .ForMember(dest => dest.CommentsNum, opt => opt.MapFrom(src => src.CommentsNum))
              .ForMember(dest => dest.CommentsClosed, opt => opt.MapFrom(src => src.CommentsClosed))
              .ForMember(dest => dest.PostVideo, opt => opt.MapFrom(src => src.PostVideo))
              .ForMember(dest => dest.ImageFile, opt => opt.Ignore())
              .ForMember(dest => dest.Comments, opt => opt.Ignore())
              .ForMember(dest => dest.CategoriesId, opt => opt.Ignore())
              .ForMember(dest => dest.Category, opt => opt.Ignore());

            CreateMap<PostViewModel, PostContent>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ContentId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.PostBody, opt => opt.MapFrom(src => src.PostBody))
                .ForMember(dest => dest.PostImage, opt => opt.MapFrom(src => src.PostImage))
                .ForMember(dest => dest.CommentsNum, opt => opt.MapFrom(src => src.CommentsNum))
                .ForMember(dest => dest.CommentsClosed, opt => opt.MapFrom(src => src.CommentsClosed))
                .ForMember(dest => dest.PostVideo, opt => opt.MapFrom(src => src.PostVideo));



            CreateMap<PostViewModel, PostContent>();

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
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.User.Role));

            CreateMap<CategoryViewModel, Category>().ReverseMap();

        }
    }
}
