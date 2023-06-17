using System.ComponentModel.DataAnnotations;

namespace UNITINS_DoisIrmaos.Models
{
    public class Employee
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

        [Required(ErrorMessage = "Please, insert a CPF")]
        [Display(Name = "CPF")]
        [MinLength(11, ErrorMessage = "Invalid CPF")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Please, insert a phone number")]
        [Display(Name = "Phone Number")]
        [MinLength(13, ErrorMessage = "Invalid Phone number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please, insert a password")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Minimum of 8 characters")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public Employee() { }
    }
}
