using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NewApp.Models;
using Newtonsoft.Json;
using System.Linq;

namespace NewApp.Controllers
{
    [Route("api/[controller]")]
    public class SpecializationController : Controller
    {
        private readonly CandidateDbContext _context;

        public SpecializationController(CandidateDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> GetSpecializations()
        {
            try
            {
                // Retrieve specialization options from the database
                var specializationOptions = _context.Specialization
                    .Select(s => s.Specialisation) // Assuming Specialisation is the property you want to send to the client
                    .ToList();

                // Log the retrieved data in the console as JSON
                Console.WriteLine($"Retrieved Specialization Options: {JsonConvert.SerializeObject(specializationOptions)}");

                return Ok(specializationOptions);
            }
            catch (Exception ex)
            {
                // Handle exceptions, log errors, etc.
                Console.WriteLine($"Error retrieving specialization options: {ex.Message}");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("GetIdByspecializationName")]
        public ActionResult<Guid?> GetIdByName(string name)
        {
            try
            {
                // Find the specialization by name and return its CoreStream ID
                var specialization = _context.Specialization
                    .FirstOrDefault(qt => qt.Specialisation == name);

                if (specialization != null)
                {
                    return Ok(specialization.CoreStreamID);
                }

                return NotFound($"Specialization with name '{name}' not found.");
            }
            catch (Exception ex)
            {
                // Handle exceptions, log errors, etc.
                Console.WriteLine($"Error retrieving specialization ID by name: {ex.Message}");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("GetSpecializationsByCoreStreamId")]
        public ActionResult<IEnumerable<string>> GetSpecializationsByCoreStreamId(Guid coreStreamId)
        {
            try
            {
                // Retrieve specialization options for the given CoreStream ID from the database
                var specializationOptions = _context.Specialization
                    .Where(s => s.CoreStreamID == coreStreamId)
                    .Select(s => s.Specialisation)
                    .ToList();

                // Log the retrieved data in the console as JSON
                Console.WriteLine($"Retrieved Specialization Options for CoreStream ID {coreStreamId}: {JsonConvert.SerializeObject(specializationOptions)}");

                return Ok(specializationOptions);
            }
            catch (Exception ex)
            {
                // Handle exceptions, log errors, etc.
                Console.WriteLine($"Error retrieving specialization options by CoreStream ID: {ex.Message}");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        // Add more actions as needed
    }
}
