using System.ComponentModel.DataAnnotations;

namespace UNITINS_DoisIrmaos.Models
{
    public class Tax
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor, insira um nome")]
        [Display(Name = "Nome")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Mínimo de 2 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please, insert a price value")]
        [Display(Name = "Daily Fee")]
        public double PricePerDay { get; set; }


        [Display(Name = "Alugueis")]
        public ICollection<RentTax> Rents { get; set; }
        public Tax() {
            Rents = new List<RentTax>();
        }
    }
}
