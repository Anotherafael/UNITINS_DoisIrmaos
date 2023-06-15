using System.ComponentModel.DataAnnotations;

namespace UNITINS_DoisIrmaos.Models
{
    public class Acessory
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor, insira um nome")]
        [Display(Name = "Nome")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Mínimo de 2 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Por favor, insira uma descrição")]
        [Display(Name = "Descrição")]
        [StringLength(280)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Por favor, insira um preço")]
        [Display(Name = "Preço")]
        public float Price { get; set; }

        [Display(Name = "Ativo")]
        public bool Active { get; set; }


        [Display(Name = "Alugueis")]
        public ICollection<RentAcessory> Rents { get; set; }

        public Acessory() {
            Rents = new List<RentAcessory>();
        }
    }
}
