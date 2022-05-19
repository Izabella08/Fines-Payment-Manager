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
    public class FineService : IFineService
    {
        private readonly IRepository repository;
        public FineService(IRepository repository)
        {
            this.repository = repository;
        }


        public bool AddFineModel(FineModel fine)
        {
            DriverEntity driver = repository.GetById<DriverEntity>(fine.Driver.Id);
            if(driver.Id == null)
            {
                return false;
            }
            else
            {
                repository.Add(new FineEntity { DeedCommitted = fine.DeedCommitted, PaymentAmount = fine.PaymentAmount, Driver = driver });
                repository.SaveChanges();
                return true;
            }

        }


        public bool DeleteFineModelById(FineModel fine)
        {
            try
            {
                var list = repository.GetAll<FineEntity>().Where(x => x.Id == fine.Id).ToList();
                foreach (var item in list)
                {
                    repository.Delete<FineEntity>(item);
                    repository.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public List<FineModel> GetAllFines()
        {
            List<FineModel> result = new List<FineModel>();
            foreach(var fine in repository.GetAll<FineEntity>())
            {
                var driver = repository.GetAll<DriverEntity>().Where(x => x.Id == fine.Driver.Id).FirstOrDefault();
                DriverModel driverModel = new DriverModel { Id = driver.Id, Name = driver.Name, BirthDate = driver.BirthDate, Address = driver.Address, DrivingLicenseSeries = driver.DrivingLicenseSeries };
                result.Add(new FineModel { Id = fine.Id, DeedCommitted = fine.DeedCommitted, PaymentAmount = fine.PaymentAmount, Driver = driverModel, isPaid = fine.isPaid});   
            }
            return result;
        }


        public FineModel GetFineModelById(Guid Id)
        {
            var fine = repository.GetById<FineEntity>(Id);
            var driver = repository.GetAll<DriverEntity>().Where(x => x.Id == fine.Driver.Id).FirstOrDefault();
            DriverModel driverModel = new DriverModel { Id = driver.Id, Name = driver.Name, BirthDate = driver.BirthDate, Address = driver.Address, DrivingLicenseSeries = driver.DrivingLicenseSeries };
            return new FineModel { Id = fine.Id, DeedCommitted = fine.DeedCommitted, PaymentAmount = fine.PaymentAmount, Driver = driverModel, isPaid = fine.isPaid };
        }


        public bool UpdateFineModel(FineModel fine)
        {
            try
            {
                var item = repository.GetAll<FineEntity>().Where(x => x.Id == fine.Id).FirstOrDefault();
                item.DeedCommitted = fine.DeedCommitted;
                item.PaymentAmount = fine.PaymentAmount;
                repository.Update(item);
                repository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public List<FineModel> GetAllFinesOfADriver(Guid driverId)
        {
            List<FineModel> result = new List<FineModel>();
            var driver = repository.GetAll<DriverEntity>().Where(x => x.Id == driverId).FirstOrDefault();
            foreach (var fine in repository.GetAll<FineEntity>())
            {
                DriverModel driverModel = new DriverModel { Id = driver.Id, Name = driver.Name, BirthDate = driver.BirthDate, Address = driver.Address, DrivingLicenseSeries = driver.DrivingLicenseSeries };
                if (fine.Driver.Id == driverId)
                {
                    result.Add(new FineModel { Id = fine.Id, DeedCommitted = fine.DeedCommitted, PaymentAmount = fine.PaymentAmount, Driver = driverModel, isPaid = fine.isPaid });
                }
            }
            return result;
        }
    }
}
