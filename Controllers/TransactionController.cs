using GeneralStoreAPI.Data;
using GeneralStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeneralStoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : Controller
    {
        private GeneralStoreDBContext _db;

        public TransactionController(GeneralStoreDBContext db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction(TransactionEdit newTransaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Transaction transaction = new Transaction()
            {
                ProductId = newTransaction.ProductId,
                CustomerId = newTransaction.CustomerId,
                Quantity = newTransaction.Quantity,
            };

            _db.Transactions.Add(transaction);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTransaction()
        {
            var transactions = await _db.Transactions.ToListAsync();
            return Ok(transactions);
        }
    }
}