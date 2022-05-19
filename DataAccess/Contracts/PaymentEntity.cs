using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contracts
{
    public class PaymentEntity
    {
        public Guid Id { get; set; }
        public FineEntity Fine { get; set; }
        public String PaymentMode { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
