using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Friends.Models;
using Friends.Models.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace Friends.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostRepository postRepository;
        private readonly ILikeRepository likeRepository;
        private readonly UserManager<Person> userManager;

        public HomeController(ILogger<HomeController> logger, IPostRepository postRepository,
            ILikeRepository likeRepository , UserManager<Person> userManager)
        {
            _logger = logger;
            this.postRepository = postRepository;
            this.likeRepository = likeRepository;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            PostViewmodel postViewmodel = new PostViewmodel()
            {
                Posts = postRepository.GetAllPost()
            };

            return View(postViewmodel); ;
        }

        public async Task<IActionResult> Post(PostViewmodel model)
        {
            Post post = new Post()
            {
                Body = model.post.Body,
                User = await userManager.FindByEmailAsync(User.Identity.Name),
                CreationDate = System.DateTime.Now,
                State = Enums.PostStatus.Active
            };
            postRepository.Add(post);
            return RedirectToAction("index", "home");
        }

        public async Task<IActionResult> Like(int id)
        {
            Post post = postRepository.GetPostById(id);
            var user = await userManager.FindByEmailAsync(User.Identity.Name);
            UserPostLike userPostLike = new UserPostLike()
            {
                Post = post,
                PostID = id,
                User = user,
                UserID = user.Id.ToString()
            };
            likeRepository.AddLike(userPostLike);
            return RedirectToAction("index", "home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
