using BusinessLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LayersOnWeb
{
    public class Complaint
    {
        public Guid Id { get; set; }
        public FineModel Fine { get; set; }
        public String ComplaintMotive { get; set; }
    }
}
