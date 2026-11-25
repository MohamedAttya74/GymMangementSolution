using GymMangement.BLL.ViewModels.MemberViewModels;
using GymMangement.DAL.Data.Models;

namespace GymMangement.PL.Services.Interfaces
{
    public interface IMemberService
    {
         Task<IEnumerable<MemberViewModel>> GetAllMembersAsync( CancellationToken ct = default);
        Task<bool> CreateMemberAsync(CreateMemberViewModel model, CancellationToken ct = default);
        Task<MemberViewModel?> GetMemberDetailsByIdAsync(int MemberId, CancellationToken ct = default);
        Task<HealthRecordViewModel?> GetMemberHealthRecordAsync(int MemberId, CancellationToken ct = default);
        Task<MemberToUpdateViewModel?> GetMemberToUpdateByIdAsync(int MemberId, CancellationToken ct = default);
        Task<bool> UpdateMemberDetailsAsync(int Id, MemberToUpdateViewModel model ,CancellationToken ct = default);
        Task<bool> RemoveMemberAsync(int memberId, CancellationToken ct =  default);


    }
}
