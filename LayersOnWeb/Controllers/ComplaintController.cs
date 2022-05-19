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
    public class ComplaintController : ControllerBase
    {
        private readonly IComplaintService complaintService;
        public ComplaintController(IComplaintService complaintService)
        {
            this.complaintService = complaintService;
        }


        [HttpGet("GetAllComplaints")]
        [Authorize(Roles = "PoliceOfficer")]
        public IEnumerable<Complaint> Get()
        {
            var result = new List<Complaint>();
            foreach (var complaint in complaintService.GetAllComplaints())
            {
                result.Add(new Complaint { Id = complaint.Id, Fine = complaint.Fine, ComplaintMotive = complaint.ComplaintMotive});
            }
            return result;
        }


        [HttpPost("AddComplaint")]
        [Authorize(Roles = "Driver")]
        public String Post(Complaint complaint)
        {
            try
            {
                complaintService.AddComplaintModel(new ComplaintModel { Fine = complaint.Fine, ComplaintMotive = complaint.ComplaintMotive });
                return "Complaint added successfully!";
            }
            catch (Exception)
            {
                return "Something went wrong!";
            }
        }

        [HttpDelete("DeleteComplaint")]
        [Authorize(Roles = "PoliceOfficer")]
        public bool DeleteComplaintById(ComplaintModel complaint)
        {
            try
            {
                complaintService.DeleteComplaintModelById(complaint);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        [HttpPut("UpdateComplaint")]
        [Authorize(Roles = "PoliceOfficer, Driver")]
        public bool UpdateComplaint(ComplaintModel complaint)
        {
            try
            {
                complaintService.UpdateComplaintModel(complaint);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        [HttpGet("GetComplaintById")]
        [Authorize(Roles = "PoliceOfficer")]
        public Object GetComplaintById(Guid Id)
        {
            try
            {
                var data = complaintService.GetComplaintModelById(Id);
                if (data == null) return NotFound();
                return data;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
    }
}
