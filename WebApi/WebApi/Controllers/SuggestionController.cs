using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Models.Dtos;

namespace WebApi.Controllers
{
    [Route("api/[championanalyzer]")]
    public class SuggestionController : Controller
    {
        // TODO use suggestion service

        [HttpGet]
        public IActionResult GetSuggestions([FromBody] SuggestionRequestDto request)
        {
            var response = new SuggestionResponseDto();

            // TODO fill response base on request

            return new ObjectResult(response);
        }
    }
}
