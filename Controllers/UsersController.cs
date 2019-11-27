/**
 * Project: PROG3050 - Virtual Game store Team Project
 * Purpose: user service
 * 
 * Created by Team one 2019.10.31
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
        public static List<SelectListItem> States = new List<SelectListItem>()
        {
            new SelectListItem() {Text="Alabama", Value="AL"},
            new SelectListItem() { Text="Alaska", Value="AK"},
            new SelectListItem() { Text="Arizona", Value="AZ"},
            new SelectListItem() { Text="Arkansas", Value="AR"},
            new SelectListItem() { Text="California", Value="CA"},
            new SelectListItem() { Text="Colorado", Value="CO"},
            new SelectListItem() { Text="Connecticut", Value="CT"},
            new SelectListItem() { Text="District of Columbia", Value="DC"},
            new SelectListItem() { Text="Delaware", Value="DE"},
            new SelectListItem() { Text="Florida", Value="FL"},
            new SelectListItem() { Text="Georgia", Value="GA"},
            new SelectListItem() { Text="Hawaii", Value="HI"},
            new SelectListItem() { Text="Idaho", Value="ID"},
            new SelectListItem() { Text="Illinois", Value="IL"},
            new SelectListItem() { Text="Indiana", Value="IN"},
            new SelectListItem() { Text="Iowa", Value="IA"},
            new SelectListItem() { Text="Kansas", Value="KS"},
            new SelectListItem() { Text="Kentucky", Value="KY"},
            new SelectListItem() { Text="Louisiana", Value="LA"},
            new SelectListItem() { Text="Maine", Value="ME"},
            new SelectListItem() { Text="Maryland", Value="MD"},
            new SelectListItem() { Text="Massachusetts", Value="MA"},
            new SelectListItem() { Text="Michigan", Value="MI"},
            new SelectListItem() { Text="Minnesota", Value="MN"},
            new SelectListItem() { Text="Mississippi", Value="MS"},
            new SelectListItem() { Text="Missouri", Value="MO"},
            new SelectListItem() { Text="Montana", Value="MT"},
            new SelectListItem() { Text="Nebraska", Value="NE"},
            new SelectListItem() { Text="Nevada", Value="NV"},
            new SelectListItem() { Text="New Hampshire", Value="NH"},
            new SelectListItem() { Text="New Jersey", Value="NJ"},
            new SelectListItem() { Text="New Mexico", Value="NM"},
            new SelectListItem() { Text="New York", Value="NY"},
            new SelectListItem() { Text="North Carolina", Value="NC"},
            new SelectListItem() { Text="North Dakota", Value="ND"},
            new SelectListItem() { Text="Ohio", Value="OH"},
            new SelectListItem() { Text="Oklahoma", Value="OK"},
            new SelectListItem() { Text="Oregon", Value="OR"},
            new SelectListItem() { Text="Pennsylvania", Value="PA"},
            new SelectListItem() { Text="Rhode Island", Value="RI"},
            new SelectListItem() { Text="South Carolina", Value="SC"},
            new SelectListItem() { Text="South Dakota", Value="SD"},
            new SelectListItem() { Text="Tennessee", Value="TN"},
            new SelectListItem() { Text="Texas", Value="TX"},
            new SelectListItem() { Text="Utah", Value="UT"},
            new SelectListItem() { Text="Vermont", Value="VT"},
            new SelectListItem() { Text="Virginia", Value="VA"},
            new SelectListItem() { Text="Washington", Value="WA"},
            new SelectListItem() { Text="West Virginia", Value="WV"},
            new SelectListItem() { Text="Wisconsin", Value="WI"},
            new SelectListItem() { Text="Wyoming", Value="WY"}
        };
        public static List<SelectListItem> provinces = new List<SelectListItem>()
        {
            new SelectListItem() {Text="Alberta", Value="AB"},
            new SelectListItem() { Text="British Columbia", Value="BC"},
            new SelectListItem() { Text="Manitoba", Value="MB"},
            new SelectListItem() { Text="New Brunswick", Value="NB"},
            new SelectListItem() { Text="Newfoundland and Labrador", Value="NL"},
            new SelectListItem() { Text="Northwest Territories", Value="NT"},
            new SelectListItem() { Text="Nova Scotia", Value="NS"},
            new SelectListItem() { Text="Nunavut", Value="NU"},
            new SelectListItem() { Text="Ontario", Value="ON"},
            new SelectListItem() { Text="Prince Edward Island", Value="PE"},
            new SelectListItem() { Text="Quebec", Value="QC"},
            new SelectListItem() { Text="Saskatchewan", Value="SK"},
            new SelectListItem() { Text="Yukon", Value="YT"}
        };

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
            ViewBag.provinces = provinces;
            return View();
        }

        // POST: Users/Create
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
            else
            {
                if(user.Country == "Canada")
                {
                    ViewBag.provinces = provinces;
                }
                else if(user.Country == "United States of America")
                {
                    ViewBag.provinces = States;
                }
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
                                HttpContext.Session.SetString("DisplayName", u.DisplayName);
                                HttpContext.Session.SetString("isAdmin", "false");
                                HttpContext.Session.SetString("Userid", Convert.ToString(u.Userid));
                            }
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
