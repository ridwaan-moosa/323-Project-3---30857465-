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
    public class DevicesController : Controller
    {
        //private readonly ConnectedOfficeContext _context;
        private readonly IDeviceRepository _deviceRepository;

        //public DevicesController(ConnectedOfficeContext context)
        //{
            //_context = context;
        //}
        public DevicesController(IDeviceRepository deviceRepository)
        {
           _deviceRepository = deviceRepository;
        }

        
        
        // TO DO: Add ‘Delete’
        


        // GET: Devices
        public async Task<IActionResult> Index()
        {
            //var connectedOfficeContext = _context.Device.Include(d => d.Category).Include(d => d.Zone);
            
            return View( _deviceRepository.GetAll());
        }

        // GET: Devices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var device = await _context.Device.Include(d => d.Category).Include(d => d.Zone).FirstOrDefaultAsync(m => m.DeviceId == id);
            var device = _deviceRepository.GetById((Guid)id);
            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        // GET: Devices/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_deviceRepository.Category(), "CategoryId", "CategoryName");
            ViewData["ZoneId"] = new SelectList(_deviceRepository.Zone(), "ZoneId", "ZoneName");
            return View();
        }

        // POST: Devices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeviceId,DeviceName,CategoryId,ZoneId,Status,IsActive,DateCreated")] Device device)
        {
            if (ModelState.IsValid)
            {
                device.DeviceId = Guid.NewGuid();
                _deviceRepository.Add(device);
                _deviceRepository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_deviceRepository.Category(), "CategoryId", "CategoryName", device.CategoryId);
            ViewData["ZoneId"] = new SelectList(_deviceRepository.Zone(), "ZoneId", "ZoneName", device.ZoneId);
            return View(device);
        }

        // GET: Devices/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = _deviceRepository.FindAsync((Guid)id);
            if (device == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_deviceRepository.Category(), "CategoryId", "CategoryName", device.CategoryId);
            ViewData["ZoneId"] = new SelectList(_deviceRepository.Zone(), "ZoneId", "ZoneName", device.ZoneId);
            return View(device);
        }

        // POST: Devices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("DeviceId,DeviceName,CategoryId,ZoneId,Status,IsActive,DateCreated")] Device device)
        {
            if (id != device.DeviceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _deviceRepository.Update(device);
                    _deviceRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeviceExists(device.DeviceId))
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
            ViewData["CategoryId"] = new SelectList(_deviceRepository.Category(), "CategoryId", "CategoryName", device.CategoryId);
            ViewData["ZoneId"] = new SelectList(_deviceRepository.Zone(), "ZoneId", "ZoneName", device.ZoneId);
            return View(device);
        }

        // GET: Devices/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var device = await _context.Device.Include(d => d.Category).Include(d => d.Zone).FirstOrDefaultAsync(m => m.DeviceId == id);
            var device = _deviceRepository.GetById((Guid)id);
            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        // POST: Devices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var device = _deviceRepository.FindAsync(id);
            _deviceRepository.Remove(device);
            _deviceRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeviceExists(Guid id)
        {
            return _deviceRepository.DeviceExists(id);
        }
    }
}
