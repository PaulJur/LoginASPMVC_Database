using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginMVC_Database.Models
{
    public class FakePersonData
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("First Name")]
        [MaxLength(30)]
        [RegularExpression(@"^[^\d]+$", ErrorMessage = "First name cannot contain digits.")]
        public string FirstName {  get; set; }

        [DisplayName("Last Name")]
        [MaxLength(30)]
        [RegularExpression(@"^[^\d]+$", ErrorMessage = "Last name cannot contain digits.")]
        public string LastName { get; set; }

        [Range(1, 100, ErrorMessage = "Age must be between 1-120")]//Minimum Age order to maximum amount
        public int Age { get; set; }

        

    }
}
