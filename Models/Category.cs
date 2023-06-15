using System.ComponentModel.DataAnnotations;

namespace UNITINS_DoisIrmaos.Models
{
    public class Category
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

        [Display(Name = "Características")]
        public ICollection<CategoryFeature> Features { get; set;}

        public Category() { 
            Features = new List<CategoryFeature>();
        }
    }
}
