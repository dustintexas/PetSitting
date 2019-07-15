using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSitting.Model.SittingViewModels
{
    class OwnerIndexData
    {
        public IEnumerable<OwnersEntity> Owners { get; set; }
        public IEnumerable<SittersEntity> Sitters { get; set; }
        public IEnumerable<SessionsEntity> Sessions { get; set; }
    }
}
