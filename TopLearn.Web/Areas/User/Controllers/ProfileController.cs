using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Utility.Hasher;
using TopLearn.Utility.TextTools;
using TopLearn.Utility.Tools;
using TopLearn.ViewModel.UserViewModels;

namespace TopLearn.Web.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class ProfileController : Controller
    {

        private IUserService _userService;
        private IUserRepository _userRepository;
        public ProfileController(IUserService userService, IUserRepository userRepository)
        {
            _userService = userService;
            _userRepository = userRepository;
        }

        public IActionResult Index() => View(GetUserInfo());


        public async Task<IActionResult> EditProfile(UserEditProFileViewModel userEditProFileViewModel, IFormFile UserImage)
        {
            var user = _userService.GetUserById(userEditProFileViewModel.UserID);

            if (user == null)
            {
                ViewBag.error = "اطلاعات را درست وارد کنید";
                return View("Index", GetUserInfo());
            }

            if (user.UserName != user.UserName)
            {
                if (_userService.UserNameIsExist(userEditProFileViewModel.UserName))
                {
                    ViewBag.error = "این نام کاربری وجود دارد";
                    return View("Index", GetUserInfo());
                }
            }
            if (user.UserEmail != userEditProFileViewModel.UserEmail)
            {
                if (_userService.UserEmialIsExist(userEditProFileViewModel.UserEmail))
                {
                    ViewBag.error = "این  ایمیل وجود دارد";
                    return View("Index", GetUserInfo());
                }
            }

            if (userEditProFileViewModel.UserImage != null)
            {
                string imgName = TextTools.GenerateUniqCode() + Path.GetExtension(userEditProFileViewModel.UserImage.FileName); ;
                string a = Directory.GetCurrentDirectory();
                string upLoad = await ImageUploader.ImageUpload(a, "/wwwroot/PorojectImg/UserImage/", user.UserImage, imgName, userEditProFileViewModel.UserImage);
                if (upLoad != "1")
                {
                    ViewBag.error = "خطا در اپلود تصویر";
                    return View("Index", GetUserInfo());
                }
                user.UserImage = imgName;
            }


            #region Update
            user.UserName = userEditProFileViewModel.UserName;
            user.UserFristName = userEditProFileViewModel.UserFristName;
            user.UserLastName = userEditProFileViewModel.UserLastName;
            user.UserEmail = userEditProFileViewModel.UserEmail;
            user.UserAbout = userEditProFileViewModel.UserAbout;
            user.UserBirthday = userEditProFileViewModel.UserBirthday;
            _userRepository.UpdateUser(user);
            _userRepository.SaveUser();
            #endregion
            return View("Index", GetUserInfo());
        }


        public ActionResult ChangePassword(ChangePasswordViewModel changePasswordViewModel) {
            var user = _userService.GetUserById(changePasswordViewModel.UserId);

            if (user == null)
            {
                ViewBag.error = "اطلاعات را درست وارد کنید";
                return View("Index", GetUserInfo());
            }

            if (changePasswordViewModel.UserNewPassword != changePasswordViewModel.UserConfigortionNewPassword) {
                ViewBag.error = "کلمه عبور و تکرار آن مغایرت دارد";
                return View("Index", GetUserInfo());
            }

            if (changePasswordViewModel.UserNewPassword.Length < 6) {
                ViewBag.error = "کلمه عبور باید حداعقل 6 کاراکتر باشد";
                return View("Index", GetUserInfo());
            }

            if (user.UserPassword != changePasswordViewModel.UserOldPassWord.ToEncodePasswordMd5())
            {
                ViewBag.error = "کلمه عبور قبلی اشتباه است";
                return View("Index", GetUserInfo());
            }
        

            if (user.UserPassword == changePasswordViewModel.UserNewPassword.ToEncodePasswordMd5())
                return View("Index", GetUserInfo());
            user.UserPassword = changePasswordViewModel.UserNewPassword.ToEncodePasswordMd5();
            _userRepository.UpdateUser(user);
            _userRepository.SaveUser();
            return View("Index", GetUserInfo());
        }

        public UserViewModel GetUserInfo()
        {


            UserViewModel userViewModel = new UserViewModel();
            ChangePasswordViewModel changePasswordViewModel = new ChangePasswordViewModel();
            UserEditProFileViewModel userEditProFileViewModel = new UserEditProFileViewModel();
            UserModel userModel = new UserModel();
            var user = _userService.GetUserById(Convert.ToInt32(User.Identity.Name));
            #region UuserModel
            userModel.UserFristName = user.UserFristName;
            userModel.UserLastName = user.UserLastName;
            userModel.UserEmail = user.UserEmail;
            userModel.UserEmailConfigurationDateTime = user.UserEmailConfigurationDateTime;
            userModel.UserBirthday = user.UserBirthday;
            userModel.UserImage = user.UserImage;
            userModel.UserAbout = user.UserAbout;
            userModel.UserRol = "کاربر";
            userModel.UserIsActive = user.UserIsActive;
            userModel.UserDateTime = user.UserDateTime;
            #endregion
            #region UuserEditProFileViewModel
            userEditProFileViewModel.UserID = user.UserID;
            userEditProFileViewModel.UserFristName = user.UserFristName;
            userEditProFileViewModel.UserLastName = user.UserLastName;
            userEditProFileViewModel.UserName = user.UserName;
            userEditProFileViewModel.UserAbout = user.UserAbout;
            userEditProFileViewModel.UserEmail = user.UserEmail;
            userEditProFileViewModel.UserBirthday = user.UserBirthday;
            #endregion

            changePasswordViewModel.UserId = user.UserID;
  
            userViewModel.UserEditProFileViewModel = userEditProFileViewModel;
            userViewModel.UserModel = userModel;
            userViewModel.ChangePasswordViewModel = changePasswordViewModel;
            return userViewModel;
        }

    }
}