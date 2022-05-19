using BusinessLayer.Contracts;
using BusinessLayer.FactoryDP;
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
    public class FineController : ControllerBase
    {
        private readonly IFineService fineService;
        public FineController(IFineService fineService)
        {
            this.fineService = fineService;
        }


        [HttpGet("GetAllFines")]
        [Authorize(Roles = "PoliceOfficer, PostOfficeEmployee")]
        public IEnumerable<Fine> Get()
        {
            var result = new List<Fine>();
            foreach (var fine in fineService.GetAllFines())
            {
                result.Add(new Fine { Id = fine.Id, DeedCommitted = fine.DeedCommitted, PaymentAmount = fine.PaymentAmount, Driver = fine.Driver, isPaid = fine.isPaid });
            }
            return result;
        }


        [HttpPost("AddFine")]
        [Authorize(Roles = "PoliceOfficer")]
        public String Post(Fine fine)
        {
            try
            {
                fineService.AddFineModel(new FineModel { DeedCommitted = fine.DeedCommitted, PaymentAmount = fine.PaymentAmount, Driver = fine.Driver });
                return "Fine added successfully!";
            }
            catch (Exception)
            {
                return "Something went wrong! Check if Driver exits in database; if not, add Driver first!";
            }
        }


        [HttpDelete("DeleteFine")]
        [Authorize(Roles = "PoliceOfficer")]
        public bool DeleteFineById(FineModel fine)
        {
            try
            {
                fineService.DeleteFineModelById(fine);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        [HttpPut("UpdateFine")]
        [Authorize(Roles = "PoliceOfficer")]
        public bool UpdateFine(FineModel fine)
        {
            try
            {
                fineService.UpdateFineModel(fine);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        [HttpGet("GetFineById")]
        [Authorize(Roles = "PoliceOfficer, PostOfficeEmployee")]
        public Object GetFineById(Guid Id)
        {
            try
            {
                var data = fineService.GetFineModelById(Id);
                if (data == null) return NotFound();
                return data;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }


        [HttpGet("ViewAllFinesOfADriver")]
        [Authorize(Roles = "PoliceOfficer, PostOfficeEmployee")]
        public IEnumerable<Fine> ViewAllFinesOfADriver(Guid driverId)
        {
            var result = new List<Fine>();
            foreach (var fine in fineService.GetAllFinesOfADriver(driverId))
            {
                if (fine.Driver.Id == driverId)
                {
                    result.Add(new Fine { Id = fine.Id, DeedCommitted = fine.DeedCommitted, PaymentAmount = fine.PaymentAmount, Driver = fine.Driver, isPaid = fine.isPaid });
                }
            }
            return result;
        }


        [HttpGet("CreateReportCSV")]
        [Authorize(Roles = "PoliceOfficer, PostOfficeEmployee")]
        public String ExportDataToCSV()
        {
            try
            {
                Creator creator = new ConcreteCreator(fineService);
                creator.createWriter("CSV");
                return "Report created successfully!";
            }
            catch (Exception e)
            {
                return "Something went wrong!";
            }
        }


        [HttpGet("CreateReportXML")]
        [Authorize(Roles = "PoliceOfficer, PostOfficeEmployee")]
        public String ExportDataToXML()
        {
            try
            {
                Creator creator = new ConcreteCreator(fineService);
                creator.createWriter("XML");
                return "Report created successfully!";
            }
            catch (Exception e)
            {
                return "Something went wrong!";
            }
        }
    }
}
