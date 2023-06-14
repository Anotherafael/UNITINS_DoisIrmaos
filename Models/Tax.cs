namespace UNITINS_DoisIrmaos.Models
{
    public class Tax
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double PricePerDay { get; set; }

        public ICollection<RentTax> Rents { get; set; }
        public Tax() {
            Rents = new List<RentTax>();
        }
    }
}
