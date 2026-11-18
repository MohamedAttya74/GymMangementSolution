using GymMangement.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangement.DAL.Data.Models
{
    public class MemberShip : BaseEntity
    {
        public  Member Member { get; set; } = default!;
        public  int  MemberId { get; set; }


        public Plan Plan { get; set; } = default!;
        public int PlanId { get; set; }


        //public DateTime StartDate { get; set; } // StartDate = CreatedAt of BaseEntity
        public DateTime EndDate { get; set; }


        public  string Status => EndDate > DateTime.Now ? "Active" : "Expired";
        public  bool  IsActive => EndDate > DateTime.Now;
    }
}
