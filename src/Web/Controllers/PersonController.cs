using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dal;
using Dal.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Controllers
{
    public class PersonController : Controller
    {
        private readonly BusinessProContext _context;
        public PersonController(BusinessProContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var items = _context.People;
            return View(items.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        //POST Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Person person)
        {
            if (ModelState.IsValid)
            {
                _context.People.Add(person);
                _context.SaveChanges();
                //return RedirectToAction(nameof(Edit), new { id = Room.Id });

            }
            return View(person);
        }

        // GET: Person/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Person person = _context.People.Find(id);
            _context.Entry(person).Collection(i => i.PersonRole).Load();
            //Load Person for PersonRole
            foreach (PersonRole pr in person.PersonRole)
            {
                pr.Role = _context.Roles.Find(pr.RoleId);
            };
            if (person == null || person.PersonRole == null)
            {
                return BadRequest();
            }
            return View(person);
        }

        // GET: Person/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Person person = _context.People.Find(id);
            if (person == null)
            {
                return BadRequest();
            }
            ViewBag.RoleList = new SelectList(_context.Roles.ToList(), "Id", "Name");

            return View(person);
        }

        [HttpPost]
        public IActionResult Edit(Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(person).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.RoleList = new SelectList(_context.Roles.ToList(), "Id", "Name");
            return View(person);
        }


        //Person/AddRole
        //To add Role to Person
        public IActionResult AddRole(int? personId, int? roleId)
        {
            if (personId == null || roleId == null)
            {
                return BadRequest();
            }

            Role role = _context.Roles.Find(roleId);
            Person person = _context.People.Find(personId);
            PersonRole personRole = new PersonRole
            {
                PersonId = (int)personId,
                Person = person,
                RoleId = (int)roleId,
                Role = role

            };

            _context.Entry(role).Collection(i => i.PersonRole).Load();
            if (role.PersonRole == null)
            {
                role.PersonRole = new List<PersonRole>();
            }

            if (person.PersonRole == null)
            {
                person.PersonRole = new List<PersonRole>();
            }
            person.PersonRole.Add(personRole);
            role.PersonRole.Add(personRole);

            if (ModelState.IsValid)
            {
                _context.Entry(person).State = EntityState.Modified;
                _context.Entry(role).State = EntityState.Modified;
                _context.Add(personRole);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }
    }
}
