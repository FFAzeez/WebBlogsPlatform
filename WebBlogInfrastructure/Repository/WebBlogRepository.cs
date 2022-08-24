using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBlogInfrastructure.Context;
using WebBlogInfrastructure.Model;

namespace WebBlogInfrastructure.Repository
{
    public class WebBlogRepository : IWebBlogRepository
    {
        private readonly WebContext _dbContext;
        public WebBlogRepository(WebContext webContext)
        {
            _dbContext = webContext;
        }

        public async Task<bool> Add(WebBlog blog)
        {
            _dbContext.Add(blog);
            return await SaveAsync();
        }

        public async Task<bool> AddRange(List<WebBlog> blog)
        {
            _dbContext.AddRange(blog);
            return await SaveAsync();
        }

        public async Task<User> GetUser(string Id)
        {
            return await _dbContext.Users.Include(x=>x.WebBlogs).FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<User> FindByEmailPassword(string email, string password)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x=>x.Email==email && x.Password==password);
        }

        public async Task<List<WebBlog>> AllBlogPosted()
        {
            return await _dbContext.WebBlogs.ToListAsync();
        }
        private async Task<bool> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
