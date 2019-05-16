using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TopLearn.DataLayer.Entities.User
{
    public class User
    {
        public User()
        {

        }

        [Key]
        public int UserID { get; set; }

        [Display(Name = "نام")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string UserFristName { get; set; }


        [Display(Name = "نام خانوادگی")]
        [MaxLength(300, ErrorMessage = "{0} نمی تواند بیشتر از 200 کاراکتر باشد .")]
        public string UserLastName { get; set; }


        [Display(Name = "نام کاربری")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string UserName { get; set; }


        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نیست")]
        public string UserEmail { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]

        public string UserPassword { get; set; }

        [Display(Name = "کد فعال سازی")]
        public string UserEmailConfigurationCode { get; set; }

        [Display(Name = "تاریخ فعال سازی")]
        public DateTime UserEmailConfigurationDateTime { get; set; }


        [Display(Name = "تاریخ تولد")]
        [MaxLength(11, ErrorMessage = "تاریخ تولید نمی تواند بیشتر از 11 کاراکتر باشد .")]
        [DataType(DataType.Date)]
        public string UserBirthday { get; set; }

        [Display(Name = "آواتار")]
        public string UserImage { get; set; }

        [Display(Name = "درباره من")]
        [MaxLength(500)]
        public string UserAbout { get; set; }

        [Display(Name = "وضعیت")]
        public bool UserIsActive { get; set; }

        [Display(Name = "تاریخ ثبت نام")]
        public DateTime UserDateTime { get; set; }

        #region Relations

        public virtual List<UserRole> UserRoles { get; set; }

        #endregion


    }
}
