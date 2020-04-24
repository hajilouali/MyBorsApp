using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Api.Enums;

namespace Models.Api.Request
{
   public class NewOrderViewModel
    {
        public int contract { get; set; }
        public orderSide orderSide { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }
        public int customer { get; set; }
        public int stopLossPrice { get; set; }
        public int takeProfitPrice { get; set; }
        //public bool marketOrder { get; set; }
        public int stopLossLimitPrice { get; set; }
        public int takeProfitLimitPrice { get; set; }
    }
}
