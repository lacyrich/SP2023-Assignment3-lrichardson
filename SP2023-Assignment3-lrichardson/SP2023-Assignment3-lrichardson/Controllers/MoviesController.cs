using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SP2023_Assignment3_lrichardson.Data;
using SP2023_Assignment3_lrichardson.Models;
using Tweetinvi;
using Tweetinvi.Core.Injectinvi;
using VaderSharp2;

namespace SP2023_Assignment3_lrichardson.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
              return View(await _context.Movies.ToListAsync());
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movies = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movies == null)
            {
                return NotFound();
            }

            ActorsMoviesVM actorsMoviesVM = new ActorsMoviesVM();
            actorsMoviesVM.Movies = movies;
            actorsMoviesVM.MoviesActors = _context.MoviesActors.Where(m => m.MovieID == movies.Id).Include(m => m.Movies).Include(m => m.Actors).ToList();

            var userClient = new TwitterClient("AAx9UfdCemph0Pg0t8Moq5c6L", "LbhoERpFGjBESYSNjTHuRvE0R80cGxZBx5lJWanM5lFpO2Hs63", "1455230009153503238-WTxQgoYUAQ3D9PTSsUu8stHkmJvuVe", "2ZVnM9tWbCSNAhyJcyC4WPIgiIbUWZ77MTLSx2Qb8TkW3");
            var searchResponse = await userClient.SearchV2.SearchTweetsAsync(movies.MovieTitle);
            var tweets = searchResponse.Tweets;
            var analyzer = new SentimentIntensityAnalyzer();

            List<Tuple<string, double>> topTweets = new List<Tuple<string, double>>();
            Double tweetTotal = 0;

            for (int i = 0; i < Math.Min(tweets.Length, 100); i++)
            {
                var results = analyzer.PolarityScores(tweets[i].Text);
                tweetTotal += results.Compound;
                topTweets.Add(Tuple.Create(tweets[i].Text, results.Compound));
            }

            ViewBag.CompoundScore = tweetTotal / topTweets.Count;
            ViewBag.TopTweets = topTweets.OrderByDescending(t => t.Item2).ToList();

            return View(actorsMoviesVM);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MovieTitle,IMDBHyperlink,Genre,ReleaseYear,Poster")] Movies movies, IFormFile Poster)
        {
            if (ModelState.IsValid)
            {
                if (Poster != null && Poster.Length > 0)
                {
                    var memoryStream = new MemoryStream();
                    await Poster.CopyToAsync(memoryStream);
                    movies.Poster = memoryStream.ToArray();
                }
                _context.Add(movies);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movies);
        }

        public async Task<IActionResult> GetMoviePoster(int id)
        {
            var movies = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movies == null)
            {
                return NotFound();
            }
            var imageData = movies.Poster;
            return File(imageData, "image/jpg");
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Movies == null)
            {

                return NotFound();
            }

            var movies = await _context.Movies.FindAsync(id);
            if (movies == null)
            {
                return NotFound();
            }
            return View(movies);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MovieTitle,IMDBHyperlink,Genre,ReleaseYear,Poster")] Movies movies, IFormFile Poster)
        {
            if (id != movies.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Poster != null && Poster.Length > 0)
                    {
                        var memoryStream = new MemoryStream();
                        await Poster.CopyToAsync(memoryStream);
                        movies.Poster = memoryStream.ToArray();
                    }
                    _context.Update(movies);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoviesExists(movies.Id))
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
            return View(movies);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movies = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movies == null)
            {
                return NotFound();
            }

            return View(movies);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Movies == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Movies'  is null.");
            }
            var movies = await _context.Movies.FindAsync(id);
            if (movies != null)
            {
                _context.Movies.Remove(movies);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MoviesExists(int id)
        {
          return _context.Movies.Any(e => e.Id == id);
        }
    }
}
