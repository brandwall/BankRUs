using BankRUs.Domain.Entities;
using BankRUs.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BankRUs.Application.UseCases.CreateDeposit;

public record CreateDepositResult(
    Guid TransactionId,
    DateTime TransactionDate,
    string AccountNumber,
    decimal AccountBalance,
    decimal DepositAmount,
    string Currency,
    [property: JsonConverter(typeof(JsonStringEnumConverter))]
    TransactionType TransactionType
);
