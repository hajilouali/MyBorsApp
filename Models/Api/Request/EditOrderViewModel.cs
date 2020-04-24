using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Api.Request
{
   public class EditOrderViewModel
    {
        public int oldOrderItemId { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }
        public int customer { get; set; }
        public int stopLossPrice { get; set; }
        public int takeProfitPrice { get; set; }
        public int stopLossLimitPrice { get; set; }
        public int takeProfitLimitPrice { get; set; }
    }
}
