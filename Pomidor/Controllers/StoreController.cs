using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pomidor.Data;

namespace Pomidor.Controllers
{
    [Authorize]
    public class StoreController : Controller
    {
        PomoDbContext context;
        UserManager<Player> userManager;
        ApplicationDbContext appContext;

        public StoreController(PomoDbContext context, UserManager<Player> userManager, ApplicationDbContext appContext)
        {
            this.context = context;
            this.userManager = userManager;
            this.appContext = appContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);
            var TypesOfPets = context.Pets.Where(x => x.UserID == user.Id).Select(x => x.TypeID);
            var goods = context.TypeOfPets.Select( x => 
                        new GoodViewModel
                        {
                            ID = x.ID,
                            Name = x.Name,
                            ImgFolder = x.ImgFolder,
                            Price = x.Price,
                            IsAvailable = !TypesOfPets.Contains(x.ID) && x.Price <= user.Money
                        });

            return View(goods);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Buy(int id)
        {
            var typeOfPet = await context.TypeOfPets.FirstOrDefaultAsync(x => x.ID == id);
            var user = await userManager.GetUserAsync(User);
            if(typeOfPet != null && user.Money >= typeOfPet.Price
                && context.Pets.Count(x=> x.UserID == user.Id && x.TypeID == id) == 0)
            {
                using(var transaction = context.Database.BeginTransaction())
                {
                    using (var appTransaction = appContext.Database.BeginTransaction())
                    {
                        context.Pets.Add(new Pet { Type = typeOfPet, UserID = user.Id });
                        await context.SaveChangesAsync();
                        user.Money -= typeOfPet.Price;
                        await userManager.UpdateAsync(user);

                        appTransaction.Commit();
                        transaction.Commit();
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}