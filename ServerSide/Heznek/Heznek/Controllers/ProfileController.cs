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
    
    public class ProfileController : AbstractController
    {
        private readonly IProfileService _profileService;
        private readonly IFormService _formService;
        public ProfileController(IProfileService profileService, IFormService formService)
        {
            _profileService = profileService;
            _formService = formService;
        }

        [HttpPut]
        [Authorize]
        [ProducesResponseType(typeof(ProfileModel), 200)]
        public async Task<IActionResult> Put([FromForm] ProfileModel model)
        {
            var response = await _profileService.Update(model);
            return Ok(response);
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(ProfileModel), 200)]
        public IActionResult Get()
        {
            var response = _profileService.Get();
            return Ok(response);
        }

        [HttpPut("admin")]
        [Authorize(Roles = "Admin,SemiAdmin")]
        [ProducesResponseType(typeof(ProfileModel), 200)]
        public async Task<IActionResult> AdminPut([FromForm] ProfileModel model)
        {
            var response = await _profileService.AdminUpdate(model);
            return Ok(response);
        }

        [HttpPost("admin")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ProfileModel), 200)]
        public async Task<IActionResult> AdminPost([FromForm] ProfileModel model)
        {
            var response = await _profileService.Create(model);
            return Ok(response);
        }

        [HttpGet("SystemDetails")]
        [Authorize(Roles = "Admin,SemiAdmin")]
        [ProducesResponseType(typeof(SystemDetailsModel), 200)]
        public IActionResult GetSystemDepails()
        {
            var response = _profileService.GetSystemDetails();
            return Ok(response);
        }

        [HttpPut("SystemDetails")]
        [Authorize(Roles = "Admin,SemiAdmin")]
        [ProducesResponseType(typeof(SystemDetailsModel), 200)]
        public IActionResult PutSystemDetails([FromBody] SystemDetailsModel model)
        {
            var response = _profileService.UpdateSystemDetails(model);
            return Ok(response);
        }

        [HttpGet("{userId}")]
        [Authorize(Roles = "Admin,SemiAdmin")]
        [ProducesResponseType(typeof(ProfileModel), 200)]
        public IActionResult AdminGet(string userId)
        {
            var response = _profileService.Get(userId);
            response.CandidateForm = _formService.GetForm(userId);
            return Ok(response);
        }

        [HttpDelete("{userId}")]
        [Authorize(Roles = "Admin,SemiAdmin")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> AdminDelete(string userId)
        {
            await _profileService.Delete(userId);
            return Ok();
        }
    }
}