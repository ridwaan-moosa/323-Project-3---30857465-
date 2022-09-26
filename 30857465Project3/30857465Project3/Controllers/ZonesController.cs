using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _30857465Project3.Data;
using _30857465Project3.Models;
using Microsoft.AspNetCore.Authorization;
using _30857465Project3.Repository;

namespace _30857465Project3.Controllers
{
    [Authorize]
    public class ZonesController : Controller
    {
       // private readonly ConnectedOfficeContext _context;
        private readonly IZoneRepository _zoneRepository;

        //public ZonesController(ConnectedOfficeContext context)
       // {
           // _context = context;
       // }

        public ZonesController(IZoneRepository zoneRepository)
        {
            _zoneRepository = zoneRepository;
        }

        // TO DO: Add ‘Delete’

        // GET: Zones
        public async Task<IActionResult> Index()
        {
            return View( _zoneRepository.GetAll()); 
        }

        // GET: Zones/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var zone = _zoneRepository.GetById((Guid)id); 
            
            if (zone == null)
            {
                return NotFound();
            }

            return View(zone);
        }

        // GET: Zones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Zones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ZoneId,ZoneName,ZoneDescription,DateCreated")] Zone zone)
        {
            if (ModelState.IsValid)
            {
                zone.ZoneId = Guid.NewGuid();
                _zoneRepository.Add(zone);
                //_context.Add(zone);
                _zoneRepository.SaveChangesAsync();
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(zone);
        }

        // GET: Zones/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zone = _zoneRepository.GetById((Guid)id);
            if (zone == null)
            {
                return NotFound();
            }
            return View(zone);
        }

        // POST: Zones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ZoneId,ZoneName,ZoneDescription,DateCreated")] Zone zone)
        {
            if (id != zone.ZoneId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _zoneRepository.Add(zone);
                    _zoneRepository.Update(zone);
                    _zoneRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZoneExists(zone.ZoneId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(zone);
        }

        // GET: Zones/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //var zone = await _context.Zone.FirstOrDefaultAsync(m => m.ZoneId == id);
           
            var zone = _zoneRepository.GetById((Guid)id);
            if (zone == null)
            {
                return NotFound();
            }

            return View(zone);
        }

        // POST: Zones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var zone = _zoneRepository.GetById((Guid)id);
            //var zone = await _context.Zone.FindAsync(id);
            _zoneRepository.Remove(zone);
            _zoneRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZoneExists(Guid id)
        {
            return _zoneRepository.ZoneExists(id);
            //return _context.Zone.Any(e => e.ZoneId == id);
        }
    }
}
