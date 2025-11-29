using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoMoyka.Backend.Application.Services.Transaction;
using PoMoyka.Backend.Contracts.DTOs.ReadingDTOs;

namespace PoMoyka.Backend.API.Controllers
{
    [Authorize]
    public class TransactionController : BaseController
    {
        /// <summary>
        /// Получить все транзакции (только для Admin)
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<TransactionDto>>> GetAll()
        {
            var query = new GetAllTransactionsQuery();
            var transactions = await Mediator.Send(query);
            return Ok(transactions);
        }

        /// <summary>
        /// Получить свои транзакции (для Client)
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Client,Admin")]
        public async Task<ActionResult<List<TransactionDto>>> GetMy()
        {
            var query = new GetMyTransactionsQuery();
            var transactions = await Mediator.Send(query);
            return Ok(transactions);
        }
    }
}

