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
    public class StatisticController : AbstractController
    {
        private readonly IStatisticService _service;
        public StatisticController(IStatisticService service)
        {
            _service = service;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(List<StatisticModel>), 200)]
        public IActionResult Get()
        {
            var response = _service.Get();
            return Ok(response);
        }
    }
}