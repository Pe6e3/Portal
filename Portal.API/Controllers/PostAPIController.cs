using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Portal.API.Entities.DTO;
using Portal.BLL;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;
namespace Portal.API.Controllers;

[ApiController]
[Route("api/posts")]
public class PostAPIController : ControllerBase
{
    private readonly UnitOfWork uow;
    private readonly IMapper mapper;

    public PostAPIController(UnitOfWork uow, IMapper mapper)
    {
        this.uow = uow;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<Post>> GetPosts()
    {
        IEnumerable<Post> posts = await uow.PostRep.ListAll("Content");
        IEnumerable<PostDTO> postsDTO = new List<PostDTO>();
        mapper.Map(posts, postsDTO);
        return posts;
    }


}