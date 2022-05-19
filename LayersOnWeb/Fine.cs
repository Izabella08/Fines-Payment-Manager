using BusinessLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LayersOnWeb
{
    public class Fine
    {
        public Guid Id { get; set; }
        public String DeedCommitted { get; set; }
        public int PaymentAmount { get; set; }
        public DriverModel Driver { get; set; }
        public Boolean isPaid { get; set; }
    }
}
