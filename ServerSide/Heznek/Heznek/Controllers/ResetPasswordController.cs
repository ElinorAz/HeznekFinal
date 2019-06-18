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
    public class ResetPasswordController : AbstractController
    {
        private readonly IForgotPaswordService _forgotPaswordService;
        public ResetPasswordController(IForgotPaswordService forgotPaswordService)
        {
            _forgotPaswordService = forgotPaswordService;

        }
        
        [HttpGet("Forgot/{email}")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var code = _forgotPaswordService.GenerateResendPaswordCode(email);
            if (code == null)
            {
                return BadRequest(new { Error = $"Account with email {email} not exist" });
            }
            try
            {
                await _forgotPaswordService.SendForgotPasswordEmail(email, code);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, new { Error = "Failed to send email" });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Reset([FromBody]ResetPaswordModel model)
        {
            var token = _forgotPaswordService.ResetPasword(model);
            return token != null ? Ok(token) : (IActionResult)BadRequest();
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> NewPassword([FromBody]ResetPaswordModel model)
        {
            var token = _forgotPaswordService.NewPassword(model);
            return token != null ? Ok(token) : (IActionResult)BadRequest();
        }
    }
}