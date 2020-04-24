using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Site.Models.Enums
{
   public enum ContractType
    {
        [Display(Name = "حالت نرمال")]
        Normaly,
        [Display(Name = "معامله معکوس")]
        UpsideDown,
        [Display(Name = "برحسب ضریب")]
        ByCoefficient,


    } 
    
}
