using GymMangement.DAL.Repositories.Classes;
using GymMangement.DAL.Repositories.Interfaces;
using GymMangement.DAL.Data.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GymMangement.DAL.Data.Models;

namespace GymMangement.Controllers
{
    public class PlansController : Controller
    {

        //private readonly IPlanRepository planRepository= new GymDbContext() ;
        //private readonly IPlanRepository planRepository=  new PlanRepository() ;
        private readonly IGenericRepository<Plan> _planRepository ;
        public PlansController( IGenericRepository<Plan> repository)
        {
            _planRepository = repository;
        }



        // Index Action 
        // GET/BaseURL/Plans/Index 
        public async Task<IActionResult> Index( CancellationToken ct)
        {
            var Plans = await _planRepository.GetAllAsync( ct: ct);
            return View(Plans);
        }

        // Details Action
        // GET/BaseURL/Plans/Details/1
    
        public async Task<IActionResult> Details(int id , CancellationToken ct)
        {
            var Plan = await _planRepository.GetByIdAsync(id ,ct) ;

            if (Plan == null)
                return RedirectToAction(nameof(Index));
            else
                return View(Plan);
        }
    
    
    }
}
