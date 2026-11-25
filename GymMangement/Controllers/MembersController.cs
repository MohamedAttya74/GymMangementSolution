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
        public async Task<IActionResult> Index(CancellationToken ct)
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
        public async Task<IActionResult> HealthRecordDetails(int id, CancellationToken ct)
        {
            var Result = await _memberService.GetMemberHealthRecordAsync(id, ct);
            if (Result is null)
            {
                TempData["ErrorMessage"] = "Health record not found.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(Result);
            }
        }


        #region Create Member 

        // GET/BaseURL/Members/Create
        // Create -  Show empty form
        [HttpGet]
        public IActionResult Create() => View();

        // POST/BaseURL/Members/CreateMember {Member}
        // CreateMember -  submitted form
        [HttpPost]
        public async Task<IActionResult> CreateMember(CreateMemberViewModel model, CancellationToken ct)
        {
            if (!ModelState.IsValid) return View(nameof(Create), model);
            var Result = await _memberService.CreateMemberAsync(model, ct);
            if (Result)
                TempData["SuccessMessage"] = "Member created successfully.";
            else
                TempData["ErrorMessage"] = "Failed to create member. Email or Phone already exists.";
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Edit Member 

        // GET/BaseURL/Members/Edit/{id}
        // Edit -   Displays edit form
        [HttpGet]
        public async Task<IActionResult> EditMember(int id, CancellationToken ct)
        {
            var member = await _memberService.GetMemberToUpdateByIdAsync(id, ct);
            if (member is null)
            {
                TempData["ErrorMessage"] = "Member not found.";
                return RedirectToAction(nameof(Index));
            }
            ;
            return View(member);
        }


        // POST/BaseURL/Members/EditMember {Member}
        // EditMember -  submitted form
        [HttpPost]
        public async Task<IActionResult> EditMember([FromRoute] int id, MemberToUpdateViewModel model, CancellationToken ct)
        {
            if (!ModelState.IsValid) return View(model);

            var Result = await _memberService.UpdateMemberDetailsAsync(id, model, ct);
            if (Result)
                TempData["SuccessMessage"] = "Member Updated  Successfully";
            else
                TempData["ErrorMessage"] = "Failed To  Update Member   ";

            return RedirectToAction(nameof(Index));

        }



        #endregion

        #region Delete Member 

        // GET/BaseURL/Members/Delete/{id}
        // Delete -   Shows deletion confirmation page 
        [HttpGet]
        public async Task<IActionResult> Delete(int Id , CancellationToken ct)
        {
            var member = await _memberService.GetMemberDetailsByIdAsync(Id, ct);
            if(member == null)
            {
                TempData["ErrorMessage"] = "Member Not Found "; 
                return RedirectToAction(nameof(Index));
            }
                return View(); 
        }

        [HttpPost]

        public async Task<IActionResult> DeleteConfirmed([FromRoute]int Id , CancellationToken ct)
        {
            var member = await _memberService.RemoveMemberAsync(Id, ct);
            if(member)
                TempData["SuccessMessage"] = "Member Deleted  Successfully";
            else
                TempData["ErrorMessage"] = "Member Not Found ";
    
            return RedirectToAction(nameof(Index)); 


        }

        // POST/BaseURL/Members/DeleteConfirmed {Member}
        // DeleteConfirmed -  submitted form




        #endregion
    }
}
