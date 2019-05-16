using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TopLearn.DataLayer.Entities.User
{

    public class Role
    {
        public Role()
        {

        }

        [Key]
        public int RoleID { get; set; }

        [Display(Name = "عنوان نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string RoleTitle { get; set; }


        [Display(Name = "کاربر ثبت کننده")]
        public int UserID { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public DateTime RoleDateTime { get; set; }


        [Display(Name = "وضعیت")]
        public bool RoleActive { get; set; }

        #region Relations
        public virtual List<UserRole> UserRoles { get; set; }

        #endregion

    }
}
