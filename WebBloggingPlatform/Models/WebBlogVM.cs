using System;

namespace WebBloggingPlatform.Models
{
    public class WebBlogVM
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Publication_date { get; set; }
    }
}
