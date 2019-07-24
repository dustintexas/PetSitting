using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace PetSitting.Model
{
    /// <summary> 
    /// Purpose: Data Contract Entity Model Class [SittersEntity] for the table [dbo].[Sitters]. 
    /// </summary> 
    public class SittersEntity : IDisposable
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

        [Required(ErrorMessage = "You must enter a sitter ID.")]
        public int SitterID { get; set; }

        [Required(ErrorMessage = "You must enter a sitter Name.")]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "You must enter a sitter Fee.")]
        public decimal Fee { get; set; }

        [Required(ErrorMessage = "You must enter a BIO.")]
        [StringLength(500, MinimumLength = 3)]
        public string Bio { get; set; }

        [Required(ErrorMessage = "You must enter a sitter Age.")]
        [Range(15, 80, ErrorMessage = "Age range between 15 and 80 years.")]
        public int Age { get; set; }

        [Display(Name="Hiring Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? HiringDate { get; set; }

        [Required(ErrorMessage = "You must enter a sitter Gross Salary.")]
        [Display(Name="Gross Salary")]
        public Decimal GrossSalary { get; set; }
        [Display(Name="Net Salary")]
        public Decimal NetSalary { get; set; }
        [Display(Name="Modified Date")]
        public DateTime ModifiedDate { get; set; }
        public decimal TotalSales { get; set; }
        [Required(ErrorMessage = "You must enter a Username")]
        [StringLength(255, MinimumLength = 3)]
        public string Username { get; set; }
        [Required(ErrorMessage = "You must enter a first name")]
        [StringLength(255, MinimumLength = 3)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "You must enter a last name")]
        [StringLength(255, MinimumLength = 3)]
        public string LastName { get; set; }
        [EmailAddress(ErrorMessage = "You must enter an email address")]
        public string Email { get; set; }
        [Required]
        [MembershipPassword(
            MinRequiredNonAlphanumericCharacters = 1,
            MinNonAlphanumericCharactersError = "Your password needs to contain at least one symbol (!, @, #, etc).",
            ErrorMessage = "Your password must be 6 characters long and contain at least one symbol (!, @, #, etc).",
            MinRequiredPasswordLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string ConfirmPassword { get; set; }
        public bool IsActive { get; set; }
        public string Role { get; set; }
        public ICollection<SessionsEntity> Sessions { get; set; }

        #endregion


    }
    public class Sitter
    {
        public string Name { get; set; }
        public int SitterID { get; set; }
    }
}
