using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Contracts
{
    public class FineModel
    {
        public Guid Id { get; set; }
        public String DeedCommitted { get; set; }
        public int PaymentAmount { get; set; }
        public DriverModel Driver { get; set; }
        public Boolean isPaid { get; set; }
    }
}
