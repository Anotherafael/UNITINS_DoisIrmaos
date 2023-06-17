using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace UNITINS_DoisIrmaos.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please, insert a name")]
        [Display(Name = "Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Minimum of 2 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please, insert an email")]
        [Display(Name = "Email")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please, insert a phone number")]
        [Display(Name = "Phone Number")]
        [MinLength(13, ErrorMessage = "Invalid Phone number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please, insert a CNH")]
        [Display(Name = "CNH")]
        [MinLength(11, ErrorMessage = "Invalid CNH")]
        public string Cnh { get; set; }

        [Required(ErrorMessage = "Please, insert a date of birth")]
        [DataType(DataType.Date)]
        [Display(Name = "Birthdate")]
        public DateTime BirthDate { get; set; }

        [StringLength(50, MinimumLength = 8, ErrorMessage = "Minimum of 8 characters")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Active")]
        public bool Active { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }
        public Client() { }
        
    }
}
