/**
 * Project: PROG3050 - Virtual Game store Team Project
 * Purpose: game rates service
 * 
 * Created by Team on 2019.10.31
 * 
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VirtualGameStore.Models;

namespace VirtualGameStore.Controllers
{
    public class GameratesController : Controller
    {
        private readonly PROG3050Context _context;

        public GameratesController(PROG3050Context context)
        {
            _context = context;
        }

        // GET: Gamerates
        public async Task<IActionResult> Index()
        {
            var pROG3050Context = _context.Gamerates.Include(g => g.Game).Include(g => g.User);
            return View(await pROG3050Context.ToListAsync());
        }

        // GET: Gamerates/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamerates = await _context.Gamerates
                .Include(g => g.Game)
                .Include(g => g.User)
                .FirstOrDefaultAsync(m => m.Rateid == id);
            if (gamerates == null)
            {
                return NotFound();
            }

            return View(gamerates);
        }

        // GET: Gamerates/Create
        public IActionResult Create()
        {
            ViewData["Gameid"] = new SelectList(_context.Game, "Gameid", "Title");
            ViewData["Userid"] = new SelectList(_context.User, "Userid", "DisplayName");
            return View();
        }

        // POST: Gamerates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Rateid,Userid,Gameid,Rates,Description,CreatedDatetime,CreatedUserid,UpdatedDatetime,UpdatedUserid")] Gamerates gamerates)
        {
            if (ModelState.IsValid)
            {
                gamerates.CreatedDatetime = DateTime.Now;
                gamerates.UpdatedDatetime = DateTime.Now;
                _context.Add(gamerates);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Gameid"] = new SelectList(_context.Game, "Gameid", "Title", gamerates.Gameid);
            ViewData["Userid"] = new SelectList(_context.User, "Userid", "DisplayName", gamerates.Userid);
            return View(gamerates);
        }

        // GET: Gamerates/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamerates = await _context.Gamerates.FindAsync(id);
            if (gamerates == null)
            {
                return NotFound();
            }
            ViewData["Gameid"] = new SelectList(_context.Game, "Gameid", "Title", gamerates.Gameid);
            ViewData["Userid"] = new SelectList(_context.User, "Userid", "DisplayName", gamerates.Userid);
            return View(gamerates);
        }

        // POST: Gamerates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Rateid,Userid,Gameid,Rates,Description,CreatedDatetime,CreatedUserid,UpdatedDatetime,UpdatedUserid")] Gamerates gamerates)
        {
            if (id != gamerates.Rateid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    gamerates.UpdatedDatetime = DateTime.Now;
                    _context.Update(gamerates);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameratesExists(gamerates.Rateid))
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
            ViewData["Gameid"] = new SelectList(_context.Game, "Gameid", "Title", gamerates.Gameid);
            ViewData["Userid"] = new SelectList(_context.User, "Userid", "DisplayName", gamerates.Userid);
            return View(gamerates);
        }

        // GET: Gamerates/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamerates = await _context.Gamerates
                .Include(g => g.Game)
                .Include(g => g.User)
                .FirstOrDefaultAsync(m => m.Rateid == id);
            if (gamerates == null)
            {
                return NotFound();
            }

            return View(gamerates);
        }

        // POST: Gamerates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var gamerates = await _context.Gamerates.FindAsync(id);
            _context.Gamerates.Remove(gamerates);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameratesExists(decimal id)
        {
            return _context.Gamerates.Any(e => e.Rateid == id);
        }
    }
}
