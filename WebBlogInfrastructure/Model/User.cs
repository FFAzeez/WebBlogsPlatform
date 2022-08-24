using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebBlogInfrastructure.Model
{
    public class User:IdentityUser
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public virtual List<WebBlog> WebBlogs { get; set; }
    }
}
