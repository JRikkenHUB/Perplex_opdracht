using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class SuggestionSuccesController : Controller
    {
        [HttpGet("success")]
        public IActionResult Success()
        {
            return View();
        }
    }
}
