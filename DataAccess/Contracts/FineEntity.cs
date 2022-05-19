using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contracts
{
    public class FineEntity
    {
        public Guid Id { get; set; }
        public String DeedCommitted { get; set; }
        public int PaymentAmount { get; set; }
        public DriverEntity Driver { get; set; }
        public Boolean isPaid { get; set; }
    }
}
