using Microsoft.EntityFrameworkCore;
using Portal.DAL.Entities;
using Portal.DAL.Enum;

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
    public DbSet<PostContent> PostContents { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<MyLogger> MyLoggers { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<ChatUser> ChatUsers { get; set; }
    public DbSet<Message> Messages { get; set; }
        

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>()
            .HasMany(e => e.Chats)
            .WithMany(e => e.Users);

        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Post>()
            .HasMany(e => e.Categories)
            .WithMany(e => e.Posts);


        //modelBuilder.Entity<Post>()
        //            .HasMany(p => p.Comments)              // Один пост имеет много комментариев
        //            .WithOne(c => c.Post)                  // Каждый комментарий принадлежит одному посту
        //            .HasForeignKey(c => c.PostId);         // Внешний ключ в таблице комментариев - PostId

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

        modelBuilder.Entity<Role>().HasData(new Role
        {
            Id = 1,
            RoleName = RoleName.Admin,
        }, new Role
        {
            Id = 2,
            RoleName = RoleName.Moderator,
        }, new Role
        {
            Id = 3,
            RoleName = RoleName.User,
        }, new Role
        {
            Id = 4,
            RoleName = RoleName.PremiumUser,
        }
        );
    }
}

