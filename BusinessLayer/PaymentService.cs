using BusinessLayer.Contracts;
using BusinessLayer.ObserverDP;
using DataAccess;
using DataAccess.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class PaymentService : IPaymentService
    {
        private readonly IRepository repository; 
        public List<IPaymentObserver> Observers = new List<IPaymentObserver>();

        public PaymentService(IRepository repository)
        {
            this.repository = repository;
        }


        public List<PaymentModel> GetAllPayments()
        {
            List<PaymentModel> result = new List<PaymentModel>();
            foreach (var payment in repository.GetAll<PaymentEntity>())
            {
                var fine = repository.GetAll<FineEntity>().Where(x => x.Id == payment.Fine.Id).FirstOrDefault();
                var driver = repository.GetAll<DriverEntity>().Where(x => x.Id == fine.Driver.Id).FirstOrDefault();
                DriverModel driverModel = new DriverModel { Id = driver.Id, Name = driver.Name, BirthDate = driver.BirthDate, Address = driver.Address, DrivingLicenseSeries = driver.DrivingLicenseSeries };
                FineModel fineModel = new FineModel { Id = fine.Id, DeedCommitted = fine.DeedCommitted, PaymentAmount = fine.PaymentAmount, Driver = driverModel, isPaid = fine.isPaid };
                result.Add(new PaymentModel { Id = payment.Id, Fine = fineModel, PaymentMode = payment.PaymentMode, PaymentDate = payment.PaymentDate});
            }
            return result;
        }


        public bool PayFineModel(PaymentModel payment)
        {
            FineEntity fineEntity = repository.GetById<FineEntity>(payment.Fine.Id);
            if(fineEntity.isPaid == false)
            {
                repository.Add(new PaymentEntity { Fine = fineEntity, PaymentMode = payment.PaymentMode, PaymentDate = payment.PaymentDate });
                fineEntity.isPaid = true;

                Notify(payment);

                repository.SaveChanges();

                return true;
            }
            return false;
        }


        public void Attach(IPaymentObserver observer)
        {
            Observers.Add(observer);
        }


        public void Detach(IPaymentObserver observer)
        {
            Observers.Remove(observer);
        }


        public void Notify(PaymentModel paymentModel)
        {
            foreach (var observer in Observers)
            {
                observer.UpdateMessage(paymentModel);
            }
        }

    }
}
