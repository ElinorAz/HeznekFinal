using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace Heznek.API.Controllers
{
    public class HomeController : Controller
    {
        public IHostingEnvironment _env { get; }
        public HomeController(IHostingEnvironment env)
        {
            _env = env;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return new PhysicalFileResult(Path.Combine(_env.WebRootPath, "index.html"), new MediaTypeHeaderValue("text/html"));
        }
    }
}