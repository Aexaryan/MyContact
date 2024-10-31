using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyContacts.Models;

namespace MyContacts.Controllers
{
    public class ContactController : Controller
    {
        private readonly ContactContext _ctx;

        public ContactController(ContactContext ctx)
        {
            _ctx = ctx;
        }

        public IActionResult Index()
        {
            var contacts = _ctx.Contacts.Include(c => c.Category).OrderBy(c => c.LastName).ToList();
            return View(contacts);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Categories = _ctx.Categories.ToList();
            ViewBag.Action = "Add";
            return View("AddEdit", new Contact());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var contact = _ctx.Contacts.Include(c => c.Category).FirstOrDefault(c => c.ContactId == id);
            if (contact == null)
            {
                return NotFound();
            }
            ViewBag.Categories = _ctx.Categories.ToList();
            ViewBag.Action = "Edit";
            return View("AddEdit", contact);
        }

        [HttpPost]
        public IActionResult AddEdit(Contact contact)
        {
            if (ModelState.IsValid)
            {
                if (contact.ContactId == 0)
                {
                    contact.DateAdded = DateTime.Now;
                    _ctx.Contacts.Add(contact);
                }
                else
                {
                    _ctx.Contacts.Update(contact);
                }
                _ctx.SaveChanges();
                return RedirectToAction("Index", "Contact");
            }
            ViewBag.Categories = _ctx.Categories.ToList();
            return View(contact);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var contact = _ctx.Contacts.Find(id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        [HttpPost]
        public IActionResult Delete(Contact contact)
        {
            _ctx.Contacts.Remove(contact);
            _ctx.SaveChanges();
            return RedirectToAction("Index", "Contact");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var contact = _ctx.Contacts.Include(c => c.Category).FirstOrDefault(c => c.ContactId == id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }
    }
}
