using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Api.Enums;

namespace Models.Site.Models.contracts
{
   public class UsersOrderLogs
    {
        [Key]
        public int LogId { get; set; }
        [Display(Name = "کاربر")]
        public int UserID { get; set; }
        [Display(Name = "کد قرار داد")]
        
        public int ContractID { get; set; }
        [Display(Name = "نوع قرار داد")]
        public orderSide OrderSide { get; set; }
        [Display(Name = "مبلغ")]
        public int Price { get; set; }
        [Display(Name = "تعداد قرار داد")]
        public int quantity { get; set; }
        [Display(Name = "ساعت ")]
        public DateTime DateTime { get; set; }
        [Display(Name = "مقدار قرارداد 1 ")]
        public int Contract1Valum { get; set; }
        [Display(Name = "مبلغ قرارداد 1 ")]
        public int Contract1Price { get; set; }
        [Display(Name = "مقدار قرارداد 2 ")]
        public int Contract2Valum { get; set; }
        [Display(Name = "مبلغ قرارداد 2 ")]
        public int contract2price { get; set; }
        public virtual Users Users{ get; set; }
    }
}
