using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Heznek.Common.Enums;
using Heznek.Services;
using Heznek.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Heznek.API.Controllers
{
    [Authorize]
    public class CandidateFormController : AbstractController
    {
        private readonly IFormService _formService;
        public CandidateFormController(IFormService formService)
        {
            _formService = formService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(FormModel), 200)]
        public IActionResult Get()
        {
            return Ok(_formService.GetForm());
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,SemiAdmin")]
        [ProducesResponseType(typeof(FormModel), 200)]
        public IActionResult Get(string id)
        {
            return Ok(_formService.GetForm(id));
        }

        [HttpPut]
        [ProducesResponseType(typeof(FormParentsSalaryModel), 200)]
        public async Task<IActionResult> Put([FromForm] FormParentsSalaryModel model)
        {
            var response = await _formService.UpdateParentsSalary(model);
            return Ok(response);
        }

        [HttpPut("task")]
        [ProducesResponseType(typeof(FormTaskModel), 200)]
        public async Task<IActionResult> Put([FromForm] FormTaskModel model)
        {
            var response = await _formService.UpdateTask(model);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Post()
        {
            await _formService.SendForm();
            return Ok();
        }

        [HttpPost("status")]
        [Authorize(Roles = "Admin,SemiAdmin")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Post([FromBody] FormModel model)
        {
            await _formService.ChangeFormStatus(model.UserId, model.Status);
            return Ok();
        }
    }
}