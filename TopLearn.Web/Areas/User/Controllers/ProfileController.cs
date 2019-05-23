using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopLearn.Core.Services.Interfaces;


namespace TopLearn.Web.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class ProfileController : Controller
    {

        private IUserService _userService;
        public ProfileController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View(_userService.GetUserById(Convert.ToInt32(User.Identity.Name)));
        }

        public IActionResult EditProfile(TopLearn.DataLayer.Entities.User.User user) {
            return View();
        }

    }
}