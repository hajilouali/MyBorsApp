using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.Models.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterViewModel
    {
        [Display(Name = "نام کاربری ")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(50, ErrorMessage = "حد اکثر تعداد {1} کاراکتر می باشد")]
        public string UserName { get; set; }
        [Display(Name = "نام و نام خانوادگی / نام شرکت ")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(150,ErrorMessage = "حد اکثر تعداد {1} کاراکتر می باشد")]
        public string FullName { get; set; }
        [Display(Name = "شماره تماس ")]
       
        [MaxLength(50, ErrorMessage = "حد اکثر تعداد {1} کاراکتر می باشد")]

        public string PhoneNumber { get; set; }
        [Display(Name = "آدرس ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(150, ErrorMessage = "حد اکثر تعداد {1} کاراکتر می باشد")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد ")]
        public string Email { get; set; }
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(50, ErrorMessage = "حد اکثر تعداد {1} کاراکتر می باشد")]
        [MinLength(6,ErrorMessage = "حداقل تعداد کاراکتر باید {1} باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "تکرار رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [Compare("Password",ErrorMessage = "کلمه عبور وارد شده یکسان نمی باشند ")]
        [DataType(DataType.Password)]
        public string RePassword { get; set; }

        
    }

    public class LoginViewModel
    {
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        public string Email { get; set; }
        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "مرا به خاطر بسپار")]
        public bool RememberMe { get; set; }
    }

    public class AccountLockViewModel
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        [Required(ErrorMessage = "لطفاْ رمز عبور خود را وارد نمایید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Email { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        public string Email { get; set; }
    }

    public class RecoveryPasswordViewModel
    {
        [Display(Name = "کلمه عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "تکرار کلمه عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "کلمه های عبور مغایرت دارند")]
        public string RePassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Display(Name = "کلمه عبور فعلی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Display(Name = "کلمه عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "تکرار کلمه عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "کلمه های عبور مغایرت دارند")]
        public string RePassword { get; set; }
    }

    public class ChangeAccountInfoViewModel
    {
        [Display(Name = "نام و نام خانوادگی / نام شرکت ")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(150, ErrorMessage = "حد اکثر تعداد {1} کاراکتر می باشد")]
        public string FullName { get; set; }
        [Display(Name = "شماره تماس ")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(50, ErrorMessage = "حد اکثر تعداد {1} کاراکتر می باشد")]

        public string PhoneNumber { get; set; }
    }

    public class SetTokenViewModel
    {
        [DataType(DataType.MultilineText)]
        [Display(Name = "توکن ")]
        public string Token { get; set; }
        [Display(Name = "کاربر ثبت شده در کارگزاری")]
        public string TokenUserName { get; set; }

    }
}