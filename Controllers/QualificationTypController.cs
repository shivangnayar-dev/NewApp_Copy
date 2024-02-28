using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NewApp.Models;
using Newtonsoft.Json;

namespace NewApp.Controllers
{
    [Route("api/[controller]")]
    public class QualificationTypController : Controller
    {
        private readonly CandidateDbContext _context;

        public QualificationTypController(CandidateDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public ActionResult<IEnumerable<QualificationTypes>> GetQualificationTypes()
        {
            try
            {
                // Retrieve qualification options from the database
                var qualificationOptions = _context.QualificationTypes
		    .OrderBy(qt => qt.SortOrder)
                    .Select(qt => qt.Name) // Assuming Name is the property you want to send to the client
                    .ToList();

                // Log the retrieved data in the console as JSON
                Console.WriteLine($"Retrieved Qualification Options: {JsonConvert.SerializeObject(qualificationOptions)}");

                return Ok(qualificationOptions);
            }
            catch (Exception ex)
            {
                // Handle exceptions, log errors, etc.
                Console.WriteLine($"Error retrieving qualification options: {ex.Message}");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        [HttpGet("GetIdByName")]
        public ActionResult<Guid?> GetIdByName(string name)
        {
            try
            {
                // Find the qualification by name and return its ID
                var qualification = _context.QualificationTypes
                    .FirstOrDefault(qt => qt.Name == name);

                if (qualification != null)
                {
                    return Ok(qualification.Id);
                }

                return NotFound($"Qualification with name '{name}' not found.");
            }
            catch (Exception ex)
            {
                // Handle exceptions, log errors, etc.
                Console.WriteLine($"Error retrieving qualification ID by name: {ex.Message}");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }

}
