using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public ICollection<SessionsEntity> Sessions { get; set; }

        #endregion


    }
}
