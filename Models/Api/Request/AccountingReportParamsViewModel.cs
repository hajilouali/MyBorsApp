using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Api.Request
{
   public class AccountingReportParamsViewModel
    {
        public int contractId { get; set; }
        public int page { get; set; }
        public int customerId { get; set; }
        public string persianFromDate { get; set; }
        public string persianToDate { get; set; }
    }
}
