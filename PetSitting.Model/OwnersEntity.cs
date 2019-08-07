using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Web.Mvc;

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

        [Required(ErrorMessage = "You must enter an owner ID.")]
        [Display(Name="Owner ID")]
        public int OwnerID { get; set; }

        [Required(ErrorMessage = "You must enter an owner Name.")]
        [StringLength(255, MinimumLength = 3)]
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
        [Required(ErrorMessage = "You must enter a username.")]
        [StringLength(255, MinimumLength = 3)]
        //[Remote("doesUserNameExist", "Users", HttpMethod = "POST", ErrorMessage = "Username already exists. Please enter a different username.")]
        //[Editable(true)]
        public string Username { get; set; }
        [Required(ErrorMessage = "You must enter a first name")]
        public string FirstName { get; set; }
        public bool IsActive { get; set; }
        public string Role { get; set; }
        [Required(ErrorMessage = "You must enter a last name")]
        [StringLength(255, MinimumLength = 3)]
        public string LastName { get; set; }
        [EmailAddress(ErrorMessage = "You must enter an email address")]
        public string Email { get; set; }
        public int Age { get; set; }
        [Required]
        [MembershipPassword(
            MinRequiredNonAlphanumericCharacters = 1,
            MinNonAlphanumericCharactersError = "Your password needs to contain at least one symbol (!, @, #, etc).",
            ErrorMessage = "Your password must be 6 characters long and contain at least one symbol (!, @, #, etc).",
            MinRequiredPasswordLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string ConfirmPassword { get; set; }
        public decimal TotalSales { get; set; }
        public int SessionsCount { get; set; }


        public ICollection<SessionsEntity> Sessions { get; set; }

        #endregion
    }
}
