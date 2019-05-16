using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TopLearn.DataLayer.Entities.Stores
{
    public class Store
    {
        public Store()
        {

        }

        [Key]
        public int StoreID { get; set; }

        [Display(Name = "نام فروشگاه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string StoreName { get; set; }


        [Display(Name = "تصویر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100)]
        public string StoreImage { get; set; }


        [Display(Name = "لوگوی فروشگاه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100)]
        public string StoreLogo { get; set; }

        [Display(Name = "درباره فروشگاه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(400, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string StoreAbout { get; set; }


        [Display(Name = "استان")]
        [Required]
        public int ProvinceID { get; set; }

        [Display(Name = "آدرس فروشگاه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(400, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string StoreAddress { get; set; }

        [Display(Name = "وضعیت")]
        public bool StoreIsActive { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public DateTime StoreDateTime { get; set; }

        [Display(Name = "تاریخ آخرین ویرایش")]
        public DateTime StoreEditeDateTime { get; set; }

        [Display(Name = "نوع فروشگاه")]
        [Required]
        public int StoreTypeID { get; set; }

        [Display(Name = " آخرین کاربر ویرایش")]
        public int UserEditorID { get; set; }

        #region Relations
        public virtual Province Province { get; set; }
        public virtual StoreType StoreType { get; set; }
        #endregion
    }
}
