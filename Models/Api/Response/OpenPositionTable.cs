using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Api.Response
{
   public class OpenPositionTable
    {
        public string symbol { get; set; }
        public int sell { get; set; }
        public int buy { get; set; }
        public int contractId { get; set; }
    }
}
