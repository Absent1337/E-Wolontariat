using E_Wolontariat.Models;
using E_Wolontariat.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Wolontariat.Controllers
{
    public class ActionsController : Controller
    {
        private readonly ApplicationDbContext context;
        public ActionsController(ApplicationDbContext context) {
            this.context = context;
        }
        [AllowAnonymous]
        public IActionResult Index()


        {
            // Jeśli użytkownik jest organizacją, pokaż tylko jego akcje
            if (User.IsInRole("Organization"))
            {
                var actions = context.Actions
                    .Where(a => a.CreatedBy == User.Identity.Name)
                    .OrderByDescending(a => a.Date)
                    .ToList();

                return View(actions);
            }
            // Jeśli użytkownik jest wolontariuszem, pokaż wszystkie dostępne akcje
            else if (User.IsInRole("Volunteer"))
            {
                // Pokaż wszystkie akcje, których data jest dzisiejsza lub przyszła
                var actions = context.Actions
                    .Where(a => a.Date >= DateTime.Today) // Uwzględnij dzisiejszą datę
                    .OrderBy(a => a.Date) // Posortuj rosnąco po dacie
                    .ToList();

                return View(actions);
            }
            // Jeśli użytkownik jest anonimowy, pokaż wszystkie dostępne akcje
            else
            {
                // Pokaż wszystkie akcje, których data jest dzisiejsza lub przyszła
                var actions = context.Actions
                    .Where(a => a.Date >= DateTime.Today) // Uwzględnij dzisiejszą datę
                    .OrderBy(a => a.Date) // Posortuj rosnąco po dacie
                    .ToList();

                return View(actions);
            }
        }

        [Authorize(Roles = "Admin,Organization")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(ActionDto ActionDto)
        {
            if (ModelState.IsValid)
            {
                var action = new Models.Action
                {
                    Title = ActionDto.Title,
                    Description = ActionDto.Description,
                    Date = ActionDto.Date,
                    Location = ActionDto.Location,
                    MaxParticipants = ActionDto.MaxParticipants,
                    CreatedBy = User.Identity.Name
                };
                context.Actions.Add(action);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        [Authorize(Roles = "Admin,Organization")]
        public IActionResult Edit(int id)
        {
            var action = context.Actions.Find(id);

            if (action == null)
            {
                return NotFound();
            }

            var model = new ActionDto
            {
                Title = action.Title,
                Description = action.Description,
                Date = action.Date,
                Location = action.Location,
                MaxParticipants = action.MaxParticipants
            };

            return View(model);
        }
        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, ActionDto model)
        {
            if (ModelState.IsValid)
            {
                var action = context.Actions.Find(id);

                if (action == null)
                {
                    return NotFound();
                }

                action.Title = model.Title;
                action.Description = model.Description;
                action.Date = model.Date;
                action.Location = model.Location;
                action.MaxParticipants = model.MaxParticipants;

                context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        [Authorize(Roles = "Admin, Organization")]
        [HttpPost]
        public IActionResult DeleteAction(int id)
        {
            // Pobierz akcję z bazy danych
            var action = context.Actions.FirstOrDefault(a => a.Id == id);

            if (action == null)
            {
                TempData["Error"] = "Akcja nie istnieje.";
                return RedirectToAction("Index");
            }

            // Sprawdź, czy użytkownik jest twórcą akcji lub adminem
            if (User.IsInRole("Organization") && action.CreatedBy != User.Identity.Name && !User.IsInRole("Admin"))
            {
                TempData["Error"] = "Nie masz uprawnień do usunięcia tej akcji.";
                return RedirectToAction("Index");
            }

            // Usuń akcję oraz powiązane zgłoszenia
            var applications = context.Applications.Where(a => a.ActionId == id).ToList();
            context.Applications.RemoveRange(applications);
            context.Actions.Remove(action);
            context.SaveChanges();

            TempData["Success"] = "Akcja została usunięta.";
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public IActionResult AvailableActions()
        {
            var actions = context.Actions
                .Where(a => a.Date >= DateTime.Now) // Tylko przyszłe akcje
                .OrderBy(a => a.Date)
                .ToList();

            return View(actions);
        }

        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            var action = context.Actions.Find(id);

            if (action == null)
            {
                return NotFound();
            }

            return View(action);
        }

        [Authorize(Roles = "Volunteer")]
        [HttpPost]
        public async Task<IActionResult> SignUp(int id, [FromServices] UserManager<IdentityUser> userManager)
        {
            // Pobranie akcji z bazy danych
            var action = await context.Actions.FindAsync(id);

            if (action == null)
            {
                TempData["Error"] = "Akcja nie istnieje.";
                return RedirectToAction("Index");
            }

            // Pobranie zalogowanego użytkownika
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["Error"] = "Nie jesteś zalogowany.";
                return RedirectToAction("Index");
            }

            // Sprawdzenie, czy użytkownik już zapisał się na tę akcję
            var existingApplication = context.Applications
                .FirstOrDefault(a => a.UserId == user.Id && a.ActionId == id);

            if (existingApplication != null)
            {
                TempData["Error"] = "Już zapisałeś się na tę akcję.";
                return RedirectToAction("Details", new { id });
            }

            // Dodanie nowego zgłoszenia
            var application = new Application
            {
                UserId = user.Id,
                ActionId = id,
                Status = "Pending" // Domyślny status
            };

            context.Applications.Add(application);
            await context.SaveChangesAsync();

            TempData["Success"] = "Zostałeś zapisany na akcję.";
            return RedirectToAction("Details", new { id });
        }


        [Authorize(Roles = "Volunteer")]
        public async Task<IActionResult> MyActions([FromServices] UserManager<IdentityUser> userManager)
        {
            // Pobierz ID zalogowanego użytkownika
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["Error"] = "Nie jesteś zalogowany.";
                return RedirectToAction("Index", "Home");
            }

            // Pobierz wszystkie zgłoszenia użytkownika i powiązane akcje
            var applications = context.Applications
                .Where(a => a.UserId == user.Id)
                .Include(a => a.Action) // Dołączenie szczegółów akcji
                .ToList();

            return View(applications);
        }

        [Authorize(Roles = "Organization")]
        public IActionResult Applications(int actionId)
        {
            // Pobierz akcję organizacji
            var action = context.Actions
                .FirstOrDefault(a => a.Id == actionId && a.CreatedBy == User.Identity.Name);

            if (action == null)
            {
                return NotFound();
            }

            // Pobierz wszystkie zgłoszenia do tej akcji
            var applications = context.Applications
                .Where(a => a.ActionId == actionId)
                .Include(a => a.User)
                .ToList();

            ViewBag.ActionTitle = action.Title; // Przekaż tytuł akcji do widoku
            return View(applications);
        }

        [Authorize(Roles = "Organization")]
        [HttpPost]
        public IActionResult ChangeApplicationStatus(int applicationId, string newStatus)
        {
            var application = context.Applications
                .Include(a => a.Action)
                .FirstOrDefault(a => a.Id == applicationId && a.Action.CreatedBy == User.Identity.Name);

            if (application == null)
            {
                return NotFound();
            }

            // Zmień status zgłoszenia
            application.Status = newStatus;
            context.SaveChanges();

            TempData["Success"] = $"Status zgłoszenia został zmieniony na {newStatus}.";
            return RedirectToAction("Applications", new { actionId = application.ActionId });
        }

        [AllowAnonymous]
        public IActionResult Search(string searchTitle, DateTime? searchDate, string searchLocation)
        {
            // Jeśli podano datę w przeszłości, zwróć pustą listę
            if (searchDate.HasValue && searchDate.Value.Date < DateTime.Today)
            {
                return PartialView("_SearchResults", new List<E_Wolontariat.Models.Action>()); // Zwróć pustą listę
            }

            // Pobierz wszystkie akcje
            var actions = context.Actions.AsQueryable();

            // Filtruj według tytułu
            if (!string.IsNullOrEmpty(searchTitle))
            {
                actions = actions.Where(a => a.Title.Contains(searchTitle));
            }

            // Filtruj według daty
            if (searchDate.HasValue)
            {
                actions = actions.Where(a => a.Date.Date == searchDate.Value.Date);
            }

            // Filtruj według lokalizacji
            if (!string.IsNullOrEmpty(searchLocation))
            {
                actions = actions.Where(a => a.Location.Contains(searchLocation));
            }

            // Filtruj tylko akcje od dzisiaj
            actions = actions.Where(a => a.Date >= DateTime.Today);

            // Zwróć wyniki
            return PartialView("_SearchResults", actions.OrderBy(a => a.Date).ToList());
        }

    }
}
