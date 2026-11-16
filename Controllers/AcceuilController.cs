using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EpicerieAlda.Models;

namespace EpicerieAlda.Controllers
{
    public class AcceuilController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
