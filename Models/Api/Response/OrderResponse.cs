using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Api.Response
{
   public class OrderResponse
    {
      public  Int64 id { get; set; }
        public Int64 orderbookId { get; set; }
        public Int32 contractId { get; set; }
        public string contractName { get; set; }
        public string symbol { get; set; }
        public string createDate { get; set; }
        public Int32 orderSide { get; set; }
        public Int32 remain { get; set; }
        public Int64 tradeId { get; set; }
        public Int32 masterStatus { get; set; }
        public bool station { get; set; }
        public double stopLossPrice { get; set; }
        public double takeProfitPrice { get; set; }
        public double stopLossLimitPrice { get; set; }
        public double takeProfitLimitPrice { get; set; }
        public double lastPrice { get; set; }
        public Int32 quantity { get; set; }
        public List<OrderItem> orderItems { get; set; }

    }
}
