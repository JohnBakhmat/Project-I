using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CourseWork.Models;
using CourseWork.Data;

namespace CourseWork.Controllers
{
    public class CrimesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CrimesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Crimes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Crime.ToListAsync());
        }

        // GET: Crimes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crime = await _context.Crime
                .FirstOrDefaultAsync(m => m.Id == id);
            if (crime == null)
            {
                return NotFound();
            }

            return View(crime);
        }

        // GET: Crimes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Crimes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,Name,Punishment")] Crime crime)
        {
            if (ModelState.IsValid)
            {
                _context.Add(crime);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(crime);
        }

        // GET: Crimes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crime = await _context.Crime.FindAsync(id);
            if (crime == null)
            {
                return NotFound();
            }
            return View(crime);
        }

        // POST: Crimes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Name,Punishment")] Crime crime)
        {
            if (id != crime.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(crime);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CrimeExists(crime.Id))
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
            return View(crime);
        }

        // GET: Crimes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crime = await _context.Crime
                .FirstOrDefaultAsync(m => m.Id == id);
            if (crime == null)
            {
                return NotFound();
            }

            return View(crime);
        }

        // POST: Crimes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var crime = await _context.Crime.FindAsync(id);
            _context.Crime.Remove(crime);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CrimeExists(int id)
        {
            return _context.Crime.Any(e => e.Id == id);
        }
    }
}
