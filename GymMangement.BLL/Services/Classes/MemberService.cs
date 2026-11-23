using GymMangement.BLL.ViewModels.MemberViewModels;
using GymMangement.DAL.Data.Models;
using GymMangement.DAL.Repositories.Interfaces;
using GymMangement.PL.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangement.BLL.Services.Classes
{
    public class MemberService : IMemberService
    {
        private readonly IGenericRepository<Member> memberRepository;

        public MemberService(IGenericRepository<Member> memberRepository)
        {
            this.memberRepository = memberRepository;
        }

        public async Task<bool> CreateMemberAsync(CreateMemberViewModel model, CancellationToken ct = default)
        {
            // Check Email is Exist 
            var EmailExist = await memberRepository.AnyAsync(m => m.Email == model.Email, ct);
            // Check Phone is Exist
            var PhoneExist = await memberRepository.AnyAsync(m => m.Phone == model.Phone, ct);
            // Email Or Phone  Exist return false

            if (EmailExist || PhoneExist) return false;

            //  Else Return true  and  Add  member
            //  
            var member = new Member()
            {
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone,
                Gender = model.Gender,
                DateOfBirth = model.DateOfBirth,
                Address = new Address()
                {
                    City = model.City,
                    street = model.Street,
                    BuildingNumber = model.BuildingNumber,
                },
                HealthRecord = new HealthRecord()
                {
                    BloodType = model.HealthRecordViewModel.BloodType,
                    Weight = model.HealthRecordViewModel.Weight,
                    Height = model.HealthRecordViewModel.Height,
                    Note = model.HealthRecordViewModel.Note
                }



            };


            var Result = await memberRepository.AddAsync(member);
            return Result > 0;
        }

        async Task<IEnumerable<MemberViewModel>> IMemberService.GetAllMembersAsync(CancellationToken ct)
        {

            var members = await memberRepository.GetAllAsync(ct: ct);
            if (!members.Any()) return [];


            var memberViewModel = members.Select(m => new MemberViewModel
            {
                Id = m.Id,
                Name = m.Name,
                Photo = m.Photo,
                Email = m.Email,
                Gender = m.Gender.ToString(),
                Phone = m.Phone
            });
            return memberViewModel;


        }
    }
}
