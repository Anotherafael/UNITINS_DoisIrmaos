namespace UNITINS_DoisIrmaos.Models
{
    public class RentTax
    {
        public int RentID { get; set; }
        public Rent Rent { get; set; }
        public int TaxID { get; set; }
        public Tax Tax { get; set; }

    }
}
