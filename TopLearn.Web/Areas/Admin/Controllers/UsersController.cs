using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopLearn.Core.Services.Interfaces;
using TopLearn.ViewModel.UserViewModels;

namespace TopLearn.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class UsersController : Controller
    {
        private IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("Admin/Users")]
        public IActionResult Index(int pageNumber = 1)
        {
            int skip = (pageNumber - 1) * 10;
            UserAdminPanelViewModel userAdminPanelViewModel = new UserAdminPanelViewModel();
            userAdminPanelViewModel.UserInactiveViewModel = _userService.GetUsersInactive(skip, 10);
            userAdminPanelViewModel.UserModel = _userService.GetAllUser(skip, 10, Convert.ToInt32(User.Identity.Name));
            userAdminPanelViewModel.UserInactiveCount = userAdminPanelViewModel.UserInactiveViewModel.Count;
            userAdminPanelViewModel.UserCount = userAdminPanelViewModel.UserModel.Count;
            return View(userAdminPanelViewModel);
        }
    }
}