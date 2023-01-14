using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weatherApp
{
    class Location
    {

        public string name { get; set; }
        public string region { get; set; }
        public string country { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public string tz_id { get; set; }
        public string localtime_epoch { get; set; } 
        public DateTime localtime { get; set; }
    }
}
