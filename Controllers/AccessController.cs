using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using VirtualGameStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace VirtualGameStore.Controllers
{
    public class AccessController : Controller
    {
        private readonly PROG3050Context _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AccessController(PROG3050Context context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public ActionResult Login()
        {
            return View(nameof(Login));
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (UserExists(model.Email))
            {
                var users = _context.User.Where(c => c.Email == model.Email);

                if (users.Any())
                {
                    foreach (var u in users as IEnumerable<User>)
                    {
                        if (u.Password.Equals(model.Password))
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

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Logout(LoginViewModel model)
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
                
        private bool UserExists(String email)
        {
            return _context.User.Any(e => e.Email == email);
        }

        public ActionResult ResetPassword()
        {
            return View(nameof(ResetPassword));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var users = _context.User.Where(c => c.Email == model.Email);
            if (users.Any())
            {
                try
                {
                    User editUser = null;
                    foreach (var u in users as IEnumerable<User>)
                    {
                        if (u.Password.Equals(model.Password))
                        {
                            editUser = u;
                        }
                    }

                    if (editUser != null)
                    {
                        editUser.UpdatedDatetime = DateTime.Now;
                        editUser.Password = model.Password;
                        _context.Update(editUser);
                        await _context.SaveChangesAsync();

                        return RedirectToAction("ResetPasswordConfirmation", "Access");
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(model.Email))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            // Don't reveal that the user does not exist
            return RedirectToAction("ResetPassword", "Access");

        }
    }
}