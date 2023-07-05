using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Portal.DAL.Data;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;

namespace Portal.BLL.Repositories;

public class PostCommentRepository : GenericRepositoryAsync<PostComment>, IPostCommentRepository
{
    private readonly AppDbContext db;

    public PostCommentRepository(AppDbContext db) : base(db)
    {
        this.db = db;
    }


 

}
