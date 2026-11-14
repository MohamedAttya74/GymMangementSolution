using GymMangement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangement.DAL.Repositories.Interfaces
{
    public interface IPlanRepository
    {

        Task<IEnumerable<Plan>> GetAllAsync(bool Tracking = false, CancellationToken ct = default);
        Task<Plan?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<int> AddAsync(Plan plan, CancellationToken ct = default);
        Task<int> UpdateAsync(Plan plan, CancellationToken ct = default);
        Task<int> DeleteAsync(Plan plan, CancellationToken ct = default);


    }
}
