/**
 * Project: PROG3050 - Virtual Game store Team Project
 * Purpose: platform service
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
    public class PlatformsController : Controller
    {
        private readonly PROG3050Context _context;

        public PlatformsController(PROG3050Context context)
        {
            _context = context;
        }

        // GET: Platforms
        public async Task<IActionResult> Index()
        {
            return View(await _context.Platform.ToListAsync());
        }

        // GET: Platforms/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var platform = await _context.Platform
                .FirstOrDefaultAsync(m => m.Platformid == id);
            if (platform == null)
            {
                return NotFound();
            }

            return View(platform);
        }

        // GET: Platforms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Platforms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Platformid,Platformname,CreatedDatetime,CreatedUserid,UpdatedDatetime,UpdatedUserid")] Platform platform)
        {
            if (ModelState.IsValid)
            {
                platform.CreatedDatetime = DateTime.Now;
                platform.UpdatedDatetime = DateTime.Now;
                _context.Add(platform);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(platform);
        }

        // GET: Platforms/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var platform = await _context.Platform.FindAsync(id);
            if (platform == null)
            {
                return NotFound();
            }
            return View(platform);
        }

        // POST: Platforms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Platformid,Platformname,CreatedDatetime,CreatedUserid,UpdatedDatetime,UpdatedUserid")] Platform platform)
        {
            if (id != platform.Platformid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    platform.UpdatedDatetime = DateTime.Now;
                    _context.Update(platform);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlatformExists(platform.Platformid))
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
            return View(platform);
        }

        // GET: Platforms/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var platform = await _context.Platform
                .FirstOrDefaultAsync(m => m.Platformid == id);
            if (platform == null)
            {
                return NotFound();
            }

            return View(platform);
        }

        // POST: Platforms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var platform = await _context.Platform.FindAsync(id);
            _context.Platform.Remove(platform);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlatformExists(decimal id)
        {
            return _context.Platform.Any(e => e.Platformid == id);
        }
    }
}
