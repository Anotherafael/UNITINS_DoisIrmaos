namespace UNITINS_DoisIrmaos.Models
{
    public class RentAcessory
    {
        public int RentID { get; set; }
        public Rent Rent { get; set; }
        public int AcessoryID { get; set; }
        public Acessory Acessory { get; set;}
    }
}
