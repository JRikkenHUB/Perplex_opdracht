using Microsoft.AspNetCore.Mvc;

public class SuggestionSuccessController : Controller
{
    [HttpGet("success")]
    public IActionResult Success()
    {
        ViewBag.Message = "Je idee is succesvol ontvangen!";
        return View();
    }
}