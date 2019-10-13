using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using csharp_email.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace csharp_email.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext; 

        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("logCheck")]
        public IActionResult logCheck(Login check)
        {
            if(ModelState.IsValid)
            {
                var acctInDb = dbContext.Users.FirstOrDefault(a => a.Email == check.LogEmail);
                if(acctInDb == null)
                {
                    ModelState.AddModelError("LogEmail", "Invalid login");
                    return View("Index");
                }
                var hasher = new PasswordHasher<Login>();
                var result = hasher.VerifyHashedPassword(check, acctInDb.Password, check.LogPassword);
                if(result == 0)
                {
                    ModelState.AddModelError("LogEmail", "Invalid login");
                    return View("Index");
                }
                HttpContext.Session.SetInt32("UID", acctInDb.UserId);
                return Redirect("/Inbox");
            }
            return View("Index");
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("RegCheck")]
        public IActionResult regCheck(User newUse)
        {
            if(ModelState.IsValid)
            {
                if(dbContext.Users.Any(b=>b.EName == newUse.EName))
                {
                   ModelState.AddModelError("EName", "EName already in use!");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                newUse.Password = Hasher.HashPassword(newUse, newUse.Password);
                newUse.Email = $"{newUse.EName}@sharpmail.com";
                dbContext.Add(newUse);
                dbContext.SaveChanges();
                HttpContext.Session.SetInt32("UID", newUse.UserId);
                return Redirect("/Inbox");
            }
            return View("Register");
        }

        [HttpGet("inbox")]
        public IActionResult Inbox()
        {
            int? Sess = HttpContext.Session.GetInt32("UID");
            if(Sess == null)
            {
                return Redirect("/logout");
            }
            var username = dbContext.Users.FirstOrDefault(u => u.UserId == Sess);
            ViewBag.username = username;
            var inMail = dbContext.Emails.Where(e => e.Receiver == username.EName).OrderByDescending(c => c.CreatedAt).ToList();
            var count = 0;
            foreach(var m in inMail)
            {
                if(m.Read == false)
                {
                    count++;
                }
            }
            ViewBag.inMail = inMail;
            ViewBag.counter = count;
            return View();
        }

        [HttpGet("mail/{UserId}/{MailId}")]
        public IActionResult Message(int UserId, int MailId)
        {
            int? Sess = HttpContext.Session.GetInt32("UID");
            if(Sess == null)
            {
                return Redirect("/logout");
            }
            var username = dbContext.Users.FirstOrDefault(u => u.UserId == Sess);
            ViewBag.username = username;
            if(UserId != Sess)
            {
                return Redirect("/logout");
            }
            var message = dbContext.Emails.FirstOrDefault(m => m.MailId == MailId);
            ViewBag.message = message;
            var inMail = dbContext.Emails.Where(e => e.Receiver == username.EName);
            var count = 0;
            foreach(var m in inMail)
            {
                if(m.Read == false)
                {
                    count++;
                }
            }
            ViewBag.counter = count;
            if(message.Read == false)
            {
                message.Read = true;
                dbContext.SaveChanges();
            }
            return View("Message");
        }

        [HttpGet("deleted")]
        public IActionResult Deleted()
        {
            int? Sess = HttpContext.Session.GetInt32("UID");
            if(Sess == null)
            {
                return Redirect("/logout");
            }
            var username = dbContext.Users.FirstOrDefault(u => u.UserId == Sess);
            ViewBag.username = username;
            var inMail = dbContext.Emails.Where(e => e.Receiver == username.EName).OrderByDescending(c => c.CreatedAt).ToList();
            var count = 0;
            foreach(var m in inMail)
            {
                if(m.Read == false)
                {
                    count++;
                }
            }
            ViewBag.inMail = inMail;
            ViewBag.counter = count;
            return View();
        }

        [HttpGet("delete/{MailId}")]
        public IActionResult Delete(int MailId)
        {
            Mail mess = dbContext.Emails.SingleOrDefault(m => m.MailId == MailId);
            mess.Deleted = true;
            dbContext.SaveChanges();
            return Redirect("/Inbox");
        }

        [HttpGet("permdelete/{MailId}")]
        public IActionResult PermDelete(int MailId)
        {
            Mail mess = dbContext.Emails.SingleOrDefault(m => m.MailId == MailId);
            mess.PermDeleted = true;
            dbContext.SaveChanges();
            return Redirect("/Inbox");
        }

        [HttpGet("compose")]
        public IActionResult Compose()
        {
            int? Sess = HttpContext.Session.GetInt32("UID");
            if(Sess == null)
            {
                return Redirect("/logout");
            }
            var username = dbContext.Users.FirstOrDefault(u => u.UserId == Sess);
            ViewBag.username = username;
            List<User> otherUsers = dbContext.Users.Where(u => u.UserId != Sess).OrderByDescending(a => a.EName).ToList();
            ViewBag.others = otherUsers;
            var inMail = dbContext.Emails.Where(e => e.Receiver == username.EName);
            var count = 0;
            foreach(var m in inMail)
            {
                if(m.Read == false)
                {
                    count++;
                }
            }
            ViewBag.counter = count;
            return View();
        }

        [HttpPost("Send")]
        public IActionResult Send(Mail newMail)
        {
            if(ModelState.IsValid)
            {
                dbContext.Add(newMail);
                dbContext.SaveChanges();
                return Redirect("/Inbox");
            }
            return Redirect("/Compose");
        }

        [HttpGet("sent")]
        public IActionResult Sent()
        {
            int? Sess = HttpContext.Session.GetInt32("UID");
            if(Sess == null)
            {
                return Redirect("/logout");
            }
            var username = dbContext.Users.FirstOrDefault(u => u.UserId == Sess);
            ViewBag.username = username;
            var sentMail = dbContext.Emails.Where(m => m.Sender == username.EName).OrderByDescending(c => c.CreatedAt).ToList();
            ViewBag.sentMail = sentMail;
            var inMail = dbContext.Emails.Where(e => e.Receiver == username.EName);
            var count = 0;
            foreach(var m in inMail)
            {
                if(m.Read == false)
                {
                    count++;
                }
            }
            ViewBag.counter = count;
            return View();
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
