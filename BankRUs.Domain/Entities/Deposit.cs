using BankRUs.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankRUs.Domain.Entities;

public class Deposit
{
    public Deposit(decimal amount, string reference)
    {
        TransactionId = Guid.NewGuid();
        TransactionDate = DateTime.Now;
        Amount = amount;
        Reference = reference;
    }
    public Guid TransactionId { get; set; }
    public Guid BankAccountId { get; set; }
    public decimal Amount { get; set; }
    public string Reference { get; set; }
    public Currency Currency { get; set; }

    public DateTime TransactionDate { get; set; }
}
