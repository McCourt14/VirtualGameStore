/**
 * Project: PROG3050 - Virtual Game store Team Project
 * Purpose: wishlist service
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
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VirtualGameStore.Models;

namespace VirtualGameStore.Controllers
{
    public class WishlistsController : Controller
    {
        private readonly PROG3050Context _context;
        private IHttpContextAccessor _HttpContextAccessor;

        public WishlistsController(PROG3050Context context, IHttpContextAccessor HttpContextAccessor)
        {
            _context = context;
            _HttpContextAccessor = HttpContextAccessor;
        }

        // GET: Wishlists
        public async Task<IActionResult> Index()
        {
            string userIdString = _HttpContextAccessor.HttpContext.Session.GetString("Userid");
            int userId;

            bool parsed = int.TryParse(userIdString, out userId);
            if (!parsed)
            {
                userId = 0;
            }

            var pROG3050Context = _context.Wishlist.Include(w => w.Game).Include(w => w.User).Where(w => w.Userid == userId);
            return View(await pROG3050Context.ToListAsync());
        }

        // GET: Wishlists/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = _HttpContextAccessor.HttpContext.Session.GetString("Userid");
            ViewData["Gameid"] = new SelectList(_context.Game, "Gameid", "Title");
            return View();
        }

        // POST: Wishlists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Wishlistid,Userid,Gameid,Access,CreatedDatetime,CreatedUserid,UpdatedDatetime,UpdatedUserid")] Wishlist wishlist)
        {
            if (ModelState.IsValid)
            {
                wishlist.CreatedDatetime = DateTime.Now;
                wishlist.UpdatedDatetime = DateTime.Now;
                _context.Add(wishlist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Gameid"] = new SelectList(_context.Game, "Gameid", "Title", wishlist.Gameid);
            ViewData["UserId"] = _HttpContextAccessor.HttpContext.Session.GetString("Userid");
            return View(wishlist);
        }

        // GET: Wishlists/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wishlist = await _context.Wishlist.FindAsync(id);
            if (wishlist == null)
            {
                return NotFound();
            }

            ViewData["Gameid"] = new SelectList(_context.Game, "Gameid", "Title", wishlist.Gameid);
            ViewData["UserId"] = _HttpContextAccessor.HttpContext.Session.GetString("Userid");
            return View(wishlist);
        }

        // POST: Wishlists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Wishlistid,Userid,Gameid,Access,CreatedDatetime,CreatedUserid,UpdatedDatetime,UpdatedUserid")] Wishlist wishlist)
        {
            if (id != wishlist.Wishlistid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    wishlist.UpdatedDatetime = DateTime.Now;
                    _context.Update(wishlist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WishlistExists(wishlist.Wishlistid))
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
            ViewData["Gameid"] = new SelectList(_context.Game, "Gameid", "Title", wishlist.Gameid);
            ViewData["Userid"] = new SelectList(_context.User, "Userid", "DisplayName", wishlist.Userid);
            return View(wishlist);
        }

        // GET: Wishlists/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wishlist = await _context.Wishlist
                .Include(w => w.Game)
                .Include(w => w.User)
                .FirstOrDefaultAsync(m => m.Wishlistid == id);
            if (wishlist == null)
            {
                return NotFound();
            }

            return View(wishlist);
        }

        // POST: Wishlists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var wishlist = await _context.Wishlist.FindAsync(id);
            _context.Wishlist.Remove(wishlist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Remove(decimal id)
        {
            var wishlist = await _context.Wishlist.FindAsync(id);
            _context.Wishlist.Remove(wishlist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WishlistExists(decimal id)
        {
            return _context.Wishlist.Any(e => e.Wishlistid == id);
        }
    }
}
