using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contracts
{
    public class ComplaintEntity
    {
        public Guid Id { get; set; }
        public FineEntity Fine { get; set; }
        public String ComplaintMotive { get; set; }
    }
}
