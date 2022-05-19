using BusinessLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ObserverDP
{
    public interface IPaymentNotifier
    {
        void Attach(IPaymentObserver observer);

        void Detach(IPaymentObserver observer);

        void Notify(PaymentModel paymentModel);
    }
}
