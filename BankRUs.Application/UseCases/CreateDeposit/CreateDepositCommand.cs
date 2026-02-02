using BankRUs.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankRUs.Application.UseCases.MakeDeposit;

public record CreateDepositCommand(
    Guid BankAccountId,
    decimal Amount, 
    string Reference,
    Currency Currency
);
