using System.ComponentModel.DataAnnotations;

namespace UNITINS_DoisIrmaos.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor, insira um nome")]
        [Display(Name = "Nome")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Mínimo de 2 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Por favor, insira um e-mail")]
        [Display(Name = "E-mail")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "E-mail não é válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Por favor, insira um CPF")]
        [Display(Name = "CPF")]
        [MinLength(14, ErrorMessage = "CPF inválida")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Por favor, insira um telefone")]
        [Display(Name = "Telefone")]
        [MinLength(13, ErrorMessage = "Número de telefone inválido")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Por favor, insira uma senha")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Mínimo de 8 caracteres")]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        public Employee() { }
    }
}
