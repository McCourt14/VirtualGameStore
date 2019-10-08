﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VirtualGameStore.Models;

namespace VirtualGameStore.Controllers
{
    public class WishlistsController : Controller
    {
        private readonly PROG3050Context _context;

        public WishlistsController(PROG3050Context context)
        {
            _context = context;
        }

        // GET: Wishlists
        public async Task<IActionResult> Index()
        {
            var pROG3050Context = _context.Wishlist.Include(w => w.Game).Include(w => w.User);
            return View(await pROG3050Context.ToListAsync());
        }

        // GET: Wishlists/Details/5
        public async Task<IActionResult> Details(decimal? id)
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

        // GET: Wishlists/Create
        public IActionResult Create()
        {
            ViewData["Gameid"] = new SelectList(_context.Game, "Gameid", "Title");
            ViewData["Userid"] = new SelectList(_context.User, "Userid", "Usertype");
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
                _context.Add(wishlist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Gameid"] = new SelectList(_context.Game, "Gameid", "Title", wishlist.Gameid);
            ViewData["Userid"] = new SelectList(_context.User, "Userid", "Usertype", wishlist.Userid);
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
            ViewData["Userid"] = new SelectList(_context.User, "Userid", "Usertype", wishlist.Userid);
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
            ViewData["Userid"] = new SelectList(_context.User, "Userid", "Usertype", wishlist.Userid);
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

        private bool WishlistExists(decimal id)
        {
            return _context.Wishlist.Any(e => e.Wishlistid == id);
        }
    }
}