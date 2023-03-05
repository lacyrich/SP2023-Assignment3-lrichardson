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
    public class MoviesActorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MoviesActorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MoviesActors
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MoviesActors.Include(m => m.Actors).Include(m => m.Movies);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MoviesActors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MoviesActors == null)
            {
                return NotFound();
            }

            var moviesActors = await _context.MoviesActors
                .Include(m => m.Actors)
                .Include(m => m.Movies)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (moviesActors == null)
            {
                return NotFound();
            }

            return View(moviesActors);
        }

        // GET: MoviesActors/Create
        public IActionResult Create()
        {
            ViewData["ActorID"] = new SelectList(_context.Actors, "Id", "Name");
            ViewData["MovieID"] = new SelectList(_context.Movies, "Id", "MovieTitle");
            return View();
        }

        // POST: MoviesActors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ActorID,MovieID")] MoviesActors moviesActors)
        {
            if (ModelState.IsValid)
            {
                _context.Add(moviesActors);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActorID"] = new SelectList(_context.Actors, "Id", "Name", moviesActors.ActorID);
            ViewData["MovieID"] = new SelectList(_context.Movies, "Id", "MovieTitle", moviesActors.MovieID);
            return View(moviesActors);
        }

        // GET: MoviesActors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MoviesActors == null)
            {
                return NotFound();
            }

            var moviesActors = await _context.MoviesActors.FindAsync(id);
            if (moviesActors == null)
            {
                return NotFound();
            }
            ViewData["ActorID"] = new SelectList(_context.Actors, "Id", "Name", moviesActors.ActorID);
            ViewData["MovieID"] = new SelectList(_context.Movies, "Id", "MovieTitle", moviesActors.MovieID);
            return View(moviesActors);
        }

        // POST: MoviesActors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ActorID,MovieID")] MoviesActors moviesActors)
        {
            if (id != moviesActors.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(moviesActors);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoviesActorsExists(moviesActors.Id))
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
            ViewData["ActorID"] = new SelectList(_context.Actors, "Id", "Name", moviesActors.ActorID);
            ViewData["MovieID"] = new SelectList(_context.Movies, "Id", "MovieTitle", moviesActors.MovieID);
            return View(moviesActors);
        }

        // GET: MoviesActors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MoviesActors == null)
            {
                return NotFound();
            }

            var moviesActors = await _context.MoviesActors
                .Include(m => m.Actors)
                .Include(m => m.Movies)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (moviesActors == null)
            {
                return NotFound();
            }

            return View(moviesActors);
        }

        // POST: MoviesActors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MoviesActors == null)
            {
                return Problem("Entity set 'ApplicationDbContext.MoviesActors'  is null.");
            }
            var moviesActors = await _context.MoviesActors.FindAsync(id);
            if (moviesActors != null)
            {
                _context.MoviesActors.Remove(moviesActors);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MoviesActorsExists(int id)
        {
          return _context.MoviesActors.Any(e => e.Id == id);
        }
    }
}
