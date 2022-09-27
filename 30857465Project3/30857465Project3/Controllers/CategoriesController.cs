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
    public class CategoriesController : Controller
    {
        //private readonly ConnectedOfficeContext _context;
        private readonly ICategoriesRepository _categoryRepository;
        

        //public CategoriesController(ConnectedOfficeContext context)
        //{
            //_context = context;
        //}

        public CategoriesController(ICategoriesRepository categoryRepository) 
        {
            _categoryRepository = categoryRepository;
        }

        
        // TO DO: Add ‘Delete’
       


        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View(_categoryRepository.GetAll());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var category = await _context.Category.FirstOrDefaultAsync(m => m.CategoryId == id);
            var category = _categoryRepository.GetById((Guid)id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,CategoryDescription,DateCreated")] Category category)
        {
            if (ModelState.IsValid)
            {
                category.CategoryId = Guid.NewGuid();
                _categoryRepository.Add(category);
                _categoryRepository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoryRepository.FindAsync((Guid)id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CategoryId,CategoryName,CategoryDescription,DateCreated")] Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _categoryRepository.Update(category);
                    _categoryRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryId))
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
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var category = await _context.Category.FirstOrDefaultAsync(m => m.CategoryId == id);
            var category = _categoryRepository.GetById((Guid)id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            //var category = await _context.Category.FindAsync(id);
            var category = _categoryRepository.FindAsync((Guid)id);
            _categoryRepository.Remove(category);
            _categoryRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(Guid id)
        {
            return _categoryRepository.CategoryExists(id);
        }
    }
}
