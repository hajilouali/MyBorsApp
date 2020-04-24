using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Api.Response
{
   public class PendingOrderResponseViewModel
    {
        Int32 customerId { get; set; }
        string fullName { get; set; }
        Int32 masterOrderSide { get; set; }
        double stopLossPrice { get; set; }
        double stopLossLimitPrice { get; set; }
        double takeProfitPrice { get; set; }
        double takeProfitLimitPrice { get; set; }
        Int32 executionStatus { get; set; }
        string symbol { get; set; }
        string createDate { get; set; }
        Int32 totalQuantity { get; set; }

    }
}
