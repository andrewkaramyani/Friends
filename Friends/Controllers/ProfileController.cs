using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friends.Models;
using Friends.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Friends.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ILogger<ProfileController> _logger;
        private readonly IPostRepository postRepository;
        private readonly ILikeRepository likeRepository;
        private readonly UserManager<Person> userManager;

        public ProfileController(ILogger<ProfileController> logger, IPostRepository postRepository,
            ILikeRepository likeRepository, UserManager<Person> userManager)
        {
            _logger = logger;
            this.postRepository = postRepository;
            this.likeRepository = likeRepository;
            this.userManager = userManager;
        }

        // GET: /<controller>/
        public async Task<IActionResult> GetProfile(string id)
        {
            Person person =await userManager.FindByIdAsync(id);
            ProfileViewmodel profileViewmodel = new ProfileViewmodel()
            {
                person = person,
                Posts = postRepository.GetAllPostToPerson(id)
            };
            return View(profileViewmodel);
        }
    }
}
