using System;
using System.Collections.Generic;
using System.Text;

namespace BankRUs.Application.UseCases.MakeDeposit;

public record CreateDepositResult(
    Guid TransactionId,
    DateTime TransactionDate,
    string AccountNumber,
    decimal Balance
);
