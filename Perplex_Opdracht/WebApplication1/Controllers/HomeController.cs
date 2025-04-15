using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var suggestions = await _context.Ideas
             .Include(i => i.Categories)
             .Include(i => i.Comments)
             .OrderByDescending(i => i.CreatedAt)
             .Take(5) // Show 5 most recent ideas
             .Select(i => new SuggestionViewModel
             {
                 Id = i.Id,
                 Onderwerp = i.Onderwerp,
                 Beschrijving = i.Beschrijving,
                 UserName = i.UserName,
                 CreatedAt = i.CreatedAt,
                 Categories = i.Categories.Select(c => c.Name).ToList(),
                 CommentCount = i.Comments.Count
             })
             .ToListAsync();

            return View(suggestions);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
