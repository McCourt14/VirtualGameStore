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
    public class EventgamesController : Controller
    {
        private readonly PROG3050Context _context;

        public EventgamesController(PROG3050Context context)
        {
            _context = context;
        }

        // GET: Eventgames
        public async Task<IActionResult> Index()
        {
            var pROG3050Context = _context.Eventgame.Include(e => e.Event).Include(e => e.Game);
            return View(await pROG3050Context.ToListAsync());
        }

        // GET: Eventgames/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventgame = await _context.Eventgame
                .Include(e => e.Event)
                .Include(e => e.Game)
                .FirstOrDefaultAsync(m => m.Eventgameid == id);
            if (eventgame == null)
            {
                return NotFound();
            }

            return View(eventgame);
        }

        // GET: Eventgames/Create
        public IActionResult Create()
        {
            ViewData["Eventid"] = new SelectList(_context.Event, "Eventid", "Eventname");
            ViewData["Gameid"] = new SelectList(_context.Game, "Gameid", "Title");
            return View();
        }

        // POST: Eventgames/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Eventgameid,Eventid,Gameid,DiscountRate,CreatedDatetime,CreatedUserid,UpdatedDatetime,UpdatedUserid")] Eventgame eventgame)
        {
            if (ModelState.IsValid)
            {
                eventgame.CreatedDatetime = DateTime.Now;
                eventgame.UpdatedDatetime = DateTime.Now;
                _context.Add(eventgame);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Eventid"] = new SelectList(_context.Event, "Eventid", "Eventname", eventgame.Eventid);
            ViewData["Gameid"] = new SelectList(_context.Game, "Gameid", "Title", eventgame.Gameid);
            return View(eventgame);
        }

        // GET: Eventgames/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventgame = await _context.Eventgame.FindAsync(id);
            if (eventgame == null)
            {
                return NotFound();
            }
            ViewData["Eventid"] = new SelectList(_context.Event, "Eventid", "Eventname", eventgame.Eventid);
            ViewData["Gameid"] = new SelectList(_context.Game, "Gameid", "Title", eventgame.Gameid);
            return View(eventgame);
        }

        // POST: Eventgames/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Eventgameid,Eventid,Gameid,DiscountRate,CreatedDatetime,CreatedUserid,UpdatedDatetime,UpdatedUserid")] Eventgame eventgame)
        {
            if (id != eventgame.Eventgameid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    eventgame.UpdatedDatetime = DateTime.Now;
                    _context.Update(eventgame);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventgameExists(eventgame.Eventgameid))
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
            ViewData["Eventid"] = new SelectList(_context.Event, "Eventid", "Eventname", eventgame.Eventid);
            ViewData["Gameid"] = new SelectList(_context.Game, "Gameid", "Title", eventgame.Gameid);
            return View(eventgame);
        }

        // GET: Eventgames/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventgame = await _context.Eventgame
                .Include(e => e.Event)
                .Include(e => e.Game)
                .FirstOrDefaultAsync(m => m.Eventgameid == id);
            if (eventgame == null)
            {
                return NotFound();
            }

            return View(eventgame);
        }

        // POST: Eventgames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var eventgame = await _context.Eventgame.FindAsync(id);
            _context.Eventgame.Remove(eventgame);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventgameExists(decimal id)
        {
            return _context.Eventgame.Any(e => e.Eventgameid == id);
        }
    }
}
