using ClientPortalWeb.Models.Entities;

namespace ClientPortalWeb.Models
{
    public class LinkedClient
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string Client_code { get; set; }
        public Contact Contact { get; set; }    
    }
}
