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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VirtualGameStore.Models;

namespace VirtualGameStore.Controllers
{
    public class OrdersController : Controller
    {
        private readonly PROG3050Context _context;
        private IHttpContextAccessor _HttpContextAccessor;

        public OrdersController(PROG3050Context context, IHttpContextAccessor HttpContextAccessor)
        {
            _context = context;
            _HttpContextAccessor = HttpContextAccessor;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var userID = _HttpContextAccessor.HttpContext.Session.GetString("Userid");
            ViewData["UserId"] = userID;

            var pROG3050Context = _context.Order.Include(o => o.Eventgame).Include(o => o.Game).Include(o => o.User)
                .Include(o => o.Card).OrderByDescending(o => o.OrderDate).Where(o => o.Userid == Decimal.Parse(userID));
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
        public IActionResult Checkout()
        {
            ViewData["UserId"] = _HttpContextAccessor.HttpContext.Session.GetString("Userid");
            ViewBag.Cardid = new SelectList(_context.Creditcard, "Cardid", "Cardnumber");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Checkout([Bind("Orderid,OrderDate,Userid,OrderCount,OrderPrice,Gameid,Cardid,Eventgameid,DiscountRate")] Order order)
        {
            if (ModelState.IsValid)
            {
                List<Item> cart = getCartSession();

                foreach(var game in cart)
                {
                    Order saveOrder = new Order();

                    saveOrder.Userid = order.Userid;
                    saveOrder.Cardid = order.Cardid;
                    saveOrder.Gameid = game.Game.Gameid;
                    saveOrder.OrderPrice = game.Game.Price * game.Quantity;
                    saveOrder.OrderCount = game.Quantity;
                    saveOrder.OrderDate = DateTime.Now;

                    _context.Add(saveOrder);
                    _context.SaveChanges();
                }

                HttpContext.Session.Remove("cart");
                return RedirectToAction(nameof(Confirmation));
            }

            ViewData["UserId"] = _HttpContextAccessor.HttpContext.Session.GetString("Userid");
            ViewBag.Cardid = new SelectList(_context.Creditcard, "Cardid", "Cardnumber");
            return View(order);
        }

        public IActionResult Confirmation()
        {
            return View();
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

        public List<Item> getCartSession()
        {
            List<Item> carts = new List<Item>();
            String cart = HttpContext.Session.GetString("cart");
            String[] rows = cart.Split(",");
            foreach (String row in rows)
            {
                if (!row.Equals(""))
                {
                    String[] items = row.Split(";");
                    Game g = new Game();
                    g.Gameid = Convert.ToDecimal(items[0]);
                    g.Title = items[1];
                    g.Price = Convert.ToDecimal(items[2]);
                    int q = Convert.ToInt32(items[3]);

                    Item item = new Item { Game = g, Quantity = q };

                    carts.Add(item);
                }
            }
            return carts;
        }
    }
}
