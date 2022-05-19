using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Contracts
{
    public interface IDriverService
    {
        List<DriverModel> GetAllDrivers();
        void AddDriverModel(DriverModel driver);
        bool DeleteDriverModelById(Guid driverId);
        bool UpdateDriverModel(DriverModel driver);
        DriverModel GetDriverModelById(Guid Id);
    }
}
