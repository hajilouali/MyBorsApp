using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Site.ViewModels
{
  public  class ChartViewmodel
    {
        public string Symbol { get; set; }
        public List<Double> Value { get; set; }
        public List<string> DateTime { get; set; }
    }
}
