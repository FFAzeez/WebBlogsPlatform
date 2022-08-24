using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using WebBlogInfrastructure.Model;

namespace WebBlogInfrastructure.Context
{
    public class WebContext:IdentityDbContext<User>
    {
        public WebContext(DbContextOptions<WebContext> option) : base(option)
        {
        }
        public DbSet<WebBlog> WebBlogs { get; set; }
    }
}
