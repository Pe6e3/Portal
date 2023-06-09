using Microsoft.EntityFrameworkCore;
using Portal.DAL.Entities;

namespace Portal.DAL.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Menu> Menus { get; set; }
    public DbSet<MenuItem> MenuItems { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<PostCategory> PostCategories { get; set; }
    public DbSet<PostComment> PostComments { get; set; }
    public DbSet<PostContent> PostContents { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Post>()
            .HasMany(e => e.Categories)
            .WithMany(e => e.Posts);

        modelBuilder.Entity<Post>()
            .HasMany(e => e.Comments)
            .WithMany(e => e.Posts);

        modelBuilder.Entity<Menu>()
            .HasData(new Menu
            {
                Id = 1,
                Name = "Меню в шапке"
            });
        modelBuilder.Entity<Menu>()
            .HasData(new Menu
            {
                Id = 2,
                Name = "Меню в подвале"
            });

        modelBuilder.Entity<Category>()
           .HasData(new Category
           {
               Id = 1,
               Name = "Экономика",
               Description = "Описание Категории Экономика",
               Slug = "economics",
           });

        modelBuilder.Entity<Category>()
           .HasData(new Category
           {
               Id = 2,
               Name = "Технологии",
               Description = "Описание Категории Технологии",
               Slug = "technology",
           });

        modelBuilder.Entity<Category>()
           .HasData(new Category
           {
               Id = 3,
               Name = "Спорт",
               Description = "Описание Категории Спорт",
               Slug = "sport",
           });
            }
}

