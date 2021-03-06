﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Friends.Models;
using Friends.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Friends.Models.Repository;

namespace Friends.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepositoryUnitWork unitWork;
        private readonly UserManager<Person> userManager;

        public HomeController(ILogger<HomeController> logger, IRepositoryUnitWork unitWork
             , UserManager<Person> userManager)
        {
            _logger = logger;
            this.unitWork = unitWork;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            PostViewmodel postViewmodel = new PostViewmodel()
            {
                Posts = unitWork.GetAllPost()
            };

            return View(postViewmodel); 
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
            unitWork.Add(post);
            return RedirectToAction("index", "home");
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
            return RedirectToAction("index", "home");
        }

        public async Task<IActionResult> AddComment(string id,PostViewmodel model)
        {
            Post post = unitWork.GetPostById(int.Parse(id));
            var user = await userManager.FindByEmailAsync(User.Identity.Name);
          
            UserPostComment userPostComment = new UserPostComment()
            {
                Post = post,
                PostID = int.Parse(id),
                User = user,
                userID=user.Id
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
            return RedirectToAction("index", "home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
