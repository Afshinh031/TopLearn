using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Core.Services.Service;
using TopLearn.DataLayer.Context;
using TopLearn.Utility.Hasher;
using TopLearn.Utility.TextTools;
using TopLearn.ViewModel.UserViewModels;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity.UI.Pages.Account.Internal;
using TopLearn.Utility.Convertor;
using TopLearn.Utility.Sender;

namespace TopLearn.Web.Controllers
{
    public class AccountController : Controller
    {


        private IUserRepository _userRepository;
        private IUserService _userService;
        private IViewRenderService _viewRenderService;
        public AccountController(IUserService userService, IUserRepository userRepository, IViewRenderService viewRenderService)
        {
            _userService = userService;
            _userRepository = userRepository;
            _viewRenderService = viewRenderService;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region Register
        [Route("Register")]
        public IActionResult Register() => View();

        [Route("Register")]
        [HttpPost]
        public IActionResult Register(UserRegisterViewModel userRegisterViewModel)
        {
            if (!ModelState.IsValid)
            {
                userRegisterViewModel.UserPassword = null;
                userRegisterViewModel.UserConfigortionPassword = null;
                return View(userRegisterViewModel);
            }
            if (userRegisterViewModel.UserPassword.Length < 5)
            {
                ModelState.AddModelError("UserPassword", "طول کلمه عبور باید بشتر از 5 کاراکتر باشد");
                userRegisterViewModel.UserPassword = null;
                userRegisterViewModel.UserConfigortionPassword = null;
                return View(userRegisterViewModel);
            }
            if (userRegisterViewModel.UserPassword != userRegisterViewModel.UserConfigortionPassword)
            {
                ModelState.AddModelError("UserPassword", "کلمه عبور مغایرت دارد");
                userRegisterViewModel.UserPassword = null;
                userRegisterViewModel.UserConfigortionPassword = null;
                return View(userRegisterViewModel);
            }

            if (_userService.EmialIsExist(userRegisterViewModel.UserEmail))
            {
                ModelState.AddModelError("UserEmail", "ایمیل معتبر نمی باشد");
                userRegisterViewModel.UserPassword = null;
                userRegisterViewModel.UserConfigortionPassword = null;
                return View(userRegisterViewModel);
            }




            var user = new DataLayer.Entities.User.User()
            {
                UserImage = "Defult.jpg",
                UserIsActive = false,
                UserEmailConfigurationCode = TextTools.GenerateUniqCode(),
                UserEmailConfigurationDateTime = DateTime.Now,
                UserDateTime = DateTime.Now,
                UserPassword = userRegisterViewModel.UserPassword.ToEncodePasswordMd5(),
                UserEmail = userRegisterViewModel.UserEmail.FixEmail(),
            };
            if (!(_userRepository.InsertUser(user)))
            {
                ModelState.AddModelError("UserEmail", "خطا در ثبت اطلاعات");
                userRegisterViewModel.UserPassword = null;
                userRegisterViewModel.UserConfigortionPassword = null;
                return View(userRegisterViewModel);
            }

            _userRepository.SaveUser();
            string emailBody = _viewRenderService.RenderToStringAsync("_ActiveCodeEmail", user);
            string send = SendEmail.Send(user.UserEmail, "کد فعال سازی حساب کاربری", emailBody);
            return View("RegisterSuccess", userRegisterViewModel);
        }

        [Route("ActiveAccount/{activeCode}")]
        public ActionResult ActiveAccount(string activeCode)
        {
            
            var user = _userService.GetUserByActiveCode(activeCode);
            if (user == null)
                return View("ActiveCodeNotFound");
            user.UserEmailConfigurationCode = TextTools.GenerateUniqCode();
            user.UserIsActive = true;
            user.UserEmailConfigurationDateTime = DateTime.Now;
            if (_userRepository.UpdateUser(user)) {
                _userRepository.SaveUser();
                return View();
            }
            return Redirect("/");
        }


        #endregion

        #region Login
        [Route("Login")]
        public ActionResult Login() => View();


        [Route("Login")]
        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("UserEmail", "نام کاربری و ایمیل را وراد کنید");
                loginViewModel.UserPassword = null;
                return View(loginViewModel);
            }
            if (_userService.LoginUser(loginViewModel))
            {
                var user = _userService.GetUserByEmail(loginViewModel.UserEmail);
                var claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier,user.UserID.ToString()),
                    new Claim(ClaimTypes.Name,(user.UserFristName==null)?"به فروشگاه ما": user.UserFristName+" ")
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                var properties = new AuthenticationProperties
                {
                    IsPersistent = loginViewModel.RemmeberMe
                };
                HttpContext.SignInAsync(principal, properties);
                return Redirect("/");
            }
            ModelState.AddModelError("UserEmail", "کاربری با این مشخصات یافت نشد");
            loginViewModel.UserPassword = null;
            return View(loginViewModel);

        }

        #endregion
    }
}