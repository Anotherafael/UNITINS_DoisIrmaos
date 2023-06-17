using System.ComponentModel.DataAnnotations;

namespace UNITINS_DoisIrmaos.Models
{
    public class Vehicle
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please, insert a name")]
        [Display(Name = "Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Minimum of 2 characters")]
        public string Name { get; set; }

        [Display(Name = "Available")]
        public bool Available { get; set; }

        [Display(Name = "Active")]
        public bool Active { get; set; }

        [Display(Name = "Category")]
        public int CategoryID { get; set; }

        [Display(Name = "Category")]
        public Category Category { get; set; }
    }
}
