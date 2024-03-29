﻿/**
 * Project: PROG3050 - Virtual Game store Team Project
 * Purpose: credit card service
 * 
 * Created by Team on 2019.10.31
 * 
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VirtualGameStore.Models;

namespace VirtualGameStore.Controllers
{
    public class CreditcardsController : Controller
    {
        private readonly PROG3050Context _context;
        private IHttpContextAccessor _HttpContextAccessor;

        public CreditcardsController(PROG3050Context context, IHttpContextAccessor HttpContextAccessor)
        {
            _context = context;
            _HttpContextAccessor = HttpContextAccessor;
        }

        // GET: Creditcards
        public async Task<IActionResult> Index()
        {
            string userIdString = _HttpContextAccessor.HttpContext.Session.GetString("Userid");
            int userId;

            bool parsed = int.TryParse(userIdString, out userId);
            if (!parsed)
            {
                userId = 0;
            }

            var pROG3050Context = _context.Creditcard.Include(c => c.User).Where(c => c.Userid == userId);
            return View(await pROG3050Context.ToListAsync());
        }

        // GET: Creditcards/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creditcard = await _context.Creditcard
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Cardid == id);
            if (creditcard == null)
            {
                return NotFound();
            }

            return View(creditcard);
        }

        // GET: Creditcards/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = _HttpContextAccessor.HttpContext.Session.GetString("Userid");

            return View();
        }

        // POST: Creditcards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cardid,Userid,Cardnumber,Cardholder,CreatedDatetime,CreatedUserid,UpdatedDatetime,UpdatedUserid")] Creditcard creditcard)
        {
            if (ModelState.IsValid)
            {
                creditcard.CreatedDatetime = DateTime.Now;
                creditcard.UpdatedDatetime = DateTime.Now;
                _context.Add(creditcard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Userid"] = new SelectList(_context.User, "Userid", "DisplayName", creditcard.Userid);
            return View(creditcard);
        }

        // GET: Creditcards/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creditcard = await _context.Creditcard.FindAsync(id);
            if (creditcard == null)
            {
                return NotFound();
            }
            ViewData["Userid"] = new SelectList(_context.User, "Userid", "DisplayName", creditcard.Userid);
            return View(creditcard);
        }

        // POST: Creditcards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Cardid,Userid,Cardnumber,Cardholder,CreatedDatetime,CreatedUserid,UpdatedDatetime,UpdatedUserid")] Creditcard creditcard)
        {
            if (id != creditcard.Cardid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    creditcard.UpdatedDatetime = DateTime.Now;
                    _context.Update(creditcard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CreditcardExists(creditcard.Cardid))
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
            ViewData["Userid"] = new SelectList(_context.User, "Userid", "DisplayName", creditcard.Userid);
            return View(creditcard);
        }

        // GET: Creditcards/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creditcard = await _context.Creditcard
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Cardid == id);
            if (creditcard == null)
            {
                return NotFound();
            }

            return View(creditcard);
        }

        // POST: Creditcards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var creditcard = await _context.Creditcard.FindAsync(id);
            _context.Creditcard.Remove(creditcard);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CreditcardExists(decimal id)
        {
            return _context.Creditcard.Any(e => e.Cardid == id);
        }
    }
}
