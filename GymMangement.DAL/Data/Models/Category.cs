using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangement.DAL.Data.Models
{
    public  class Category : BaseEntity
    {
        public  string CategoryName { get; set; }


        #region RelationShips


        public  ICollection<Session> Sessions { get; set; } = default!;


        #endregion


    }
}
