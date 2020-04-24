using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Api.Response
{
   public class OrderItem
    {
        public int id { get; set; }
        public int orderId { get; set; }
        public int state { get; set; }
        public double price { get; set; }
        public int quantity { get; set; }
        public int status { get; set; }
        public int dataID { get; set; }
        public int result { get; set; }
        public string serverOrderTime { get; set; }
        public string createDate { get; set; }
        public string ip { get; set; }
        public int creatorUser { get; set; }
        public int modifyUser { get; set; }
    }
}
