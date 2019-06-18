using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Heznek.Services;
using Heznek.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Heznek.API.Controllers
{
    public class TelephonyController : AbstractController
    {
        public readonly ITelephonyService _service;

        public TelephonyController(ITelephonyService service)
        {
            _service = service;
        }

        [HttpGet("{name}")]
        [Authorize]
        [ProducesResponseType(typeof(List<TelephonyProfileModel>), 200)]
        public IActionResult Get(string name)
        {
            var response = _service.Get(name);
            return Ok(response);
        }

        [HttpPut]
        [Authorize]
        [ProducesResponseType(typeof(TelephonyProfileModel), 200)]
        public IActionResult Put([FromBody] TelephonyProfileModel model)
        {
            var response = _service.Update(model);
            return Ok(response);
        }

        [HttpGet("events")]
        [Authorize]
        [ProducesResponseType(typeof(List<TelephonyEventsModel>), 200)]
        public IActionResult MyEvents()
        {
            var response = _service.MyEvents();
            return Ok(response);
        }
    }
}