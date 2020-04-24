using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Site.Models.Chart
{
   public class ChartInformation
    {
        [Display(Name = "نام")] public string Title { get; set; }
        [Display(Name = "پایین ترین قیمت")]
        public double LowPrice { get; set; }
        [Display(Name = "بالاترین قیمت")]
        public double HightPrice { get; set; }
        [Display(Name = "میانگین قیمت")]
        public double Avarage { get; set; }
    }
}
