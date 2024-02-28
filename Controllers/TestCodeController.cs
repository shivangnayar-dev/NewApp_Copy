using Microsoft.AspNetCore.Mvc;
using NewApp.Models;
using System.Linq;

namespace NewApp.Controllers
{
    [Route("api/[controller]")]
    public class TestCodeController : Controller
    {
        private readonly CandidateDbContext _context;

        public TestCodeController(CandidateDbContext context)
        {
            _context = context;
        }

        // POST api/TestCode/CheckTestCodeValidity
        [HttpPost]
        [Route("CheckTestCodeValidity")]
        public IActionResult CheckTestCodeValidity([FromBody] TestCode testCode)
        {
            bool isValid = CheckTestCodeValidityInDatabase(testCode.Code);
            string? reportId = GetReportIdByCode(testCode.Code);

            return Json(new { isValid, ReportId = reportId });
        }

        // Check if the test code is valid
        private bool CheckTestCodeValidityInDatabase(string testCode)
        {
            return _context.TestCodes.Any(tc => tc.Code == testCode);
        }

        // Get the reportId for the given code
        private string? GetReportIdByCode(string code)
        {
            var testCode = _context.TestCodes.FirstOrDefault(tc => tc.Code == code);

            if (testCode != null)
            {
                return testCode.ReportId.ToString();
            }

            return null;
        }
    }
}
