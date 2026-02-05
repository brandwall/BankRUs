using BankRUs.Api.Dtos.BankAccounts;
using BankRUs.Application.UseCases.CreateDeposit;
using BankRUs.Application.UseCases.OpenBankAccount;
using BankRUs.Domain.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace BankRUs.Api.Controllers;

[Route("api/bank-accounts")]
[ApiController]
public class BankAccountsController : ControllerBase
{
    private readonly OpenBankAccountHandler _openBankAccountHandler;
    private readonly CreateDepositHandler _depositHandler;

    public BankAccountsController(
        OpenBankAccountHandler openBankAccountHandler,
        CreateDepositHandler depositHandler)
    {
        _openBankAccountHandler = openBankAccountHandler;
        _depositHandler = depositHandler;
    }

    // POST /api/bank-accounts
    // {
    //    "userId": "",
    //    "bankAccountName": "Semester"
    // }
    [HttpPost]
    public async Task<IActionResult> CreateBankAccount(CreateBankAccountRequestDto request)
    {
        var openBankAccountResult = await _openBankAccountHandler.HandleAsync(
            new OpenBankAccountCommand(UserId: request.UserId));

        // TODO: Hårdkodad information nedan ska komma från 
        // resultatobjektet
        var response = new BankAccountDto(
            Id: openBankAccountResult.Id,
            AccountNumber: "100.200.300",
            Name: "Standardkonto",
            IsLocked: false,
            Balance: 0m,
            UserId: Guid.NewGuid());

        return Created(string.Empty, response);
    }

    [HttpPost("{id}/deposit")]
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> CreateDeposit(Guid id, CreateDepositDto request)
    {
        var currency = Currency.FromCode(request.Currency);

        var result = await _depositHandler.HandleAsync(
            new CreateDepositCommand(
                id,
                request.Amount,
                request.Reference,
                request.Currency));

        return Created(string.Empty, result);
    }

}
