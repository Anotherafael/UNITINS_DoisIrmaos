using System.ComponentModel.DataAnnotations;

namespace UNITINS_DoisIrmaos.Models
{
    public class RentTax
    {
        public int RentID { get; set; }

        [Display(Name = "Aluguel")]
        public Rent Rent { get; set; }

        public int TaxID { get; set; }

        [Display(Name = "Taxa")]
        public Tax Tax { get; set; }

    }
}
