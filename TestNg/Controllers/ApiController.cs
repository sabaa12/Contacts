using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestNg.Controllers
{

    [ApiController]
    [Route("[Controller]")]
    
    public abstract class ApiController: ControllerBase
    {
    }
}
