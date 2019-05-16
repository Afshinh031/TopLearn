using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TopLearn.DataLayer.Entities.Stores
{
    public class Province
    {
        public Province()
        {

        }

        [Key]
        public int ProvinceID { get; set; }

        [Display(Name = "نام استان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string ProvinceName { get; set; }


        #region Relations
        public virtual List<Store> Stores { get; set; }

        #endregion

    }
}
