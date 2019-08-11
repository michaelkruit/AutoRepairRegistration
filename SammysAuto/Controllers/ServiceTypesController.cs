using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SammysAuto.Data;
using SammysAuto.Models;
using SammysAuto.Utility;

namespace SammysAuto.Controllers
{
    [Authorize(Roles = SD.AdminEndUser)]
    public class ServiceTypesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ServiceTypesController(ApplicationDbContext db)
        {
            _db = db;
        }

        //GET: ServiceTypes
        public IActionResult Index()
        {
            return View(_db.ServiceTypes.ToList());
        }

        //GET: Servicetypes/Create
        public IActionResult Create()
        {
            return View();
        }

        //POST: Services/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceType serviceType)
        {
            if (ModelState.IsValid)
            {
                _db.Add(serviceType);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(serviceType);
        }

        //Details: ServiceTypes/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ServiceType = await _db.ServiceTypes.SingleOrDefaultAsync(m => m.Id == id);
            if (ServiceType == null)
            {
                return NotFound();
            }
            return View(ServiceType);
        }

        //Edit: ServiceTypes/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ServiceType = await _db.ServiceTypes.SingleOrDefaultAsync(m => m.Id == id);
            if (ServiceType == null)
            {
                return NotFound();
            }
            return View(ServiceType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ServiceType serviceType)
        {
            if(id != serviceType.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Update(serviceType);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(serviceType);
        }

        //Details: ServiceTypes/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ServiceType = await _db.ServiceTypes.SingleOrDefaultAsync(m => m.Id == id);
            if (ServiceType == null)
            {
                return NotFound();
            }
            return View(ServiceType);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveServiceType(int id)
        {
            var serviceType = await _db.ServiceTypes.SingleOrDefaultAsync(st => st.Id == id);
            _db.Remove(serviceType);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
        }
    }
}