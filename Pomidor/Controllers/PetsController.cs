using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Pomidor.Controllers
{
    public class PetsController : Controller
    {
        PomoDbContext context;
        UserManager<Player> userManager;

        public PetsController(PomoDbContext context, UserManager<Player> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var pets = context.Pets.Include(x => x.Type).Where(x => x.UserID == userId);
            return View(pets);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetPrimary(int id)
        {
            var user = await userManager.GetUserAsync(User);
            var pet = await context.Pets.FirstOrDefaultAsync(x => x.UserID == user.Id && x.ID == id);
            if(pet != null)
            {
                user.PrimaryPet = pet.ID;
                await userManager.UpdateAsync(user);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}