using BusinessLayer.Contracts;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.FactoryDP
{
    public class WriterCSV : IWriter
    {
        public void CreateReport(List<FineModel> fines)
        {
            try
            {
                MemoryStream mem = new MemoryStream();
                StreamWriter writer = new StreamWriter(mem);
                var csvWriter = new CsvWriter(writer, CultureInfo.CurrentCulture);

                csvWriter.WriteField("Fine ID");
                csvWriter.WriteField("Deed Committed");
                csvWriter.WriteField("Payment Amount");
                csvWriter.WriteField("Driver ID");
                csvWriter.NextRecord();

                foreach (var f in fines)
                {
                    if(f.isPaid == false)
                    {
                        csvWriter.WriteField(f.Id);
                        csvWriter.WriteField(f.DeedCommitted);
                        csvWriter.WriteField(f.PaymentAmount);
                        csvWriter.WriteField(f.Driver.Id);
                        csvWriter.NextRecord();
                    }
                }

                writer.Flush();

                var res = Encoding.UTF8.GetString(mem.ToArray());
                File.WriteAllText("D:\\AN3\\an3_sem2\\PS Proiectare Software\\PROIECT\\Project\\report.csv", res);
                Console.WriteLine(res);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
