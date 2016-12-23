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
    public class RoleController : Controller
    {
        private readonly BusinessProContext _context;
        public RoleController(BusinessProContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var items = _context.Roles;
            return View(items.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }

        //POST Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Role role)
        {
            if (ModelState.IsValid)
            {
                _context.Roles.Add(role);
                _context.SaveChanges();
                //return RedirectToAction(nameof(Edit), new { id = Room.Id });

            }
            return View(role);
        }
        // GET: Rooms/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Role role = _context.Roles.Find(id);
            if (role == null)
            {
                return BadRequest();
            }
            ViewBag.PeopleList = new SelectList(_context.People.ToList(), "Id", "Name");

            return View(role);
        }

        [HttpPost]
        public IActionResult Edit(Role role)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(role).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.PeopleList = new SelectList(_context.People.ToList(), "Id", "Name");
            return View(role);
        }

        //Department/AddPerson
        //To add department to room
        public IActionResult AddPerson(int? roleId, int? personId)
        {
            if(roleId == null || personId == null)
            {
                return BadRequest();
            }

            Person person = _context.People.Find(personId);
            Role role = _context.Roles.Find(roleId);
            PersonRole personRole = new PersonRole
            {
                PersonId = (int)personId,
                Person = person,
                RoleId = (int)roleId,
                Role = role            
                
            };

            _context.Entry(person).Collection(i => i.PersonRole).Load();
            if (person.PersonRole == null)
            {
               person.PersonRole = new List<PersonRole>();
            }

            if (role.PersonRole == null)
            {
                role.PersonRole = new List<PersonRole>();
            }
            role.PersonRole.Add(personRole);
            person.PersonRole.Add(personRole);

            if (ModelState.IsValid)
            {
                _context.Entry(role).State = EntityState.Modified;
                _context.Entry(person).State = EntityState.Modified;
                _context.Entry(personRole).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        // GET: Roles/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Role role = _context.Roles.Find(id);
            _context.Entry(role).Collection(i => i.PersonRole).Load();
            //Left off Here 
            foreach(PersonRole pr in role.PersonRole){
                //_context.Entry(pr).Collection(i => i.Person).Load();
            };
           
            //role.PersonRole = _context
            if (role == null || role.PersonRole == null)
            {
                return BadRequest();
            }

            return View(role);
        }

    }
}