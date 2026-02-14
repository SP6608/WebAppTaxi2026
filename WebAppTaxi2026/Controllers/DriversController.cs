using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppTaxi2026.Data;
using WebAppTaxi2026.Models;
using WebAppTaxi2026.ViewModels;

namespace WebAppTaxi2026.Controllers
{
    public class DriversController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<IdentityUser> userManager;
        public DriversController(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager )
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }
        [HttpGet]
        public IActionResult Details()
        {
            var userId = userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
                return Challenge();

            var model = dbContext.Drivers
                .AsNoTracking()
                .Where(d => d.UserId == userId)
                .Select(d => new DriverViewModel
                {
                    Id = d.Id,
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                    IDCard = d.IDCard,
                    Address = d.Address,
                    GSM = d.GSM,
                    UserName = d.User.UserName,
                    Cars = d.Cars.Select(c => new CarListItemViewModel
                    {
                        Id = c.Id,
                        Brand = c.Brand,
                        RegNumber = c.RegNumber,
                        Year = c.Year,
                        Places = c.Places
                    }).ToList()
                })
                .FirstOrDefault();

            if (model == null)
                return RedirectToAction(nameof(Create));


            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var userId = userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
                return Challenge();

            
            var hasProfile = dbContext.Drivers.AsNoTracking().Any(d => d.UserId == userId);
            if (hasProfile)
                return RedirectToAction(nameof(Details));

            return View();
        }
        [HttpPost]

        public IActionResult Create(DriverViewModel model)
        {
            var userId = userManager.GetUserId(User);

            if (string.IsNullOrEmpty(userId))
                return Challenge();

            
            var alreadyExists = dbContext.Drivers.Any(d => d.UserId == userId);
            if (alreadyExists)
                return RedirectToAction(nameof(Details));

            if (!ModelState.IsValid)
                return View(model);

            var driver = new Driver
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                IDCard = model.IDCard,
                Address = model.Address,
                GSM = model.GSM,
                UserId = userId
            };

                dbContext.Drivers.Add(driver);
                dbContext.SaveChanges();
                return RedirectToAction(nameof(Details));
           
        }
        [HttpGet]
  
        public IActionResult Edit(int id)
        {
            var userId = userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
                return Challenge();

            var model = dbContext.Drivers
                .AsNoTracking()
                .Where(d => d.Id == id && d.UserId == userId)
                .Select(d => new DriverViewModel
                {
                    Id = d.Id,
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                    IDCard = d.IDCard,
                    Address = d.Address,
                    GSM = d.GSM
                })
                .SingleOrDefault();

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }
        [HttpPost]

        public IActionResult Edit(int id, DriverViewModel model)
        {
            var userId = userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
                return Challenge();

            if (id != model.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var driver = dbContext.Drivers
                .FirstOrDefault(d => d.Id == id && d.UserId == userId);

            if (driver == null)
                return NotFound();

            driver.FirstName = model.FirstName;
            driver.LastName = model.LastName;
            driver.IDCard = model.IDCard;
            driver.Address = model.Address;
            driver.GSM = model.GSM;

            dbContext.SaveChanges();

            return RedirectToAction(nameof(Details));
        }

    }
}
