using FastFinance.Data;
using FastFinance.FastCrud.EFCore.Dtos;
using FastFinance.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers;

[ApiController]
[Route("/accounts")]
public class AccountController : ControllerBase
{
    private readonly IChartOfAccounts _chartOfAccount;

    public AccountController(IChartOfAccounts chartOfAccount)
    {
        _chartOfAccount = chartOfAccount;
    }

    [HttpPost]
    public async Task<ActionResult> AddAccount([FromBody] AddAccountRequest request)
    {
        var account = await _chartOfAccount.AddAsync<Account, AddAccountRequest>(request);
        _ = await _chartOfAccount.SaveChangesAsync<Account>();
        return Ok(account);
    }

    [HttpGet]
    public async Task<ActionResult> GetAccounts([FromQuery] GetAccountsQuery query)
    {
        var accounts = await _chartOfAccount.GetAsync<Account, GetAccountsQuery>(query);
        return Ok(accounts);
    }
}
