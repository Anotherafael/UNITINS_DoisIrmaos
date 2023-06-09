using System.Runtime.InteropServices;

namespace UNITINS_DoisIrmaos.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Cnh { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Active { get; set; }
        public string Address { get; set; }
        public Client() { }
        
    }
}
