using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TopLearn.DataLayer.Entities.Stores
{
    public class StoreType
    {
        public StoreType()
        {

        }

        [Key]
        public int StoreTypeID { get; set; }

        [Display(Name = "عنوان نوع فروشگاه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string StoreTypeTitle { get; set; }

        [Display(Name = "کاربر ثبت کننده")]
        public int UserID { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public DateTime StoreTypeDateTime { get; set; }

        [Display(Name = "تاریخ آخرین ویرایش")]
        public DateTime StoreTypeEditeDateTime { get; set; }


        [Display(Name = " آخرین کاربر ویرایش")]
        public int UserEditorID { get; set; }

        #region Relations
        public virtual List<Store> Stores { get; set; }
        #endregion
    }
}
