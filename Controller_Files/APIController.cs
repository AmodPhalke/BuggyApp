using Microsoft.AspNetCore.Mvc;

namespace BuggyApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetData()
        {
            string result = "SELECT * FROM INVOICEITEMS";
            if (!string.IsNullOrEmpty(result))
            {
                return Ok(new { message = "Data fetched", data = result });
            }
            return BadRequest("No data");
        }
    }
}
