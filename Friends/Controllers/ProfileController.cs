using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friends.Models;
using Friends.Models.Repository;
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
        private readonly IRepositoryUnitWork unitWork;
        private readonly UserManager<Person> userManager;

        public ProfileController(ILogger<ProfileController> logger, IRepositoryUnitWork UnitWork,
            UserManager<Person> userManager)
        {
            _logger = logger;
            unitWork = UnitWork;
            this.userManager = userManager;
        }

        // GET: /<controller>/
        public async Task<IActionResult> GetProfile(string id)
        {
            Person person =await userManager.FindByIdAsync(id);
            ProfileViewmodel profileViewmodel = new ProfileViewmodel()
            {
                person = person,
                Posts = unitWork.GetAllPostToPerson(id)
            };
            return View(profileViewmodel);
        }

        public async Task<IActionResult> Like(int id)
        {
            Post post = unitWork.GetPostById(id);
            var user = await userManager.FindByEmailAsync(User.Identity.Name);
            UserPostLike userPostLike = new UserPostLike()
            {
                Post = post,
                PostID = id,
                User = user,
                UserID = user.Id.ToString()
            };
            unitWork.AddLike(userPostLike);
            return RedirectToAction("GetProfile","Profile",new {id= user.Id });
        }

        public async Task<IActionResult> AddComment(string id, PostViewmodel model)
        {
            Post post = unitWork.GetPostById(int.Parse(id));
            var user = await userManager.FindByEmailAsync(User.Identity.Name);

            UserPostComment userPostComment = new UserPostComment()
            {
                Post = post,
                PostID = int.Parse(id),
                User = user,
                userID = user.Id
            };
            Comment comment = new Comment()
            {
                Body = model.userComment.Comment.Body,
                State = Enums.CommentStatus.Active,
                CreationDate = System.DateTime.Now,
                UserPostComments = userPostComment
            };
            userPostComment.Comment = comment;
            unitWork.AddComment(userPostComment);
            return RedirectToAction("GetProfile", "Profile", new { id = user.Id });
        }
    }
}
