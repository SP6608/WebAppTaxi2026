using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;
using WebAppTaxi2026.Data;
using WebAppTaxi2026.Models;
using WebAppTaxi2026.ViewModels;

namespace WebAppTaxi2026.Controllers
{
    [Authorize]
    public class CarsController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<IdentityUser> userManager;
        public CarsController(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        [HttpGet]
        
        public IActionResult Create()
        {

            var userId = userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
                return Challenge();

            var hasDriver = dbContext.Drivers.AsNoTracking().Any(d => d.UserId == userId);
            if (!hasDriver)
                return RedirectToAction("Create", "Drivers");

            return View(new CarCreateViewModel());
        }

        [HttpPost]
        
        public IActionResult Create(CarCreateViewModel model)
        {
            var userId = userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return Challenge();
            }

            // 1) Трябва да ИМА Driver профил, иначе не може да добавя кола
            var driverId = dbContext.Drivers
                .Where(d => d.UserId == userId)
                .Select(d => d.Id)
                .SingleOrDefault();

            if (driverId == 0)
            {
                return RedirectToAction("Create", "Drivers");
            }

            if (!ModelState.IsValid)
                return View(model);

            
            var car = new Car
            {
                Brand = model.Brand,
                RegNumber = model.RegNumber,
                Year = model.Year,
                Places = model.Places,
                InitialFee = model.InitialFee,
                PricePerKm = model.PricePerKm,
                PricePerMinute = model.PricePerMinute,
                DriverId = driverId
            };

            var regExists = dbContext.Cars
            .AsNoTracking()
            .Any(c => c.RegNumber == model.RegNumber);

            if (regExists)
            {
                ModelState.AddModelError(nameof(model.RegNumber), "Този регистрационен номер вече съществува.");
                return View(model);
            }
            else
            {
                dbContext.Cars.Add(car);
                dbContext.SaveChanges();
                return RedirectToAction("Details","Drivers");
            }

        }
        [HttpGet]
        public IActionResult All()
        {
            //Колите на точно определен шофьор
            var userId = userManager.GetUserId(User);
            var driverId = dbContext.Drivers
                .AsNoTracking()
                .Where(d => d.UserId == userId)
                .Select(d => d.Id)
                .SingleOrDefault();

            ICollection<CarListItemViewModel>model=dbContext
                .Cars
                .Include(c=>c.Driver)
                .AsNoTracking()
                .Where(c => c.DriverId == driverId)
                .Select(c=>new CarListItemViewModel()
                {
                    Id = c.Id,
                    Brand = c.Brand,
                    RegNumber = c.RegNumber,
                    Year = c.Year,
                    Places = c.Places,

                })
                .ToList();
            return View(model); 
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var userId = userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return Challenge();
            }

            var car = dbContext.Cars
                .AsNoTracking()
                .Where(c => c.Id == id && c.Driver.UserId == userId)
                .Select(c => new CarDetailsViewModel
                {
                    Id = c.Id,
                    Brand = c.Brand,
                    RegNumber = c.RegNumber,
                    Year = c.Year,
                    Places = c.Places,
                    InitialFee = c.InitialFee,
                    PricePerKm = c.PricePerKm,
                    PricePerMinute = c.PricePerMinute
                })
                .SingleOrDefault();

            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var userId = userManager.GetUserId(User);

            var car = dbContext.Cars
                .Where(c => c.Id == id && c.Driver.UserId == userId)
                .Select(c => new CarCreateViewModel
                {
                    Id = c.Id,                     
                    Brand = c.Brand,
                    RegNumber = c.RegNumber,
                    Year = c.Year,
                    Places = c.Places,
                    InitialFee = c.InitialFee,
                    PricePerKm = c.PricePerKm,
                    PricePerMinute = c.PricePerMinute
                })
                .SingleOrDefault();

            if (car == null)
                return NotFound();

            return View(car);
        }

        [HttpPost]
        public IActionResult Edit(CarCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userId = userManager.GetUserId(User);

            var car = dbContext.Cars
                .FirstOrDefault(c => c.Id == model.Id && c.Driver.UserId == userId);

            if (car == null)
                return NotFound();

            car.Brand = model.Brand;
            car.RegNumber = model.RegNumber;
            car.Year = model.Year;
            car.Places = model.Places;
            car.InitialFee = model.InitialFee;
            car.PricePerKm = model.PricePerKm;
            car.PricePerMinute = model.PricePerMinute;

            dbContext.SaveChanges();

            return RedirectToAction(nameof(All));
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var userId = userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return Challenge();
            }
            var model = dbContext.Cars
                .AsNoTracking()
                .Where(c => c.Id == id && c.Driver.UserId == userId)
                .Select(c => new CarDeleteViewModel
                {
                    Id = c.Id,
                    Brand = c.Brand,
                    RegNumber = c.RegNumber,
                    Year = c.Year
                })
                .SingleOrDefault();

            if (model == null)
                return NotFound();

            return View(model);
        }
        [HttpPost]
        
        public IActionResult Delete(CarDeleteViewModel model)
        {
            var userId = userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return Challenge();
            }

            var car = dbContext.Cars
                .Include(c => c.Driver)
                .SingleOrDefault(c => c.Id == model.Id && c.Driver.UserId == userId);

            if (car == null)
            {
                return NotFound();
            }

            dbContext.Cars.Remove(car);
            dbContext.SaveChanges();

            return RedirectToAction(nameof(All));
        }

    }
}
