using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VirtualGameStore.Models;

namespace VirtualGameStore.Controllers
{
    public class GameController : Controller
    {
        private readonly PROG3050Context _context;
        private readonly UserManager<IdentityUser> _userManager;

        public GameController(PROG3050Context context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Game
        public async Task<IActionResult> Index()
        {
            IdentityUser identityUser = _userManager.GetUserAsync(User).Result;
            if (User.Identity.IsAuthenticated && _userManager.IsInRoleAsync(identityUser, "administrators").Result)
            {
                ViewBag.isAdmin = "true";
            }
            var pROG3050Context = _context.Game.Include(g => g.Category).Include(g => g.Company).Include(g => g.Platform);
            return View(await pROG3050Context.ToListAsync());
        }

        // GET: Game/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            IdentityUser identityUser = _userManager.GetUserAsync(User).Result;
            if (User.Identity.IsAuthenticated && _userManager.IsInRoleAsync(identityUser, "administrators").Result)
            {
                ViewBag.isAdmin = "true";
            }

            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game
                .Include(g => g.Category)
                .Include(g => g.Company)
                .Include(g => g.Platform)
                .FirstOrDefaultAsync(m => m.Gameid == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // GET: Game/Create
        public IActionResult Create()
        {
            IdentityUser identityUser = _userManager.GetUserAsync(User).Result;
            if (User.Identity.IsAuthenticated && _userManager.IsInRoleAsync(identityUser, "administrators").Result)
            {
                ViewBag.isAdmin = "true";
            }

            ViewData["Categoryid"] = new SelectList(_context.Category, "Categoryid", "Categoryid");
            ViewData["Companyid"] = new SelectList(_context.Company, "Companyid", "CompanyName");
            ViewData["Platformid"] = new SelectList(_context.Platform, "Platformid", "Platformid");
            return View();
        }

        // POST: Game/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Gameid,Title,Companyid,Price,LaunchDate,Platformid,Categoryid,Description,CreatedDatetime,CreatedUserid,UpdatedDatetime,UpdatedUserid")] Game game)
        {
            if (ModelState.IsValid)
            {
                _context.Add(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Categoryid"] = new SelectList(_context.Category, "Categoryid", "Categoryid", game.Categoryid);
            ViewData["Companyid"] = new SelectList(_context.Company, "Companyid", "CompanyName", game.Companyid);
            ViewData["Platformid"] = new SelectList(_context.Platform, "Platformid", "Platformid", game.Platformid);
            return View(game);
        }

        // GET: Game/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            ViewData["Categoryid"] = new SelectList(_context.Category, "Categoryid", "Categoryid", game.Categoryid);
            ViewData["Companyid"] = new SelectList(_context.Company, "Companyid", "CompanyName", game.Companyid);
            ViewData["Platformid"] = new SelectList(_context.Platform, "Platformid", "Platformid", game.Platformid);
            return View(game);
        }

        // POST: Game/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Gameid,Title,Companyid,Price,LaunchDate,Platformid,Categoryid,Description,CreatedDatetime,CreatedUserid,UpdatedDatetime,UpdatedUserid")] Game game)
        {
            if (id != game.Gameid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(game);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.Gameid))
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
            ViewData["Categoryid"] = new SelectList(_context.Category, "Categoryid", "Categoryid", game.Categoryid);
            ViewData["Companyid"] = new SelectList(_context.Company, "Companyid", "CompanyName", game.Companyid);
            ViewData["Platformid"] = new SelectList(_context.Platform, "Platformid", "Platformid", game.Platformid);
            return View(game);
        }

        // GET: Game/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game
                .Include(g => g.Category)
                .Include(g => g.Company)
                .Include(g => g.Platform)
                .FirstOrDefaultAsync(m => m.Gameid == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Game/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var game = await _context.Game.FindAsync(id);
            _context.Game.Remove(game);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(decimal id)
        {
            return _context.Game.Any(e => e.Gameid == id);
        }
    }
}
