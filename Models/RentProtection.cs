namespace UNITINS_DoisIrmaos.Models
{
    public class RentProtection
    {
        public int RentID { get; set; }
        public Rent Rent { get; set; }
        public int ProtectionID { get; set; }
        public Protection Protection { get; set; }
    }
}
