using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskAuthenticationAuthorization.Models;
using TaskAuthenticationAuthorization.ViewModels;

namespace TaskAuthenticationAuthorization.Controllers
{
    public class RolesController : Controller
    {
        private readonly ShoppingContext db;
        public RolesController(ShoppingContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.Users.ToListAsync());
        }
        public async Task<IActionResult> Edit(int id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(User model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FindAsync(model.Id);
                Role userRole = await db.Roles.FirstOrDefaultAsync(r => model.Role.Name == "admin" ? r.Name == "admin" : r.Name == "buyer");
                if (user != null)
                {
                    user.Email = model.Email;
                    user.BuyerType = model.BuyerType;
                    user.Role = userRole;

                    db.Update(user);
                    await db.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            User user = await db.Users.FindAsync(id);
            if (user != null)
            {
                db.Users.Remove(user);
               await db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}

