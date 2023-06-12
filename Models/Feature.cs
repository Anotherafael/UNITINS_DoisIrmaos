namespace UNITINS_DoisIrmaos.Models
{
    public class Feature
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<CategoryFeature> Categories { get; set; }

        public Feature()
        {
            Categories = new List<CategoryFeature>();
        }
    }
}
