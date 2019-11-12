/**
 * Project: PROG3050 - Virtual Game store Team Project
 * Purpose: favorites service
 * 
 * Created by Team on 2019.10.31
 * 
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VirtualGameStore.Models;

namespace VirtualGameStore.Controllers
{
    public class FavoritesController : Controller
    {
        private readonly PROG3050Context _context;
        private IHttpContextAccessor _HttpContextAccessor;

        public FavoritesController(PROG3050Context context, IHttpContextAccessor HttpContextAccessor)
        {
            _context = context;
            _HttpContextAccessor = HttpContextAccessor;
        }

        // GET: Favorites
        public async Task<IActionResult> Index()
        {
            string userIdString = _HttpContextAccessor.HttpContext.Session.GetString("Userid");
            int userId;

            bool parsed = int.TryParse(userIdString, out userId);
            if (!parsed)
            {
                userId = 0;
            }

            var pROG3050Context = _context.Favorite.Include(f => f.Category).Include(f => f.Favorit).Include(f => f.Platform).Where(f => f.Userid == userId);
            return View(await pROG3050Context.ToListAsync());
        }

        // GET: Favorites/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favorite = await _context.Favorite
                .Include(f => f.Category)
                .Include(f => f.Favorit)
                .Include(f => f.Platform)
                .FirstOrDefaultAsync(m => m.Favoritid == id);
            if (favorite == null)
            {
                return NotFound();
            }

            return View(favorite);
        }

        // GET: Favorites/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = _HttpContextAccessor.HttpContext.Session.GetString("Userid");
            ViewData["Categoryid"] = new SelectList(_context.Category, "Categoryid", "Categoriname");
            ViewData["Favoritid"] = new SelectList(_context.User, "Userid", "DisplayName");
            ViewData["Platformid"] = new SelectList(_context.Platform, "Platformid", "Platformname");
            return View();
        }

        // POST: Favorites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Favoritid,Userid,Categoryid,Platformid,CreatedDatetime,CreatedUserid,UpdatedDatetime,UpdatedUserid")] Favorite favorite)
        {
            if (ModelState.IsValid)
            {
                favorite.CreatedDatetime = DateTime.Now;
                favorite.UpdatedDatetime = DateTime.Now;
                _context.Add(favorite);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Categoryid"] = new SelectList(_context.Category, "Categoryid", "Categoriname", favorite.Categoryid);
            ViewData["Favoritid"] = new SelectList(_context.User, "Userid", "DisplayName", favorite.Favoritid);
            ViewData["Platformid"] = new SelectList(_context.Platform, "Platformid", "Platformname", favorite.Platformid);
            return View(favorite);
        }

        // GET: Favorites/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favorite = await _context.Favorite.FindAsync(id);
            if (favorite == null)
            {
                return NotFound();
            }
            ViewData["Categoryid"] = new SelectList(_context.Category, "Categoryid", "Categoriname", favorite.Categoryid);
            ViewData["Favoritid"] = new SelectList(_context.User, "Userid", "DisplayName", favorite.Favoritid);
            ViewData["Platformid"] = new SelectList(_context.Platform, "Platformid", "Platformname", favorite.Platformid);
            return View(favorite);
        }

        // POST: Favorites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Favoritid,Userid,Categoryid,Platformid,CreatedDatetime,CreatedUserid,UpdatedDatetime,UpdatedUserid")] Favorite favorite)
        {
            if (id != favorite.Favoritid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    favorite.UpdatedDatetime = DateTime.Now;
                    _context.Update(favorite);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FavoriteExists(favorite.Favoritid))
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
            ViewData["Categoryid"] = new SelectList(_context.Category, "Categoryid", "Categoriname", favorite.Categoryid);
            ViewData["Favoritid"] = new SelectList(_context.User, "Userid", "DisplayName", favorite.Favoritid);
            ViewData["Platformid"] = new SelectList(_context.Platform, "Platformid", "Platformname", favorite.Platformid);
            return View(favorite);
        }

        // GET: Favorites/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favorite = await _context.Favorite
                .Include(f => f.Category)
                .Include(f => f.Favorit)
                .Include(f => f.Platform)
                .FirstOrDefaultAsync(m => m.Favoritid == id);
            if (favorite == null)
            {
                return NotFound();
            }

            return View(favorite);
        }

        // POST: Favorites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var favorite = await _context.Favorite.FindAsync(id);
            _context.Favorite.Remove(favorite);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Remove(decimal id)
        {
            var favorite = await _context.Favorite.FindAsync(id);
            _context.Favorite.Remove(favorite);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FavoriteExists(decimal id)
        {
            return _context.Favorite.Any(e => e.Favoritid == id);
        }
    }
}
