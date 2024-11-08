
namespace ClientPortalWeb.Models.Entities
{
    public class Contact
    {
        public String FullName { get; set; }
        public String Email { get; set; }

        public int ContactId { get; set; }

        public int? ClientId { get; set; }
        public int No_of_linked_clients { get; set; }
        public Client Client { get; set; }  // Navigation property to the Client
    }
}
