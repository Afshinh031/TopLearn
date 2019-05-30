using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TopLearn.ViewModel.UserViewModels
{
    class AccountViewModel
    {
    }
    public class UserRegisterViewModel
    {
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نیست")]
        public string UserEmail { get; set; }


        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [MinLength(6, ErrorMessage = "{0} نمیتواند کمتر از 6 کاراکتر باشد")]
        public string UserPassword { get; set; }


        [Display(Name = "تکرار کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [Compare("UserPassword", ErrorMessage = "کلمه های عبور مغایرت دارند")]
        [MinLength(6, ErrorMessage = "{0} نمیتواند کمتر از 6 کاراکتر باشد")]
        public string UserConfigortionPassword { get; set; }
    }


    public class LoginViewModel {
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نیست")]
        public string UserEmail { get; set; }


        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [MinLength(6, ErrorMessage = "{0} نمیتواند کمتر از 6 کاراکتر باشد")]

        public string UserPassword { get; set; }


        [Display(Name = "مرا بخواطر بسپار")]
        public bool RemmeberMe { get; set; }

        public string ReturnUrl{ get; set; }

    }

    public class ForgotPasswordViewModel
    {
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نیست")]
        public string UserEmail { get; set; }
    }


    public class RestPasswordViewModel {
        public string RestPasswordCode { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [MinLength(6, ErrorMessage = "{0} نمیتواند کمتر از 6 کاراکتر باشد")]

        public string UserPassword { get; set; }


        [Display(Name = "تکرار کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [Compare("UserPassword", ErrorMessage = "کلمه های عبور مغایرت دارند")]
        [MinLength(6, ErrorMessage = "{0} نمیتواند کمتر از 6 کاراکتر باشد")]

        public string UserConfigortionPassword { get; set; }

    }



    public class ChangePasswordViewModel {
        public int UserId { get; set; }

        [Display(Name = "کلمه قبلی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از 200 کاراکتر باشد .")]
        [MinLength(6, ErrorMessage = "{0} نمیتواند کمتر از 6 کاراکتر باشد")]
        public string UserOldPassWord { get; set; }


        [Display(Name = "کلمه عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از 200 کاراکتر باشد .")]
        [MinLength(6,ErrorMessage ="{0} نمیتواند کمتر از 6 کاراکتر باشد")]
        public string UserNewPassword { get; set; }


        [Display(Name = "تکرار کلمه عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از 200 کاراکتر باشد .")]
        [Compare("UserNewPassword", ErrorMessage = "کلمه های عبور مغایرت دارند")]
        [MinLength(6, ErrorMessage = "{0} نمیتواند کمتر از 6 کاراکتر باشد")]
        public string UserConfigortionNewPassword { get; set; }
    }

}
