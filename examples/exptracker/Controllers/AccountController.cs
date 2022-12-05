using FastFinance.Data;
using FastFinance.FastCrud.EFCore.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers;

[ApiController]
[Route("/accounts")]
public class AccountController : ControllerBase
{
    private readonly ChartOfAccount _chartOfAccount;

    public AccountController(ChartOfAccount chartOfAccount)
    {
        _chartOfAccount = chartOfAccount;
    }

    [HttpPost]
    public async Task<ActionResult> AddAccount([FromBody] AddAccountRequest request)
    {
        var account = await _chartOfAccount.AddAsync(request);
        _ = await _chartOfAccount.SaveChangesAsync();
        return Ok(account);
    }

    [HttpGet]
    public async Task<ActionResult> GetAccounts([FromQuery] GetAccountsQuery query)
    {
        var accounts = await _chartOfAccount.GetAsync(query);
        return Ok(accounts);
    }
}
