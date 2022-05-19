using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LayersOnWeb
{
    public class Driver
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public DateTime BirthDate { get; set; }
        public String Address { get; set; }
        public String DrivingLicenseSeries { get; set; }
    }
}
