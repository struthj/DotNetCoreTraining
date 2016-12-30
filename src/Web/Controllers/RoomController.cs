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
    public class RoomController : Controller
    {
        private readonly BusinessProContext _context;
        public RoomController(BusinessProContext context)
        {
            _context = context;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var items = _context.Rooms;

            return View(items.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }
        //POST Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Room Room)
        {
            if (ModelState.IsValid)
            {
                _context.Rooms.Add(Room);
                _context.SaveChanges();
                return RedirectToAction(nameof(Edit), new { id = Room.Id });

            }
            return View(Room);
        }

        // GET: Rooms/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Room room = _context.Rooms.Find(id);
            if (room == null)
            {
                return BadRequest();
            }
            ViewBag.DepartmentList = new SelectList(_context.Departments.ToList(), "Id", "Name");
            return View(room);
        }

        [HttpPost]
        public IActionResult Edit(Room room)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(room).State = EntityState.Modified;

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.DepartmentList = new SelectList(_context.Departments.ToList(), "Id", "Name");
            return View(room);
        }


        // GET: Rooms/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Room room = _context.Rooms.Find(id);
            //Left off here check if Room.Departments is null before loading to avoid exception
            _context.Entry(room).Collection(i => i.Departments).Load();
          
           
            if (room == null)
            {
                return BadRequest();
            }

            return View(room);
        }

        //Room/CreateDepartment
        //To create department
        public IActionResult CreateDepartment(int roomId, int deptId)
        {
            Room room = _context.Rooms.Find(roomId);
            Department department = _context.Departments.Find(deptId);

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
            return View(room);
        }

        // GET: Room/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Room room = _context.Rooms.Find(id);
            if (room == null)
            {
                return BadRequest();
            }
            return View(room);
        }

        // POST: Room/Delete/5
        [HttpPost]
        public IActionResult Delete(int id)
        {
            Room room = _context.Rooms.Find(id);
            _context.Entry(room).Collection(i => i.Departments).Load();
            _context.Rooms.Remove(room);
            _context.Entry(room).State = EntityState.Deleted;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
