using BankRUs.Api.Dtos.BankAccounts;
using BankRUs.Application.UseCases.CreateDeposit;
using BankRUs.Application.UseCases.CreateWithdrawal;
using BankRUs.Application.UseCases.OpenBankAccount;
using BankRUs.Domain.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Security.Claims;

namespace BankRUs.Api.Controllers;

[Route("api/bank-accounts")]
[ApiController]
public class BankAccountsController : ControllerBase
{
    private readonly OpenBankAccountHandler _openBankAccountHandler;
    private readonly CreateDepositHandler _depositHandler;
    private readonly CreateWithdrawalHandler _withdrawalHandler;

    public BankAccountsController(
        OpenBankAccountHandler openBankAccountHandler,
        CreateDepositHandler depositHandler,
        CreateWithdrawalHandler withdrawalHandler)
    {
        _openBankAccountHandler = openBankAccountHandler;
        _depositHandler = depositHandler;
        _withdrawalHandler = withdrawalHandler;
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
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var result = await _depositHandler.HandleAsync(
            new CreateDepositCommand(
                id,
                request.Amount,
                request.Reference,
                request.CurrencyCode));

        return Created(string.Empty, result);
    }

    [HttpPost("{id}/withdraw")]
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> CreateWithdrawal(Guid id, CreateWithdrawalDto request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var result = await _withdrawalHandler.HandleAsync(
            new CreateWithdrawalCommand(
                id,
                request.Amount,
                request.Reference,
                request.CurrencyCode,
                userId));

        return Created(string.Empty, result);
    }

}
