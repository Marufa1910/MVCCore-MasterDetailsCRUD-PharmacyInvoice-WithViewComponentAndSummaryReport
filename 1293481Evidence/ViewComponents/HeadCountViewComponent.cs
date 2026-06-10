using _1293481Evidence.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace _1293481Evidence.ViewComponents
{
    public class HeadCountViewComponent: ViewComponent
    {
        private readonly StoredProcedureDbpharmaContext _db;

        public HeadCountViewComponent(StoredProcedureDbpharmaContext db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
           
            var summaryData = await _db.SaleItems
                .Include(s => s.Invoice) 
                .GroupBy(s => s.Invoice.ClientName)
                .Select(g => new SaleItemHeadCount
                {
                    ClientName = g.Key,
                    
                    TotalQuantity = g.Count()
                })
                .OrderBy(x => x.ClientName)
                .ToListAsync();

            return View(summaryData);
        }
    }
}
