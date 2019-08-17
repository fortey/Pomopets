using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pomidor.ViewComponents
{
    public class HeroSummaryViewComponent: ViewComponent
    {
        PomoDbContext context;
        UserManager<Player> userManager;

        public HeroSummaryViewComponent(PomoDbContext context, UserManager<Player> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new HeroSummaryViewModel();
            if (User.Identity.IsAuthenticated)
            {
                var user = await userManager.FindByNameAsync(User.Identity.Name);
                model.IsAuthenticated = true;
                model.Level = user.Level;
                model.Experience = user.Experience;
                model.Money = user.Money;
                model.ExperienceLimit = Player.Levels[user.Level];
            }
            return View(model);
        }
        
    }
}
