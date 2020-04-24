using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Api.Response
{
  public  class PendingPageResponseViewModel
    {
        public int count { get; set; }
        public List<PendingOrderResponseViewModel> rows { get; set; }
    }
}
