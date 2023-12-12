using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
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
    public class ContractsController : Controller
    {
        private readonly RealEstateAgencyContext _context;

        private readonly int pageSize = 12;

        public ContractsController(RealEstateAgencyContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(SortState sortOrder, int page = 1)
        {
            var cache = HttpContext.RequestServices.GetRequiredService<ContractsCache>();
            ContractsViewModel contractsViewModel;

            var contracts = HttpContext.Session.Get<ContractsViewModel>("Contract");

            if (contracts == null)
            {
                contracts = new ContractsViewModel();
            }

            IEnumerable<Contract> contractContext = cache.GetContracts();

            contractContext = Sort_Search(contractContext, sortOrder, contracts.DateOfContract, contracts.SellerName ?? "", contracts.DealAmount, contracts.ServiceCost,
                contracts.Employee ?? "", contracts.Fiobuyer ?? "");

            var count = contractContext.Count();
            contractContext = contractContext.Skip((page - 1) * pageSize).Take(pageSize);

            ContractsViewModel contractModel = new ContractsViewModel
            {
                contracts = contractContext,
                pageViewModel = new PageViewModel(count, page, pageSize),
                sortViewModel = new SortViewModel(sortOrder),
                DateOfContract = contracts.DateOfContract,
                SellerName = contracts.SellerName,
                DealAmount = contracts.DealAmount,
                ServiceCost = contracts.ServiceCost,
                Employee = contracts.Employee,
                Fiobuyer = contracts.Fiobuyer
            };
            return View(contractModel);

        }

        [HttpPost]
        public async Task<IActionResult> Index(ContractsViewModel contract)
        {
            HttpContext.Session.Set("Contract", contract);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var cache = HttpContext.RequestServices.GetRequiredService<ContractsCache>();

            if (id == null || _context.Contracts == null)
            {
                return NotFound();
            }

            var contract =  cache.GetContracts()
                .FirstOrDefault(m => m.Id == id);
            if (contract == null)
            {
                return NotFound();
            }

            return View(contract);
        }

        public async Task<IActionResult> Details(int? id)
        {
            var cache = HttpContext.RequestServices.GetRequiredService<ContractsCache>();

            if (id == null || _context.Contracts == null)
            {
                return NotFound();
            }

            var contract = cache.GetContracts()
                .FirstOrDefault(m => m.Id == id);
            if (contract == null)
            {
                return NotFound();
            }

            return View(contract);
        }

        public IActionResult Create()
        {
            ViewData["SellerId"] = new SelectList(_context.Sellers, "Id", "FullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create(Contract contract)
        {
            var cache = HttpContext.RequestServices.GetRequiredService<ContractsCache>();

            if (ModelState.IsValid)
            {
                _context.Add(contract);
                await _context.SaveChangesAsync();
                cache.SetContracts();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SellerId"] = new SelectList(_context.Sellers, "Id", "FullName", contract.Id);
            return View(contract);
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            var cache = HttpContext.RequestServices.GetRequiredService<ContractsCache>();

            if (id == null || _context.Contracts == null)
            {
                return NotFound();
            }

            var contract = cache.GetContracts().FirstOrDefault(m => m.Id == id);
            if (contract == null)
            {
                return NotFound();
            }
            ViewData["SellerId"] = new SelectList(_context.Sellers, "Id", "FullName", contract.Id);
            return View(contract);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, Contract contract)
        {
            var cache = HttpContext.RequestServices.GetRequiredService<ContractsCache>();

            if (id != contract.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contract);
                    await _context.SaveChangesAsync();
                    cache.SetContracts();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContractExists(contract.Id))
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
            ViewData["SellerId"] = new SelectList(_context.Sellers, "Id", "FullName", contract.SellerId);
            return View(contract);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cache = HttpContext.RequestServices.GetRequiredService<ContractsCache>();

            if (_context.Contracts == null)
            {
                return Problem("Entity set 'RealEstateAgencyContext.Contracts'  is null.");
            }
            var contract = cache.GetContracts().FirstOrDefault(m => m.Id == id);

            if (contract != null)
            {
                _context.Contracts.Remove(contract);
            }

            await _context.SaveChangesAsync();
            cache.SetContracts();
            return RedirectToAction(nameof(Index));
        }

        private bool ContractExists(int id)
        {
            return (_context.Contracts?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private IEnumerable<Contract> Sort_Search(IEnumerable<Contract> contracts, SortState sortOrder, DateTime? searchDate, string searchSellerName,
            decimal searchDealAmount, decimal searchServiceCost, string searchEmployee, string searchFiobuyer)
        {
            switch (sortOrder)
            {
                case SortState.DateOfContractAsc:
                    contracts = contracts.OrderBy(s => s.DateOfContract);
                    break;
                case SortState.DateOfContractDesc:
                    contracts = contracts.OrderByDescending(s => s.DateOfContract);
                    break;
                case SortState.DealAmountAsc:
                    contracts = contracts.OrderBy(s => s.DealAmount);
                    break;
                case SortState.DealAmountDesc:
                    contracts = contracts.OrderByDescending(s => s.DealAmount);
                    break;
                case SortState.ServiceCostAsc:
                    contracts = contracts.OrderBy(s => s.ServiceCost);
                    break;
                case SortState.ServiceCostDesc:
                    contracts = contracts.OrderByDescending(s => s.ServiceCost);
                    break;


            }
            contracts = contracts
                .Where(c => (c.DateOfContract == searchDate || searchDate == new DateTime() || searchDate == null)
                && (c.Seller.FullName.Contains(searchSellerName ?? ""))
                && (c.DealAmount == searchDealAmount || searchDealAmount == 0)
                && (c.ServiceCost == searchServiceCost || searchServiceCost == 0)
                && (c.Employee == searchEmployee || searchEmployee.IsNullOrEmpty())
                && (c.Fiobuyer == searchFiobuyer || searchFiobuyer.IsNullOrEmpty()));



            return contracts;
        }
    }
}
