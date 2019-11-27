/**
 * Project: PROG3050 - Virtual Game store Team Project
 * Purpose: order service
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
    public class OrdersController : Controller
    {
        private readonly PROG3050Context _context;

        public OrdersController(PROG3050Context context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var pROG3050Context = _context.Order.Include(o => o.Eventgame).Include(o => o.Game).Include(o => o.User);
            return View(await pROG3050Context.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Eventgame)
                .Include(o => o.Game)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.Orderid == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["Eventgameid"] = new SelectList(_context.Eventgame, "Eventgameid", "Eventgameid");
            ViewData["Gameid"] = new SelectList(_context.Game, "Gameid", "Description");
            ViewData["Userid"] = new SelectList(_context.User, "Userid", "Password");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Orderid,OrderDate,Userid,OrderCount,OrderPrice,Gameid,Cardid,Eventgameid,DiscountRate")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Gameid"] = new SelectList(_context.Game, "Gameid", "Description", order.Gameid);
            ViewData["Userid"] = new SelectList(_context.User, "Userid", "Password", order.Userid);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            ViewData["Gameid"] = new SelectList(_context.Game, "Gameid", "Description", order.Gameid);
            ViewData["Userid"] = new SelectList(_context.User, "Userid", "Password", order.Userid);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Orderid,OrderDate,Userid,OrderCount,OrderPrice,Gameid,Cardid,Eventgameid,DiscountRate")] Order order)
        {
            if (id != order.Orderid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Orderid))
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

            ViewData["Gameid"] = new SelectList(_context.Game, "Gameid", "Description", order.Gameid);
            ViewData["Userid"] = new SelectList(_context.User, "Userid", "Password", order.Userid);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Eventgame)
                .Include(o => o.Game)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.Orderid == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var order = await _context.Order.FindAsync(id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(decimal id)
        {
            return _context.Order.Any(e => e.Orderid == id);
        }
    }
}
