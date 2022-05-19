using BusinessLayer.Contracts;
using DataAccess;
using DataAccess.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class ComplaintService : IComplaintService
    {
        private readonly IRepository repository;
        public ComplaintService(IRepository repository)
        {
            this.repository = repository;
        }


        public void AddComplaintModel(ComplaintModel complaint)
        {
            FineEntity fineEntity = repository.GetById<FineEntity>(complaint.Fine.Id);
            repository.Add(new ComplaintEntity { Fine = fineEntity, ComplaintMotive = complaint.ComplaintMotive });
            repository.SaveChanges();
        }


        public bool DeleteComplaintModelById(ComplaintModel complaint)
        {
            try
            {
                var list = repository.GetAll<ComplaintEntity>().Where(x => x.Id == complaint.Id).ToList();
                foreach (var item in list)
                {
                    repository.Delete<ComplaintEntity>(item);
                    repository.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public List<ComplaintModel> GetAllComplaints()
        {
            List<ComplaintModel> result = new List<ComplaintModel>();
            foreach(var complaint in repository.GetAll<ComplaintEntity>())
            {
                var fine = repository.GetAll<FineEntity>().Where(x => x.Id == complaint.Fine.Id).FirstOrDefault();
                var driver = repository.GetAll<DriverEntity>().Where(x => x.Id == fine.Driver.Id).FirstOrDefault();
                DriverModel driverModel = new DriverModel { Id = driver.Id, Name = driver.Name, BirthDate = driver.BirthDate, Address = driver.Address, DrivingLicenseSeries = driver.DrivingLicenseSeries };
                FineModel fineModel = new FineModel { Id = fine.Id, DeedCommitted = fine.DeedCommitted, PaymentAmount = fine.PaymentAmount, Driver = driverModel, isPaid = fine.isPaid };
                result.Add(new ComplaintModel { Id = complaint.Id, Fine = fineModel, ComplaintMotive = complaint.ComplaintMotive });
            }
            return result;
        }


        public ComplaintModel GetComplaintModelById(Guid Id)
        {
            var complaint = repository.GetById<ComplaintEntity>(Id);
            var fine = repository.GetAll<FineEntity>().Where(x => x.Id == complaint.Fine.Id).FirstOrDefault();
            var driver = repository.GetAll<DriverEntity>().Where(x => x.Id == fine.Driver.Id).FirstOrDefault();
            DriverModel driverModel = new DriverModel { Id = driver.Id, Name = driver.Name, BirthDate = driver.BirthDate, Address = driver.Address, DrivingLicenseSeries = driver.DrivingLicenseSeries };
            FineModel fineModel = new FineModel { Id = fine.Id, DeedCommitted = fine.DeedCommitted, PaymentAmount = fine.PaymentAmount, Driver = driverModel, isPaid = fine.isPaid };
            return new ComplaintModel { Id = complaint.Id, Fine = fineModel, ComplaintMotive = complaint.ComplaintMotive };
        }


        public bool UpdateComplaintModel(ComplaintModel complaint)
        {
            try
            {
                var item = repository.GetAll<ComplaintEntity>().Where(x => x.Id == complaint.Id).FirstOrDefault();
                item.ComplaintMotive = complaint.ComplaintMotive;
                repository.Update(item);
                repository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
