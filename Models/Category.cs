namespace UNITINS_DoisIrmaos.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<CategoryFeature> Features { get; set;}

        public Category() { 
            Features = new List<CategoryFeature>();
        }
    }
}
