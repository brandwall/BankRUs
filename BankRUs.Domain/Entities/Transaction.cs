using BankRUs.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BankRUs.Domain.Entities;

public class Transaction
{
    // EF behöver tydligen en ren konstruktor
    private Transaction() { }
    public Transaction(Guid bankAccountId, decimal amount, string reference, Currency currency, TransactionType transactionType)
    {
        Id = Guid.NewGuid();
        TransactionDate = DateTime.Now;

        BankAccountId = bankAccountId;

        if (amount <= 0)
            throw new ArgumentException("Amount must be positive");
        amount = Math.Round(amount, 2);
        Amount = amount;

        TransactionType = transactionType;
        Reference = reference;
        Currency = currency ?? throw new ArgumentNullException(nameof(currency));
    }
    public Guid Id { get; private set; }
    public Guid BankAccountId { get; private set; }
    public TransactionType TransactionType { get; private set; }
    public decimal Amount { get; private set; }
    [MaxLength(140)]
    public string? Reference { get; private set; }
    public Currency Currency { get; private set; }

    public DateTime TransactionDate { get; private set; }
}

public enum TransactionType
{
    Deposit,
    Withdrawal
}
