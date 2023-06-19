using System.ComponentModel.DataAnnotations;

namespace UNITINS_DoisIrmaos.Models
{
    public class Rent
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please, insert a price value")]
        [Display(Name = "Price")]
        public float Price { get; set; }

        [Required(ErrorMessage = "Please, insert a start date")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Start Date")]
        public DateTime StartAt { get; set; }

        [Required(ErrorMessage = "Please, insert an ending date")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Ending Date")]
        public DateTime EndAt { get; set; }

        [Required(ErrorMessage = "Please, insert a pick-up date")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Pick-up Date")]
        public DateTime TakenAt { get; set; }

        [Required(ErrorMessage = "Please, insert a drop-off date")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Drop-off Date")]
        public DateTime ReturnedAt { get; set; }

        [Display(Name = "Category")]
        public int CategoryID { get; set; }

        [Display(Name = "Category")]
        public Category Category { get; set; }

        [Display(Name = "Vehicle")]
        public int? VehicleID { get; set; }

        [Display(Name = "Vehicle")]
        public Vehicle Vehicle { get; set; }

        [Display(Name = "Buyer")]
        public int BuyerID { get; set; }

        [Display(Name = "Buyer")]
        public Client Buyer { get; set; }

        [Display(Name = "Driver")]
        public int? DriverID { get; set; }

        [Display(Name = "Driver")]
        public Client Driver { get; set; }

        [Display(Name = "Employee")]
        public int EmployeeID { get; set; }

        [Display(Name = "Employee")]
        public Employee Employee { get; set; }

        [Display(Name = "Protection")]
        public int? ProtectionID { get; set; }

        [Display(Name = "Protection")]
        public Protection Protection { get; set; }


        [Display(Name = "Acessories")]
        public ICollection<RentAcessory> Acessories { get; set; }

        [Display(Name = "Taxes")]
        public ICollection<RentTax> Taxes { get; set; }

        public Rent()
        {
            Acessories = new List<RentAcessory>();
            Taxes = new List<RentTax>();
        }

    }
}
