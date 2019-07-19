using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using System.Web.Security;

namespace PetSitting.Model
{
    public class UsersEntity
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
       
        [Required(ErrorMessage = "You must enter a User ID.")]
        public int UserID { get; set; }
        [Required(ErrorMessage = "You must enter a Username.")]
        [StringLength(255, MinimumLength = 3)]
        public string Username { get; set; }
        [Required(ErrorMessage ="You must enter a First name.")]
        [StringLength(255, MinimumLength = 3)]
        [Display(Name="First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "You must enter a Last name.")]
        [StringLength(255, MinimumLength = 3)]
        [Display(Name="Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        [MembershipPassword(
            MinRequiredNonAlphanumericCharacters = 1,
            MinNonAlphanumericCharactersError = "Your password needs to contain at least one symbol (!, @, #, etc).",
            ErrorMessage = "Your password must be 6 characters long and contain at least one symbol (!, @, #, etc).",
            MinRequiredPasswordLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name="Active")]
        public bool? IsActive { get; set; }
        [Required(ErrorMessage = "You must specifiy your age.")]
        [Range(15, 100, ErrorMessage = "Age range between 15 and 100 years.")]
        public int Age { get; set; }
        [Display(Name = "Discount")]
        public decimal Discount { get; set; }
        public string Role { get; set; }
        [Display(Name="Full name")]
        public string OwnerName { get; set; }
        public string PetName { get; set; }
        public int PetAge { get; set; }
        public string ContactPhone { get; set; }
        [Display(Name="Full name")]
        public string Name { get; set; }
        public decimal Fee { get; set; }
        public string Bio { get; set; }
        public DateTime? HiringDate { get; set; }
        public decimal GrossSalary { get; set; }

        #endregion
    }
}
