using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UNITINS_DoisIrmaos.Models
{
    public class CategoryFeature
    {
        public int CategoryID { get; set; }
        public Category Category { get; set; }
        public int FeatureID { get; set; }
        public Feature Feature { get; set; }
    }
}
