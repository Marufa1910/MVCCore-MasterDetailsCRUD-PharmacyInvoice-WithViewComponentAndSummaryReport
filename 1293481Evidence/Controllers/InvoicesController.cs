using _1293481Evidence.Models;
using _1293481Evidence.Models.ViewModels;
using _1293481Evidence.Models.ViewModels._1293481Evidence.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace _1293481Evidence.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly StoredProcedureDbpharmaContext _context;
        private readonly IWebHostEnvironment _env;

        public InvoicesController(StoredProcedureDbpharmaContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var invoices = await _context.Invoices
                .Include(i => i.TransactionType)
                .Include(i => i.SaleItems)
                .ToListAsync();
            return View(invoices);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new InvoiceViewModel();
            viewModel.TransactionTypes = _context.TransactionTypes.ToList();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(InvoiceViewModel model)
        {
            string imageFileName = await GetImageFileNameAsync(model.ProfileFile);
            string imageUrl = "~/images/" + imageFileName;

            var saleItemsTable = new DataTable();
            saleItemsTable.Columns.Add("MedicineName", typeof(string));
            saleItemsTable.Columns.Add("Quantity", typeof(int));
            saleItemsTable.Columns.Add("UnitPrice", typeof(decimal));

            if (model.SaleItems != null)
            {
                foreach (var item in model.SaleItems)
                {
                    saleItemsTable.Rows.Add(item.MedicineName, item.Quantity, item.UnitPrice);
                }
            }

            var parameters = new[] {
                    new SqlParameter("@InvoiceNo", model.InvoiceNo),
                    new SqlParameter("@TransactionDate", model.TransactionDate),
                    new SqlParameter("@ClientName", model.ClientName),
                    new SqlParameter("@IsNewClient", model.IsNewClient),
                    new SqlParameter("@ReferrerName", model.ReferrerName),
                    new SqlParameter("@TransactionTypeId", model.TransactionTypeId),
                    new SqlParameter("@ImageUrl", imageUrl),
                    new SqlParameter("@SaleItems", SqlDbType.Structured) {
                        Value = saleItemsTable,
                        TypeName = "dbo.ParamSaleItemType"
                    }
                };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC dbo.InsertInvoiceSP @InvoiceNo, @TransactionDate, @ClientName, @IsNewClient, @ReferrerName, @TransactionTypeId, @ImageUrl, @SaleItems",
                parameters);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var invoice = await _context.Invoices
                .Include(i => i.SaleItems)
                .FirstOrDefaultAsync(i => i.InvoiceId == id);

            if (invoice == null) return NotFound();

            var viewModel = new InvoiceViewModel
            {
                InvoiceId = invoice.InvoiceId,
                InvoiceNo = invoice.InvoiceNo,
                TransactionDate = invoice.TransactionDate,
                ClientName = invoice.ClientName,
                IsNewClient = invoice.IsNewClient,
                ReferrerName = invoice.ReferrerName,
                TransactionTypeId = invoice.TransactionTypeId,
                ImageUrl = invoice.ImageUrl,
                
                SaleItems = invoice.SaleItems.ToList() ?? new List<SaleItem>(),
              
                TransactionTypes = await _context.TransactionTypes.ToListAsync()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(InvoiceViewModel model, string OldImageUrl)
        {
            
            string imageUrl = OldImageUrl;

            if (model.ProfileFile != null && model.ProfileFile.Length > 0)
            {
                if (!string.IsNullOrEmpty(OldImageUrl))
                {
                    string oldPath = Path.Combine(_env.WebRootPath, OldImageUrl.TrimStart('~', '/'));
                    if (System.IO.File.Exists(oldPath)) { System.IO.File.Delete(oldPath); }
                }
                string imageFileName = await GetImageFileNameAsync(model.ProfileFile);
                imageUrl = "~/images/" + imageFileName;
            }

            
            var saleItemsTable = new DataTable();
            saleItemsTable.Columns.Add("MedicineName", typeof(string));
            saleItemsTable.Columns.Add("Quantity", typeof(int));
            saleItemsTable.Columns.Add("UnitPrice", typeof(decimal));

            foreach (var item in model.SaleItems ?? new List<SaleItem>())
            {
                if (!string.IsNullOrWhiteSpace(item.MedicineName))
                {
                    saleItemsTable.Rows.Add(item.MedicineName, item.Quantity, item.UnitPrice);
                }
            }

            var parameters = new[] {
        new SqlParameter("@InvoiceId", model.InvoiceId),
        new SqlParameter("@InvoiceNo", model.InvoiceNo ?? (object)DBNull.Value),
        new SqlParameter("@TransactionDate", model.TransactionDate),
        new SqlParameter("@ClientName", model.ClientName ?? (object)DBNull.Value),
        new SqlParameter("@IsNewClient", model.IsNewClient),
        new SqlParameter("@ReferrerName", model.ReferrerName ?? (object)DBNull.Value),
        new SqlParameter("@TransactionTypeId", model.TransactionTypeId),
        new SqlParameter("@ImageUrl", (object)imageUrl ?? DBNull.Value),
        new SqlParameter("@SaleItems", SqlDbType.Structured) {
            Value = saleItemsTable,
            TypeName = "dbo.ParamSaleItemType"
        }
    };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC dbo.UpdateInvoiceSP @InvoiceId, @InvoiceNo, @TransactionDate, @ClientName, @IsNewClient, @ReferrerName, @TransactionTypeId, @ImageUrl, @SaleItems",
                parameters);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var parameter = new SqlParameter("@InvoiceId", id);
            await _context.Database.ExecuteSqlRawAsync("EXEC dbo.DeleteInvoiceItemSp @InvoiceId", parameter);
            return RedirectToAction("Index");


        }


        private async Task<string> GetImageFileNameAsync(IFormFile profileFile)
        {
            if (profileFile == null) return "";
            string name = Guid.NewGuid().ToString() + "-" + profileFile.FileName;
            var path = Path.Combine(_env.WebRootPath, "images", name);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await profileFile.CopyToAsync(stream);
            }
            return name;
        }
        public async Task<IActionResult> Aggregate()
        {

            var data = _context.SaleItems
                .Include(s => s.Invoice)
                .ThenInclude(i => i.TransactionType);

            bool hasData = await data.AnyAsync();
            var min = hasData ? await data.MinAsync(s => s.UnitPrice) : 0;
            var max = hasData ? await data.MaxAsync(s => s.UnitPrice) : 0;
            var sum = hasData ? await data.SumAsync(s => s.UnitPrice * s.Quantity) : 0;
            var avg = hasData ? await data.AverageAsync(s => s.UnitPrice) : 0;
            var count = await data.CountAsync();

            var groupByResult = await data.GroupBy(s => new {

                Id = s.Invoice.TransactionTypeId,
                Name = s.Invoice.TransactionType.TransactionTypeName
            })
            .Select(g => new GroupByAggregateViewModel
            {
                TransactionTypeId = g.Key.Id,
                TransactionTypeName = g.Key.Name ?? "N/A",
                Count = g.Count(),
                MinValue = g.Min(s => s.UnitPrice),
                MaxValue = g.Max(s => s.UnitPrice),
                SumValue = g.Sum(s => s.UnitPrice * s.Quantity),
                AvgValue = g.Average(s => s.UnitPrice)
            }).ToListAsync();

            var model = new AggregateViewModel
            {
                MinValue = min,
                MaxValue = max,
                SumValue = sum,
                AvgValue = avg,
                Count = count,
                GroupByAggregateViewModelResult = groupByResult
            };

            return View(model);
        }
    }
}
