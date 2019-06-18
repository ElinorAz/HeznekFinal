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
    public class EventsController : AbstractController
    {
        private readonly IEventService _service;
        public EventsController(IEventService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(List<EventModel>), 200)]
        public IActionResult Get()
        {
            return Ok(_service.GetByAdmin());
        }

        [HttpGet("my")]
        [Authorize]
        [ProducesResponseType(typeof(List<EventModel>), 200)]
        public IActionResult GetMyEvents()
        {
            
            return Ok(_service.GetMyEvents());
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(EventModel), 200)]
        public IActionResult Get(int id)
        {
            return Ok(_service.GetById(id));
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        [ProducesResponseType(typeof(EventModel), 200)]
        public async Task<IActionResult> Post([FromBody] EventModel model)
        {
            return Ok(await _service.Create(model));
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(EventModel), 200)]
        public async Task<IActionResult> Put([FromBody] EventModel model)
        {
            return Ok(await _service.Update(model));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return Ok();
        }

        [HttpPut("attend")]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] AttendEventModel model)
        {
            await _service.Attend(model);
            return Ok();
        }
    }
}