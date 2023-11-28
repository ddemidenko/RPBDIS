using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RealEstateAgency4.Filters;
using RealEstateAgency4.Models;
using RealEstateAgency4.ViewModels;

namespace RealEstateAgency4.Controllers
{
    [Authorize(Roles = "admin")]
    public class SellersController : Controller
    {
        private readonly RealEstateAgencyContext _context;

        private readonly int pageSize = 12;
        public SellersController(RealEstateAgencyContext context)
        {
            _context = context;
        }

        // GET: Sellers
        public async Task<IActionResult> Index(SortState sortOrder, int page = 1)
        {
            SellersViewModel sellersViewModel;

            var sellers = HttpContext.Session.Get<SellersViewModel>("Seller");

            if (sellers == null)
            {
                sellers = new SellersViewModel();
            }

            IQueryable<Seller> sellerContext = _context.Sellers;

            sellerContext = Sort_Search(sellerContext, sortOrder, sellers.DateOfBirth, sellers.FullName ?? "", sellers.Gender ?? "", sellers.Address ?? "", sellers.Phone ?? "", sellers.PassportDate ?? "", sellers.ApartmentAddress ?? "", sellers.Price,
            sellers.AdditionalInformation ?? "", sellers.ApartmentName ?? "");

            // Разбиение на страницы
            var count = sellerContext.Count();
            sellerContext = sellerContext.Skip((page - 1) * pageSize).Take(pageSize);

            // Формирование модели для передачи представлению
            SellersViewModel sellerModel = new SellersViewModel
            {
                sellers = sellerContext,
                pageViewModel = new PageViewModel(count, page, pageSize),
                sortViewModel = new SortViewModel(sortOrder),
                DateOfBirth = sellers.DateOfBirth,
                FullName = sellers.FullName,
                Gender = sellers.Gender,
                Address = sellers.Address,
                Phone = sellers.Phone,
                PassportDate = sellers.PassportDate,
                ApartmentAddress = sellers.ApartmentAddress,
                Price = sellers.Price,
                AdditionalInformation = sellers.AdditionalInformation,
                ApartmentName = sellers.ApartmentName,
            };
            return View(sellerModel);

        }

        [HttpPost]
        public async Task<IActionResult> Index(SellersViewModel seller)
        {
            HttpContext.Session.Set("Seller", seller);

            return RedirectToAction("Index");
        }

        // GET: Sellers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sellers == null)
            {
                return NotFound();
            }

            var seller = await _context.Sellers
                .Include(s => s.Apartment)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (seller == null)
            {
                return NotFound();
            }

            return View(seller);
        }

        // GET: Sellers/Create
        public IActionResult Create()
        {
            ViewData["ApartmentId"] = new SelectList(_context.Apartments, "Id", "Name");
            return View();
        }

        // POST: Sellers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            if (ModelState.IsValid)
            {
                _context.Add(seller);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApartmentId"] = new SelectList(_context.Apartments, "Id", "Name", seller.ApartmentId);
            return View(seller);
        }

        // GET: Sellers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sellers == null)
            {
                return NotFound();
            }

            var seller = await _context.Sellers.Include(m => m.Apartment)
                .Include(s => s.Apartment).FirstOrDefaultAsync(m => m.Id == id);
            if (seller == null)
            {
                return NotFound();
            }
            ViewData["ApartmentId"] = new SelectList(_context.Apartments, "Id", "Name", seller.ApartmentId);
            return View(seller);
        }

        // POST: Sellers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (id != seller.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(seller);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SellerExists(seller.Id))
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
            ViewData["ApartmentId"] = new SelectList(_context.Apartments, "Id", "Name", seller.ApartmentId);
            return View(seller);
        }

        // GET: Sellers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sellers == null)
            {
                return NotFound();
            }

            var seller = await _context.Sellers
                .Include(s => s.Apartment)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (seller == null)
            {
                return NotFound();
            }

            return View(seller);
        }

        // POST: Sellers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sellers == null)
            {
                return Problem("Entity set 'RealEstateAgencyContext.Sellers'  is null.");
            }
            var seller = await _context.Sellers.FindAsync(id);
            if (seller != null)
            {
                _context.Sellers.Remove(seller);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SellerExists(int id)
        {
            return (_context.Sellers?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private IQueryable<Seller> Sort_Search(IQueryable<Seller> sellers, SortState sortOrder, DateTime? searchDateOfBirth, string searchFullName,
            string searchGender, string searchAddress, string searchPhone, string searchPassportDate, string searchApartmentAddress, decimal SellerPrice, string AdditionalInformation, string ApartmentName)
        {
            switch (sortOrder)
            {
                case SortState.DateOfBirthAsc:
                    sellers = sellers.OrderBy(s => s.DateOfBirth);
                    break;
                case SortState.DateOfBirthDesc:
                    sellers = sellers.OrderByDescending(s => s.DateOfBirth);
                    break;
                case SortState.PriceSellerAsc:
                    sellers = sellers.OrderBy(s => s.Price);
                    break;
                case SortState.PriceSellerDesc:
                    sellers = sellers.OrderByDescending(s => s.Price);
                    break;


            }
            sellers = sellers.Include(o => o.Apartment)
        .Where(c =>
            (c.DateOfBirth == searchDateOfBirth || searchDateOfBirth == DateTime.MinValue || searchDateOfBirth == null)
            && (c.FullName == searchFullName || searchFullName.IsNullOrEmpty())
            && (c.Gender.Contains(searchGender ?? ""))
            && (c.Address.Contains(searchAddress ?? ""))
            && (c.Phone.Contains(searchPhone ?? ""))
            && (c.PassportData.Contains(searchPassportDate ?? ""))
            && (c.Apartment.Name == ApartmentName || ApartmentName.IsNullOrEmpty())
            && (c.ApartmentAddress == searchApartmentAddress || searchApartmentAddress.IsNullOrEmpty())
            && (c.Price == SellerPrice || SellerPrice == 0)
            && (c.AdditionalInformation == AdditionalInformation || AdditionalInformation.IsNullOrEmpty())
        );
            return sellers;
        }
    }
}
