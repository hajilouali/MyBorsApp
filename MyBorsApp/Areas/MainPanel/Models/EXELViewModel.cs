using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBorsApp.Areas.MainPanel.Models
{
    public class EXELViewModel
    {
        [Display(Name = "مقدار")]
        public double Value { get; set; }
        [Display(Name = "زمان")]
        public string DataTime { get; set; }
    }
}