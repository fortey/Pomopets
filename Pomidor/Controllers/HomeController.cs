using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pomidor.Models;

namespace Pomidor.Controllers
{
    public class HomeController : Controller
    {
        PomoDbContext context;
        UserManager<Player> userManager;

        public HomeController(PomoDbContext context, UserManager<Player> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var user = await userManager.GetUserAsync(User);

                var pomidor = context.Pomidors.FirstOrDefault(x => x.UserID == id);
                if (pomidor == null)
                {
                    return View(new PomidorViewModel { Rest = TimeSpan.Zero });
                }
                return View(new PomidorViewModel { Rest = pomidor.Rest, EndTime = pomidor.EndTime });
            }
            return View();
        }

        [Authorize]
        public async Task<JsonResult> Start()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var pomidor = context.Pomidors.FirstOrDefault(x => x.UserID == id);
            if (pomidor == null)
            {
                pomidor = new Pomidor() { UserID = id };
                pomidor.Start(25);
                context.Pomidors.Add(pomidor);
                await context.SaveChangesAsync();
            }
            else
            {
                pomidor.Start(25);
                context.Update(pomidor);
                await context.SaveChangesAsync();
            }
            return new JsonResult(new TimerJSON { Rest = pomidor.Rest.TotalMilliseconds, IsPomidor = true });
        }

        [Authorize, HttpPost]
        public async Task<JsonResult> End()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var pomidor = context.Pomidors.FirstOrDefault(x => x.UserID == id);
            var result = new EndTimerJSON();
            if (pomidor != null && !pomidor.PrizeIssued && pomidor.EndTime <= DateTime.UtcNow)
            {
                pomidor.PrizeIssued = true;
                context.Update(pomidor);
                var user = await userManager.GetUserAsync(User);
                user.AddExperience(Pomidor.ExperienceAward);
                user.Money += Pomidor.MoneyAward;
                await userManager.UpdateAsync(user);

                var pet = context.Pets.FirstOrDefault(x => x.ID == user.PrimaryPet);
                if(pet!= null)
                {
                    pet.AddExperience(Pomidor.ExperienceAward);
                    context.Update(pet);
                }

                await context.SaveChangesAsync();

                result.IsSuccesfully = true;
                result.Experience = Pomidor.ExperienceAward;
                result.Money = Pomidor.MoneyAward;
            }
            return new JsonResult(result);
        }

        public IActionResult HeroSummary()
        {
            return ViewComponent("HeroSummary");
        }

        public IActionResult PrimaryPet()
        {
            return ViewComponent("PrimaryPet");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
