namespace UNITINS_DoisIrmaos.Models
{
    public class Rent
    {
        public int Id { get; set; }
        public float Price { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public DateTime TakenAt { get; set; }
        public DateTime ReturnedAt { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }
        public int VehicleID { get; set; }
        public Vehicle Vehicle { get; set; }
        public int BuyerID { get; set; }
        public Client Buyer { get; set; }
        public int DriverID { get; set; }
        public Client Driver { get; set; }
        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }
        public int ProtectionID { get; set; }
        public Protection Protection { get; set; }

        public ICollection<RentAcessory> Acessories { get; set; }
        public ICollection<RentTax> Taxes { get; set; }

        public Rent()
        {
            Acessories = new List<RentAcessory>();
            Taxes = new List<RentTax>();
        }

    }
}
