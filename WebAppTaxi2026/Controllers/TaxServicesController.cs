using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppTaxi2026.Data;
using WebAppTaxi2026.Models;
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
        public async Task<IActionResult> All()
        {
            var userId = userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
                return Challenge();

            var driverId = await dbContext.Drivers
                .AsNoTracking()
                .Where(d => d.UserId == userId)
                .Select(d => d.Id)
                .SingleOrDefaultAsync();

            if (driverId == 0)
                return RedirectToAction("Create", "Drivers");

            var model = await dbContext.TaxServices
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
                .ToListAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var userId = userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
                return Challenge();

            var driverId = await dbContext.Drivers
                .AsNoTracking()
                .Where(d => d.UserId == userId)
                .Select(d => d.Id)
                .SingleOrDefaultAsync();

            if (driverId == 0)
                return RedirectToAction("Create", "Drivers");

            var cars = await dbContext.Cars
                .AsNoTracking()
                .Where(c => c.DriverId == driverId)
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = $"{c.Brand} ({c.RegNumber})"
                })
                .ToListAsync();

            var model = new TaxServiceCreateViewModel
            {
                HireDateTime = DateTime.Now,
                Cars = cars
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaxServiceCreateViewModel model)
        {
            var userId = userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
                return Challenge();

            var driverId = await dbContext.Drivers
                .AsNoTracking()
                .Where(d => d.UserId == userId)
                .Select(d => d.Id)
                .SingleOrDefaultAsync();

            if (driverId == 0)
                return RedirectToAction("Create", "Drivers");

            var carExistsForDriver = await dbContext.Cars
                .AsNoTracking()
                .AnyAsync(c => c.Id == model.CarId && c.DriverId == driverId);

            if (!carExistsForDriver)
                ModelState.AddModelError(nameof(model.CarId), "Невалиден автомобил.");

            if (!ModelState.IsValid)
            {
                model.Cars = await dbContext.Cars
                    .AsNoTracking()
                    .Where(c => c.DriverId == driverId)
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = $"{c.Brand} ({c.RegNumber})"
                    })
                    .ToListAsync();

                return View(model);
            }

            var taxService = new TaxService
            {
                HireDateTime = model.HireDateTime,
                DownTime = model.DownTime,
                TraveledKm = model.TraveledKm,
                CarId = model.CarId
            };

            await dbContext.TaxServices.AddAsync(taxService);
            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var userId = userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
                return Challenge();

            var model = await dbContext.TaxServices
                .AsNoTracking()
                .Where(t => t.Id == id && t.Car.Driver.UserId == userId)
                .Select(t => new TaxServiceDetailsViewModel
                {
                    Id = t.Id,
                    HireDateTime = t.HireDateTime,
                    DownTime = t.DownTime,
                    TraveledKm = t.TraveledKm,
                    CarInfo = $"{t.Car.Brand} ({t.Car.RegNumber})"
                })
                .SingleOrDefaultAsync();

            if (model == null)
                return NotFound();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
                return Challenge();

            var model = await dbContext.TaxServices
                .AsNoTracking()
                .Where(t => t.Id == id && t.Car.Driver.UserId == userId)
                .Select(t => new TaxServiceDetailsViewModel
                {
                    Id = t.Id,
                    HireDateTime = t.HireDateTime,
                    DownTime = t.DownTime,
                    TraveledKm = t.TraveledKm,
                    CarInfo = $"{t.Car.Brand} ({t.Car.RegNumber})"
                })
                .SingleOrDefaultAsync();

            if (model == null)
                return NotFound();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, TaxServiceDetailsViewModel model)
        {
            var userId = userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
                return Challenge();

            var service = await dbContext.TaxServices
                .SingleOrDefaultAsync(t => t.Id == id && t.Car.Driver.UserId == userId);

            if (service == null)
                return NotFound();

            dbContext.TaxServices.Remove(service);
            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
                return Challenge();

            var service = await dbContext.TaxServices
                .AsNoTracking()
                .Where(t => t.Id == id && t.Car.Driver.UserId == userId)
                .Select(t => new
                {
                    t.Id,
                    t.HireDateTime,
                    t.DownTime,
                    t.TraveledKm,
                    t.CarId
                })
                .SingleOrDefaultAsync();

            if (service == null)
                return NotFound();

            var cars = await dbContext.Cars
                .AsNoTracking()
                .Where(c => c.Driver.UserId == userId)
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = $"{c.Brand} ({c.RegNumber})"
                })
                .ToListAsync();

            var model = new TaxServiceCreateViewModel
            {
                Id = service.Id,
                HireDateTime = service.HireDateTime,
                DownTime = service.DownTime,
                TraveledKm = service.TraveledKm,
                CarId = service.CarId,
                Cars = cars
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, TaxServiceCreateViewModel model)
        {
            var userId = userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
                return Challenge();

            if (id != model.Id)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                model.Cars = await dbContext.Cars
                    .AsNoTracking()
                    .Where(c => c.Driver.UserId == userId)
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = $"{c.Brand} ({c.RegNumber})"
                    })
                    .ToListAsync();

                return View(model);
            }

            var service = await dbContext.TaxServices
                .SingleOrDefaultAsync(t => t.Id == id && t.Car.Driver.UserId == userId);

            if (service == null)
                return NotFound();

            service.HireDateTime = model.HireDateTime;
            service.DownTime = model.DownTime;
            service.TraveledKm = model.TraveledKm;
            service.CarId = model.CarId;

            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }
    }
}
