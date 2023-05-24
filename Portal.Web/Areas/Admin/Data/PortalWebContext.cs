using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Portal.DAL.Entities;

namespace Portal.Web.Data
{
    public class PortalWebContext : DbContext
    {
        public PortalWebContext (DbContextOptions<PortalWebContext> options)
            : base(options)
        {
        }

        public DbSet<Portal.DAL.Entities.Category> Category { get; set; } = default!;

        public DbSet<Portal.DAL.Entities.Comment> Comment { get; set; } = default!;

        public DbSet<Portal.DAL.Entities.Menu> Menu { get; set; } = default!;

        public DbSet<Portal.DAL.Entities.MenuItem> MenuItem { get; set; } = default!;

        public DbSet<Portal.DAL.Entities.Post> Post { get; set; } = default!;

        public DbSet<Portal.DAL.Entities.Role> Role { get; set; } = default!;

        public DbSet<Portal.DAL.Entities.PostContent> PostContent { get; set; } = default!;

        public DbSet<Portal.DAL.Entities.User> User { get; set; } = default!;
    }
}
