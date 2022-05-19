using BusinessLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.FactoryDP
{
    public interface IWriter
    {
        void CreateReport(List<FineModel> fines);
    }
}
