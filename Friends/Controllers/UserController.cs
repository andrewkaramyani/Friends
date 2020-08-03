using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Friends.Models;
using Friends.Models.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Friends.Controllers
{
    public class UserController : Controller
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<Person> userManager;
        private readonly IWebHostEnvironment webHostEnvironment;

        public UserController(UserManager<Person> userManager, IWebHostEnvironment webHostEnvironment)
        {
            this.userManager = userManager;
            this.webHostEnvironment = webHostEnvironment;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            Person person = await userManager.FindByIdAsync(id);
            EditViewModel editviewmodel = new EditViewModel
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Gender = person.Gender,
                BirthDay = person.BirthDay,
                Email = person.Email,
                City = person.City,
                ExistingPhotoPath = person.PhotoPath
            };
            return View(editviewmodel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel Model)
        {
            if (ModelState.IsValid)
            {
                Person person = await userManager.FindByIdAsync(Model.Id.ToString());
                person.FirstName = Model.FirstName;
                person.LastName = Model.LastName;
                person.Gender = Model.Gender;
                person.BirthDay = Model.BirthDay;
                person.Email = Model.Email;
                person.City = Model.City;
               

                if (Model.Photo != null)
                {
                    if (Model.ExistingPhotoPath != null)
                    {
                        string filepate = Path.Combine(webHostEnvironment.WebRootPath, "images", Model.ExistingPhotoPath);
                        System.IO.File.Delete(filepate);
                    }
                    person.PhotoPath = ProcessUploadFile(Model);
                }


                await userManager.UpdateAsync(person);
                return RedirectToAction("index","home");
            }
            return View();
        }

        private string ProcessUploadFile(RegisterViewModel Model)
        {
            string uniquefile = null;
            if (Model.Photo != null)
            {
                string uploadfolder = Path.Combine(webHostEnvironment.WebRootPath + "/images");
                uniquefile = Guid.NewGuid().ToString() + "_" + Model.Photo.FileName;
                string filepath = Path.Combine(uploadfolder, uniquefile);
                using (var filestream = new FileStream(filepath, FileMode.Create))
                {
                    Model.Photo.CopyTo(filestream);
                }

            }

            return uniquefile;
        }
    }
}
