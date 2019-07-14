using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;

namespace PetSitting.Model
{
    public class RolesEntity
    {
        #region Class Public Methods 

        /// <summary> 
        /// Purpose: Implements the IDispose interface. 
        /// </summary> 
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Class Property Declarations
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public virtual ICollection<UsersEntity> Users { get; set; }

        #endregion
    }
}
