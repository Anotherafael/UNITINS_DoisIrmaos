using System.ComponentModel.DataAnnotations;

namespace UNITINS_DoisIrmaos.Models
{
    public class Feature
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor, insira um nome")]
        [Display(Name = "Nome")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Mínimo de 2 caracteres")]
        public string Name { get; set; }


        [Display(Name = "Categorias")]
        public ICollection<CategoryFeature> Categories { get; set; }

        public Feature()
        {
            Categories = new List<CategoryFeature>();
        }
    }
}
