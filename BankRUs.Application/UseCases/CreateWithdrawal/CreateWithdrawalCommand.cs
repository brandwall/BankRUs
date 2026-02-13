using BankRUs.Domain.Entities;
using BankRUs.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankRUs.Application.UseCases.CreateWithdrawal;

public class CreateWithdrawalCommand
{
    public Guid BankAccountId { get; }
    public decimal Amount { get; }
    public string? Reference { get; }
    public string CurrencyCode { get; }
    public TransactionType TransactionType => TransactionType.Withdrawal;
    public string? UserId { get; }

    public CreateWithdrawalCommand(Guid bankAccountId, decimal amount, string reference, string currency, string userId)
    {
        BankAccountId = bankAccountId;
        Amount = amount;
        Reference = reference;
        CurrencyCode = currency;
        UserId = userId;
    }
}
