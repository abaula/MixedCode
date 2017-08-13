using System;
using Microsoft.AspNetCore.Mvc;
using Templaqte.WebApi.Model;

namespace Templaqte.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class PingController : Controller
    {
        [HttpGet]
        public JsonResult Ping()
        {
            return Json(new Ping
            {
                DateTime = DateTime.Now,
                Value = "Hello from Ping Controller!"
            });
        }
    }
}
