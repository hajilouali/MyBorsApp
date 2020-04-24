using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models.Models.ViewModels
{
    public class UserFaqs
    {
        [Display(Name = "نام شما ")]
        
        public string YourName { get; set; }
        [Display(Name = "ایمیل شما")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمیباشد")]
        public string Email { get; set; }
        [Display(Name = "سوال شما ؟")]
        [DataType(DataType.MultilineText)]
        public string Questions { get; set; }
    }

    public class SendCommentViewModel
    {

        [Display(Name = "نام شما ")]
        [Required(ErrorMessage = "لطفا {0}‌را وارد کنید ")]
        public string Name { get; set; }
        [Display(Name = "ادرس ایمیل")]
        [Required(ErrorMessage = "لطفا {0}‌را وارد کنید ")]
        [EmailAddress(ErrorMessage = "آدرس ایمیل وارد شده معتبر نمی باشد ")]
        public string Email { get; set; }
        [Display(Name = "نظر")][DataType(DataType.MultilineText)]
        [MaxLength(300,ErrorMessage = "حداکثر {1} کاراکتر مورد قبول میباشد")]
        public string Comment { get; set; }
        
    }

    public class ContactusViewModel
    {
        [Required(ErrorMessage = "لطفا {0}‌ را وارد نمایید")]
        [Display(Name = "نام / شرکت")]
        public string Name { get; set; }
        [Display(Name = "آدرس ایمیل")]
        [Required(ErrorMessage = "لطفا {0}‌ را وارد نمایید")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمیباشد")]
        public string Email { get; set; }
        [Display(Name = "شماره تماس")]
        [Required(ErrorMessage = "لطفا {0}‌ را وارد نمایید")]
        public string Phone { get; set; }
        [Display(Name = "عنوان پیغام")]
        [Required(ErrorMessage = "لطفا {0}‌ را وارد نمایید")]
        public string Subject { get; set; }
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }

    }

    public class SearchViewModel
    {
        public string SearchTitle { get; set; }
        public string SearchURL { get; set; }
        public DateTime DateTime { get; set; }
        public string ShortDiscription { get; set; }
    }
}