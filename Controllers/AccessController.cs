using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using VirtualGameStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

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

        public async Task<ActionResult> Login(decimal id, string passwd)
        {
            if (UserExists(id))
            {
                var user = await _context.User.FindAsync(id);

                User u = (User)user;

                if (u.Password.Equals(passwd))
                {
                    if (u.Usertype.Equals("99")) {
                        HttpContext.Session.SetString("isAdmin", "true");
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

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout(decimal id)
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
                
        private bool UserExists(decimal id)
        {
            return _context.User.Any(e => e.Userid == id);
        }
    }
}