using AutoMapper;
using System.Collections.Generic;
using WebBloggingPlatform.Models;
using WebBlogInfrastructure.Model;

namespace WebBloggingPlatform.Mapping
{
    public class WebMapping:Profile
    {
        public WebMapping()
        {
            CreateMap<WebBlog, WebBlogVM>().ReverseMap()
                .ForMember(x=>x.PublicationDate, y=>y.MapFrom(x=>x.Publication_date));
            CreateMap<User, RegisterVM>().ReverseMap();
        }

        public static List<WebBlogVM>  GetWebBlogs(List<WebBlog> blogs)
        {
            List<WebBlogVM> webBlog = new();
            foreach (var model in blogs)
            {
                webBlog.Add(new WebBlogVM
                {
                    Title = model.Title,
                    Publication_date = model.PublicationDate,
                    Description = model.Description
                });
            }
            return webBlog;
        }
    }
    
}
