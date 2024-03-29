﻿/**
 * Project: PROG3050 - Virtual Game store Team Project
 * Purpose: event service
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
    public class EventsController : Controller
    {
        private readonly PROG3050Context _context;
        private IHttpContextAccessor _HttpContextAccessor;

        public EventsController(PROG3050Context context, IHttpContextAccessor HttpContextAccessor)
        {
            _context = context;
            _HttpContextAccessor = HttpContextAccessor;
        }

        // GET: Events
        public async Task<IActionResult> Index()
        {
            String userID = _HttpContextAccessor.HttpContext.Session.GetString("Userid");

            ViewData["UserId"] = userID;
            var registered = _context.Eventgame.Include(e => e.Event).Where(eg => eg.userid == Decimal.Parse(userID));

            int[] checkArray = new int[registered.Count()];

            int i = 0;
            foreach(var item in registered)
            {
                checkArray[i] = (int)item.Eventid;
                i++;
            }

            ViewBag.Registered = checkArray;

            var pROG3050Context = _context.Event.Include(e => e.RegisterUser);
            return View(await pROG3050Context.ToListAsync());
        }

        // GET: Events
        public async Task<IActionResult> AdminIndex()
        {
            var pROG3050Context = _context.Event.Include(e => e.RegisterUser);
            return View(await pROG3050Context.ToListAsync());
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event
                .Include(e => e.RegisterUser)
                .FirstOrDefaultAsync(m => m.Eventid == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            ViewData["RegisterUserid"] = new SelectList(_context.User, "Userid", "DisplayName");
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Eventid,Eventname,Startdate,Enddate,RegisterUserid,CreatedDatetime,CreatedUserid,UpdatedDatetime,UpdatedUserid")] Event @event)
        {
            if (ModelState.IsValid)
            {
                @event.CreatedDatetime = DateTime.Now;
                @event.UpdatedDatetime = DateTime.Now;
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(AdminIndex));
            }
            ViewData["RegisterUserid"] = new SelectList(_context.User, "Userid", "DisplayName", @event.RegisterUserid);
            return View(@event);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            ViewData["RegisterUserid"] = new SelectList(_context.User, "Userid", "DisplayName", @event.RegisterUserid);
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Eventid,Eventname,Startdate,Enddate,RegisterUserid,CreatedDatetime,CreatedUserid,UpdatedDatetime,UpdatedUserid")] Event @event)
        {
            if (id != @event.Eventid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    @event.UpdatedDatetime = DateTime.Now;
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.Eventid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(AdminIndex));
            }
            ViewData["RegisterUserid"] = new SelectList(_context.User, "Userid", "DisplayName", @event.RegisterUserid);
            return View(@event);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            var @event = await _context.Event.FindAsync(id);
            _context.Event.Remove(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AdminIndex));
        }

        private bool EventExists(decimal id)
        {
            return _context.Event.Any(e => e.Eventid == id);
        }
    }
}
