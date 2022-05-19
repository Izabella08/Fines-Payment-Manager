using BusinessLayer.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LayersOnWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DriverController : ControllerBase
    {
        private readonly IDriverService driverService;
        public DriverController(IDriverService driverService)
        {
            this.driverService = driverService;
        }


        [HttpGet("GetAllDrivers")]
        [Authorize(Roles = "PoliceOfficer, PostOfficeEmployee")]
        public IEnumerable<Driver> Get()
        {
            var result = new List<Driver>();
            foreach (var driver in driverService.GetAllDrivers())
            {
                result.Add(new Driver { Id = driver.Id, Name = driver.Name, BirthDate = driver.BirthDate, Address = driver.Address, DrivingLicenseSeries = driver.DrivingLicenseSeries });
            }
            return result;
        }


        [HttpPost("AddDriver")]
        [Authorize(Roles = "PoliceOfficer")]
        //[Auth]
        public void Post(Driver driver)
        {
            driverService.AddDriverModel(new DriverModel { Name = driver.Name, BirthDate = driver.BirthDate, Address = driver.Address, DrivingLicenseSeries = driver.DrivingLicenseSeries });
        }


        [HttpDelete("DeleteDriverById")]
        [Authorize(Roles = "PoliceOfficer")]
        public bool DeleteDriverById(Guid driverId)
        {
            try
            {
                driverService.DeleteDriverModelById(driverId);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        [HttpGet("GetDriverById")]
        [Authorize(Roles = "PoliceOfficer, PostOfficeEmployee")]
        public Object GetDriverById(Guid Id)
        {
            try
            {
                var data = driverService.GetDriverModelById(Id);
                if (data == null) return NotFound();
                return data;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }


        [HttpPut("UpdateDriver")]
        [Authorize(Roles = "PoliceOfficer")]
        public bool UpdateDriver(DriverModel driver)
        {
            try
            {
                driverService.UpdateDriverModel(driver);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
