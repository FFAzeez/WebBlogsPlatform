using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebBlogInfrastructure.Model;
using WebBlogInfrastructure.Repository;
using WebBloggingPlatform.Models;
using WebBloggingPlatform.Mapping;
using System.Security.Claims;
using WebBloggingPlatform.Utility;

namespace WebBloggingPlatform.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly IWebBlogRepository _blogRepository;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signManager;
        private readonly ISendHttpRequest _sendHttp;
        private readonly IMapper _mapper;
        CommonViewModel common = new();
        private string userId { get; set; }


        public HomeController(IWebBlogRepository blogRepository,
            IMapper mapper, UserManager<User> userManager, SignInManager<User> signManager, ISendHttpRequest sendHttp)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
            _userManager = userManager;
            _signManager = signManager;
            _sendHttp = sendHttp;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            userId = Common.GetUserId(this.User);
            if(userId == null)
            {
                var blogs = await _blogRepository.AllBlogPosted();
                common.WebBlogs = WebMapping.GetWebBlogs(blogs);
                return View(common);
            }
            else
            {
                var user = await _blogRepository.GetUser(userId);
                common.WebBlogs = WebMapping.GetWebBlogs(user.WebBlogs);
                return View(common);
            }
            
        }


        [HttpPost]
        public async Task<IActionResult> Dash(CommonViewModel blog)
        {
            if (!blog.ImportBlogs)
            {
                blog.WebBlog.Id = Guid.NewGuid().ToString();
                blog.WebBlog.Publication_date = DateTime.Now;
                var result = _mapper.Map<WebBlog>(blog.WebBlog);
                result.UserId = Common.GetUserId(this.User);
                await _blogRepository.Add(result);
                return RedirectToAction("Index");
            }

            var answer = await _sendHttp.SendAsync<ImportBlogVM>();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<User>(register);
                user.UserName = register.Email;
                var result = await _userManager.CreateAsync(user,register.Password);
                if (result.Succeeded)
                {
                    await _signManager.SignInAsync(user, false);
                    return RedirectToAction("Login");
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> Login(LoginVM login)
        {
            if (ModelState.IsValid)
            {
                var user = await _signManager.PasswordSignInAsync(login.Email, login.Password, login.RememberMe, false);
                if (user.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Username or Password incorrect");
            }
            
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signManager.SignOutAsync();
            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
    }
}
