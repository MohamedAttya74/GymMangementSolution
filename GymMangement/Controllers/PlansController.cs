using GymMangement.DAL.Repositories.Classes;
using GymMangement.DAL.Repositories.Interfaces;
using GymMangement.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymMangement.Controllers
{
    public class PlansController : Controller
    {
        private readonly IPlanRepository planRepository = new PlanRepository(); 

        // Index Action 
        // GET/BaseURL/Plans/Index 
        public async Task<IActionResult> Index( CancellationToken ct)
        {
            var Plans = await planRepository.GetAllAsync( ct: ct);
            return View(Plans);
        }

        // Details Action
        // GET/BaseURL/Plans/Details/1
    
        public async Task<IActionResult> Details(int id , CancellationToken ct)
        {
            var Plan = await planRepository.GetByIdAsync(id ,ct) ;

            if (Plan == null)
                return RedirectToAction(nameof(Index));
            else
                return View(Plan);
        }
    
    
    }
}
