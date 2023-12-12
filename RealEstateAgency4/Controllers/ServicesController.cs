using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RealEstateAgency4.Filters;
using RealEstateAgency4.Middleware;
using RealEstateAgency4.Models;
using RealEstateAgency4.Services;
using RealEstateAgency4.ViewModels;

namespace RealEstateAgency4.Controllers
{
    [Authorize]
    public class ServicesController : Controller
    {
        private readonly RealEstateAgencyContext _context;

        private readonly int pageSize = 12;
        public ServicesController(RealEstateAgencyContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(SortState sortOrder, int page = 1)
        {
            var cache = HttpContext.RequestServices.GetRequiredService<ServicesCache>();
            ServicesViewModel servicesViewModel;

            var services = HttpContext.Session.Get<ServicesViewModel>("Service");

            if (services == null)
            {
                services = new ServicesViewModel();
            }

            IEnumerable<Service> serviceContext = _context.Services;

            serviceContext = Sort_Search(serviceContext, sortOrder, services.Name, services.Description, services.Price);

            var count = serviceContext.Count();
            serviceContext = serviceContext.Skip((page - 1) * pageSize).Take(pageSize);

            ServicesViewModel serviceModel = new ServicesViewModel
            {
                services = serviceContext,
                pageViewModel = new PageViewModel(count, page, pageSize),
                sortViewModel = new SortViewModel(sortOrder),
                Name = services.Name,
                Description = services.Description,
                Price = services.Price
            };
            return View(serviceModel);

        }

        [HttpPost]
        public async Task<IActionResult> Index(ServicesViewModel service)
        {
            HttpContext.Session.Set("Service", service);

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Details(int? id)
        {
            var cache = HttpContext.RequestServices.GetRequiredService<ServicesCache>();

            if (id == null || _context.Services == null)
            {
                return NotFound();
            }

            var service = cache.GetServices()
                .FirstOrDefault(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create(Service service)
        {
            var cache = HttpContext.RequestServices.GetRequiredService<ServicesCache>();

            if (ModelState.IsValid)
            {
                _context.Add(service);
                await _context.SaveChangesAsync();
                cache.SetServices();
                return RedirectToAction(nameof(Index));
            }
            return View(service);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            var cache = HttpContext.RequestServices.GetRequiredService<ServicesCache>();

            if (id == null || _context.Services == null)
            {
                return NotFound();
            }

            var service = cache.GetServices().FirstOrDefault(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, Service service)
        {
            var cache = HttpContext.RequestServices.GetRequiredService<ServicesCache>();

            if (id != service.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(service);
                    await _context.SaveChangesAsync();
                    cache.SetServices();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(service.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(service);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Services == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cache = HttpContext.RequestServices.GetRequiredService<ServicesCache>();

            if (_context.Services == null)
            {
                return Problem("Entity set 'RealEstateAgencyContext.Services'  is null.");
            }
            var service = cache.GetServices().FirstOrDefault(m => m.Id == id);
            if (service != null)
            {
                _context.Services.Remove(service);
            }
            
            await _context.SaveChangesAsync();
            cache.SetServices();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceExists(int id)
        {
          return (_context.Services?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private IEnumerable<Service> Sort_Search(IEnumerable<Service> services, SortState sortOrder, string searchName, string searchDescription,
            decimal searchPrice)
        {
            switch (sortOrder)
            {
                case SortState.PriceAsc:
                    services = services.OrderBy(s => s.Price);
                    break;
                case SortState.PriceDesc:
                    services = services.OrderByDescending(s => s.Price);
                    break;
            }
            services = services.Where(s => (s.Name == searchName || searchName.IsNullOrEmpty())
                && (s.Description == searchDescription || searchDescription.IsNullOrEmpty())
                && (s.Price == searchPrice || searchPrice == 0));
            return services;
        }
    }
}
