namespace UNITINS_DoisIrmaos.Models
{
    public class Acessory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public bool Active { get; set; }

        public ICollection<RentAcessory> Rents { get; set; }
        public Acessory() {
            Rents = new List<RentAcessory>();
        }
    }
}
