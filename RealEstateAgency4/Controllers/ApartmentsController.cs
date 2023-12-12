using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class ApartmentsController : Controller
    {
        private readonly RealEstateAgencyContext _context;

        private readonly int pageSize = 12;
        public ApartmentsController(RealEstateAgencyContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(SortState sortOrder, int page = 1)
        {
            var cache = HttpContext.RequestServices.GetRequiredService<ApartmentsCache>();
            ApartmentsViewModel apartmentsViewModel;

            var apartments = HttpContext.Session.Get<ApartmentsViewModel>("Apartments");

            if (apartments == null)
            {
                apartments = new ApartmentsViewModel();
            }

            IEnumerable<Apartment> apartmentContext = _context.Apartments.AsEnumerable();

            apartmentContext = Sort_Search(apartmentContext, sortOrder, apartments.Name ?? "", apartments.Description ?? "", apartments.NumberOfRooms,
                apartments.AdditionalPreferences ?? "", apartments.MaxPrice, apartments.HasPhone, apartments.Area, apartments.SeparateBathroom);

            var count = apartmentContext.Count();
            apartmentContext = apartmentContext.Skip((page - 1) * pageSize).Take(pageSize);

            ApartmentsViewModel apartmentModel = new ApartmentsViewModel
            {
                apartments = apartmentContext,
                pageViewModel = new PageViewModel(count, page, pageSize),
                sortViewModel = new SortViewModel(sortOrder),
                Name = apartments.Name,
                Description = apartments.Description,
                NumberOfRooms = apartments.NumberOfRooms,
                Area = apartments.Area,
                SeparateBathroom = apartments.SeparateBathroom,
                HasPhone = apartments.HasPhone,
                MaxPrice = apartments.MaxPrice,
                AdditionalPreferences = apartments.AdditionalPreferences
            };
            return View(apartmentModel);

        }

        [HttpPost]
        public async Task<IActionResult> Index(ApartmentsViewModel apartment)
        {
            HttpContext.Session.Set("Apartments", apartment);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int? id)
        {
            var cache = HttpContext.RequestServices.GetRequiredService<ApartmentsCache>();

            if (id == null || _context.Apartments == null)
            {
                return NotFound();
            }

            var apartment = cache.GetApartments()
                .FirstOrDefault(m => m.Id == id);
            if (apartment == null)
            {
                return NotFound();
            }

            return View(apartment);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> Create(Apartment apartment)
        {
            var cache = HttpContext.RequestServices.GetRequiredService<ApartmentsCache>();

            if (ModelState.IsValid)
            {
                _context.Add(apartment);
                await _context.SaveChangesAsync();
                cache.SetApartments();
                return RedirectToAction(nameof(Index));
            }
            return View(apartment);
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            var cache = HttpContext.RequestServices.GetRequiredService<ApartmentsCache>();

            if (id == null || _context.Apartments == null)
            {
                return NotFound();
            }

            var apartment = cache.GetApartments().FirstOrDefault(m => m.Id == id);
            if (apartment == null)
            {
                return NotFound();
            }
            return View(apartment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, Apartment apartment)
        {
            var cache = HttpContext.RequestServices.GetRequiredService<ApartmentsCache>();

            if (id != apartment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(apartment);
                    await _context.SaveChangesAsync();
                    cache.SetApartments();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApartmentExists(apartment.Id))
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
            return View(apartment);
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            var cache = HttpContext.RequestServices.GetRequiredService<ApartmentsCache>();

            if (id == null || _context.Apartments == null)
            {
                return NotFound();
            }

            var apartment = cache.GetApartments().FirstOrDefault(m => m.Id == id);
            if (apartment == null)
            {
                return NotFound();
            }

            return View(apartment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cache = HttpContext.RequestServices.GetRequiredService<ApartmentsCache>();

            var apartment = cache.GetApartments().FirstOrDefault(m => m.Id == id);
            if (apartment != null)
            {
                _context.Apartments.Remove(apartment);
            }

            await _context.SaveChangesAsync();
            cache.SetApartments();
            return RedirectToAction(nameof(Index));
        }

        private bool ApartmentExists(int id)
        {
            return (_context.Apartments?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private IEnumerable<Apartment> Sort_Search(IEnumerable<Apartment> apartments, SortState sortOrder, string searchName, string searchDescription, int searchNumberOfRooms, string searchAdditionalPreferences, decimal searchMaxPrice, bool? searchHasPhone, decimal searchArea, bool? searchSeparateBathroom)
        {
            switch (sortOrder)
            {
                case SortState.DescriptionApartmentAsc:
                    apartments = apartments.OrderBy(a => a.Description);
                    break;
                case SortState.DescriptionApartmentDesc:
                    apartments = apartments.OrderByDescending(a => a.Description);
                    break;
                case SortState.NumberOfRoomsAsc:
                    apartments = apartments.OrderBy(a => a.NumberOfRooms);
                    break;
                case SortState.NumberOfRoomsDesc:
                    apartments = apartments.OrderByDescending(a => a.NumberOfRooms);
                    break;
                case SortState.AreaAsc:
                    apartments = apartments.OrderBy(a => a.Area);
                    break;
                case SortState.AreaDesc:
                    apartments = apartments.OrderByDescending(a => a.Area);
                    break;
                case SortState.SeparateBathroomAsc:
                    apartments = apartments.OrderBy(a => a.SeparateBathroom);
                    break;
                case SortState.SeparateBathroomDesc:
                    apartments = apartments.OrderByDescending(a => a.SeparateBathroom);
                    break;
                case SortState.HasPhoneAsc:
                    apartments = apartments.OrderBy(a => a.HasPhone);
                    break;
                case SortState.HasPhoneDesc:
                    apartments = apartments.OrderByDescending(a => a.HasPhone);
                    break;
                case SortState.MaxPriceAsc:
                    apartments = apartments.OrderBy(a => a.MaxPrice);
                    break;
                case SortState.MaxPriceDesc:
                    apartments = apartments.OrderByDescending(a => a.MaxPrice);
                    break;

            }
            apartments = apartments.Where(a => (a.Name == searchName || searchName.IsNullOrEmpty())
                && (a.Area == searchArea || searchArea == 0)
                && (a.MaxPrice == searchMaxPrice || searchMaxPrice == 0)
                && (a.NumberOfRooms == searchNumberOfRooms || searchNumberOfRooms == 0)
                && (a.Description == searchDescription || searchDescription.IsNullOrEmpty())
                && (searchHasPhone == null || a.HasPhone == searchHasPhone)
                && (searchSeparateBathroom == null || a.SeparateBathroom == searchSeparateBathroom));

            return apartments;
        }
    }
}
