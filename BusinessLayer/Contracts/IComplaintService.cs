using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Contracts
{
    public interface IComplaintService
    {
        List<ComplaintModel> GetAllComplaints();
        void AddComplaintModel(ComplaintModel complaint);
        bool DeleteComplaintModelById(ComplaintModel complaint);
        bool UpdateComplaintModel(ComplaintModel complaint);
        ComplaintModel GetComplaintModelById(Guid Id);
    }
}
