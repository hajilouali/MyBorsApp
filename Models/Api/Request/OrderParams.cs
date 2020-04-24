using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Api.Request
{
    public class OrderParams
    {
        public Int32 page { get; set; }
        public Int32 status { get; set; }
        public Int64 orderbookId { get; set; }
        public Int32 type { get; set; }
        public Int32 contractId { get; set; }
        public Int32 customerId { get; set; }
        public Int32 station { get; set; }
        public string persianFromDate { get; set; }
        public string persianToDate { get; set; }

    }
}
