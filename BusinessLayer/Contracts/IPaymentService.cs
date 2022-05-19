using BusinessLayer.ObserverDP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Contracts
{
    public interface IPaymentService : IPaymentNotifier
    {
        bool PayFineModel(PaymentModel payment);
        List<PaymentModel> GetAllPayments();

    }
}
