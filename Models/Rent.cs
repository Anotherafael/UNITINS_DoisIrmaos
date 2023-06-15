using System.ComponentModel.DataAnnotations;

namespace UNITINS_DoisIrmaos.Models
{
    public class Rent
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor, insira o preço do aluguel")]
        [Display(Name = "Preço")]
        public float Price { get; set; }

        [Required(ErrorMessage = "Por favor, insira a data de retida prevista")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Retirada Prevista")]
        public DateTime StartAt { get; set; }

        [Required(ErrorMessage = "Por favor, insira a data de devolução prevista")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Devolução Prevista")]
        public DateTime EndAt { get; set; }

        [Required(ErrorMessage = "Por favor, insira a data dA retida")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Retirada Efetiva")]
        public DateTime TakenAt { get; set; }

        [Required(ErrorMessage = "Por favor, insira a data da devolução")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Devolução Efetiva")]
        public DateTime ReturnedAt { get; set; }
        public int CategoryID { get; set; }

        [Display(Name = "Categoria")]
        public Category Category { get; set; }
        public int VehicleID { get; set; }

        [Display(Name = "Veículo")]
        public Vehicle Vehicle { get; set; }
        public int BuyerID { get; set; }

        [Display(Name = "Comprador")]
        public Client Buyer { get; set; }
        public int DriverID { get; set; }

        [Display(Name = "Motorista")]
        public Client Driver { get; set; }
        public int EmployeeID { get; set; }

        [Display(Name = "Funcionário")]
        public Employee Employee { get; set; }
        public int ProtectionID { get; set; }

        [Display(Name = "Proteção")]
        public Protection Protection { get; set; }


        [Display(Name = "Acessórios")]
        public ICollection<RentAcessory> Acessories { get; set; }

        [Display(Name = "Taxas")]
        public ICollection<RentTax> Taxes { get; set; }

        public Rent()
        {
            Acessories = new List<RentAcessory>();
            Taxes = new List<RentTax>();
        }

    }
}
