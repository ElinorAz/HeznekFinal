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
    public class MessageController : AbstractController
    {
        private readonly IMessageService _service;
        public MessageController(IMessageService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] MessageModel model)
        {
            await _service.SendMessage(model);
            return Ok();
        }
    }
}