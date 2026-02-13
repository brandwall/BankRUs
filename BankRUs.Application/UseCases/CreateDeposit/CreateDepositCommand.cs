using BankRUs.Domain.Entities;
using BankRUs.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankRUs.Application.UseCases.CreateDeposit;

public record CreateDepositCommand
{
    public Guid BankAccountId { get; }
    public decimal Amount { get; }
    public string? Reference { get; }
    public string CurrencyCode { get; }
    public TransactionType TransactionType => TransactionType.Deposit;

    public CreateDepositCommand(Guid bankAccountId, decimal amount, string reference, string currency)
    {
        BankAccountId = bankAccountId;
        Amount = amount;
        Reference = reference;
        CurrencyCode = currency;
    }
}
