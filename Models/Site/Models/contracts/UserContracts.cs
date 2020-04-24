using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Site.Models.Enums;

namespace Models
{
   public class UserContracts
    {
        [Key]
        public int  ID { get; set; }
        [Display(Name = "کاربر")]
        public int UserID { get; set; }
        [Display(Name = "نوع قرارداد")]
        public ContractType ContractType { get; set; }
        [Display(Name = "کالای 1")]
        public int ContractID1 { get; set; }
        [Display(Name = "کالای 2")]
        public int ContractID2 { get; set; }
        [Display(Name = "مبلغ سقف فاصله")]
        public int Max { get; set; }
        [Display(Name = "مبلغ کف فاصله")]
        public int Min { get; set; }

        [Display(Name = "تعداد معامله")]
        public int ContContract { get; set; }
        [Display(Name = "تاریخ ایجاد ")]
        public DateTime DateTime { get; set; }
        [Display(Name = "فعال")]
        public bool IsActive { get; set; }
        [Display(Name = "تعداد معامله اتوماتیک")]
        public int ContractCountActive { get; set; }
        public virtual Users Users { get; set; }
    }
}
