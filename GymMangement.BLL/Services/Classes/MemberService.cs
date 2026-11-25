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
        private readonly IGenericRepository<MemberShip> memberShipRepository;
        private readonly IGenericRepository<Plan> planRepository;
        private readonly IGenericRepository<HealthRecord> healthRecordRepository;
        private readonly IGenericRepository<Booking> bookingRepository;

        public MemberService(IGenericRepository<Member> memberRepository
            , IGenericRepository<MemberShip> memberShipRepository,
            IGenericRepository<Plan> planRepository,
            IGenericRepository<HealthRecord> healthRecordRepository , IGenericRepository<Booking> bookingRepository)
        {
            this.memberRepository = memberRepository;
            this.memberShipRepository = memberShipRepository;
            this.planRepository = planRepository;
            this.healthRecordRepository = healthRecordRepository;
            this.bookingRepository = bookingRepository;
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

        public async Task<HealthRecordViewModel?> GetMemberHealthRecordAsync(int MemberId, CancellationToken ct = default)
        {
            var record = await healthRecordRepository.FirstOrDefaultAsync(x => x.MemberId == MemberId, ct: ct);

            if (record == null) return null;
            else
                return new HealthRecordViewModel()
                {
                    Height = record.Height,
                    Weight = record.Weight,
                    BloodType = record.BloodType,
                    Note = record.Note
                };


        }

        public async Task<MemberViewModel?> GetMemberDetailsByIdAsync(int MemberId, CancellationToken ct = default)
        {
            var member = await memberRepository.GetByIdAsync(MemberId, ct);
            if (member == null) return null;

            var model = new MemberViewModel()
            {
                Name = member.Name,
                Phone = member.Phone,
                DateOfBirth = member.DateOfBirth.ToShortDateString(),
                Gender = member.Gender.ToString(),
                Address = $"{member.Address.City} , {member.Address.street} , {member.Address.BuildingNumber}",
            };



            var ActiveMembership = await memberShipRepository.FirstOrDefaultAsync(x => x.MemberId == MemberId && x.EndDate > DateTime.Now);

            if (ActiveMembership is not null)
            {

                var ActivePlan = await planRepository.GetByIdAsync(ActiveMembership.PlanId, ct);
                model.PlanName = ActivePlan?.Name;

                model.MembershipStartDate = ActiveMembership.CreatedAt.ToString();
                model.MembershipEndDate = ActiveMembership.EndDate.ToString();



            }


            return model;

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

        public async Task<MemberToUpdateViewModel?> GetMemberToUpdateByIdAsync(int MemberId, CancellationToken ct = default)
        {
            var member = await memberRepository.GetByIdAsync(MemberId, ct);
            if (member == null) return null;
            else
                return new MemberToUpdateViewModel()
                {
                    Name = member.Name,
                    Photo = member.Photo,
                    Email = member.Email,
                    Phone = member.Phone,
                    BuildingNumber = member.Address.BuildingNumber,
                    City = member.Address.City,
                    Street = member.Address.street
                };
        }

        public async Task<bool> UpdateMemberDetailsAsync(int Id, MemberToUpdateViewModel model, CancellationToken ct = default)
        {
            var member = await memberRepository.GetByIdAsync(Id, ct);
            if (member == null) return false;
            // Check Email And Phone If Exist ?
            var EmailExists = await memberRepository.AnyAsync(x => x.Email == model.Email && x.Id != Id);
            var PhoneExists = await memberRepository.AnyAsync(x => x.Phone == model.Phone && x.Id != Id);

            if (EmailExists || PhoneExists) return false;
            member.Email = model.Email;
            member.Phone = model.Phone;
            member.Address.City = model.City;
            member.Address.BuildingNumber = model.BuildingNumber;
            member.Address.street = model.Street;
            member.UpdatedAt = DateTime.Now;

            var Result = await memberRepository.UpdateAsync(member);
            return Result > 0;




        }

        public async Task<bool> RemoveMemberAsync(int memberId, CancellationToken ct)
        {
            var member = await memberRepository.GetByIdAsync(memberId, ct);
            if (member == null) return false;
                                                                                                // Problem In future We Will Handel : أن شاء الله 
            var HasFutureSession = await bookingRepository.AnyAsync(b => b.MemberId == memberId && b.Session.StartDate > DateTime.Now ,ct);
            if (HasFutureSession) return false;

           var Result =  await memberRepository.DeleteAsync(member,ct );
            return Result > 0;



        }
    }
}
