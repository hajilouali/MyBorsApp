using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Api.Response
{
   public class PageResponse
    {
        public int count { get; set; }
        public List<OrderResponse> rows { get; set; }
    }
}
