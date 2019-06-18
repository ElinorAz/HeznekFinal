using Heznek.Services.Models;
using Heznek.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Heznek.API.Controllers
{
    public class StudentScholarshipController : AbstractController
    {
        private readonly IStudentScholarshipService _service;
        public StudentScholarshipController(IStudentScholarshipService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize(Roles = "Admin,SemiAdmin")]
        [ProducesResponseType(typeof(StudentScholarShipModel), 200)]
        public async Task<IActionResult> Post([FromBody]StudentScholarShipModel model)
        {
            return Ok(await _service.InsertAsync(model));
        }

        [HttpPut]
        [Authorize(Roles = "Admin,SemiAdmin")]
        [ProducesResponseType(typeof(StudentScholarShipModel), 200)]
        public async Task<IActionResult> Put([FromBody]StudentScholarShipModel model)
        {
            return Ok(await _service.UpdateAsync(model));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,SemiAdmin")]
        [ProducesResponseType(typeof(StudentScholarShipModel), 200)]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return Ok();
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(List<StudentScholarShipModel>), 200)]
        public IActionResult Get()
        {
            return Ok(_service.GetMy());
        }

        [HttpGet]
        [Authorize(Roles = "Admin,SemiAdmin")]
        [ProducesResponseType(typeof(List<StudentScholarShipModel>), 200)]
        public IActionResult GetList(string userId)
        {
            return Ok(_service.GetList(userId));
        }
    }
}
