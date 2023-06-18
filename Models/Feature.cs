using System.ComponentModel.DataAnnotations;

namespace UNITINS_DoisIrmaos.Models
{
    public class Feature
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please, insert a name")]
        [Display(Name = "Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Minimum of 2 characters")]
        public string Name { get; set; }


        [Display(Name = "Categories")]
        public ICollection<CategoryFeature> Categories { get; set; }

        public Feature()
        {
            Categories = new List<CategoryFeature>();
        }
    }
}
