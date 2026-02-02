using BankRUs.Domain.ValueObjects;

namespace BankRUs.Api.Dtos.BankAccounts;

public record CreateDepositDto(
    Guid BankAccountId,
    decimal Amount,
    string Reference,
    Currency Currency
);