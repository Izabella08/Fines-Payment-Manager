using BusinessLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ObserverDP
{
    public class MessageObserver : IPaymentObserver
    {
        public void UpdateMessage(PaymentModel paymentModel)
        {
            Console.WriteLine("Fine with Id '{1}' was paid. A notification was sent to the police station.",
               paymentModel.Id, paymentModel.Fine.Id );
        }
    }
}
