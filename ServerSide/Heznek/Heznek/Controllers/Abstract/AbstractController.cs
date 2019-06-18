using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Heznek.Common.DomainTaskStatus;

namespace Heznek.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public abstract class AbstractController : Controller
    {

        public AbstractController()
        {
        }
    }
}