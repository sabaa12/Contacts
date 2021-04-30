using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace TestNg.Controllers
{
    public class HomeController : ApiController
    { 
        [Authorize]
        [HttpGet]
        public ActionResult<string> get()
        {
            return "Home controller";
        }

    }
}
