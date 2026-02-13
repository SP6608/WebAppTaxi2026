using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppTaxi2026.Data;
using WebAppTaxi2026.ViewModels;

namespace WebAppTaxi2026.Controllers
{
    [Authorize]
    public class TaxServicesController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<IdentityUser> userManager;
        public TaxServicesController(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }
        [HttpGet]
        public IActionResult All()
        {
            var userId = userManager.GetUserId(User);

            if (string.IsNullOrEmpty(userId))
                return Challenge();

            // Намираме текущия Driver
            var driverId = dbContext.Drivers
                .AsNoTracking()
                .Where(d => d.UserId == userId)
                .Select(d => d.Id)
                .SingleOrDefault();

            if (driverId == 0)
                return RedirectToAction("Create", "Drivers");

            var model = dbContext.TaxServices
                .AsNoTracking()
                .Where(t => t.Car.DriverId == driverId)
                .Select(t => new TaxServiceCreateViewModel
                {
                    Id = t.Id,
                    HireDateTime = t.HireDateTime,
                    DownTime = t.DownTime,
                    TraveledKm = t.TraveledKm,
                    CarBrand = t.Car.Brand,
                    CarRegNumber = t.Car.RegNumber
                })
                .ToList();

            return View(model);
        }

    }
}
