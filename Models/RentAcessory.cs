using System.ComponentModel.DataAnnotations;

namespace UNITINS_DoisIrmaos.Models
{
    public class RentAcessory
    {
        public int RentID { get; set; }

        [Display(Name = "Aluguel")]
        public Rent Rent { get; set; }

        public int AcessoryID { get; set; }

        [Display(Name = "Acessório")]
        public Acessory Acessory { get; set;}
    }
}
