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
    }
}
