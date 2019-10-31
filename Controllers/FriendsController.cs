/**
 * Project: PROG3050 - Virtual Game store Team Project
 * Purpose: friends service
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
    public class FriendsController : Controller
    {
        private readonly PROG3050Context _context;

        public FriendsController(PROG3050Context context)
        {
            _context = context;
        }

        // GET: Friends
        public async Task<IActionResult> Index()
        {
            var pROG3050Context = _context.Friends.Include(f => f.FriendUser).Include(f => f.User);
            return View(await pROG3050Context.ToListAsync());
        }

        // GET: Friends/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friends = await _context.Friends
                .Include(f => f.FriendUser)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Friendid == id);
            if (friends == null)
            {
                return NotFound();
            }

            return View(friends);
        }

        // GET: Friends/Create
        public IActionResult Create()
        {
            ViewData["FriendUserid"] = new SelectList(_context.User, "Userid", "DisplayName");
            ViewData["Userid"] = new SelectList(_context.User, "Userid", "DisplayName");
            return View();
        }

        // POST: Friends/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Friendid,Userid,FriendUserid,UpdatedUserid,CreatedDatetime,CreatedUserid,UpdatedDatetime")] Friends friends)
        {
            if (ModelState.IsValid)
            {
                friends.CreatedDatetime = DateTime.Now;
                friends.UpdatedDatetime = DateTime.Now;
                _context.Add(friends);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FriendUserid"] = new SelectList(_context.User, "Userid", "DisplayName", friends.FriendUserid);
            ViewData["Userid"] = new SelectList(_context.User, "Userid", "DisplayName", friends.Userid);
            return View(friends);
        }

        // GET: Friends/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friends = await _context.Friends.FindAsync(id);
            if (friends == null)
            {
                return NotFound();
            }
            ViewData["FriendUserid"] = new SelectList(_context.User, "Userid", "DisplayName", friends.FriendUserid);
            ViewData["Userid"] = new SelectList(_context.User, "Userid", "DisplayName", friends.Userid);
            return View(friends);
        }

        // POST: Friends/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Friendid,Userid,FriendUserid,UpdatedUserid,CreatedDatetime,CreatedUserid,UpdatedDatetime")] Friends friends)
        {
            if (id != friends.Friendid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    friends.UpdatedDatetime = DateTime.Now;
                    _context.Update(friends);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FriendsExists(friends.Friendid))
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
            ViewData["FriendUserid"] = new SelectList(_context.User, "Userid", "DisplayName", friends.FriendUserid);
            ViewData["Userid"] = new SelectList(_context.User, "Userid", "DisplayName", friends.Userid);
            return View(friends);
        }

        // GET: Friends/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friends = await _context.Friends
                .Include(f => f.FriendUser)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Friendid == id);
            if (friends == null)
            {
                return NotFound();
            }

            return View(friends);
        }

        // POST: Friends/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var friends = await _context.Friends.FindAsync(id);
            _context.Friends.Remove(friends);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FriendsExists(decimal id)
        {
            return _context.Friends.Any(e => e.Friendid == id);
        }
    }
}
