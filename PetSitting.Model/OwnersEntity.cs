using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSitting.Model
{
    public class OwnersEntity : IDisposable
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

        [Required(ErrorMessage = "You must enter a owner ID.")]
        [Display(Name="Owner ID")]
        public int OwnerID { get; set; }

        [Required(ErrorMessage = "You must enter a owner Name.")]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name="Owner Name")]
        public string OwnerName { get; set; }

        [Required(ErrorMessage = "You must enter a pet Name.")]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name="Pet Name")]
        public string PetName { get; set; }

        [Required(ErrorMessage = "You must enter a pet Age.")]
        [Range(0, 25, ErrorMessage = "Age range between 0 and 25 years.")]
        [Display(Name="Pet Age")]
        public int PetAge { get; set; }

        [Required(ErrorMessage = "You must enter a contact phone number.")]
        [StringLength(20, MinimumLength = 2)]
        [Display(Name="Contact Phone")]
        public string ContactPhone { get; set; }
        [Display(Name = "Pet Years")]
        public int PetYears { get; set; }
        [Display(Name="Modified Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ModifiedDate { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string Password { get; set; }

        public ICollection<SessionsEntity> Sessions { get; set; }

        #endregion
    }
}
