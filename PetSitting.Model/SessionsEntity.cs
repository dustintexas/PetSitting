using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PetSitting.Model
{
    public enum Status
    {
        Requested, Scheduled, Completed, Cancelled
    }
    public class SessionsEntity
    {
        public int SessionID { get; set; }
        public int SitterID { get; set; }
        public int OwnerID { get; set; }
        [DisplayFormat(NullDisplayText = "No status")]
        public Status? Status { get; set; }
        public OwnersEntity Owner { get; set; }
        public SittersEntity Sitter { get; set; }
    }
}
