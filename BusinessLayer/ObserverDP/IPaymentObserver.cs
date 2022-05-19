using BusinessLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ObserverDP
{
    public interface IPaymentObserver
    {
        void UpdateMessage(PaymentModel paymentModel);
    }
}
