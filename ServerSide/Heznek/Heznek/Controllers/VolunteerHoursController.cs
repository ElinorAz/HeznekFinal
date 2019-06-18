using Heznek.Services;
using Heznek.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Heznek.API.Controllers
{
    [Authorize]
    public class VolunteerHoursController : AbstractController
    {
        private readonly IVolunteerHoursService _service;
        public VolunteerHoursController(IVolunteerHoursService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(SeparatedVolunteerHoursModel), 200)]
        public IActionResult Get()
        {
            var result = _service.GetMyHours();
            return Ok(result);
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(SeparatedVolunteerHoursModel), 200)]
        public IActionResult GetByProfile(string userId)
        {
            var result = _service.GetHours(userId);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(VolunteerHoursModel), 200)]
        public IActionResult Post([FromBody]VolunteerHoursModel model)
        {
            var result = _service.Insert(model);
            return Ok(result);
        }
    }
}
