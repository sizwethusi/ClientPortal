using ClientPortalWeb.Models.Entities;

namespace ClientPortalWeb.Models
{
    public class LinkedContacts
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Client Client { get; set; }
    }
}
