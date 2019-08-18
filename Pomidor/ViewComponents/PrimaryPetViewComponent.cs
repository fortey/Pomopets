using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pomidor.ViewComponents
{
    [Authorize]
    public class PrimaryPetViewComponent : ViewComponent
    {
        PomoDbContext context;
        UserManager<Player> userManager;

        public PrimaryPetViewComponent(PomoDbContext context, UserManager<Player> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var primaryPet = context.Pets.Include(x => x.Type).FirstOrDefault(x => x.ID == user.PrimaryPet);
            if (primaryPet == null)
            {
                primaryPet = new Pet { Type = context.TypeOfPets.First(), Level = 0, UserID = user.Id };
                context.Pets.Add(primaryPet);
                await context.SaveChangesAsync();
                user.PrimaryPet = primaryPet.ID;
                await userManager.UpdateAsync(user);
            }

            return View(primaryPet);
        }
    }
}
