using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iSoft.InfluxDB
{
    public class InfluxModel
    {
        public Instant Time { get; set; }
        public string Name { get; set; }
        public object Value { get; set; }
    }
}
