
namespace PetSitting.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class SessionsEntity
    {
        public int SessionID { get; set; }
        public int SitterID { get; set; }
        [Display(Name="Sitter Name")]
        public string SitterName { get; set; }
        public int OwnerID { get; set; }
        [Display(Name="Owner Name")]
        public string OwnerName { get; set; }
        [Display(Name="Pet Name")]
        public string PetName { get; set; }
        public string Status { get; set; }
        
        [Display(Name = "Session Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; }
        [Display(Name="Fee Charged")]
        public decimal Fee { get; set; }
        public decimal FeeCap { get; set; }
        
    }

}
