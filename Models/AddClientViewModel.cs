namespace ClientPortalWeb.Models
{
    public class AddClientViewModel
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string ClientCode { get; set; }
        public int No_of_linked_contacts { get; set; }
        public AddClientViewModel Contact { get; set; }  // Navigation property to the Client
        public string Email { get; set; }   

    }
}
