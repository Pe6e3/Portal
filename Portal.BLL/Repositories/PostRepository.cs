﻿using Microsoft.EntityFrameworkCore;
using Portal.DAL.Data;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;

namespace Portal.BLL.Repositories;

public class PostRepository : GenericRepositoryAsync<Post>, IPostRepository
{
    private readonly AppDbContext db;

    public PostRepository(AppDbContext db) : base(db)
    {
        this.db = db;
    }

    public async Task<Post> GetPostByIdAsync(int id) => await db.Posts.Include(p => p.Content).Include(p => p.CreatedBy).FirstOrDefaultAsync(x => x.Id == id);

    public async Task<Post?> GetPostBySlugAsync(string postSlug) => await db.Posts.Include(x=>x.Categories).Include(x=>x.Content).FirstOrDefaultAsync(post => post.Slug == postSlug);



    public async Task<IReadOnlyList<Post>?> ListAllPostsWithContentsAsync() => await db.Posts.Include(p => p.Content).ToListAsync();
}
