using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Models.Site.Models.Chart
{
    [DataContract]
    public class DataPoint
    {
        public DataPoint(string label, double y)
        {
            this.label = label;
            this.Y = y;

        }

        public DataPoint()
        {
        }

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "label")]
        public string label = null;

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "y")]
        public double Y ;
    }
    public class dataPointss
    {
        public string name;
        public string type;
        public bool showInLegend;
        public List<DataPoint> dataPoints;
    }
}
