using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Api.Request
{
  public  class DeleteOrderViewModel
    {
        public int orderItemId { get; set; }
        public int customer { get; set; }
    }
}
