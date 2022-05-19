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
    public class DriverService : IDriverService
    {
        private readonly IRepository repository;
        public DriverService(IRepository repository)
        {
            this.repository = repository;
        }


        public void AddDriverModel(DriverModel driver)
        {
            repository.Add(new DriverEntity { Name = driver.Name, BirthDate = driver.BirthDate, Address = driver.Address, DrivingLicenseSeries = driver.DrivingLicenseSeries });
            repository.SaveChanges();
        }
      

        public List<DriverModel> GetAllDrivers()
        {
            List<DriverModel> result = new List<DriverModel>();
            foreach (var driver in repository.GetAll<DriverEntity>())
            {
                result.Add(new DriverModel { Id = driver.Id, Name = driver.Name, BirthDate = driver.BirthDate, Address = driver.Address, DrivingLicenseSeries = driver.DrivingLicenseSeries });
            }
            return result;
        }


        public bool DeleteDriverModelById(Guid driverId)
        {
            try
            {
                var result = repository.GetAll<DriverEntity>().Where(x => x.Id == driverId).ToList();
                foreach (var driver in result)
                {
                    repository.Delete(driver);
                    repository.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public DriverModel GetDriverModelById(Guid Id)
        {
            var driver = repository.GetById<DriverEntity>(Id);
            return new DriverModel { Id = driver.Id, Name = driver.Name, BirthDate = driver.BirthDate, Address = driver.Address, DrivingLicenseSeries = driver.DrivingLicenseSeries };

        }


        public bool UpdateDriverModel(DriverModel driver)
        {
            try
            {
                var item = repository.GetAll<DriverEntity>().Where(x => x.Id == driver.Id).FirstOrDefault();
                item.Name = driver.Name;
                item.BirthDate = driver.BirthDate;
                item.Address = driver.Address;
                item.DrivingLicenseSeries = driver.DrivingLicenseSeries;
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
