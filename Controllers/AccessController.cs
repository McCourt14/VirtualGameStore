/**
 * Project: PROG3050 - Virtual Game store Team Project
 * Purpose: check login
 * 
 * Created by Team on 2019.10.31
 * 
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using VirtualGameStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;

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
                            string isAdmin = "";
                            if (u.Usertype.Equals("99"))
                            {
                                isAdmin = "true";
                            }
                            else
                            {
                                isAdmin = "false";
                            }

                            HttpContext.Session.SetString("DisplayName", u.DisplayName);
                            HttpContext.Session.SetString("isAdmin", isAdmin);
                            HttpContext.Session.SetString("Userid", Convert.ToString(u.Userid));

                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ViewBag.Message = "Please check password!";
                            HttpContext.Session.Clear();
                        }
                    }
                }                
            }

            ViewData["Message"] = "Incorrect Email or Password";

            return View(model);
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
                        if (!u.Password.Equals(model.Password))
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

                        //sendMail(editUser.Email, editUser.Password);
                        ViewData["message"] = "Password Changed";
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

        /*public void sendMail(string to, string password)
        {
            var body = "<p>Email From: {0} ({1})</p><p>Password:</p><p>{2}</p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress(to));  // replace with valid value 
            message.From = new MailAddress("mccourt737@gmail.com");  // replace with valid value
            message.Subject = "Password Reset";
            message.Body = string.Format(body, "noreply", "noreply@vgamestore.com", password);
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {

                var credential = new NetworkCredential();
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = credential;

                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Send(message);
            }
        }*/
    }
}