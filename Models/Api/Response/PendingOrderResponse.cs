using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Api.Response
{
  public  class PendingOrderResponse
    {
        public Int32 id { get; set; }
        public Int32 masterOrderSide { get; set; }
        public Int32 quantity { get; set; }
        public double stopLossPrice { get; set; }
        public double takeProfitPrice { get; set; }
        public double stopLossLimitPrice { get; set; }
        public double takeProfitLimitPrice { get; set; }
        public Int32 contractId { get; set; }
        public Int32 customerId { get; set; }
        public double masterOrderPrice { get; set; }
        public Int64 masterOrderBookId { get; set; }
        public string symbol { get; set; }
        public string contractName { get; set; }

    }
}
