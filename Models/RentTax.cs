using System.ComponentModel.DataAnnotations;

namespace UNITINS_DoisIrmaos.Models
{
    public class RentTax
    {
        public int RentID { get; set; }

        [Display(Name = "Rent")]
        public Rent Rent { get; set; }

        public int TaxID { get; set; }

        [Display(Name = "Tax")]
        public Tax Tax { get; set; }

    }
}
