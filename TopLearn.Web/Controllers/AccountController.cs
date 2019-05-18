using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Utility.Convertor;
using TopLearn.Utility.Hasher;
using TopLearn.Utility.Sender;
using TopLearn.Utility.TextTools;
using TopLearn.ViewModel.UserViewModels;

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
            string send = SendEmail.Send(user.UserEmail, "کد فعال سازی حساب کاربری", _viewRenderService.RenderToStringAsync("_ActiveCodeEmail", user));
            if (send != "1")
                return Content("خطا در ارسال ایمیل فعال سازی با پشتیبانی تماس بگیرید");
            return View("RegisterSuccess", userRegisterViewModel);
        }

        [Route("ActiveAccount/{activeCode}")]
        public ActionResult ActiveAccount(string activeCode)
        {

            var user = _userService.GetUserByActiveCode(activeCode);
            if (user == null)
                return View();
            user.UserEmailConfigurationCode = TextTools.GenerateUniqCode();
            user.UserIsActive = true;
            user.UserEmailConfigurationDateTime = DateTime.Now;
            if (_userRepository.UpdateUser(user))
            {
                _userRepository.SaveUser();
                return View(user);
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

        #region ForgotPassword
        [Route("ForgotPassword")]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [Route("ForgotPassword")]
        public ActionResult ForgotPassword(ForgotPasswordViewModel forgotPasswordViewModel)
        {
            if (!(ModelState.IsValid))
            {
                ModelState.AddModelError("UserEmail", "ایمیل را وارد کنید");
                return View(forgotPasswordViewModel);
            }
            var user = _userService.GetUserByEmail(forgotPasswordViewModel.UserEmail);
            if (user == null)
            {
                ModelState.AddModelError("UserEmail", "این ایمیل یافت نشد");
                return View(forgotPasswordViewModel);
            }
            string send = SendEmail.Send(user.UserEmail, "باز یابی کلمه عبور", _viewRenderService.RenderToStringAsync("_ForgotPassworEmail", user));
            if (send != "1")
            {
                ModelState.AddModelError("UserEmail", "خطا در ارسال ایمیل بازیابی ");
                return View(forgotPasswordViewModel);
            }
            ViewBag.isSucsses = true;
            return View();
        }

        [Route("ResetPassword/{restPasswordCode}")]
        public ActionResult ResetPassword(string restPasswordCode)
        {
            var user = _userService.GetUserByActiveCode(restPasswordCode);
            if (user == null)
                return View();
            return View(new RestPasswordViewModel()
            {
                RestPasswordCode = restPasswordCode
            });
        }

        [HttpPost]
        [Route("ResetPassword")]
        public ActionResult ResetPassword(RestPasswordViewModel restPasswordViewModel)
        {
            
            if (!(ModelState.IsValid))
                return View(restPasswordViewModel);
        
            if (restPasswordViewModel.UserPassword.Length < 5)
            {
                ModelState.AddModelError("UserPassword", "طول کلمه عبور باید بشتر از 5 کاراکتر باشد");
                restPasswordViewModel.UserPassword = null;
                restPasswordViewModel.UserConfigortionPassword = null;
                return View(restPasswordViewModel);
            }
            if (restPasswordViewModel.UserPassword != restPasswordViewModel.UserConfigortionPassword)
            {
                ModelState.AddModelError("UserPassword", "کلمه عبور مغایرت دارد");
                restPasswordViewModel.UserPassword = null;
                restPasswordViewModel.UserConfigortionPassword = null;
                return View(restPasswordViewModel);
            }
            var user = _userService.GetUserByActiveCode(restPasswordViewModel.RestPasswordCode);
            if (user == null) {
                ModelState.AddModelError("UserPassword", "این کاربر یافت نشد");
                return View(restPasswordViewModel);
            }
            user.UserPassword = restPasswordViewModel.UserPassword.ToEncodePasswordMd5();
            user.UserEmailConfigurationCode = TextTools.GenerateUniqCode();
            if (!(_userRepository.UpdateUser(user))) {
                ModelState.AddModelError("UserPassword", "خطا در بازیابی کلمه عبور ");
                return View(restPasswordViewModel);
            }
            _userRepository.SaveUser();
            return View("ResetPasswordSucsses");
        }
        #endregion
    }
}