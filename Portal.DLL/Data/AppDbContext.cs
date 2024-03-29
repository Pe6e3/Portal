﻿using Microsoft.EntityFrameworkCore;
using Portal.DAL.Entities;
using Portal.DAL.Enum;
using System.Xml.Linq;

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

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.User)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Post)
            .WithMany(p => p.Comments)
            .HasForeignKey(c => c.PostId)
            .OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<Menu>().HasData(new Menu
        {
            Id = 1,
            Name = "Меню в шапке"
        }, new Menu
        {
            Id = 2,
            Name = "Меню в подвале"
        });

        modelBuilder.Entity<MenuItem>().HasData(new MenuItem
        {
            Name = "Экономика",
            Id = 1,
            MenuId = 1,
            Slug = "category/economics",
            Position = 1,
        }, new MenuItem
        {
            Name = "Технологии",
            Id = 2,
            MenuId = 1,
            Slug = "category/technology",
            Position = 2,
        }, new MenuItem
        {
            Name = "Спорт",
            Id = 3,
            MenuId = 1,
            Slug = "category/sport",
            Position = 3,
        }, new MenuItem
        {
            Name = "Админка",
            Id = 4,
            MenuId = 1,
            Slug = "admin",
            Position = 5,
        }, new MenuItem
        {
            Name = "Лайфхаки",
            Id = 5,
            MenuId = 1,
            Slug = "category/lifehacks",
            Position = 4,
        });

        modelBuilder.Entity<Category>().HasData(new Category
        {
            Id = 1,
            Name = "Экономика",
            Description = "Новости из мира Экономики",
            Slug = "economics",
            CategoryImage = "5.jpg",
        }, new Category
        {
            Id = 2,
            Name = "Технологии",
            Description = "Новейшие технологии, открытия",
            Slug = "technology",
            CategoryImage = "2.jpg",
        }, new Category
        {
            Id = 3,
            Name = "Спорт",
            Description = "Все, что связано со спортом",
            Slug = "sport",
            CategoryImage = "7.jpg",
        }, new Category
        {
            Id = 4,
            Name = "Лайфхаки",
            Description = "Различные интересные способы",
            Slug = "lifehacks",
            CategoryImage = "6.jpg",
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
        });

        modelBuilder.Entity<User>().HasData(new User
        {
            Id = 1,
            Login = "DefaultUser",
            Password = "$2a$11$uw8Pqz0Iap7IY530hPeZ8u.ebtvnxfFeXAECB65DI1JS3wLaTipda",
            RoleId = 3
        }, new User
        {
            Id = 2,
            Login = "Admin",
            Password = "$2a$11$uw8Pqz0Iap7IY530hPeZ8u.ebtvnxfFeXAECB65DI1JS3wLaTipda",
            RoleId = 1
        });

        modelBuilder.Entity<UserProfile>().HasData(new UserProfile
        {
            Id = 1,
            UserId = 1,
            Firstname = "Стандартный",
            Lastname = "Пользователь",
            RegistrationDate = DateTime.Now,
            AvatarImg = "default-avatar.png"
        }, new UserProfile
        {
            Id = 2,
            UserId = 2,
            Firstname = "Админ",
            RegistrationDate = DateTime.Now,
            AvatarImg = "admin-default.png"
        });

        modelBuilder.Entity<Post>().HasData(new Post
        {
            Id = 1,
            Slug = "start",
            CreatedAt = DateTime.Now,
            CreatedById = 2,
        });


        modelBuilder.Entity<PostContent>().HasData(new PostContent
        {
            Id = 1,
            PostId = 1,
            Title = "Стартовый пост (прочитайте)",
            PostBody = "Авторизуйтесь. \r\n<br />  Логин <b> Admin</b>, Пароль <b>111</b>.\r\n<br /> Перейдите по ссылке ниже, чтобы сгенерировать 20 случайных постов (занимает около 20 секунд после нажатия, ждите)\r\n<br />  <b><a href=\"https://localhost:7164/Admin/Posts/GenerateRandomPosts?count=20\"> Генератор постов</a> </b>",
            PostImage = "10.jpg"
        });
        modelBuilder.Entity<PostCategory>().HasData(new PostCategory
        {
            Id = 1,
            PostId = 1,
            CategoryId = 2
        });
    }
}

