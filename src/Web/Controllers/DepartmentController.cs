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

namespace Web.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly BusinessProContext _context;
        public DepartmentController(BusinessProContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var items = _context.Departments;
            return View(items.ToList());
        }


        public IActionResult Create()
        {
            return View();
        }
        //POST Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Departments.Add(department);
                _context.SaveChanges();
                return RedirectToAction(nameof(Edit), new { id = department.Id });

            }
            return View();
        }

        // GET: Rooms/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Department department = _context.Departments.Find(id);
            if (department == null)
            {
                return BadRequest();
            }
            ViewBag.RoomList = new SelectList(_context.Rooms.ToList(), "Id", "Name");

            return View(department);
        }

        [HttpPost]
        public IActionResult Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(department).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.RoomList = new SelectList(_context.Rooms.ToList(), "Id", "Name");
            return View(department);
        }

        //Department/AddRoom
        //To add department to room
        public IActionResult AddRoom(int? deptId, int? roomId)
        {
            Room room = _context.Rooms.Find(roomId);
            Department department = _context.Departments.Find(deptId);
            _context.Entry(room).Collection(i => i.Departments).Load();
            if (room.Departments == null)
            {
                room.Departments = new List<Department>();
            }
            room.Departments.Add(department);
            if (ModelState.IsValid)
            {
                _context.Entry(room).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Rooms/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Department department = _context.Departments.Find(id);
            if (department == null)
            {
                return BadRequest();
            }
            return View(department);
        }
    }
}
