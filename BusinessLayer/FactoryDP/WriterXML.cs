using BusinessLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BusinessLayer.FactoryDP
{
    public class WriterXML : IWriter
    {
        public void CreateReport(List<FineModel> fines)
        {
            try
            {
                XmlWriter xmlWriter = XmlWriter.Create("D:\\AN3\\an3_sem2\\PS Proiectare Software\\PROIECT\\Project\\report.xml");
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("UnpaidFines");
                foreach (var f in fines)
                {
                    if (f.isPaid == false)
                    {
                        xmlWriter.WriteStartElement("FINE");
                        xmlWriter.WriteAttributeString("FineID", f.Id.ToString());
                        xmlWriter.WriteAttributeString("DeedCommitted", f.DeedCommitted.ToString());
                        xmlWriter.WriteAttributeString("PaymentAmount", f.PaymentAmount.ToString());
                        xmlWriter.WriteAttributeString("DriverID", f.Driver.Id.ToString());
                        xmlWriter.WriteEndElement();
                    }
                }
                xmlWriter.WriteEndDocument();
                xmlWriter.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
