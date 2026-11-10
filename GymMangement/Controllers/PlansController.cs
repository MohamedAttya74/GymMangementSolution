using GymMangement.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymMangement.Controllers
{
    public class PlansController : Controller
    {
        private readonly GymDbContext dbContext;

        public PlansController()
        {
            dbContext = new GymDbContext();
        }

        // Index Action 
        // GET/BaseURL/Plans/Index

        public async Task<IActionResult> Index()
        {
            var Plans = await dbContext.Plans.ToListAsync();
            return View(Plans);
        }

        // Details Action
        // GET/BaseURL/Plans/Details/1
    
        public async Task<IActionResult> Details(int id )
        {
            var Plan = await dbContext.Plans.FindAsync(id);
            if (Plan == null)
            {
                return RedirectToAction(nameof(Index));
            }
            else
                return View(Plan);
        }
    
    
    }
}
