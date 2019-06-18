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
    public class AccountsController : AbstractController
    {
        private readonly IAccountsService _accountsService;
        public AccountsController(IAccountsService accountsService)
        {
            _accountsService = accountsService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody]RegisterModel model)
        {

            var link = Url.Action(nameof(Confirm), "Accounts", null, Request.Scheme, Request.Host.Value);
            return await _accountsService.Register(model, link) ? Ok() : (IActionResult)BadRequest("Account with this ID or Email already exist");
        }

        [HttpGet("confirm/{code}")]
        [AllowAnonymous]
        public IActionResult Confirm(string code)
        {

            return _accountsService.Confirm(code) ?
                Redirect(Url.Content("~/"))
                : (IActionResult)BadRequest("Account not exist or already confirmed");
        }

        [HttpGet("candidates")]
        [Authorize(Roles = "Admin,SemiAdmin")]
        [ProducesResponseType(typeof(List<UserExtendedModel>), 200)]
        public IActionResult GetCandidates()
        {
            var response = _accountsService.GetCandidates();
            return Ok(response);
        }
        [HttpPost("export")]
        [Authorize(Roles = "Admin,SemiAdmin")]
        public FileResult ExportCandidates([FromBody] string[] ids)
        {
            var csv = _accountsService.ExportCSV(ids);
            return File(csv, "application/octet-stream", "List.csv");
        }

        [HttpGet("Students")]
        [Authorize(Roles = "Admin,SemiAdmin")]
        [ProducesResponseType(typeof(List<UserExtendedModel>), 200)]
        public IActionResult GetStudents()
        {
            var response = _accountsService.GetStudents();
            return Ok(response);
        }

        [HttpPut("Permission")]
        [Authorize(Roles = "Admin,SemiAdmin")]
        public async Task<IActionResult> GrandPermission([FromBody] UserModel model)
        {
            await _accountsService.GrandPermission(model);
            return Ok();
        }

    }
}