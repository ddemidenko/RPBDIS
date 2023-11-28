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
using RealEstateAgency4.Models;
using RealEstateAgency4.ViewModels;

namespace RealEstateAgency4.Controllers
{
    [Authorize(Roles = "admin")]
    public class ContractsController : Controller
    {
        private readonly RealEstateAgencyContext _context;

        private readonly int pageSize = 12;

        public ContractsController(RealEstateAgencyContext context)
        {
            _context = context;
        }

        // GET: Contracts
        public async Task<IActionResult> Index(SortState sortOrder, int page = 1)
        {
            ContractsViewModel contractsViewModel;

            var contracts = HttpContext.Session.Get<ContractsViewModel>("Contract");

            if (contracts == null)
            {
                contracts = new ContractsViewModel();
            }

            IQueryable<Contract> contractContext = _context.Contracts;

            contractContext = Sort_Search(contractContext, sortOrder, contracts.DateOfContract, contracts.SellerName ?? "", contracts.DealAmount, contracts.ServiceCost,
                contracts.Employee ?? "", contracts.Fiobuyer ?? "");

            // Разбиение на страницы
            var count = contractContext.Count();
            contractContext = contractContext.Skip((page - 1) * pageSize).Take(pageSize);

            // Формирование модели для передачи представлению
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
            if (id == null || _context.Contracts == null)
            {
                return NotFound();
            }

            var contract = await _context.Contracts
                .Include(c => c.Seller)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contract == null)
            {
                return NotFound();
            }

            return View(contract);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Contracts == null)
            {
                return NotFound();
            }

            var contract = await _context.Contracts
                .Include(c => c.Seller)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contract == null)
            {
                return NotFound();
            }

            return View(contract);
        }

        // GET: Contracts/Create
        public IActionResult Create()
        {
            ViewData["SellerId"] = new SelectList(_context.Sellers, "Id", "FullName");
            return View();
        }

        // POST: Contracts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contract contract)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contract);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SellerId"] = new SelectList(_context.Sellers, "Id", "FullName", contract.Id);
            return View(contract);
        }

        // GET: Contracts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Contracts == null)
            {
                return NotFound();
            }

            var contract = await _context.Contracts.FindAsync(id);
            if (contract == null)
            {
                return NotFound();
            }
            ViewData["SellerId"] = new SelectList(_context.Sellers, "Id", "FullName", contract.Id);
            return View(contract);
        }

        // POST: Contracts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Contract contract)
        {
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Contracts == null)
            {
                return Problem("Entity set 'RealEstateAgencyContext.Contracts'  is null.");
            }
            var contract = await _context.Contracts.FindAsync(id);
            if (contract != null)
            {
                _context.Contracts.Remove(contract);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContractExists(int id)
        {
            return (_context.Contracts?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private IQueryable<Contract> Sort_Search(IQueryable<Contract> contracts, SortState sortOrder, DateTime? searchDate, string searchSellerName,
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
            contracts = contracts.Include(o => o.Seller)
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
