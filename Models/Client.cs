using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace UNITINS_DoisIrmaos.Models
{
    public class Client
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

        [Required(ErrorMessage = "Por favor, insira um telefone")]
        [Display(Name = "Telefone")]
        [MinLength(13, ErrorMessage = "Número de telefone inválido")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Por favor, insira uma CNH")]
        [Display(Name = "CNH")]
        [MinLength(11, ErrorMessage = "CNH inválida")]
        public string Cnh { get; set; }

        [Required(ErrorMessage = "Por favor, insira sua data de nascimento")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        public DateTime BirthDate { get; set; }

        [StringLength(50, MinimumLength = 8, ErrorMessage = "Mínimo de 8 caracteres")]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Display(Name = "Ativo")]
        public bool Active { get; set; }

        [Display(Name = "Endereço")]
        public string Address { get; set; }

        public ICollection<Rent> BuyerRents { get; set; }
        public ICollection<Rent> DriverRents { get; set; }
        public Client() {
            BuyerRents = new List<Rent>();
            DriverRents = new List<Rent>();
        }
        
    }
}
