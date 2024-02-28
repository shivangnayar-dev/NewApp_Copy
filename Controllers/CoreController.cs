using System;
using Microsoft.AspNetCore.Mvc;
using NewApp.Models;
using Newtonsoft.Json;

namespace NewApp.Controllers
{
    [Route("api/[controller]")]
    public class CoreController : Controller
    {
        private readonly CandidateDbContext _context;

        public CoreController(CandidateDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<QualificationTypes>> GetStreamName()
        {
            try
            {
                // Retrieve qualification options from the database
                var StreamNameOptions = _context.Core
                    .Select(qt => qt.StreamName) // Assuming Name is the property you want to send to the client
                    .ToList();

                // Log the retrieved data in the console as JSON
                Console.WriteLine($"Retrieved Qualification Options: {JsonConvert.SerializeObject(StreamNameOptions)}");

                return Ok(StreamNameOptions);
            }
            catch (Exception ex)
            {
                // Handle exceptions, log errors, etc.
                Console.WriteLine($"Error retrieving qualification options: {ex.Message}");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        [HttpGet("GetIdByStreamName")]
        public ActionResult<Guid?> GetIdByStreamName(string streamName)
        {
            try
            {
                // Retrieve the ID based on the stream name from the database
                var coreStream = _context.Core
                    .FirstOrDefault(qt => qt.StreamName == streamName);

                // If found, return the ID; otherwise, return null
                return Ok(coreStream?.Id);
            }
            catch (Exception ex)
            {
                // Handle exceptions, log errors, etc.
                Console.WriteLine($"Error retrieving core stream ID: {ex.Message}");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


    }
}



