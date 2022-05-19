using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contracts
{
    public class DriverEntity
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public DateTime BirthDate { get; set; }
        public String Address { get; set; }
        public String DrivingLicenseSeries { get; set; }
    }
}
