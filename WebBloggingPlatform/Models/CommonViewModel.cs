using System.Collections.Generic;

namespace WebBloggingPlatform.Models
{
    public class CommonViewModel
    {
        public WebBlogVM WebBlog { get; set; } = new();
        public List<WebBlogVM> WebBlogs { get; set; } = new();
        public bool ImportBlogs { get; set; }
    }
}
