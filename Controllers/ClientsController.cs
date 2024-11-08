using ClientPortalWeb.Data;
using ClientPortalWeb.Models;
using ClientPortalWeb.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClientPortalWeb.Controllers
{
    public class ClientsController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public ClientsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var contacts = dbContext.Contacts.ToList();  // Fetching data from your database
            return View(contacts);  // Passing the list of contacts to the view
        }
        // GET: Clients/LinkClients/5
        public IActionResult LinkClients(int clientId)
        {
            // Find the client by the provided clientId
            var client = dbContext.Clients
                .Include(c => c.ClientId)
                .ThenInclude(cl => cl.ToString())
                .FirstOrDefault(c => c.ClientId == clientId);

            if (client == null)
            {
                return NotFound();
            }

            // Get all clients excluding the one we're linking to (since it can't be linked to itself)
            var availableClients = dbContext.Clients
                .Where(c => c.ClientId != clientId && !client.LinkedClients.Any(l => l.LinkedClientId == c.ClientId))
                .ToList();

            ViewBag.AvailableClients = availableClients;
            return View(client);
        }

        // POST: Clients/LinkClients
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LinkClients(int clientId, List<int> clientIdsToLink)
        {
            var client = await dbContext.LinkedClient
                .Include(c => c.Contact)
                .FirstOrDefaultAsync(c => c.ClientId == clientId);

            if (client == null)
            {
                return NotFound();
            }

            // Loop through the selected clientIds to link
            foreach (var linkedClientId in clientIdsToLink)
            {
                var linkedClient = await dbContext.LinkedClient.FindAsync(linkedClientId);
                if (linkedClient != null && !client.ClientId.Any(l => l.LinkedClientId == linkedClientId))
                {
                    // Create a new ClientLink
                    var clientLink = new LinkedClient
                    {
                        ClientId = clientId
                         
                    };
                    dbContext.Add(clientLink);
                }
            }

            // Save changes
            await dbContext.SaveChangesAsync();

            // Optionally, update the No_of_linked_clients
            client.Contact.No_of_linked_clients = client.Contact.No_of_linked_clients.;
            dbContext.Update(client);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("Details", new { id = clientId });
        }
        // GET: Clients/LinkContacts/{clientId}
        public IActionResult LinkContacts(int clientId)
        {
            // Find the client by id
            var client = dbContext.Clients.FirstOrDefault(c => c.ClientId == clientId);
            if (client == null)
            {
                return NotFound();  // If the client doesn't exist, return a 404
            }

            // Get all contacts that are not already linked to any client (i.e., ClientId is null)
            var availableContacts = dbContext.Contacts
                .Where(c => c.ClientId == null)  // Only unlinked contacts
                .ToList();

            if (availableContacts == null || !availableContacts.Any())
            {
                // If no unlinked contacts exist, you could display a message or handle this case
                ModelState.AddModelError(string.Empty, "No available contacts to link.");
            }

            // Pass the client and available contacts to the view
            ViewBag.AvailableContacts = availableContacts;
            return View(client);
        }

        // POST: Clients/LinkContacts
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LinkContacts(int clientId, int[] contactIds)
        {
            if (contactIds == null || contactIds.Length == 0)
            {
                // Handle case where no contacts are selected
                ModelState.AddModelError(string.Empty, "Please select at least one contact to link.");
                return RedirectToAction("LinkContacts", new { clientId });
            }

            // Find the client by id
            var client = dbContext.Clients.FirstOrDefault(c => c.ClientId == clientId);
            if (client == null)
            {
                return NotFound();  // If the client doesn't exist, return a 404
            }

            // Get the contacts based on the selected IDs
            var contactsToLink = dbContext.Contacts
                .Where(c => contactIds.Contains(c.ContactId)) // Only the selected contacts
                .ToList();

            // Link the contacts to the client
            foreach (var contact in contactsToLink)
            {
                contact.ClientId = client.ClientId; // Set the ClientId of the contact
            }

            // Save changes to the database
            dbContext.SaveChanges();

            // Redirect back to the client details page or another appropriate page
            return RedirectToAction("Details", new { id = client.ClientId });
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add2()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add3()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Add4()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Add5()
        {
            return View();
        }

        [HttpGet]
        public IActionResult LinkContacts()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Add(AddClientViewModel viewModel)
        {
            var client = new Client
            {
                Name = viewModel.Name,
                 Client_code = viewModel.Name.Substring(0, 1).ToUpper() +
                     (viewModel.Name.Length > 1 ? viewModel.Name.Substring(1, 2).ToUpper() : "") +
                     string.Format("{0:D3}", viewModel.ClientId) 
                //Client_code = viewModel.ClientCode,
                //No_of_linked_contacts = viewModel.No_of_linked_contacts
            };

            await dbContext.Clients.AddAsync(client);
            await dbContext.SaveChangesAsync();



            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add2(AddClientViewModel2 viewModel)
        {
            var contacts = new Contact
            {
                FullName = viewModel.FullName,
                Email = viewModel.EmailAddress,
                
            };


            await dbContext.Contacts.AddAsync(contacts);
            await dbContext.SaveChangesAsync();


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add3(AddClientViewModel2 viewModel)
        {
            var contacts = new Contact
            {
                FullName = viewModel.FullName,
                Email = viewModel.EmailAddress,

            };


            await dbContext.Contacts.AddAsync(contacts);
            await dbContext.SaveChangesAsync();


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add4(AddClientViewModel viewModel)
        {
            var contacts = new Client
            {
                Name = viewModel.Name,
                Client_code = viewModel.ClientCode,

            };


            await dbContext.Clients.AddAsync(contacts);
            await dbContext.SaveChangesAsync();


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add5(AddClientViewModel2 viewModel)
        {
            var contacts = new Contact
            {
                ClientId = viewModel.ClientId,
                FullName = viewModel.FullName,
                Email = viewModel.EmailAddress,

            };


            await dbContext.Contacts.AddAsync(contacts);
            await dbContext.SaveChangesAsync();


            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var clients = await dbContext.Clients.ToListAsync();

            return View(clients);
        }

        [HttpGet]
        public async Task<IActionResult> List2()
        {
            var contacts = await dbContext.Contacts.ToListAsync();

            return View(contacts);
        }

        [HttpGet]
        public async Task<IActionResult> List3()
        {
            var contacts = await dbContext.Contacts.ToListAsync();

            return View(contacts);
        }
        [HttpGet]
        public async Task<IActionResult> List4()
        {
            var contacts = await dbContext.Contacts.ToListAsync();

            return View(contacts);
        }
        [HttpGet]
        public async Task<IActionResult> List5()
        {
            var contacts = await dbContext.LinkedContacts.ToListAsync();

            return View(contacts);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var client = await dbContext.Clients.FindAsync(id);

            return View(client);
        }
        [HttpGet]
        public async Task<IActionResult> Edit2(int id)
        {
            var client = await dbContext.Contacts.FindAsync(id);

            return View(client);
        }

        [HttpGet]
        public async Task<IActionResult> Edit4(int id)
        {
            var client = await dbContext.Contacts.FindAsync(id);

            return View(client);
        }

        [HttpGet]
        public IActionResult Edit3(int clientId)
        {
            var client = dbContext.Contacts
                                 .Include(c => c.ContactId)  // Ensure Contacts are included if needed
                                 .FirstOrDefault(c => c.ClientId == clientId);

            if (client == null)
            {
                return NotFound();  // Ensure the client is found
            }

            return View(client);  // Pass the client object to the view
        }



        [HttpPost]
        public async Task<IActionResult> Edit(Client viewModel)
        {
            var client = await dbContext.Clients.FindAsync(viewModel.ClientId);

            if(client is not null)
            {
                client.Name = viewModel.Name;
                //client.Client_code = viewModel.Client_code;
                //client.No_of_linked_contacts = viewModel.No_of_linked_contacts;

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Clients");
        }

        [HttpPost]
        public async Task<IActionResult> Edit2(Contact viewModel)
        {
            var client = await dbContext.Contacts.FindAsync(viewModel.ContactId);

            if (client is not null)
            {
                client.FullName = viewModel.FullName;
                client.Email = viewModel.Email;

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List2", "Contacts");
        }
        [HttpPost]
        public async Task<IActionResult> Edit3(Contact viewModel)
        {
            var client = await dbContext.Contacts.FindAsync(viewModel.ContactId);

            if (client is not null)
            {
                client.FullName = viewModel.FullName;
                client.Email = viewModel.Email;

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List2", "Contacts");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(Client viewModel)
        {
            var client = await dbContext.Clients.AsNoTracking().FirstOrDefaultAsync(x => x.ClientId ==viewModel.ClientId);

            if(client is not null)
            {
                dbContext.Clients.Remove(viewModel);
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Clients");
        }

        [HttpPost]
        public async Task<IActionResult> Delete2(Contact viewModel)
        {
            var client = await dbContext.Contacts.AsNoTracking().FirstOrDefaultAsync(x => x.ClientId == viewModel.ClientId);

            if (client is not null)
            {
                dbContext.Contacts.Remove(viewModel);
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Contacts");
        }

        [HttpPost]
        public async Task<IActionResult> Delete3(Contact viewModel)
        {
            var contact = await dbContext.Contacts.AsNoTracking().FirstOrDefaultAsync(x => x.ContactId == viewModel.ContactId);

            if (contact is not null)
            {
                dbContext.Contacts.Remove(viewModel);
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Contacts");
        }

        [HttpPost]
        public IActionResult CombinedData()
        {
            var clientContacts = from c in dbContext.Clients
                                 join con in dbContext.Contacts on c.ClientId equals con.ClientId
                                 select new LinkedClient
                                 {
                                     ClientId = c.ClientId,
                                     ClientName = c.Name,
                                     Client_code = c.Client_code,
                                 };

            return View(clientContacts.ToList());
        }
    }
}
