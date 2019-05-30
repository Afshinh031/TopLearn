using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TopLearn.ViewModel.UserViewModels
{
    #region UserPanel
    public class UserModel
    {

        public int UserId { get; set; }
        public string UserFristName { get; set; }

        public string UserLastName { get; set; }

        public string UserName { get; set; }

        public string UserEmail { get; set; }
        public DateTime UserEmailConfigurationDateTime { get; set; }

        public string UserBirthday { get; set; }

        public string UserImage { get; set; }

        public string UserAbout { get; set; }
        public string UserRol { get; set; }

        public bool UserIsActive { get; set; }

        public DateTime UserDateTime { get; set; }
    }
    public class UserEditProFileViewModel {
        public int UserID { get; set; }

        [Display(Name = "نام")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از 200 کاراکتر باشد .")]
        public string UserFristName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [MaxLength(300, ErrorMessage = "{0} نمی تواند بیشتر از 200 کاراکتر باشد .")]
        public string UserLastName { get; set; }

        [Display(Name = "نام کاربری")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از 200 کاراکتر باشد .")]
        public string UserName { get; set; }

        [Display(Name = "درباره من")]
        [MaxLength(500, ErrorMessage = "{0} نمی تواند بیشتر از 500 کاراکتر باشد .")]
        public string UserAbout { get; set; }

        [Display(Name = "ایمیل")]  
        [MaxLength(300, ErrorMessage = "{0} نمی تواند بیشتر از 300 کاراکتر باشد .")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نیست")]
        public string UserEmail { get; set; }


        public IFormFile UserImage { get; set; }

        [Display(Name = "تاریخ تولد")]
        [MaxLength(10, ErrorMessage = "{0} نمی تواند بیشتر از 10 کاراکتر باشد .")]
        public string UserBirthday { get; set; }
    }

    public class UserViewModel {
        public UserModel UserModel { get; set; }
        public UserEditProFileViewModel UserEditProFileViewModel { get; set; }
        public ChangePasswordViewModel ChangePasswordViewModel { get; set; }
    }
    #endregion

    #region AdminPanel
    public class UserInactiveViewModel {
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserDescription { get; set; }
        public DateTime UserDateTime { get; set; }
        public DateTime UserLastUpdateDateTime { get; set; }
    }

    public class UserAdminPanelViewModel {
        public List<UserInactiveViewModel> UserInactiveViewModel { get; set; }
        public List<UserModel> UserModel { get; set; }
        public int UserInactiveCount { get; set; }
        public int UserCount { get; set; }
    }
    #endregion
}
