using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PetSitting.Model
{
    public class SessionsEntity
    {
        public int SessionID { get; set; }
        public int SitterID { get; set; }
        public string SitterName { get; set; }
        public int OwnerID { get; set; }
        public string OwnerName { get; set; }
        public string Status { get; set; }
        
        [Display(Name = "Session Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; }
        public decimal Fee { get; set; }
        public decimal FeeCap { get; set; }
        
    }

}
