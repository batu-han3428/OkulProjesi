using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class DashbordController : Controller
    {
        public DashbordController()
        {

        }

        public IActionResult dashbord()
        {
            return View();
        }
    }
}
