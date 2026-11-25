using GymMangement.DAL.Data.Models;

namespace GymMangement.DAL.Data.Models
{
    public class Plan : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public int DurationDays { get; set; }
        public bool  IsActive { get; set; }

        #region RelationShips
        public  ICollection<MemberShip> PlanMembers { get; set; } = default!;





        #endregion
    }
}
