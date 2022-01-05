using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Task1._1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DateTimeController : ControllerBase
    {
        [HttpGet("/date")]
        [HttpGet("/date/{format}")]
        public IActionResult GetDate(string format = "dd.MM.yyyy")
        {
            return Ok(DateTime.Now.ToString(format));
        }

        [HttpGet("/time")]
        [HttpGet("/time/{format}")]
        public IActionResult GetTime(string format = "HH:mm:ss")
        {
            return Ok(DateTime.Now.ToString(format));
        }
    }
}
