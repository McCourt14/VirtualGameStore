/**
 * Project: PROG3050 - Virtual Game store Team Project
 * Purpose: event game service
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
            var pROG3050Context = _context.Eventgame.Include(e => e.Event);
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

            return View(eventgame);
        }

        public async Task<IActionResult> Register(decimal userID,decimal eventID)
        {
            Eventgame eventgame = new Eventgame();

            eventgame.Eventid = eventID;
            eventgame.userid = userID;

            if (ModelState.IsValid)
            {
                eventgame.CreatedDatetime = DateTime.Now;
                eventgame.UpdatedDatetime = DateTime.Now;
                _context.Add(eventgame);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), "Events");
            }

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
