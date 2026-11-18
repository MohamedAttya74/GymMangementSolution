using GymMangement.DAL.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangement.DAL.Data.Models
{
    public  class Trainer : GymUser
    {
        public  Specialty  Specialty { get; set; }

        // HireDate = CreatedAt of BaseEntity 


        #region RelationShips

        public ICollection<Session> Sessions { get; set; } = default!;


        #endregion
    }
}
