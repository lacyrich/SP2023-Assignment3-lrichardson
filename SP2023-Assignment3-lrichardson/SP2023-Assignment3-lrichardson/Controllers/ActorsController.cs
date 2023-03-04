using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SP2023_Assignment3_lrichardson.Data;
using SP2023_Assignment3_lrichardson.Models;

namespace SP2023_Assignment3_lrichardson.Controllers
{
    public class ActorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Actors
        public async Task<IActionResult> Index()
        {
              return View(await _context.Actors.ToListAsync());
        }

        // GET: Actors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Actors == null)
            {
                return NotFound();
            }

            var actors = await _context.Actors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actors == null)
            {
                return NotFound();
            }

            return View(actors);
        }

        

        // GET: Actors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Actors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> GetActorPhoto(int id)
        {
            var actors = await _context.Actors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actors == null)
            {
                return NotFound();
            }
            var imageData = actors.Photo;
            return File(imageData, "image/jpg");
        }
        public async Task<IActionResult> Create([Bind("Id,Name,Gender,Age,IMDBHyperlink,Photo")] Actors actors, IFormFile Photo)
        {

            if (ModelState.IsValid)
            {
                if (Photo != null && Photo.Length > 0)
                {
                    var memoryStream = new MemoryStream();
                    await Photo.CopyToAsync(memoryStream);
                    actors.Photo = memoryStream.ToArray();
                }
                _context.Add(actors);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(actors);
        }

        // GET: Actors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Actors == null)
            {
                return NotFound();
            }

            var actors = await _context.Actors.FindAsync(id);
            if (actors == null)
            {
                return NotFound();
            }
            return View(actors);
        }

        // POST: Actors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Gender,Age,IMDBHyperlink,Photo")] Actors actors,  IFormFile Photo)
        {
            if (id != actors.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Photo != null && Photo.Length > 0)
                    {
                        var memoryStream = new MemoryStream();
                        await Photo.CopyToAsync(memoryStream);
                        actors.Photo = memoryStream.ToArray();
                    }
                    _context.Update(actors);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActorsExists(actors.Id))
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
            return View(actors);
        }

        // GET: Actors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Actors == null)
            {
                return NotFound();
            }

            var actors = await _context.Actors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actors == null)
            {
                return NotFound();
            }

            return View(actors);
        }

        // POST: Actors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Actors == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Actors'  is null.");
            }
            var actors = await _context.Actors.FindAsync(id);
            if (actors != null)
            {
                _context.Actors.Remove(actors);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActorsExists(int id)
        {
          return _context.Actors.Any(e => e.Id == id);
        }
    }
}
