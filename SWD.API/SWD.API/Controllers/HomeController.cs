using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SWD.API.Controllers
{
    /// <summary>
    /// Контроллер по умолчанию
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary/>
        public IActionResult Index()
        {
            return new RedirectResult("~/help");
        }
    }
}
