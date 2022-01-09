using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task2._1.Services;

namespace Task2._1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {        
        FileService FileService { get; }

        public FileController()
        {
            FileService = new FileService();
        }

        [HttpGet]
        public IActionResult GetAllText()
        {
            var text = FileService.GetAllText();
            if (string.IsNullOrEmpty(text)) return NotFound();
            else return Ok(text);
        }

        [HttpGet("{row}")]
        public IActionResult GetLine(int row)
        {
            try
            {
                return Ok(FileService.GetTextFromLine(row));
            }
            catch(ArgumentOutOfRangeException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("range")]
        public IActionResult GetRange(int start, int end)
        {
            try
            {
                return Ok(FileService.GetTextRange(start, end));
            }
            catch(ArgumentOutOfRangeException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [HttpPost("{force}")]
        public IActionResult PostLine([FromBody] string inputText, string force = null)
        {
            var boolForce = false;
            if (force != null) boolForce = true;

            var response = FileService.PostLine(inputText, boolForce);
            if (response == 0)
            {
                return Ok();
            }
            else
            {
                return StatusCode(422, $"Данный текст уже сохранен в строке {response}");
            }
        }

        [HttpPut("{row}")]
        public IActionResult PutLine([FromBody] string inputText, int row)
        {
            FileService.PutLine(inputText, row);
            return Ok();
        }

        [HttpDelete("{row}")]
        public IActionResult DeleteLine(int row)
        {
            FileService.DeleteLine(row);
            return Ok();
        }
    }
}
