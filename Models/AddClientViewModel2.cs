using ClientPortalWeb.Models.Entities;

namespace ClientPortalWeb.Models
{
    public class AddClientViewModel2
    {
        public int ClientId { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public Client Client { get; set; }  // Navigation property to the Client
    }
}
