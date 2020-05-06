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
    public class CriminalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CriminalsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> AddOrganisation(int criminal_id)
        {
            var organisations = await _context.Organisation.Include(c => c.Members).ToListAsync();
            var viewModels =
                organisations.Select(item => new OrganisationViewModel()
                {
                    Organisation = item,
                    IsSelected = item.Members.Any(item => item.CriminalId == criminal_id)
                }).ToList();

            return View(new AddOrganisationViewModel() { Organisations = viewModels, CriminalId = criminal_id });
        }
        public async Task<IActionResult> OrgChangeStatus(int org_id, int criminal_id, bool status)
        {
            var criminal = _context.Criminal.Include(c => c.Organisations).FirstOrDefault(item => item.Id == criminal_id);
            if (criminal != null)
            {
                if (status)
                {
                    var organisation = new CriminalOrganisation() { OrganisationId = org_id, CriminalId = criminal_id };
                    criminal.Organisations.Add(organisation);
                    criminal.OrganisationsAsString += $"{organisation.OrganisationId}, ";
                }
                else
                {
                    criminal.Organisations.Remove(criminal.Organisations.FirstOrDefault(item => item.OrganisationId == org_id));
                    criminal.OrganisationsAsString = criminal.OrganisationsAsString.Replace($"{org_id}, ", "");

                }
                await _context.SaveChangesAsync();
                return RedirectToAction("AddOrganisation", new { criminal_id = criminal_id });
            }

            return NotFound();

        }
        public async Task<IActionResult> AddCrime(int criminal_id)
        {
            var crimes = await _context.Crime.Include(c => c.Members).ToListAsync();
            var viewModels = 
                crimes.Select(item => new CrimeViewModel()
                {
                    Crime = item, 
                    IsSelected = item.Members.Any(item => item.CriminalId == criminal_id)
                }).ToList();

            return View(new AddCrimeViewModel() {Crimes = viewModels, CriminalId = criminal_id });
        }

        public async Task<IActionResult> ChangeStatus(int crime_id, int criminal_id, bool status)
        {
            var criminal = _context.Criminal.Include(c => c.Crimes).FirstOrDefault(item => item.Id == criminal_id);
            if (criminal != null)
            {
                if (status)
                {
                    var crime = new CrimeCriminal() {CrimeId = crime_id, CriminalId = criminal_id};
                    criminal.Crimes.Add(crime);
                    criminal.CrimesAsString += $"{crime.CrimeId}, ";
                }
                else
                {
                    criminal.Crimes.Remove(criminal.Crimes.FirstOrDefault(item => item.CrimeId == crime_id));
                    criminal.CrimesAsString = criminal.CrimesAsString.Replace($"{crime_id}, ", "");
                    
                }
                await _context.SaveChangesAsync(); 
                return RedirectToAction("AddCrime", new { criminal_id = criminal_id });
            }

            return NotFound();

        }
        // GET: Criminals
        public async Task<IActionResult> Index(string searchString)
        {
            
            var criminal = from s in _context.Criminal
                   select s;
             if (!String.IsNullOrEmpty(searchString))
            {
                var sS = searchString.Split(' ');
                foreach (var parameter in sS)
                {
                    criminal = criminal.Where(s => s.LastName.Contains(parameter)
                                        || s.FirstName.Contains(parameter)
                                        || s.NickName.Contains(parameter)
                                        || s.ExtraSigns.Contains(parameter)
                                        || s.HairColor.Contains(parameter)
                                        || s.EyeColor.Contains(parameter)
                                        || s.Age.ToString().Contains(parameter)
                                        || s.Height.ToString().Contains(parameter)
                                        || s.Sex.Contains(parameter));
                }
            }
                return View(await criminal.ToListAsync());
        }
        public async Task<IActionResult> Archived(string searchString)
        {

            var criminal = from s in _context.Criminal
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                var sS = searchString.Split(' ');
                foreach (var parameter in sS)
                {
                    criminal = criminal.Where(s => s.LastName.Contains(parameter)
                                        || s.FirstName.Contains(parameter)
                                        || s.NickName.Contains(parameter)
                                        || s.ExtraSigns.Contains(parameter)
                                        || s.HairColor.Contains(parameter)
                                        || s.EyeColor.Contains(parameter)
                                        || s.Age.ToString().Contains(parameter)
                                        || s.Height.ToString().Contains(parameter)
                                        || s.Sex.Contains(parameter));
                }
            }
            return View(await criminal.ToListAsync());
        }
        // GET: Criminals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var criminal = await _context.Criminal
                .FirstOrDefaultAsync(m => m.Id == id);
            if (criminal == null)
            {
                return NotFound();
            }

            return View(criminal);
        }

        // GET: Criminals/Create
        public IActionResult Create()
        {
            return View();
        }
        // POST: Criminals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,NickName,Age,Height,BirthDate,BirthPlace,Sex,HairColor,EyeColor,ExtraSigns,LastAccomodation")] Criminal criminal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(criminal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(criminal);
        }
        
        // GET: Criminals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var criminal = await _context.Criminal.FindAsync(id);
            if (criminal == null)
            {
                return NotFound();
            }
            return View(criminal);
        }

        // POST: Criminals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,NickName,Age,Height,BirthDate,BirthPlace,Sex,HairColor,EyeColor,ExtraSigns,LastAccomodation")] Criminal criminal)
        {
            if (id != criminal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(criminal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CriminalExists(criminal.Id))
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
            return View(criminal);
        }

        // GET: Criminals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var criminal = await _context.Criminal
                .FirstOrDefaultAsync(m => m.Id == id);
            if (criminal == null)
            {
                return NotFound();
            }

            return View(criminal);
        }

        // POST: Criminals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var criminal = await _context.Criminal.FindAsync(id);
            _context.Criminal.Remove(criminal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Archive(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var criminal = await _context.Criminal
                .FirstOrDefaultAsync(m => m.Id == id);
            if (criminal == null)
            {
                return NotFound();
            }

            return View(criminal);
        }

        // POST: Criminals/Delete/5
        [HttpPost, ActionName("Archive")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ArchiveConfirmed(int? id)
        {
            var criminal = await _context.Criminal.FindAsync(id);
            criminal.IsArchived= !criminal.IsArchived;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CriminalExists(int id)
        {
            return _context.Criminal.Any(e => e.Id == id);
        }
    }
}
