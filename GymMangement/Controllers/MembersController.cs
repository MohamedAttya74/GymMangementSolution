using GymMangement.BLL.ViewModels.MemberViewModels;
using GymMangement.DAL.Data.Models;
using GymMangement.DAL.Repositories.Interfaces;
using GymMangement.PL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymMangement.PL.Controllers
{
    public class MembersController : Controller
    {
        private readonly IMemberService _memberService;

        public MembersController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        // GET//BaseURL/Members/Index
        // Index - List all members
        public async Task<IActionResult> Index( CancellationToken ct)
        {
            var Members = await _memberService.GetAllMembersAsync(ct);
            return View(Members);
        }

        // GET/BaseURL/Members/MemberDetails/{id}
        // MemberDetails - Show one member's details 
        public async Task<IActionResult> MemberDetails(int id, CancellationToken ct)
        {
             var member = await _memberService.GetMemberDetailsByIdAsync(id, ct);

            if (member == null)
            {
                TempData["ErrorMessage"] = "Member not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(member);

        }

        // GET/BaseURL/Members/HealthRecordDetails/{id}
        // HealthRecordDetails - Shows health record member's details
        //public IActionResult HealthRecordDetails(int id, CancellationToken ct)
        //{
        //    // Get Health Record by id
        //    // Check if Health Record is null => Return Index with error message
        //    // If Health Record is not null => Return HealthRecordDetails view with health record details
        //}



        #region Create Member 

        // GET/BaseURL/Members/Create
        // Create -  Show empty form
        [HttpGet]
        public IActionResult Create() =>  View();

        // POST/BaseURL/Members/CreateMember {Member}
        // CreateMember -  submitted form
        [HttpPost]
        public async Task<IActionResult> CreateMember(CreateMemberViewModel model, CancellationToken ct)
        {
           if(!ModelState.IsValid) return View(nameof(Create) , model);
          var Result = await  _memberService.CreateMemberAsync(model,ct);
          if(Result)
                TempData["SuccessMessage"] = "Member created successfully.";
            else
                TempData["ErrorMessage"] = "Failed to create member. Email or Phone already exists.";
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Edit Member 

        // GET/BaseURL/Members/Edit/{id}
        // Edit -   Displays edit form

        // POST/BaseURL/Members/EditMember {Member}
        // EditMember -  submitted form




        #endregion

        #region Delete Member 

        // GET/BaseURL/Members/Delete/{id}
        // Delete -   Shows deletion confirmation page 

        // POST/BaseURL/Members/DeleteConfirmed {Member}
        // DeleteConfirmed -  submitted form




        #endregion
    }
}
