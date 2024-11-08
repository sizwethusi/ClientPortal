using System;
using System.Collections.Generic;


namespace ClientPortalWeb.Models
{
    public class ClientsTabViewModel
    {
        public Tab ActiveTab { get; set; }
    }

    public enum Tab
    {
        General,
        Contacts
    }
}
