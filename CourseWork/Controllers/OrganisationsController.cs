using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CourseWork.Models;
using CourseWork.Data;

namespace Курсовая.Controllers
{
    public class OrganisationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrganisationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Organisations
        public async Task<IActionResult> Index()
        {
            return View(await _context.Organisation.ToListAsync());
        }

        // GET: Organisations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organisation = await _context.Organisation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (organisation == null)
            {
                return NotFound();
            }

            return View(organisation);
        }

        // GET: Organisations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Organisations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Boss")] Organisation organisation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(organisation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(organisation);
        }

        // GET: Organisations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organisation = await _context.Organisation.FindAsync(id);
            if (organisation == null)
            {
                return NotFound();
            }
            return View(organisation);
        }

        // POST: Organisations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Boss")] Organisation organisation)
        {
            if (id != organisation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(organisation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrganisationExists(organisation.Id))
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
            return View(organisation);
        }

        // GET: Organisations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organisation = await _context.Organisation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (organisation == null)
            {
                return NotFound();
            }

            return View(organisation);
        }

        // POST: Organisations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var organisation = await _context.Organisation.FindAsync(id);
            _context.Organisation.Remove(organisation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrganisationExists(int id)
        {
            return _context.Organisation.Any(e => e.Id == id);
        }
    }
}
