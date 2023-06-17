using System.ComponentModel.DataAnnotations;

namespace UNITINS_DoisIrmaos.Models
{
    public class RentAcessory
    {
        public int RentID { get; set; }

        [Display(Name = "Rent")]
        public Rent Rent { get; set; }

        public int AcessoryID { get; set; }

        [Display(Name = "Acessory")]
        public Acessory Acessory { get; set;}
    }
}
