
namespace PetSitting.Model.SittingViewModels
{
    using System.Collections.Generic;
    class OwnerIndexData
    {
        public IEnumerable<OwnersEntity> Owners { get; set; }
        public IEnumerable<SittersEntity> Sitters { get; set; }
        public IEnumerable<SessionsEntity> Sessions { get; set; }
    }
}
