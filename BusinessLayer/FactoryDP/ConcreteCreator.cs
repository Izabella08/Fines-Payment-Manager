using BusinessLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.FactoryDP
{
    public class ConcreteCreator : Creator
    {
        public IFineService fineService;

        public ConcreteCreator(IFineService fineService)
        {
            this.fineService = fineService;
        }

        public override void createWriter(string method)
        {
            switch (method)
            {
                case "CSV":
                    IWriter csv = new WriterCSV();
                    csv.CreateReport(fineService.GetAllFines());
                    break;
                case "XML":
                    IWriter xml = new WriterXML();
                    xml.CreateReport(fineService.GetAllFines());
                    break;
                default:
                    throw new ApplicationException(string.Format("Report '{0}' cannot be created", method));
            }
        }
    }
}
