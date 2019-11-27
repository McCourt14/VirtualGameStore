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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VirtualGameStore.Models;

namespace VirtualGameStore.Controllers
{
    public class FriendsController : Controller
    {
        private readonly PROG3050Context _context;
        private IHttpContextAccessor _HttpContextAccessor;

        public FriendsController(PROG3050Context context, IHttpContextAccessor HttpContextAccessor)
        {
            _context = context;
            _HttpContextAccessor = HttpContextAccessor;

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
        public async Task<IActionResult> Create(string searching)
        {
            ViewBag.Message = TempData["Message"];
            String userID = _HttpContextAccessor.HttpContext.Session.GetString("Userid");
            ViewData["UserId"] = userID;

            var addFriends = _context.Friends.Where(f => f.Userid == Decimal.Parse(userID));
            return View(await _context.User.Where(g => g.DisplayName.Contains(searching) || searching == null)
                .Where(u => !addFriends.Any(f => f.FriendUserid == u.Userid)).Where(u => u.Userid != Decimal.Parse(userID)).ToListAsync());
        }

        public async Task<IActionResult> AddFriend(decimal userID, decimal friendID)
        {
            Friends friends = new Friends();

            if (ModelState.IsValid)
            {
                friends.Userid = userID;
                friends.FriendUserid = friendID;
                friends.CreatedDatetime = DateTime.Now;
                friends.UpdatedDatetime = DateTime.Now;
                _context.Add(friends);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }

            TempData["Message"] = "Error Adding Friend";
            return RedirectToAction(nameof(Create));
        }

        public async Task<IActionResult> Delete(decimal id)
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
