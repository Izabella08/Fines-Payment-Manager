using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Contracts
{
    public class PaymentModel
    {
        public Guid Id { get; set; }
        public FineModel Fine { get; set; }
        public String PaymentMode { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
