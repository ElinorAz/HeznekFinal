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
    public class ScholarshipController : AbstractController
    {
        private readonly IScholarshipService _service;
        public ScholarshipController(IScholarshipService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,SemiAdmin")]
        [ProducesResponseType(typeof(List<ScholarshipModel>), 200)]
        public IActionResult Get()
        {
            return Ok(_service.Get());
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,SemiAdmin")]
        [ProducesResponseType(typeof(ScholarshipModel), 200)]
        public IActionResult Get(int id)
        {
            return Ok(_service.Get(id));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,SemiAdmin")]
        [ProducesResponseType(typeof(ScholarshipModel), 200)]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,SemiAdmin")]
        [ProducesResponseType(typeof(ScholarshipModel), 200)]
        public async Task<IActionResult> Post([FromBody]ScholarshipModel model)
        {
            return Ok(await _service.InsertAsync(model));
        }

        [HttpPut]
        [Authorize(Roles = "Admin,SemiAdmin")]
        [ProducesResponseType(typeof(ScholarshipModel), 200)]
        public async Task<IActionResult> Put([FromBody]ScholarshipModel model)
        {
            return Ok(await _service.UpdateAsync(model));
        }

        [HttpGet("students/{ScholarshipId}")]
        [Authorize(Roles = "Admin,SemiAdmin")]
        [ProducesResponseType(typeof(ScholarshipModel), 200)]
        public IActionResult GetByScholarship(int ScholarshipId)
        {
            return Ok(_service.StudentByScholarship(ScholarshipId));
        }

    }
}