using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TicketSystem.Models;

namespace TicketSystem.Controllers
{
    public class HomeController : Controller
    {
        private TicketContext context;
        public HomeController(TicketContext ctx) => context = ctx;

        public IActionResult Index(string id)
        {
            //store current filters and data needed for filter drop downs in TicketViewModel
            TicketViewModel model = new TicketViewModel();

            var filters = new Filters(id);

            model.Filters = new Filters(id);
            model.Points = context.Points.OrderBy(p => p.PointId).ToList();
            model.Sprints = context.Sprints.OrderBy(s => s.SprintId).ToList();
            model.Statuses = context.Statuses.OrderBy(s => s.StatusId).ToList();


            // get Ticket objects from database based on current filters
            IQueryable<Ticket> query = context.Tickets
                .Include(t => t.Point)
                .Include(t => t.Sprint)
                .Include(t => t.Status);
            if (filters.HasSprint)
            {
                query = query.Where(t => t.SprintId == filters.SprintId);
            }
            if (filters.HasStatus)
            {
                query = query.Where(t => t.StatusId == filters.StatusId);
            }

            var tickets = query.OrderBy(t => t.TicketId).ToList();

            model.Tickets = tickets;
            return View(model);
        }

        public IActionResult Add()
        {
            TicketViewModel model = new TicketViewModel();
            model.Points = context.Points.OrderBy(p => p.PointId).ToList();
            model.Sprints = context.Sprints.OrderBy(s => s.SprintId).ToList();
            model.Statuses = context.Statuses.OrderBy(s => s.StatusId).ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(TicketViewModel model)
        {
            if (ModelState.IsValid)
            {
                context.Tickets.Add(model.CurrentTicket);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                model.Points = context.Points.OrderBy(p => p.PointId).ToList();
                model.Sprints = context.Sprints.OrderBy(s => s.SprintId).ToList();
                model.Statuses = context.Statuses.OrderBy(s => s.StatusId).ToList();
                return View(model);
            }
        }

        [HttpPost]
        public IActionResult Filter(string[] filter)
        {
            string id = string.Join('-', filter);
            return RedirectToAction("Index", new { ID = id });
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Points = context.Points.OrderBy(p => p.PointId).ToList();
            ViewBag.Sprints = context.Sprints.OrderBy(s => s.SprintId).ToList();
            ViewBag.Statuses = context.Statuses.OrderBy(s => s.StatusId).ToList();
            var ticket = context.Tickets.Find(id);
            return View(ticket);
        }

        [HttpPost]
        public IActionResult Edit(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                if (ticket.TicketId == 0)
                    context.Tickets.Add(ticket);
                else
                    context.Tickets.Update(ticket);

                context.SaveChanges();
                return RedirectToAction("Index", "Home");

            }
            else
            {
                ViewBag.Action = (ticket.TicketId == 0) ? "Add" : "Edit";
                ViewBag.Points = context.Points.OrderBy(p => p.PointId).ToList();
                ViewBag.Sprints = context.Sprints.OrderBy(s => s.SprintId).ToList();
                ViewBag.Statuses = context.Statuses.OrderBy(s => s.StatusId).ToList();
                return View(ticket);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Ticket ticket = context.Tickets.Find(id);
            return View(ticket);
        }

        [HttpPost]
        public IActionResult Delete(Ticket ticket)
        {
            context.Tickets.Remove(ticket);
            context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
