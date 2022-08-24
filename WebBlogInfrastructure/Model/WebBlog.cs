using System;
using System.ComponentModel.DataAnnotations;

namespace WebBlogInfrastructure.Model
{
    public class WebBlog
    {
        [Key]
        [Required]
        public string Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime PublicationDate { get; set; }
    }
}
