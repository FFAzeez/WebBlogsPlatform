using System.Collections.Generic;
using System.Threading.Tasks;
using WebBlogInfrastructure.Model;

namespace WebBlogInfrastructure.Repository
{
    public interface IWebBlogRepository
    {
        Task<bool> Add(WebBlog blog);
        Task<List<WebBlog>> AllBlogPosted();
        Task<User> GetUser(string Id);
        Task<User> FindByEmailPassword(string email, string password);
    }
}