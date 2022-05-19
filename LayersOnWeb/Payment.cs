using BusinessLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LayersOnWeb
{
    public class Payment
    {
        public Guid Id { get; set; }
        public FineModel Fine { get; set; }
        public String PaymentMode { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
