using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }

        [Display(Name = " نقش کاربر")]
        
        public int RoleID { get; set; }
        [Display(Name = "نام و نام خانوادگی ")]
        [MaxLength(150, ErrorMessage = "حداکثر تعداد کاراکتر مورد قبول {0} می باشد. ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string FullName { get; set; }
        [Display(Name = "نام کاربری")]
        [MaxLength(150, ErrorMessage = "حداکثر تعداد کاراکتر مورد قبول {0} می باشد. ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserName { get; set; }
        [Display(Name = "ایمیل")]
        [MaxLength(150, ErrorMessage = "حداکثر تعداد کاراکتر مورد قبول {0} می باشد. ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد.")]
        public string Email { get; set; }
        [Display(Name = "رمز عبور")]
        [MaxLength(150, ErrorMessage = "حداکثر تعداد کاراکتر مورد قبول {0} می باشد. ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "کد فعال سازی")]

        public string ActiveCode { get; set; }
        [Display(Name = "کاربر فعال ؟")]

        public bool IsActive { get; set; }
        [Display(Name = "تاریخ ثبت نام کاربر")]
        public DateTime RegisterDate { get; set; }
        [MaxLength(50,ErrorMessage = "حداکثر تعداد کاراکتر {0} می باشد")]
        [Display(Name = "شماره تماس ")]
        public string PhoneNumer { get; set; }
        [Display(Name = "توکن")]
        public string Token { get; set; }
        [Display(Name = "کاربر توکن")]
        [MaxLength(500)]
        public string UserSetToken { get; set; }
        public virtual Roles Roles { get; set; }
        
       
    }
}