using System.ComponentModel.DataAnnotations;

namespace UNITINS_DoisIrmaos.Models
{
    public class Vehicle
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor, insira um nome")]
        [Display(Name = "Nome")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Mínimo de 2 caracteres")]
        public string Name { get; set; }

        [Display(Name = "Disponível")]
        public bool Available { get; set; }

        [Display(Name = "Ativo")]
        public bool Active { get; set; }

        public int CategoryID { get; set; }

        [Display(Name = "Categoria")]
        public Category Category { get; set; }
    }
}
