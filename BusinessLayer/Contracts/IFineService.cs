using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Contracts
{
    public interface IFineService
    {
        List<FineModel> GetAllFines();
        bool AddFineModel(FineModel fine);
        bool DeleteFineModelById(FineModel fine);
        bool UpdateFineModel(FineModel fine);
        FineModel GetFineModelById(Guid Id);
        List<FineModel> GetAllFinesOfADriver(Guid driverId);

    }
}
