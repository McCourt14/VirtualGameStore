/**
 * Project: PROG3050 - Virtual Game store Team Project
 * Purpose: user service
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
    public class UsersController : Controller
    {
        private readonly PROG3050Context _context;

        public UsersController(PROG3050Context context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.User.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Userid == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Userid,DisplayName,StartDate,EndDate,TryCount,LockDatetime,Password,Usertype,ReceiveEmail,Email,FirstName,LastName,Gender,BirthDate,PostCode,Country,Province,City,Address,Address2,CellPhone,HomePhone,OfficePhone,CreatedDatetime,CreatedUserid,UpdatedDatetime,UpdatedUserid")] User user)
        {
            ViewBag.Message = "";
            if (ModelState.IsValid)
            {

                var displayCheck = await _context.User
                    .FirstOrDefaultAsync(m => m.DisplayName == user.DisplayName);

                if (displayCheck != null)
                {
                    ViewBag.Message = "Display Name already exists";
                    return View(user);
                }

                var ux = await _context.User
                .FirstOrDefaultAsync(m => m.Email == user.Email);
                if (ux != null)
                {
                    ViewBag.Message = "Email already exists";
                    return View(user);
                }

                user.StartDate = DateTime.Now;
                user.CreatedDatetime = DateTime.Now;
                user.UpdatedDatetime = DateTime.Now;
                _context.Add(user);
                await _context.SaveChangesAsync();

                loginUser(user.Email, user.Password);

                return RedirectToAction(nameof(Index), "Home");
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Userid,DisplayName,StartDate,EndDate,TryCount,LockDatetime,Password,Usertype,ReceiveEmail,Email,FirstName,LastName,Gender,BirthDate,PostCode,Country,Province,City,Address,Address2,CellPhone,HomePhone,OfficePhone,CreatedDatetime,CreatedUserid,UpdatedDatetime,UpdatedUserid")] User user)
        {
            if (id != user.Userid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    user.UpdatedDatetime = DateTime.Now;
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Userid))
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
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Update(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(decimal id, [Bind("Userid,DisplayName,StartDate,EndDate,TryCount,LockDatetime,Password,Usertype,ReceiveEmail,Email,FirstName,LastName,Gender,BirthDate,PostCode,Country,Province,City,Address,Address2,CellPhone,HomePhone,OfficePhone,CreatedDatetime,CreatedUserid,UpdatedDatetime,UpdatedUserid")] User user)
        {
            if (id != user.Userid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    user.UpdatedDatetime = DateTime.Now;
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Userid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), "Home");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Userid == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var user = await _context.User.FindAsync(id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void loginUser(String email, string password)
        {
            if (UserExists(email))
            {
                var users = _context.User.Where(c => c.Email == email);

                if (users.Any())
                {
                    foreach (var u in users as IEnumerable<User>)
                    {
                        if (u.Password.Equals(password))
                        {
                            if (u.Usertype.Equals("99"))
                            {
                                HttpContext.Session.SetString("DisplayName", u.DisplayName);
                                HttpContext.Session.SetString("isAdmin", "true");
                                HttpContext.Session.SetString("Userid", Convert.ToString(u.Userid));
                                ViewBag.isAdmin = "true";
                            }
                            else
                            {
                                HttpContext.Session.SetString("isAdmin", "false");
                            }
                            HttpContext.Session.SetString("userId", Convert.ToString(u.Userid));
                        }
                        else
                        {
                            ViewBag.Message = "Please check password!";
                            HttpContext.Session.Clear();
                        }
                    }
                }
            }
        }

        private bool UserExists(decimal id)
        {
            return _context.User.Any(e => e.Userid == id);
        }

        private bool UserExists(String email)
        {
            return _context.User.Any(e => e.Email == email);
        }
    }
}
