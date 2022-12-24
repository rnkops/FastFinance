using FastFinance.Data;
using FastFinance.FastCrud.EFCore.Dtos;
using FastFinance.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers;

[ApiController]
[Route("/transactions")]
public class TransactionController : ControllerBase
{
    private readonly Journal _journal;

    public TransactionController(Journal journal)
    {
        _journal = journal;
    }

    [HttpPost]
    public async Task<ActionResult> AddTransaction([FromBody] AddTransactionRequest request)
    {
        var transaction = request.GetTransaction();
        await _journal.AppendAsync(transaction);
        _ = await _journal.SaveChangesAsync<Transaction>();
        return Ok(transaction);
    }

    [HttpGet]
    public async Task<ActionResult> GetTransactions([FromQuery] GetTransactionsQuery getTransactionsQuery)
    {
        var transactions = await _journal.GetAsync<Transaction, GetTransactionsQuery>(getTransactionsQuery);
        return Ok(transactions);
    }
}
